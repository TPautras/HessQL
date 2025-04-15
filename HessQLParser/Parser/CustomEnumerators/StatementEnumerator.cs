using System;
using System.Collections;
using System.Collections.Generic;
using HessQLParser.Parser;

namespace HessQLParser.Parser.CustomEnumerators
{
    public class StatementEnumerator : IEnumerable<IStatement>, IEnumerator<IStatement>
    {
        private readonly IList<IStatement> _statements;
        private int _index;

        public StatementEnumerator() : this(new List<IStatement>()) { }

        public StatementEnumerator(IEnumerable<IStatement> statements)
        {
            _statements = new List<IStatement>(statements);
            _index = -1;
        }

        public IEnumerator<IStatement> GetEnumerator()
        {
            return new StatementEnumerator(_statements);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            _index++;
            return _index < _statements.Count;
        }

        public void Reset()
        {
            _index = -1;
        }

        public IStatement Current
        {
            get
            {
                if (_index < 0 || _index >= _statements.Count)
                    throw new InvalidOperationException();
                return _statements[_index];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}