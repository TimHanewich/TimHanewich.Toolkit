using System;
using System.Collections.Generic;

namespace TimHanewich.Toolkit.TextAnalysis.Highlighting
{
    public class TextHighlight
    {
        public int BeginPosition {get; set;}
        public int Length {get; set;}

        public static TextHighlight[] MakeHighlights(string body, string highlight)
        {
            //Get all
            List<TextHighlight> ToReturn = new List<TextHighlight>();
            int NextIndex = body.IndexOf(highlight);
            while (NextIndex >= 0)
            {
                TextHighlight th = new TextHighlight();
                th.BeginPosition = NextIndex;
                th.Length = highlight.Length;
                ToReturn.Add(th);
                NextIndex = body.IndexOf(highlight, NextIndex + 1);
            }
            return ToReturn.ToArray();
        }
    
        public static string ReadHighlight(string body, TextHighlight highlight, int buffer_around = 0)
        {
            string ToReturn = body.Substring(highlight.BeginPosition - buffer_around, highlight.Length + (buffer_around * 2));
            return ToReturn;
        }
    }
}