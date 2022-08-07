using System;
using System.Collections.Generic;

namespace TimHanewich.Toolkit.CommandLine
{
    public class CommandLineArguments
    {
        
        public ArgumentValue[] Arguments {get; set;}

        public static CommandLineArguments Parse(string[] arguments)
        {

            CommandLineArguments ToReturn = new CommandLineArguments();

            //Get the argument values
            List<int> IndexesToSkip = new List<int>();
            List<ArgumentValue> ArgumentValues = new List<ArgumentValue>();
            for (int t = 0; t < arguments.Length; t++)
            {
                if (IndexesToSkip.Contains(t) == false)
                {
                    string ThisString = arguments[t];
                    ArgumentValue av = new ArgumentValue();
                
                    //Set the label
                    av.LabelRaw = ThisString;

                    //Try to get the next one
                    string NextString = null;
                    int NextUpIndex = t + 1;
                    if (arguments.Length >= (NextUpIndex + 1))
                    {
                        NextString = arguments[NextUpIndex];
                    }

                    //Set the property, if needed
                    if (NextString != null) //it is even worth exploring if this is a label to a value because we DO in fact have more data
                    {
                        if (ToReturn.HasFlag(ThisString))
                        {
                            if (ToReturn.HasFlag(NextString) == false)
                            {
                                av.Value = NextString;
                                IndexesToSkip.Add(NextUpIndex);
                            }
                        }
                    }

                    //Add it
                    ArgumentValues.Add(av);
                }
            }
            ToReturn.Arguments = ArgumentValues.ToArray();

            return ToReturn;
        }

        private bool HasFlag(string arg)
        {
            if (arg == null)
            {
                return false;
            }

            if (arg.Length == 0)
            {
                return false;
            }

            if (arg.Substring(0, 1) == "-")
            {
                return true;
            }

            return false;
        }

    }
}