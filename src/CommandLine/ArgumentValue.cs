using System;

namespace TimHanewich.Toolkit.CommandLine
{
    public class ArgumentValue
    {
        private string _label;
        private string _value;

        public ArgumentValue()
        {
            
        }

        public ArgumentValue(string label_raw)
        {
            _label = label_raw;
        }

        public ArgumentValue(string label_raw, string value_raw)
        {
            _label = label_raw;
            _value = value_raw;
        }

        public string LabelRaw
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }

        public string Label
        {
            get
            {
                if (_label.Substring(0, 2) == "--")
                {
                    return _label.Substring(2);
                }
                else if (_label.Substring(0, 1) == "-")
                {
                    return _label.Substring(1);
                }
                else
                {
                    return _label;
                }
            }
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

    }
}