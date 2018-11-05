using System.Collections.Generic;
using System.Linq;
using DiMissionfileParser.Parser.Functions;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionText
    {
        public string FileName { get; }
        public IDictionary<string, MissionContents> Labels { get; }
        private IList<string> _source;

        public MissionText(string fileName, Dictionary<string, MissionContents> labels = null, IList<string> lines = null)
        {
            FileName = fileName;
            Labels = labels != null ? labels.Where(l => l.Key != "").ToDictionary(l => l.Key.Replace(":", ""), l => l.Value.ToContents()) : new Dictionary<string, MissionContents>();
            _source = lines;
        }
        public static MissionText Parse(string fileName, IList<string> lines)
        {
            return MissionTextParser.Parse(fileName, lines);
        }

        public static bool AreEqual(IEnumerable<MissionText> missionTexts)
        {
            var otherLabels = new List<string>();
            foreach (var missionText in missionTexts)
            {
                var labels = missionText.Labels.Keys;
                if (!otherLabels.Any())
                {
                    otherLabels.AddRange(labels);
                }
                if (labels.Any(l => !otherLabels.Contains(l)))
                {
                    return false;
                }
            }

            return true;
        }

        protected string AggregateLabelText(IList<string> lines)
        {
            return lines.Any() ? lines.Aggregate((a, b) => string.Join(",", a, b)) : "";
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}