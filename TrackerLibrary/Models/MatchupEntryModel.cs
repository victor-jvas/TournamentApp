namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {

        
        /// <summary>
        /// Represents one team in the matchup
        /// </summary>
        public TeamModel TeamCompeting { get; set; }

        public int TeamCompetingId { get; set; }
        
        /// <summary>
        /// Represents the final score of this team on the matchup
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Represents the matchup that this team came from as winner
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

        public int ParentMatchupId { get; set; }

        public int Id { get; set; }
    }
}