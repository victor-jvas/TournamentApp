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
    }
}