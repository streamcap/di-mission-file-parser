using System.Collections.Generic;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionTextWithResult : MissionTextTitled
    {
        public MissionTextWithResult(string fileName, Dictionary<string, IList<string>> labels, IList<string> lines)
            : base(fileName, labels, lines)
        {
            Outstanding = AggregateLabelText(labels["Outstanding"]);
            Success = AggregateLabelText(labels["Success"]);
            Failue = AggregateLabelText(labels["Failure"]);
        }

        public string Outstanding { get; set; }

        public string Success { get; set; }

        public string Failue { get; set; }


        public override string ToString()
        {
            return $"Title: {Title} with result ({FileName})";
        }
    }
}