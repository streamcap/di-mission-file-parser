using System.Collections.Generic;

namespace DiMissionfileParser.Parser.Functions
{
    public class MissionContents : List<string>
    {
        public MissionContents()
        {
        }

        public MissionContents(IEnumerable<string> lines)
            : base(lines)
        {

        }
    }
}