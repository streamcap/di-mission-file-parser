using System.Collections.Generic;

namespace DiMissionfileParser.Parser
{
    internal class MissionTextTitled : MissionText
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

    internal class MissionTextCampaign : MissionText
    {
        public string Description { get; set; }

        public string Successful { get; set; }

        public string Failure { get; set; }

        public MissionTextCampaign(string fileName, Dictionary<string, IList<string>> labels, IList<string> lines)
            : base(fileName, labels, lines)
        {
            Description = AggregateLabelText(labels["Description"]);
            Successful = AggregateLabelText(labels["Successful"]);
            Failure = AggregateLabelText(labels["Failure"]);
        }

        public override string ToString()
        {
            return $"Campaign description: {Description} ({FileName})";
        }
    }
}