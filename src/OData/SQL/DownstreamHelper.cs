using System;
using System.Collections.Generic;
using TimHanewich.Sql;

namespace TimHanewich.Sql
{
    public class DownstreamHelper
    {

        //Top
        public int? Top {get; set;} = null;
        
        //Columns
        public List<string> Columns {get; set;}

        //Resource
        public string Table {get; set;}

        //Where clauses
        public List<ConditionalClause> Where {get; set;}

        //Order by
        public List<ReadOrder> OrderBy {get; set;}


        public DownstreamHelper()
        {
            Columns = new List<string>();
            Where = new List<ConditionalClause>();
            OrderBy = new List<ReadOrder>();
        }

        public override string ToString()
        {
            string cmd = "select ";

            //Columns
            if (Columns.Count == 0)
            {
                cmd = cmd + "*";
            }
            else
            {
                foreach (string fn in Columns)
                {
                    cmd = cmd + fn + ",";
                }
                cmd = cmd.Substring(0, cmd.Length - 1); //Remove the last comma
            }

            //From table
            if (Table == null || Table == "")
            {
                throw new Exception("Unable to create SQL read statement. Target table was not identified.");
            }
            cmd = cmd + " from " + Table;

            //Where
            if (Where.Count > 0)
            {
                cmd = cmd + " where";
                for (int t = 0; t < Where.Count; t++)
                {
                    if (t > 0)
                    {
                        cmd = cmd + " and"; //This will need to change later once I incorporate the LogicalOperator into the ConditionalClause
                    }
                    string quote = "";
                    if (Where[t].UseQuotes)
                    {
                        quote = "'";
                    }
                    cmd = cmd + " " + Where[t].ColumnName + " " + Where[t].Operator.ToSymbol() + " " + quote + Where[t].Value + quote;
                }
            }
            

            //Order by
            if (OrderBy.Count > 0)
            {
                cmd = cmd + " order by";
                foreach (ReadOrder ro in OrderBy)
                {
                    cmd = cmd + " " + ro.ColumnName + " " + ro.Direction.ToSymbol() + ",";
                }
                cmd = cmd.Substring(0, cmd.Length-1); //Remove the last trailing comma from the order by
            }
            

            return cmd;
        }

    }
}