using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiMissionfileParser.Parser
{
    public static class MissionTextStatistics
    {
        public static string GetCommonLabels(IList<MissionText> texts)
        {
            var commonLabels = texts.SelectMany(t => t.Labels.Keys).ToList();
            var distincts = commonLabels.Distinct().ToList();
            var counts = distincts.ToDictionary(i => i, i => commonLabels.Count(t => t == i));

            var commons = counts.Where(s => s.Value == texts.Count);
            var commonsLines = commons.Select(c => c.Key);
            return string.Join(",", commonsLines);
        }

        public static string GetAllLabels(IEnumerable<MissionText> texts)
        {
            var commonLabels = texts.SelectMany(t => t.Labels.Keys).ToList();
            var distincts = commonLabels.Distinct().ToList();
            var counts = distincts.ToDictionary(i => i, i => commonLabels.Count(t => t == i));

            var commonsLines = counts.Select(c => $"{c.Key} ({c.Value}files)");
            return string.Join(",", commonsLines);
        }

        public static void PrintStatistics(List<MissionText> missionTexts, Action<string> writeLine)
        {
            var grouping = missionTexts.GroupBy(g => g.Labels.Count);
            foreach (var part in grouping)
            {
                writeLine($"{part.Count()} files have {part.Key} labels");
                var texts = part.AsEnumerable().ToList();
                var areEqual = MissionText.AreEqual(texts);
                writeLine($"All texts have the same labels: {areEqual}");
                writeLine($"All labels: {GetAllLabels(texts)}");
                if (!areEqual)
                {
                    writeLine($"The common labels are: {GetCommonLabels(texts.ToList())}");
                }

                writeLine(string.Empty);
            }        }

        public static void PrintStatistics(List<MissionText> missionTexts, StreamWriter writer)
        {
            var grouping = missionTexts.GroupBy(g => g.Labels.Count);
            foreach (var part in grouping)
            {
                writer.WriteLine($"{part.Count()} files have {part.Key} labels");
                var texts = part.AsEnumerable().ToList();
                var areEqual = MissionText.AreEqual(texts);
                writer.WriteLine($"All texts have the same labels: {areEqual}");
                writer.WriteLine($"All labels: {GetAllLabels(texts)}");
                if (!areEqual)
                {
                    writer.WriteLine($"The common labels are: {GetCommonLabels(texts.ToList())}");
                }

                writer.WriteLine();
            }
        }
    }
}