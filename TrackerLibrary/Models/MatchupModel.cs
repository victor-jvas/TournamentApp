using System.Collections.Generic;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents a match between two teams
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        public TeamModel Winner { get; set; }
        public int WinnerId { get; set; }
        public int MatchupRound { get; set; }
        public int Id { get; set; }

        public string DisplayName
        {
            get
            {
                var result = "";
                
                foreach (var m in Entries)
                {
                    if (m.TeamCompeting != null)
                    {
                        if (result.Length == 0)
                        {
                            result = m.TeamCompeting.TeamName;
                        }
                        else
                        {
                            result += $" vs. {m.TeamCompeting.TeamName}";
                        }
                    }
                    else
                    {
                        result = "To be decided.";
                        break;
                    }
                }

                return result;
            }
        }
    }
}