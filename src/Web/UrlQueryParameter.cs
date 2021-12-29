using System;

namespace TimHanewich.Toolkit.Web
{
    public class UrlQueryParameter
    {
        public string Label {get; set;}
        public string Value {get; set;}

        public UrlQueryParameter()
        {

        }

        public UrlQueryParameter(string label, string value)
        {
            Label = label;
            Value = value;
        }
    }
}