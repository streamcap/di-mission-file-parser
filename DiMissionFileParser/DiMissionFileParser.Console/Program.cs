using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DiMissionfileParser.Parser;

namespace DiMissionFileParser.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting...");
            var dirInfo = new DirectoryInfo(@"C:\temp\apache_texts\texts");
            var missionTexts = new List<MissionText>();
            foreach (var file in dirInfo.EnumerateFiles())
            {
                using (var reader = new StreamReader(file.FullName))
                {
                    var contents = reader.ReadToEnd().Split(Environment.NewLine);
                    var missionText = MissionText.Parse(file.Name, contents);
                    missionTexts.Add(missionText);
                }
            }

            missionTexts = missionTexts.Where(t => t.Labels.Any()).ToList();

            MissionTextStatistics.PrintStatistics(missionTexts, System.Console.WriteLine);

            foreach (var missionText in missionTexts)
            {
                System.Console.WriteLine(missionText.ToString());
            }
        }
    }
}
