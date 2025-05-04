using System.Collections;
using System.Collections.Generic;
using HessQLParser.Parser.CustomEnumerators;

namespace HessQLParser.Parser.Statements
{
    public class BlockStmt :  IStatement, IEnumerable<IStatement>
    {
        public List<IStatement> body { get; set; }

        public BlockStmt(List<IStatement> statements)
        {
            body = statements;
        }

        public void Statement()
        {
        
        }

        public string Debug()
        {
            string res = "type: blockStatement{";

            foreach (IStatement statement in body)
            {
                res += statement.Debug();
            }
        
            return res+"}";
        }
    
        public IEnumerator<IStatement> GetEnumerator()
        {
            return new StatementEnumerator(body);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}