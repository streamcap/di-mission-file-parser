using System.Collections.Generic;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionTextSituation : MissionText
    {
        public MissionTextSituation(string fileName, Dictionary<string, IList<string>> labels, IList<string> lines)
            : base(fileName, labels, lines)
        {
            Situation = AggregateLabelText(labels["Situation"]);
        }

        public string Situation { get; set; }

        public override string ToString()
        {
            return $"Situation: {Situation.Substring(0, 140)} ({FileName})";
        }
    }
}