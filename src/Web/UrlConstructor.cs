using System;
using System.Collections.Generic;

namespace TimHanewich.Toolkit.Web
{
    public class UrlConstructor
    {
        public string Base {get; set;}
        public UrlQueryParameter[] QueryParameters
        {
            get
            {
                return _QueryParameters.ToArray();
            }
        }
        private List<UrlQueryParameter> _QueryParameters;

        public UrlConstructor()
        {
            _QueryParameters = new List<UrlQueryParameter>();
        }

        public void AddQueryParameter(UrlQueryParameter param)
        {
            _QueryParameters.Add(param);
        }

        public override string ToString()
        {
            string ToReturn = Base;
            
            //If there are query params, add them
            if (QueryParameters.Length > 0)
            {

                //If the last character is a forward slash, remove it
                if (ToReturn.Substring(ToReturn.Length - 1, 1) == "/")
                {
                    ToReturn = ToReturn.Substring(0, ToReturn.Length - 1);
                }

                //Add an ending question mark if there isn't one.
                if (ToReturn.Substring(ToReturn.Length - 1, 1) != "?")
                {
                    ToReturn = ToReturn + "?";
                }

                //Query portion
                string QueryPortion = "";
                foreach (UrlQueryParameter param in QueryParameters)
                {
                    QueryPortion = QueryPortion + param.Label + "=" + param.Value + "&";
                }
                QueryPortion = QueryPortion.Substring(0, QueryPortion.Length - 1); //Trim the last "&"
                ToReturn = ToReturn + QueryPortion;
            }

            return ToReturn;

        }
    
        
    }
}