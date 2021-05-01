﻿using System.Collections.Generic;
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
        
        public PrizeModel CreatePrize(PrizeModel model)
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

            return model;

        }

        public PersonModel CreatePerson(PersonModel personModel)
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

            return personModel;
        }

        public TeamModel CreateTeam(TeamModel model)
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

        public TournamentModel CreateTournament(TournamentModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}