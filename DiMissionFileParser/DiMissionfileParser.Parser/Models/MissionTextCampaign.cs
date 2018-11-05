using System.Collections.Generic;
using DiMissionfileParser.Parser.Functions;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionTextCampaign : MissionText
    {
        public string Description { get; set; }

        public string Successful { get; set; }

        public string Failure { get; set; }

        public MissionTextCampaign(string fileName, Dictionary<string, MissionContents> labels, IList<string> lines)
            : base(fileName, labels, lines)
        {
            Situations = new List<MissionTextSituation>();
            Description = AggregateLabelText(labels["Description"]);
            Successful = AggregateLabelText(labels["Successful"]);
            Failure = AggregateLabelText(labels["Failure"]);
        }

        public IList<MissionTextSituation> Situations { get; }

        public override string ToString()
        {
            return $"Campaign: {Description.Substring(0, 140)} ({FileName})";
        }
    }
}