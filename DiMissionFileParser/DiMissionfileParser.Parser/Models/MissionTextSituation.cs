using System.Collections.Generic;
using DiMissionfileParser.Parser.Functions;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionTextSituation : MissionText
    {
        public MissionTextSituation(string fileName, Dictionary<string, MissionContents> labels, IList<string> lines)
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