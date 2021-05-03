using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static void UpdateTournamentResults(TournamentModel tournament)
        {
            var startingRound = tournament.CheckCurrentRound();
            var toScore = new List<MatchupModel>();
            
            foreach (var round in tournament.Rounds)
            {
                foreach (var roundMatch in round)
                {
                    if (roundMatch.Winner == null && (roundMatch.Entries.Any(x => x.Score != 0) || roundMatch.Entries.Count == 1))
                    {
                        toScore.Add(roundMatch);
                    }
                }
            }

            SetWinner(toScore);
            
            AdvanceBracketWinner(tournament, toScore);
            
            toScore.ForEach(x => GlobalConfig.Connection.UpdateMatchup(x));
            var endingRound = tournament.CheckCurrentRound();

            if (endingRound > startingRound)
            {
                tournament.AlertUsersToNewRound();
            }
        }

        public static void AlertUsersToNewRound(this TournamentModel model)
        {
            var currentRoundCount = model.CheckCurrentRound();
            var currentRound = model.Rounds.First(x => x.First().MatchupRound == currentRoundCount);

            foreach (var matchup in currentRound)
            {
                foreach (var me in matchup.Entries)
                {
                    foreach (var member in me.TeamCompeting.TeamMembers)
                    {
                        AlertPersonToNewRound(member,
                            matchup.Entries.FirstOrDefault(x => x.TeamCompeting != me.TeamCompeting));
                    }
                }
            }
        }

        private static void AlertPersonToNewRound(PersonModel member, MatchupEntryModel adversary)
        {
            if (member.EmailAddress.Length == 0)
            {
                return;
            }

            var to = member.EmailAddress;
            var subject = "";
            var body = new StringBuilder();

            if (adversary != null)
            {
                subject = $"You have a upcoming match against {adversary.TeamCompeting.TeamName}";

                body.AppendLine("You have a new matchup");
                body.Append("Adversary: ");
                body.AppendLine(adversary.TeamCompeting.TeamName);
                body.AppendLine();
                body.AppendLine();
                body.AppendLine("Good Match, have fun!");
            }
            else
            {
                subject = "You have a bye this round";
                
                body.AppendLine("Enjoy your round off!");
                body.AppendLine("Have fun!");
            }

            EmailLogic.SendEmail(to, subject, body.ToString());
        }

        private static int CheckCurrentRound(this TournamentModel model)
        {
            var currentRound = 1;

            foreach (var round in model.Rounds)
            {
                if (round.All(x => x.Winner != null))
                {
                    currentRound++;
                }
                else
                {
                    return currentRound;
                }
            }

            CompleteTournament(model);

            return --currentRound;
        }

        private static void CompleteTournament(TournamentModel tournament)
        {
            GlobalConfig.Connection.CompleteTournament(tournament);
            
            //var winners = tournament.Rounds.Last().First().Winner;
            
            if (tournament.Prizes.Count > 0)
            {
                var totalIncome = (tournament.EnteredTeams.Count * tournament.EntryFee);
                var firstPrize = tournament.Prizes.FirstOrDefault(x => x.PlaceNumber == 1);
                
                if (firstPrize != null)
                {
                    CalculatePayout(firstPrize, totalIncome);
                }
            }
            tournament.CompleteTournamentCaller();
        }

        private static decimal CalculatePayout(PrizeModel prize, decimal totalIncome)
        {
            decimal payout = 0;

            payout = prize.PrizeAmount > 0
                ? prize.PrizeAmount
                : decimal.Multiply(totalIncome, Convert.ToDecimal(prize.PrizePercentage / 100));

            return payout;
        }

        private static void AdvanceBracketWinner(TournamentModel tournament, List<MatchupModel> models)
        {

            foreach (var m in models)
            {
                foreach (var round in tournament.Rounds)
                {
                    foreach (var roundMatch in round)
                    {
                        foreach (var matchEntry in roundMatch.Entries)
                        {
                            if (matchEntry.ParentMatchup != null)
                            {
                                if (matchEntry.ParentMatchup.Id == m.Id)
                                {
                                    matchEntry.TeamCompeting = m.Winner;
                                    GlobalConfig.Connection.UpdateMatchup(roundMatch);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SetWinner(List<MatchupModel> toScore)
        {

            foreach (var matchupModel in toScore)
            {
                if (matchupModel.Entries.Count > 1)
                {
                    if (matchupModel.Entries[0].Score == matchupModel.Entries[1].Score)
                    {
                        throw new Exception("The match must have a winner.");
                    }

                    matchupModel.Winner = (matchupModel.Entries[0].Score > matchupModel.Entries[1].Score) ?
                        matchupModel.Entries[0].TeamCompeting : matchupModel.Entries[1].TeamCompeting;
                }
                else
                {
                    matchupModel.Winner = matchupModel.Entries[0].TeamCompeting;
                }
            }
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
            var teamsNeeded = 1;

            for (int i = 1; i <= rounds; i++)
            {
                teamsNeeded *= 2;
            }

            var output = teamsNeeded - teamCount;
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