using System.Collections.Generic;
using System.Linq;

namespace DiMissionfileParser.Parser
{
    public static class MissionTextParser
    {
        private static readonly List<string> Nonlines = new List<string> { "", ":" };

        public static MissionText Parse(string fileName, IList<string> lines)
        {
            var contents = new List<string>();
            if (!lines.Any() || !lines.First().StartsWith(":") || IsTemplate(lines))
            {
                return new MissionText(fileName);
            }

            var labels = new Dictionary<string, IList<string>>();

            string pastlabel = lines.First();

            foreach (var line in lines)
            {
                if (line == ":EOF")
                {
                    break;
                }

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

                    AddContentsToLabel(labels, pastlabel, contents);
                    contents = new List<string>();

                    labels[washedLine] = new List<string>();
                    pastlabel = washedLine;
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

        private static MissionText GetMissionTextTypeByLabels(string fileName, Dictionary<string, IList<string>> labels, IList<string> lines)
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
        private static void AddContentsToLabel(Dictionary<string, IList<string>> labels, string label, List<string> contents)
        {
            if (label == string.Empty || contents.Count == 0)
            {
                return;
            }
            if (!labels.ContainsKey(label))
            {
                labels[label] = new List<string>();
            }

            labels[label] = labels[label].Concat(contents).ToList();
        }

    }
}