using System;
using System.Collections.Generic;

namespace TimHanewich.Toolkit.TextAnalysis.Highlighting
{
    public class TextHighlight
    {
        public int BeginPosition {get; set;}
        public int EndPosition {get; set;}

        public static TextHighlight[] MakeHighlights(string body, string highlight)
        {
            //Get all
            List<TextHighlight> ToReturn = new List<TextHighlight>();
            int NextIndex = body.IndexOf(highlight);
            while (NextIndex >= 0)
            {
                TextHighlight th = new TextHighlight();
                th.BeginPosition = NextIndex;
                th.EndPosition = NextIndex + highlight.Length;
                ToReturn.Add(th);
                NextIndex = body.IndexOf(highlight, NextIndex + 1);
            }
            return ToReturn.ToArray();
        }
    }
}