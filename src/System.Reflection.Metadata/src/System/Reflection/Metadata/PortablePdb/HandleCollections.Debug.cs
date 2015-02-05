﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
    public struct DocumentHandleCollection : IReadOnlyCollection<DocumentHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal DocumentHandleCollection(MetadataReader reader)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.DocumentTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<DocumentHandle> IEnumerable<DocumentHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<DocumentHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public DocumentHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return DocumentHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct MethodBodyHandleCollection : IReadOnlyCollection<MethodBodyHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal MethodBodyHandleCollection(MetadataReader reader)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.MethodBodyTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<MethodBodyHandle> IEnumerable<MethodBodyHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<MethodBodyHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public MethodBodyHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return MethodBodyHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct LocalScopeHandleCollection : IReadOnlyCollection<LocalScopeHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal LocalScopeHandleCollection(MetadataReader reader)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.LocalScopeTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<LocalScopeHandle> IEnumerable<LocalScopeHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<LocalScopeHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public LocalScopeHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return LocalScopeHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct LocalVariableHandleCollection : IReadOnlyCollection<LocalVariableHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal LocalVariableHandleCollection(MetadataReader reader, LocalScopeHandle scope)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            if (scope.IsNil)
            {
                _firstRowId = 1;
                _lastRowId = (int)reader.LocalVariableTable.NumberOfRows;
            }
            else
            {
                reader.GetLocalVariableRange(scope, out _firstRowId, out _lastRowId);
            }
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<LocalVariableHandle> IEnumerable<LocalVariableHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<LocalVariableHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public LocalVariableHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return LocalVariableHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct LocalConstantHandleCollection : IReadOnlyCollection<LocalConstantHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal LocalConstantHandleCollection(MetadataReader reader, LocalScopeHandle scope)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.LocalConstantTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<LocalConstantHandle> IEnumerable<LocalConstantHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<LocalConstantHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public LocalConstantHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return LocalConstantHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct AsyncMethodHandleCollection : IReadOnlyCollection<AsyncMethodHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal AsyncMethodHandleCollection(MetadataReader reader)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.AsyncMethodTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<AsyncMethodHandle> IEnumerable<AsyncMethodHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<AsyncMethodHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public AsyncMethodHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return AsyncMethodHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct ImportScopeCollection : IReadOnlyCollection<ImportScopeHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal ImportScopeCollection(MetadataReader reader)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.ImportScopeTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<ImportScopeHandle> IEnumerable<ImportScopeHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<ImportScopeHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public ImportScopeHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return ImportScopeHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    public struct CustomDebugInformationHandleCollection : IReadOnlyCollection<CustomDebugInformationHandle>
    {
        private readonly MetadataReader reader;

        private readonly int _firstRowId;
        private readonly int _lastRowId;

        internal CustomDebugInformationHandleCollection(MetadataReader reader)
        {
            Debug.Assert(reader != null);
            this.reader = reader;

            _firstRowId = 1;
            _lastRowId = (int)reader.CustomDebugInformationTable.NumberOfRows;
        }

        public int Count
        {
            get
            {
                return _lastRowId - _firstRowId + 1;
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(reader, _firstRowId, _lastRowId);
        }

        IEnumerator<CustomDebugInformationHandle> IEnumerable<CustomDebugInformationHandle>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<CustomDebugInformationHandle>, IEnumerator
        {
            private readonly MetadataReader _reader;
            private readonly int _lastRowId; // inclusive

            // first Parameter rid - 1: initial state
            // EnumEnded: enumeration ended
            private int _currentRowId;

            // greater than any RowId and with last 24 bits clear, so that Current returns nil token
            private const int EnumEnded = (int)TokenTypeIds.RIDMask + 1;

            internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
            {
                _reader = reader;
                _lastRowId = lastRowId;
                _currentRowId = firstRowId - 1;
            }

            public CustomDebugInformationHandle Current
            {
                get
                {
                    // PERF: keep this code small to enable inlining.
                    return CustomDebugInformationHandle.FromRowId((uint)_currentRowId & TokenTypeIds.RIDMask);
                }
            }

            public bool MoveNext()
            {
                // PERF: keep this code small to enable inlining.

                if (_currentRowId >= _lastRowId)
                {
                    _currentRowId = EnumEnded;
                    return false;
                }
                else
                {
                    _currentRowId++;
                    return true;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }
}
