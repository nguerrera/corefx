﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace System.Reflection.Metadata.Decoding
{
    // Parses an assembly name.
    //
    // The assembly name grammer below is written in a custom form of BNF (Backus-Naur Form), the key is as follows:
    //
    //      Symbol:     <name>
    //      Optional:   [<name>]
    //      Literal:    ","
    //      Or:         <pointer>
    //                  <array>
    //
    //  <format> ::=
    //      <assemblyName>
    //
    //  <assemblyName> ::=
    //      <name>[<components>]
    //
    //  <name> ::=
    //      [<whitespace>] <identifierOrQuotedIdentifier> [<whitespace>]
    //
    //  <components> ::=
    //      [<components>]<component>
    //
    //  <component> ::=
    //      "," <componentName> "=" <componentValue>
    //
    //  <componentName> ::=
    //      <identifierOrQuotedIdentifier>
    //
    //  <componentValue> ::=
    //      """"
    //      <identifierOrQuotedIdentifier>
    //
    //  <identifierOrQuotedIdentifier> ::=
    //      <identifier>
    //      """ <quotedIdentifier> """
    //
    //  <identifier> ::=
    //      [<identifier>]<identifierChar>
    //      [<identifier>]<escapedChar>
    //
    //  <quotedIdentifier> ::=
    //      [<quotedIdentifier>]<quotedIdentifierChar>
    //      [<quotedIdentifier>]<escapedChar>
    //
    //  <quotedIdentifierChar> ::=
    //      any unicode character except """
    //
    //  <identifierChar> ::=
    //      any unicode character except <delimiter>
    //      any unicode character except <genericTypeArgumentDelimiter> (within generic type argument)
    //
    //  <identifierCharWithinGenericTypeArgument> ::=
    //      any unicode character except ",", "=", """ and "]"
    //
    //  <escapedChar> ::=
    //      "\" <delimiter>
    //      "\" <genericTypeArgumentDelimiter> (within generic type argument)
    //
    //  <genericTypeArgumentDelimiter> ::=
    //      <delimiter>
    //      "]"
    //  
    //  <whitespace> ::=
    //      [<whitespace>] " "
    //
    //  <delimiter> ::=
    //      ","
    //      "="
    //      """
    //      "\"
    //
    internal partial class AssemblyNameParser
    {
        private readonly AssemblyNameBuilder _builder = new AssemblyNameBuilder();
        private Tokenizer _tokenizer;
        private readonly bool _ownTokenizer;

        private AssemblyNameParser(string input, int startIndex, ParseMode mode)
        {
            _ownTokenizer = mode == ParseMode.AssemblyName;
            _tokenizer = new Tokenizer(input, startIndex, mode == ParseMode.AssemblyName ? Delimiters.AssemblyName :
                                                                                           Delimiters.AssemblyNameWithinGenericTypeArgument);
        }

        public static AssemblyNameComponents Parse(string input, ref int index, bool withinGenericTypeArgument)
        {
            AssemblyNameParser parser = new AssemblyNameParser(input, index, withinGenericTypeArgument ? ParseMode.AssemblyNameWithinGenericTypeArgument : ParseMode.AssemblyName);
            AssemblyNameComponents name = parser.Parse();
            index = parser._tokenizer.Position;

            return name;
        }

        public int Position
        {
            get { return _tokenizer.Position; }
        }

        private AssemblyNameComponents Parse()
        {
            ParseName();
            ParseComponents();

            if (_ownTokenizer)
            {
                _tokenizer.Close();
            }

            return _builder.ToAssemblyName();
        }

        // Parses 'System' in 'System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'
        //
        // <name>
        private void ParseName()
        {
            // Unless quoted, the parsed assembly name does not contain
            // leading or ending spaces to remain consistent with Reflection
            string name = ReadIdentifier(true);

            _builder.SetName(name);
        }

        // Parses ', Version=1.0.0.0, Culture=neutral', etc in 'System, Version=1.0.0.0, Culture=neutral'
        //
        // <components>
        private void ParseComponents()
        {
            while (_tokenizer.SkipIf(TokenType.Comma))
            {
                string componentName = ReadIdentifier(true);

                _tokenizer.Skip(TokenType.Equals);

                string componentValue = ReadIdentifier(false);

                SetComponent(componentName, componentValue);
            }
        }

        // <identifierOrQuotedIdentifier>
        private string ReadIdentifier(bool required)
        {
            string identifier = TryParseQuotedIdentifier(required);
            if (identifier == null)
            {
                identifier = ParseIdentifier();
            }

            return identifier;
        }

        // Attempts to parse '"Value"' in 'System, Culture="Value"'
        //
        // <quotedIdentifier>
        private string TryParseQuotedIdentifier(bool required)
        {
            if (_tokenizer.Peek() != TokenType.Quote)
                return null;

            Tokenizer quotedTokenizer = _tokenizer.WithDelimiters(Delimiters.QuotedIdentifier);

            // Consume '"'
            quotedTokenizer.Skip(TokenType.Quote);

            // Within a quoted identifier, all leading/trailing white space is treated as significant
            string identifier = quotedTokenizer.ReadId(required ? IdentifierOptions.Required : IdentifierOptions.None);

            // Consume '"'
            quotedTokenizer.Skip(TokenType.Quote);

            _tokenizer.Position = quotedTokenizer.Position;
            return identifier;
        }

        // <identifier>
        private string ParseIdentifier()
        {
            // Normal identifier where leading/trailing white space is 
            // not significant and the identifier is always required
            return _tokenizer.ReadId(IdentifierOptions.Required | IdentifierOptions.Trim);
        }

        private void SetComponent(string componentName, string componentValue)
        {
            // Have we already set this?
            EnsureNotSet(componentName);

            _builder.SetComponent(componentName, componentValue);
        }

        private void EnsureNotSet(string componentName)
        {
            if (_builder.IsSet(componentName))
            {
                throw _tokenizer.FormatException(TypeNameFormatErrorId.DuplicateAssemblyComponent,
                                                 SR.TypeFormat_DuplicateAssemblyComponent,
                                                 componentName);
            }
        }
    }
}
