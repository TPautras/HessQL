using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HessQLParser.Parser.CustomEnumerators
{
    public class ExpressionEnumerator : IEnumerable<Expression>, IEnumerator<Expression>
    {
        private readonly IList<Expression> _expressions;
        private int _index;

        public ExpressionEnumerator(IEnumerable<Expression> expressions)
        {
            _expressions = new List<Expression>(expressions);
            _index = -1;
        }

        protected ExpressionEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Expression> GetEnumerator()
        {
            return new ExpressionEnumerator(_expressions);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            _index++;
            return _index < _expressions.Count;
        }

        public void Reset()
        {
            _index = -1;
        }

        public Expression Current
        {
            get
            {
                if (_index < 0 || _index >= _expressions.Count)
                    throw new InvalidOperationException("Current is not valid");
                return _expressions[_index];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}