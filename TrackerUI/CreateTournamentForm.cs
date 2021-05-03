using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        private BindingList<TeamModel> availableTeams = GlobalConfig.Connection.getTeam_All();
        private BindingList<TeamModel> selectedTeams = new BindingList<TeamModel>();
        private BindingList<PrizeModel> prizesList = new BindingList<PrizeModel>();
        public CreateTournamentForm()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentPlayersListBox.DataSource = selectedTeams;
            tournamentPlayersListBox.DisplayMember = "TeamName";

            prizeListBox.DataSource = prizesList;
            prizeListBox.DisplayMember = "PlaceName";
        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            var team = (TeamModel) selectTeamDropDown.SelectedItem;

            if (team == null) return;
            selectedTeams.Add(team);
            availableTeams.Remove(team);
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            var prizeForm = new CreatePrizeForm(this);
            prizeForm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            prizesList.Add(model);
        }

        private void createNewTeamLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var teamForm = new CreateTeamForm(this);
            teamForm.Show();
        }

        public void TeamComplete(TeamModel model)
        {
            availableTeams.Add(model);
        }

        private void removeSelectedPlayersButton_Click(object sender, EventArgs e)
        {
            var team = (TeamModel) tournamentPlayersListBox.SelectedItem;

            if (team == null) return;
            selectedTeams.Remove(team);
            availableTeams.Add(team);
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            var prize = (PrizeModel) prizeListBox.SelectedItem;
            
            if(prize == null) return;
            prizesList.Remove(prize);
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            var tournament = new TournamentModel();

            if (!ValidateForm()) return;

            var fee = decimal.Parse(entryFeeValue.Text);
            
            tournament.TournamentName = tournamentNameValue.Text;
            tournament.EntryFee = fee;
            tournament.EnteredTeams = new List<TeamModel>(selectedTeams);
            tournament.Prizes = new List<PrizeModel>(prizesList);
            
            TournamentLogic.CreateRounds(tournament);

            GlobalConfig.Connection.CreateTournament(tournament);
            
            tournament.AlertUsersToNewRound();
            var form = new TournamentViewerForm(tournament);
            form.Show();
            this.Close();
        }

        private bool ValidateForm()
        {
            var validFee = decimal.TryParse(entryFeeValue.Text, out _);

            if (!validFee)
            {
                MessageBox.Show(
                    @"Insert a valid entry fee",
                    @"Invalid Fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            if (tournamentNameValue.Text.Length < 1)
            {
                MessageBox.Show(
                    @"Insert a valid name",
                    @"Invalid Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            return true;
        }
    }
}