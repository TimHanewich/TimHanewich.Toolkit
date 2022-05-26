using System;

namespace TimHanewich.Sql
{
    public static class SqlToolkit
    {
        public static string ToSymbol(this ComparisonOperator op)
        {
            switch (op)
            {
                case ComparisonOperator.Equals:
                    return "=";
                case ComparisonOperator.GreaterThan:
                    return ">";
                case ComparisonOperator.LessThan:
                    return "<";
                case ComparisonOperator.GreaterThanOrEqual:
                    return ">=";
                case ComparisonOperator.LessThanOrEqual:
                    return "<=";
                case ComparisonOperator.Not:
                    return "!=";
                default:
                    throw new Exception("A comparison operator does not exist for enum value '" + op.ToString () + "'");
            }
        }

        public static string ToSymbol(this OrderDirection od)
        {
            if (od == OrderDirection.Ascending)
            {
                return "asc";
            }
            else if (od == OrderDirection.Descending)
            {
                return "desc";
            }
            else
            {
                throw new Exception("A symbol does not exist for OrderDirection '" + od.ToString() + "'");
            }
        }
    }
}