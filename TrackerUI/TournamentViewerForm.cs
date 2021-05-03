using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;
        private List<int> rounds = new List<int>();
        private List<MatchupModel> roundsMatchups = new List<MatchupModel>();

        public TournamentViewerForm(TournamentModel tournamentModel)
        {
            InitializeComponent();

            tournament = tournamentModel;

            tournament.OnTournamentComplete += Tournament_OnTournamentComplete;
            LoadFormData();
            LoadRounds();
        }

        private void Tournament_OnTournamentComplete(object sender, EventArgs e)
        {
            var winners = tournament.Rounds.Last().First().Winner;

            MessageBox.Show(
                $@"The tournament: {tournament.TournamentName} has been concluded." + 
                $@"Congratulations to {winners.TeamName} for winning!",
                @"Winner", MessageBoxButtons.OK);
            this.Close();
        }

        private void LoadFormData()
        {
            tournamentNameLabel.Text = tournament.TournamentName;
            
        }

        private void UpdateDropDowList()
        {
            roundDropDown.DataSource = null;
            roundDropDown.DataSource = rounds;
        }

        private void UpdateMatchupsList()
        {
            matchupListBox.DataSource = null;
            matchupListBox.DataSource = roundsMatchups;
            matchupListBox.DisplayMember = "DisplayName";
        }

        private void LoadRounds()
        {
            rounds.Add(1);
            int currentRound = 1;

            foreach (var matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound > currentRound)
                {
                    currentRound++;
                    rounds.Add(currentRound);
                }
            }
            
            UpdateDropDowList();
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void LoadMatchups()
        {
            var round = (int) roundDropDown.SelectedItem;
            roundsMatchups.Clear();
            
            foreach (var matchups in tournament.Rounds)
            {
                if (matchups.First().MatchupRound == round)
                {
                    foreach (var m in matchups)
                    {
                        if (!unplayedOnlyCheckBox.Checked || m.Winner == null)
                        {
                            roundsMatchups.Add(m);
                        }
                    }
                }
            }
            
            UpdateMatchupsList();
            DisplayMatchupInfo();
        }

        private void DisplayMatchupInfo()
        {
            var isVisible = roundsMatchups.Count > 0;

            teamOneNameLabel.Visible = isVisible;
            teamOneScoreValue.Visible = isVisible;
            scoreTeamOneLabel.Visible = isVisible;
            teamTwoNameLabel.Visible = isVisible;
            teamTwoScoreValue.Visible = isVisible;
            scoreTeamTwoLabel.Visible = isVisible;
            scoreButton.Visible = isVisible;
            vsLabel.Visible = isVisible;

        }

        private void LoadMatch()
        {
            var m = (MatchupModel) matchupListBox.SelectedItem;

            if (m == null) return;

                for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        teamOneNameLabel.Text = m.Entries[0].TeamCompeting.TeamName;
                        teamOneScoreValue.Text = m.Entries[0].Score.ToString();
                        
                        teamTwoNameLabel.Text = "<Bye>";
                        teamTwoScoreValue.Text = "0";
                    }
                    else
                    {
                        teamOneNameLabel.Text = "To be decided";
                        teamOneScoreValue.Text = "";
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        teamTwoNameLabel.Text = m.Entries[1].TeamCompeting.TeamName;
                        teamTwoScoreValue.Text = m.Entries[1].Score.ToString();
                    }
                    else
                    {
                        teamTwoNameLabel.Text = "To be decided";
                        teamTwoScoreValue.Text = "";
                    }
                }
            }
        }

        private void matchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatch();
        }

        private void unplayedOnlyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadMatchups();
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            SetMatch();
            LoadMatchups();
        }

        private void SetMatch()
        {
            var m = (MatchupModel) matchupListBox.SelectedItem;
            var teamOneScore = 0.0;
            var teamTwoScore = 0.0;
            

            if (m == null) return;

            for (int i = 0; i < m.Entries.Count; i++)
            {
                if (i == 0)
                {
                    if (m.Entries[0].TeamCompeting != null)
                    {
                        bool scoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
                        if (scoreValid)
                        {
                            m.Entries[0].Score = teamOneScore;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Score for team 1.");
                            return;
                        }
                    }
                }

                if (i == 1)
                {
                    if (m.Entries[1].TeamCompeting != null)
                    {
                        var scoreValid = double.TryParse(teamTwoScoreValue.Text, out teamTwoScore);
                        if (scoreValid)
                        {
                            m.Entries[1].Score = teamTwoScore;
                        }
                        else
                        {
                            MessageBox.Show("Invalid Score for team 2.");
                            return;
                        }
                    }
                }
            }
            TournamentLogic.UpdateTournamentResults(tournament);
        }
    }
}