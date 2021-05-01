using System;
using System.ComponentModel;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentDashBoardForm : Form
    {
        private BindingList<TournamentModel> availableTournaments =
            new(GlobalConfig.Connection.getTournament_All()); 
        public TournamentDashBoardForm()
        {
            InitializeComponent();
            loadExistingTournamentDropDown.DataSource = availableTournaments;
            loadExistingTournamentDropDown.DisplayMember = "TournamentName";
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            var tournamentForm = new CreateTournamentForm();
            tournamentForm.Show();
        }
    }
}