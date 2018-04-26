using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {

        private List<PersonModel> availableTeamMember = GlobalConfig.Connection.GetPersonAll();
        private List<PersonModel> selectedTeammembers = new List<PersonModel>();
        private ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
            WireUpList();

        }

        private void CreateSampleData()
        {
            availableTeamMember.Add(new PersonModel {FirstName = "Martynas", LastName = "Sa"});
            availableTeamMember.Add(new PersonModel {FirstName = "Vilius", LastName = "Au"});

            selectedTeammembers.Add(new PersonModel {FirstName = "Agne", LastName = "Pi"});
            selectedTeammembers.Add(new PersonModel {FirstName = "Egle", LastName = "Ju" });
        }

        private void WireUpList()
        {
            selectopDown.DataSource = null;

            selectopDown.DataSource = availableTeamMember;
            selectopDown.DisplayMember = "FullName";

            teamMemberListBox.DataSource = null;

            teamMemberListBox.DataSource = selectedTeammembers;
            teamMemberListBox.DisplayMember = "FullName";

            
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = EmailValue.Text;
                p.CellphoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);
                selectedTeammembers.Add(p);
                WireUpList();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                EmailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Fill in all the fiels");
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

            if (EmailValue.Text.Length == 0)
            {
                return false;
            }

            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addTeamMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel) selectopDown.SelectedItem;


            if (p != null)
            {
                availableTeamMember.Remove(p);
                selectedTeammembers.Add(p);
                WireUpList();
            }

        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMemberListBox.SelectedItem;
            if (p != null)
            {
                selectedTeammembers.Remove(p);
                availableTeamMember.Add(p);
                WireUpList();
            }
        }

        private void CreateTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();
            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeammembers;
            GlobalConfig.Connection.CreateTeam(t);

            callingForm.TeamComplete(t);
            this.Close();
        }
    }
}
