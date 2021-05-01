using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string Db = "Tournaments";
        
        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">The prize information.</param>
        /// <returns> THe prize information including the identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            
            using (var connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellphoneNumber", model.CellPhoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                p.Add("@Id", 0, DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@Id");

                foreach (var member in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", model.Id);
                    p.Add("@PersonId", member.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public BindingList<PersonModel> GetPerson_All()
        {
            BindingList<PersonModel> output;
            using (var connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                output = new BindingList<PersonModel>(connection.Query<PersonModel>("dbo.People_GetAll").ToList());
            }

            return output;
        }

        public BindingList<TeamModel> getTeam_All()
        {
            BindingList<TeamModel> output;
            using (var connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                output = new BindingList<TeamModel>(connection.Query<TeamModel>("select * from dbo.Teams").ToList());
            }

            return output;
        }

        public TournamentModel CreateTournament(TournamentModel model)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                SaveTournament(model, connection);
                
                SaveTournamentTeams(model, connection);
                
                SaveTournamentPrizes(model, connection);

                SaveTournamentRounds(model, connection);
                
                return model;
            }
        }

        public BindingList<TournamentModel> getTournament_All()
        {
            BindingList<TournamentModel> output;
            DynamicParameters p;

            using (var connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(Db)))
            {
                output = new BindingList<TournamentModel>(connection.Query<TournamentModel>("dbo.spTournaments_GetAll").ToList());

                foreach (var tournamentModel in output)
                {
                    p = new DynamicParameters();
                    p.Add("@TournamentId", tournamentModel.Id);
                    
                    // Populate Tournament Prizes
                    // TODO - Create Procedure PrizeGetByTournament
                    tournamentModel.Prizes = connection.Query<PrizeModel>("dbo.spPrizes_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();
                    
                    // Populate Tournament Teams
                    // TODO - Create Procedure TeamGetByTournament
                    tournamentModel.EnteredTeams = connection.Query<TeamModel>("dbo.spTeams_GetByTournament", p, commandType: CommandType.StoredProcedure).ToList();

                    // Populate Team with Team members
                    foreach (var team in tournamentModel.EnteredTeams)
                    {
                        p = new DynamicParameters();
                        
                        p.Add("@TeamId", team.Id);
                        // TODO - Create Procedure MemberByTeam
                        team.TeamMembers =  connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", p,
                            commandType: CommandType.StoredProcedure).ToList();
                    }
                    p = new DynamicParameters();
                    
                    // Populate Tournament Rounds
                    p.Add("@TournamentId", tournamentModel.Id);
                    var matchups = connection.Query<MatchupModel>("dbo.spMatchups_GetByTournament", p,
                        commandType: CommandType.StoredProcedure).ToList();

                    
                    foreach (var match in matchups)
                    {
                        p = new DynamicParameters();
                        p.Add("@MatchupId", match.Id);

                        match.Entries = connection.Query<MatchupEntryModel>("dbo.spMatchupEntries_GetByMatchup", p,
                            commandType: CommandType.StoredProcedure).ToList();

                        //
                        List<TeamModel> allTeams = new List<TeamModel>(getTeam_All());

                        if (match.WinnerId > 0)
                        {
                            match.Winner = allTeams.Where(x => x.Id == match.WinnerId).First();
                        }
                        
                        foreach (var matchEntry in match.Entries)
                        {
                            if (matchEntry.TeamCompetingId > 0)
                            {
                                matchEntry.TeamCompeting =
                                    allTeams.Where(x => x.Id == matchEntry.TeamCompeting.Id).First();
                            }

                            if (matchEntry.ParentMatchupId > 0)
                            {
                                matchEntry.ParentMatchup =
                                    matchups.Where(x => x.Id == matchEntry.ParentMatchupId).First();
                            }
                        }
                    }
                    
                    List<MatchupModel> currRow = new List<MatchupModel>();
                    int currRound = 1;
                    foreach (var m in matchups)
                    {
                        if (m.MatchupRound > currRound)
                        {
                            tournamentModel.Rounds.Add(currRow);
                            currRow = new List<MatchupModel>();
                            currRound += 1;
                        }
                        currRow.Add(m);
                    }
                    tournamentModel.Rounds.Add(currRow);
                }
            }

            return output;
        }

        private void SaveTournamentRounds(TournamentModel model, IDbConnection connection)
        {
            foreach (var round in model.Rounds)
            {
                foreach (var match in round)
                {
                    var p = new DynamicParameters();
                    p.Add("@MatchupRound", match.MatchupRound);
                    p.Add("@TournamentId", model.Id);
                    p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);

                    match.Id = p.Get<int>("@id");

                    foreach (var matchEntry in match.Entries)
                    {
                        p = new DynamicParameters();
                        p.Add("@MatchupId", match.Id);
                        if (matchEntry.ParentMatchup != null)
                        {
                            p.Add("@ParentMatchupId", matchEntry.ParentMatchup.Id);
                        }
                        else
                        {
                            p.Add("@ParentMatchupId", null);
                        }
                        if (matchEntry.TeamCompeting != null)
                        {
                            p.Add("@TeamCompetingId", matchEntry.TeamCompeting.Id);
                        }
                        else
                        {
                            p.Add("@TeamCompetingId", null);
                        }
                        p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);

                        connection.Execute("dbo.spMatchupsEntries_Insert", p, commandType: CommandType.StoredProcedure);

                        matchEntry.Id = p.Get<int>("@id");
                    }
                }
            }
        }

        private void SaveTournament(TournamentModel model, IDbConnection connection)
        {
            var p = new DynamicParameters();
            p.Add("@TournamentName", model.TournamentName);
            p.Add("@EntryFee", model.EntryFee);
            p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);

            connection.Execute("dbo.spTournaments_Insert", p, commandType: CommandType.StoredProcedure);

            model.Id = p.Get<int>("@id");
        }

        private void SaveTournamentTeams(TournamentModel model, IDbConnection connection)
        {
            foreach (var team in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@TeamId", team.Id);
                p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);

                connection.Execute("dbo.spTournamentsEntries_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }

        private void SaveTournamentPrizes(TournamentModel model, IDbConnection connection)
        {
            foreach (var prize in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@PrizeId", prize.Id);
                p.Add("@id", 0, dbType: DbType.Int32, ParameterDirection.Output);
                    
                connection.Execute("dbo.spTournamentsPrizes_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}