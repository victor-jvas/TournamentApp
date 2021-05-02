using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TrackerLibrary.DataAccess.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        
        public void CreatePrize(PrizeModel model)
        {
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            
            prizes.Add(model);
            
            prizes.SaveToPrizeFile(PrizesFile);
        }

        public void CreatePerson(PersonModel personModel)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            personModel.Id = currentId;
            
            people.Add(personModel);

            people.SaveToPeopleFile(PeopleFile);
        }

        public void CreateTeam(TeamModel model)
        {
            throw new System.NotImplementedException();
        }

        public BindingList<PersonModel> GetPerson_All()
        {
            return new BindingList<PersonModel>(PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels());
        }

        public BindingList<TeamModel> getTeam_All()
        {
            throw new System.NotImplementedException();
        }

        public void CreateTournament(TournamentModel model)
        {
            throw new System.NotImplementedException();
        }

        public BindingList<TournamentModel> getTournament_All()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateMatchup(MatchupModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}