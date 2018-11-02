using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DiMissionfileParser.Parser.Models;

namespace DiMissionfileParser.Parser.Functions
{
    public class MissionTextDataContext
    {
        public MissionTextDataContext(string path)
        {
            MissionTexts = GetMissionTexts(path);
        }

        public List<MissionText> MissionTexts { get; }

        private List<MissionText> GetMissionTexts(string path)
        {
            var missionTexts = new List<MissionText>();
            var dirInfo = new DirectoryInfo(path);
            foreach (var file in dirInfo.EnumerateFiles())
            {
                using (var reader = new StreamReader(file.FullName))
                {
                    var contents = reader.ReadToEnd().Split(Environment.NewLine);
                    var missionText = MissionText.Parse(file.Name, contents);
                    missionTexts.Add(missionText);
                }
            }

            var campaigns = missionTexts.Where(text => text is MissionTextCampaign).Cast<MissionTextCampaign>().ToList();
            var situations = missionTexts.Where(text => text is MissionTextSituation).Cast<MissionTextSituation>().ToList();
            foreach (var campaign in campaigns)
            {
                var fileNumber = int.Parse(campaign.FileName.Substring(1, 2));
                for (var i = fileNumber + 1; i < fileNumber + 9; i++)
                {
                    var fileName = $"C{i}.TXT";
                    var file = situations.Single(f => f.FileName == fileName);
                    campaign.InsertSituation(file);
                    missionTexts.Remove(file);
                }
            }
            return missionTexts;
        }

    }
}