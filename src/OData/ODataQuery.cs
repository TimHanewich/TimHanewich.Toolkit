using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

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

        //orderby
        private ODataOrder[] _orderby;
        public ODataOrder[] orderby
        {
            get
            {
                if (_orderby == null)
                {
                    return new ODataOrder[]{};
                }
                else
                {
                    return _orderby;
                }
            }
        }

        //top
        public int? top {get; set;} = null;

        //skpi
        public int? skip {get; set;} = null;
    

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
                    filterSplitters.Add("%20or%20");
                    filterSplitters.Add("%20and%20");
                    filterSplitters.Add(" or ".ToUpper());
                    filterSplitters.Add(" and ".ToUpper());
                    filterSplitters.Add("%20or%20".ToUpper());
                    filterSplitters.Add("%20and%20".ToUpper());

                    string[] filters = kvp.Value.Split(filterSplitters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    List<ODataFilter> ParsedFilters = new List<ODataFilter>();
                    foreach (string f in filters)
                    {

                        //Splitters to separate out the components. This is designed to split the 3 parts (column name, operator, value)
                        List<string> SplitterToComponents = new List<string>();
                        SplitterToComponents.Add(" eq ");
                        SplitterToComponents.Add(" gt ");
                        SplitterToComponents.Add(" lt ");
                        SplitterToComponents.Add(" ne ");
                        SplitterToComponents.Add(" ge ");
                        SplitterToComponents.Add(" le ");
                        SplitterToComponents.Add("%20eq%20");
                        SplitterToComponents.Add("%20gt%20");
                        SplitterToComponents.Add("%20lt%20");
                        SplitterToComponents.Add("%20ne%20");
                        SplitterToComponents.Add("%20ge%20");
                        SplitterToComponents.Add("%20le%20");
                        SplitterToComponents.Add(" eq ".ToUpper());
                        SplitterToComponents.Add(" gt ".ToUpper());
                        SplitterToComponents.Add(" lt ".ToUpper());
                        SplitterToComponents.Add(" ne ".ToUpper());
                        SplitterToComponents.Add(" ge ".ToUpper());
                        SplitterToComponents.Add(" le ".ToUpper());
                        SplitterToComponents.Add("%20eq%20".ToUpper());
                        SplitterToComponents.Add("%20gt%20".ToUpper());
                        SplitterToComponents.Add("%20lt%20".ToUpper());
                        SplitterToComponents.Add("%20ne%20".ToUpper());
                        SplitterToComponents.Add("%20ge%20".ToUpper());
                        SplitterToComponents.Add("%20le%20".ToUpper());
                        
                        //Split it
                        string[] filterParts = f.Split(SplitterToComponents.ToArray(), StringSplitOptions.RemoveEmptyEntries);  //The first string will be the column name, the second will be the value. The comparison operator will be removed because it was split out.

                        //Determine which one was the one that split it
                        int StartLocationOfValue = f.IndexOf(filterParts[1]);
                        string ComparisonOperatorString = f.Substring(filterParts[0].Length, StartLocationOfValue - filterParts[0].Length);
                        ComparisonOperatorString = ComparisonOperatorString.ToLower().Replace("%20", "").Replace(" ", "");

                        //Construct the filter
                        ODataFilter ThisFilter = new ODataFilter();
                        ThisFilter.ColumnName = filterParts[0];
                        ThisFilter.Operator = StringToOperator(ComparisonOperatorString);
                        ThisFilter.SetValue(filterParts[1].Replace("%20", " "));

                        //Is there a logical operator attached to this?
                        List<string> OrTests = new List<string>();
                        OrTests.Add("or " + f);
                        OrTests.Add("or%20" + f);
                        OrTests.Add("OR " + f);
                        OrTests.Add("OR%20" + f);
                        List<string> AndTests = new List<string>();
                        AndTests.Add("and " + f);
                        AndTests.Add("and%20" + f);
                        AndTests.Add("AND " + f);
                        AndTests.Add("AND%20" + f);
                        foreach (string OrTest in OrTests)
                        {
                            if (kvp.Value.Contains(OrTest))
                            {
                                ThisFilter.LogicalOperatorPrefix = LogicalOperator.Or;
                            }
                        }
                        foreach (string AndTest in AndTests)
                        {
                            if (kvp.Value.Contains(AndTest))
                            {
                                ThisFilter.LogicalOperatorPrefix = LogicalOperator.And;
                            }
                        }

                        ParsedFilters.Add(ThisFilter);
                    }
                    _filter = ParsedFilters.ToArray();
                }

                //orderby
                if (kvp.Key.ToLower() == "$orderby")
                {
                    string[] OrderByOrders = kvp.Value.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries);
                    List<ODataOrder> orders = new List<ODataOrder>();
                    foreach (string orderStr in OrderByOrders)
                    {
                        string[] OrderParts = orderStr.Split(new string[]{" ", "%20"}, StringSplitOptions.RemoveEmptyEntries);
                        if (OrderParts.Length == 2)
                        {
                            ODataOrder order = new ODataOrder();
                            order.ColumnName = OrderParts[0];
                            if (OrderParts[1].ToLower() == "asc")
                            {
                                order.Direction = OrderDirection.Ascending;
                            }
                            else if (OrderParts[1].ToLower() == "desc")
                            {
                                order.Direction = OrderDirection.Descending;
                            }
                            else
                            {
                                throw new Exception("OrderBy direction '" + orderStr + "' not recognized as valid order direction.");
                            }
                            orders.Add(order);
                        }
                    }
                    _orderby = orders.ToArray();
                }

                //top
                if (kvp.Key.ToLower() == "$top")
                {
                    try
                    {
                        top = Convert.ToInt32(kvp.Value);
                    }
                    catch
                    {
                        throw new Exception("Value '" + kvp.Value + "' is not a valid integer, used as the top parameter in the query");
                    }
                }

                //skip
                if (kvp.Key.ToLower() == "$skip")
                {
                    try
                    {
                        skip = Convert.ToInt32(kvp.Value);
                    }
                    catch
                    {
                        throw new Exception("Value '" + kvp.Value + "' is not a valid integer, used as the skip parameter in the query");
                    }
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