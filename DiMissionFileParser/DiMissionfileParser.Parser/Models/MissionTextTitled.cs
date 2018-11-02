using System.Collections.Generic;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionTextTitled : MissionText
    {
        public MissionTextTitled(string fileName, Dictionary<string, IList<string>> labels, IList<string> lines)
            : base(fileName, labels, lines)
        {
            Title = AggregateLabelText(labels["Title"]);
            Description = AggregateLabelText(labels["Description"]);
            Para = AggregateLabelText(labels["Para"]);
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Para { get; set; }

        public override string ToString()
        {
            return $"Title: {Title} ({FileName})";
        }
    }
}