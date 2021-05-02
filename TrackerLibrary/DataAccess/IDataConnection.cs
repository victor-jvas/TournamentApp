using System.Collections.Generic;
using System.ComponentModel;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        void CreatePrize(PrizeModel model);
        void CreatePerson(PersonModel personModel);
        void CreateTeam(TeamModel model);
        BindingList<PersonModel> GetPerson_All();
        BindingList<TeamModel> getTeam_All();
        void CreateTournament(TournamentModel model);
        BindingList<TournamentModel> getTournament_All();
        void UpdateMatchup(MatchupModel model);
    }
}