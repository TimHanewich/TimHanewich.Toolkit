using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Net.Http;

namespace TimHanewich.Toolkit.OData
{
    public class ODataQuery
    {
        //Example queries:
        //https://host.com/service/Products?$select=Rating,ReleaseDate

        #region "Query params"

        //select
        private string[] _select;
        public string[] select
        {
            get
            {
                if (_select == null)
                {
                    return new string[]{};
                    
                }
                else
                {
                    return _select;
                }
            }
        }

        //filter
        private ODataFilter[] _filter;
        public ODataFilter[] filter
        {
            get
            {
                if (_filter == null)
                {
                    return new ODataFilter[]{};
                }
                else
                {
                    return _filter;
                }
            }
        }

        #endregion

        #region "Query param manipulation"

        public void AddSelect(string column)
        {
            if (_select == null)
            {
                _select = new string[]{column};
            }
            else
            {
                List<string> ToSetSelectTo = new List<string>();
                ToSetSelectTo.AddRange(_select);
                ToSetSelectTo.Add(column);
            }
        }

        public void RemoveSelect(string column)
        {
            if (_select != null)
            {
                List<string> ToSetSelectTo = new List<string>();
                ToSetSelectTo.AddRange(_select);
                ToSetSelectTo.Remove(column);
                _select = ToSetSelectTo.ToArray();
            }
        }

        public void ClearSelect()
        {
            _select = null;
        }

        #endregion
    
        
        public ODataQuery()
        {

        }

        public ODataQuery(Uri path)
        {
            //Get the query portion
            string queryPortion = path.Query;
            queryPortion = queryPortion.Replace("?", "");
            string[] paramPortions = queryPortion.Split(new string[]{"&"}, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();
            foreach (string s in paramPortions)
            {
                int loc1 = s.IndexOf("=");
                if (loc1 != -1)
                {
                    string paramName = s.Substring(0, loc1);
                    string paramValue = s.Substring(loc1 + 1);
                    queryParameters.Add(paramName, paramValue);
                }
            }

            
            foreach (KeyValuePair<string, string> kvp in queryParameters)
            {

                //select
                if (kvp.Key.ToLower() == "$select")
                {
                    string[] columns = kvp.Value.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
                    _select = columns;
                }

                //filter
                if (kvp.Key.ToLower() == "$filter")
                {
                    List<string> filterSplitters = new List<string>();
                    filterSplitters.Add(" or ");
                    filterSplitters.Add(" and ");
                    filterSplitters.Add(" OR ");
                    filterSplitters.Add(" AND ");
                    string[] filters = kvp.Value.Split(filterSplitters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    List<ODataFilter> ParsedFilters = new List<ODataFilter>();
                    foreach (string f in filters)
                    {
                        string[] filterParts = f.Split(new string[]{" ", "%20"}, StringSplitOptions.RemoveEmptyEntries);
                        ODataFilter ThisFilter = new ODataFilter();
                        ThisFilter.ColumnName = filterParts[0];
                        ThisFilter.Operator = StringToOperator(filterParts[1]);
                        ThisFilter.SetValue(filterParts[2]);
                        ParsedFilters.Add(ThisFilter);
                    }
                    _filter = ParsedFilters.ToArray();
                }


            }


        }


        #region "Utility functions"

        private string OperatorToString(ComparisonOperator op)
        {
            switch (op)
            {
                case ComparisonOperator.Equals:
                    return "eq";
                case ComparisonOperator.GreaterThan:
                    return "gt";
                case ComparisonOperator.LessThan:
                    return "lt";
                case ComparisonOperator.NotEqualTo:
                    return "ne";
                case ComparisonOperator.GreaterThanOrEqualTo:
                    return "ge";
                case ComparisonOperator.LessThanOrEqualTo:
                    return "le";
                default:
                    throw new Exception("String operator unknown for '" + op.ToString() + "'");
            }
        }

        private ComparisonOperator StringToOperator(string s)
        {
            switch (s.ToLower())
            {
                case "eq":
                    return ComparisonOperator.Equals;
                case "gt":
                    return ComparisonOperator.GreaterThan;
                case "lt":
                    return ComparisonOperator.LessThan;
                case "ne":
                    return ComparisonOperator.NotEqualTo;
                case "ge":
                    return ComparisonOperator.GreaterThanOrEqualTo;
                case "le":
                    return ComparisonOperator.LessThanOrEqualTo;
                default:
                    throw new Exception("String '" + s + "' not recognized as a valid comparsion operator.");
            }
        }

        #endregion

    }
}