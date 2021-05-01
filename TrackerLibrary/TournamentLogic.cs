using System;
using System.Collections.Generic;
using System.Linq;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        public static void CreateRounds(TournamentModel model)
        {
            var randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);
            var rounds = GetNumberOfRounds(randomizedTeams.Count);
            var byes = GetNumberOfByes(rounds, randomizedTeams.Count);
            
            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));
            
            CreateRounds(model, rounds);
        }

        private static void CreateRounds(TournamentModel tournament, int rounds)
        {
            var round = 2;
            var previousRound = tournament.Rounds[0];
            var currRound = new List<MatchupModel>();
            var currMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (var match in previousRound)
                {
                    currMatchup.Entries.Add(new MatchupEntryModel{ParentMatchup = match});

                    if (currMatchup.Entries.Count > 1)
                    {
                        currMatchup.MatchupRound = rounds;
                        currRound.Add(currMatchup);
                        currMatchup = new MatchupModel();
                    }
                }

                tournament.Rounds.Add(currRound);
                previousRound = currRound;
                currRound = new List<MatchupModel>();
                round += 1;
            }
        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            var firstRound = new List<MatchupModel>();
            var match = new MatchupModel();

            foreach (var team in teams)
            {
                match.Entries.Add(new MatchupEntryModel{TeamCompeting = team});

                if (byes > 0 || match.Entries.Count > 1)
                {
                    match.MatchupRound = 1;
                    firstRound.Add(match);
                    match = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }
            }

            return firstRound;
        }

        private static int GetNumberOfByes(int rounds, int teamCount)
        {
            var output = 0;
            var teamsNeeded = 1;

            for (int i = 1; i <= rounds; i++)
            {
                teamsNeeded *= 2;
            }

            output = teamsNeeded - teamCount;
            return output;
        }

        private static int GetNumberOfRounds(int teamCount)
        {
            var numberOfRounds = 1;
            var val = 2;

            while (val < teamCount)
            {
                numberOfRounds += 1;
                val *= 2;
            }

            return numberOfRounds;
        }

        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}