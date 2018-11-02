using System.Collections.Generic;
using System.Linq;

namespace DiMissionfileParser.Parser.Models
{
    public class MissionTextCampaign : MissionText
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

        public IEnumerable<MissionTextSituation> Situations { get; set; }

        public override string ToString()
        {
            return $"Campaign: {Description.Substring(0, 140)} ({FileName})";
        }

        public void InsertSituation(MissionTextSituation file)
        {
            if (Situations == null)
            {
                Situations = new List<MissionTextSituation>();
            }

            Situations = Situations.Union(new[] { file });
        }
    }
}