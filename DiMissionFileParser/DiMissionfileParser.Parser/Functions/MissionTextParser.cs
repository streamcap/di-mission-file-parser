using System.Collections.Generic;
using System.Linq;
using DiMissionfileParser.Parser.Models;

namespace DiMissionfileParser.Parser.Functions
{
    internal static class Extensions
    {
        internal static void Flush(this MissionContents contents, IDictionary<string, MissionContents> labels, string label)
        {
            if (label == string.Empty || contents.Count == 0)
            {
                return;
            }
            if (!labels.ContainsKey(label))
            {
                labels[label] = new MissionContents();
            }

            labels[label] = labels[label].Concat(contents).ToContents();
            contents.Clear();
        }

        internal static MissionContents ToContents(this IEnumerable<string> lines)
        {
            return new MissionContents(lines);
        }
    }

    public static class MissionTextParser
    {
        private static readonly List<string> Nonlines = new MissionContents { "", ":" };

        public static MissionText Parse(string fileName, IList<string> lines)
        {
            var contents = new MissionContents();
            if (!lines.Any() || !lines.First().StartsWith(":") || IsTemplate(lines))
            {
                return new MissionText(fileName);
            }

            var labels = new Dictionary<string, MissionContents>();

            var activeLabel = lines.First(l => l.StartsWith(":"));

            foreach (var line in lines)
            {
                if (Nonlines.Contains(line))
                {
                    continue;
                }
                if (line.StartsWith(":") && line != "")
                {
                    var washedLine = line.Trim().Trim(':');
                    if (washedLine == "")
                    {
                        continue;
                    }

                    contents.Flush(labels, activeLabel);

                    if (line == ":EOF")
                    {
                        break;
                    }

                    labels[washedLine] = new MissionContents();
                    activeLabel = washedLine;
                }
                else
                {
                    var washedLine = line.Trim().Trim(':');
                    contents.Add(washedLine);
                }
            }

            var missionText = GetMissionTextTypeByLabels(fileName, labels, lines);

            return missionText;
        }

        private static bool IsTemplate(IList<string> lines)
        {
            return lines.Count == 5 && lines.Any(line => line == "New Mission") && lines.Any(l => l == "No Description");
        }

        private static MissionText GetMissionTextTypeByLabels(string fileName, Dictionary<string, MissionContents> labels, IList<string> lines)
        {
            if (labels.Count == 6 && labels.ContainsKey("Outstanding"))
            {
                return new MissionTextWithResult(fileName, labels, lines);
            }
            if (labels.Count == 3 && labels.ContainsKey("Title"))
            {
                return new MissionTextTitled(fileName, labels, lines);
            }
            if (labels.Count == 3 && labels.ContainsKey("Successful"))
            {
                return new MissionTextCampaign(fileName, labels, lines);
            }
            if (labels.Count == 2 && labels.ContainsKey("Situation"))
            {
                return new MissionTextSituation(fileName, labels, lines);
            }

            return new MissionText(fileName, labels, lines);
        }
    }
}