using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private BindingList<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private BindingList<PersonModel> selectedTeamMembers = new BindingList<PersonModel>(); 
        public CreateTeamForm()
        {
            InitializeComponent();
            
            //CreateSampleData();
            
            WireUpLists();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel("Tom", "Bombaldi", "", ""));
            availableTeamMembers.Add(new PersonModel("Aragorn", "Lotro", "", ""));
            
            selectedTeamMembers.Add(new PersonModel("Adams", "Safira", "", ""));
            selectedTeamMembers.Add(new PersonModel("Rose", "Ruby", "", ""));
        }

        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel(
                    firstNameValue.Text,
                    lastNameValue.Text,
                    emailValue.Text,
                    cellphoneValue.Text);

                GlobalConfig.Connection.CreatePerson(p);
                availableTeamMembers.Add(p);

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Invalid Field.");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            
            return true;
        }

        private void addTeamMemberButton_Click(object sender, EventArgs e)
        {
            var p = (PersonModel) selectTeamMemberDropDown.SelectedItem;

            if (p == null) return;
            availableTeamMembers.Remove(p);
            selectedTeamMembers.Add(p);
        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            var p = (PersonModel) teamMembersListBox.SelectedItem;

            if (p == null) return;
            selectedTeamMembers.Remove(p);
            availableTeamMembers.Add(p);

        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            var teamModel = new TeamModel
            {
                TeamName = teamNameValue.Text, TeamMembers = new List<PersonModel>(selectedTeamMembers)
            };

            teamModel = GlobalConfig.Connection.CreateTeam(teamModel);
            
            // TODO - reset the form after


        }
    }
}