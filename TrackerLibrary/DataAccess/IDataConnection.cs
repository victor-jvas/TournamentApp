using System.Collections.Generic;
using System.ComponentModel;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel model);
        PersonModel CreatePerson(PersonModel personModel);
        TeamModel CreateTeam(TeamModel model);
        BindingList<PersonModel> GetPerson_All();
        BindingList<TeamModel> getTeam_All();
    }
}