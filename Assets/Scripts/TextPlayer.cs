using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class TextPlayer
{
    private class TextLabel
    {
        public string BeginLabel;
        public string Content;
        public string EndLabel;

        public int Length {
            get => Content.Length;
        }

        public string GetText()
        {
            return BeginLabel + Content + EndLabel;
        }

        public string GetText(int size)
        {
            return BeginLabel + Content.Substring(0, size) + EndLabel;
        }
    }

    List<TextLabel> _labels;

    public int Length { get; private set; }

    private const string LABEL_PATTERN = @"(<.*?>)(.*?)(</.*?>)";

    public TextPlayer(string text)
    {
        _labels = _Parse(text);
        Length = _labels.Sum(label => label.Length);
    }

    private List<TextLabel> _Parse(string format)
    {
        var labels = new List<TextLabel>();
        var matches = Regex.Matches(format, LABEL_PATTERN);
        int prevIndex = 0;
        for (int i = 0; i < matches.Count; i++)
        {
            int currentIndex = matches[i].Index;
            labels.Add(new TextLabel
            {
                BeginLabel = string.Empty,
                Content = format.Substring(prevIndex, currentIndex - prevIndex),
                EndLabel = string.Empty,
            });
            labels.Add(new TextLabel
            {
                BeginLabel = matches[i].Groups[1].Value,
                Content = matches[i].Groups[2].Value,
                EndLabel = matches[i].Groups[3].Value,
            });
            prevIndex = currentIndex + matches[i].Groups[0].Length;
        }
        labels.Add(new TextLabel
        {
            BeginLabel = string.Empty,
            Content = format.Substring(prevIndex),
            EndLabel = string.Empty,
        });
        return labels;
    }

    public string GetText(int size)
    {
        StringBuilder sb = new StringBuilder();
        int currLength = 0;
        foreach(var label in _labels)
        {
            if(currLength + label.Length >= size)
            {
                sb.Append(label.GetText(size - currLength));
                break;
            }
            else
            {
                sb.Append(label.GetText());
                currLength += label.Length;
            }
        }
        return sb.ToString();
    }

    public string GetText()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var label in _labels)
            sb.Append(label.GetText());
        return sb.ToString();
    }
}