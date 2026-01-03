using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Profile_Builder
{
    public partial class ProfileBuilder : Form
    {
        public ProfileBuilder()
        {
            InitializeComponent();
        }

        enum enGender { Male=1,Female=2};

        struct stProfileInfo
        {
            public string Name;
            public byte Age;
            public enGender Gender;
            public string Bio;
            public string Hobbies;
            public Image PersonImage;
        }
        stProfileInfo ProfileInfo = new stProfileInfo();


        void ResetForm()
        {
            pnPersonalInfo.Enabled = true;
            pnProfilePicture.Enabled = false;
            pnAdditionalInfo.Enabled = false;
            pnReviewConfirm.Enabled = false;

            tabControl1.SelectedIndex = 0;       
            txtName.Clear();
            txtAge.Clear();
            rbfemale.Checked = false;
            rbMale.Checked = false;
            pbImage.Image = null;
            label4.Text = "No Image";

            txtBio.Clear();
            txtHobbies.Clear();

        }
        string GetUserGender(enGender Gender)
        {
           return Gender == enGender.Male ? "Male" : "Female";
        }
        void ShowFullInfo()
        {
            pbUserImage.Image = pbImage.Image;

            lblUserName.Text = $"Name : {ProfileInfo.Name}";
            lblUserAge.Text = $"Age : {ProfileInfo.Age}";
            lblUserGender.Text = $"Gender : {GetUserGender(ProfileInfo.Gender)}";

            if(ProfileInfo.Bio != "")
                lblUserBio.Text = $"Bio : {ProfileInfo.Bio}";
            else
                lblUserBio.Text = $"Bio : Not Provided";

            if (ProfileInfo.Hobbies != "")
                lblUserHobbies.Text = $"Hobbies : {ProfileInfo.Hobbies}";
            else
                lblUserHobbies.Text = $"Hobbies : Not Provided";

        }

        void BackToAdditionalInfoTab()
        {
            pnReviewConfirm.Enabled = false;
            pnAdditionalInfo.Enabled = true;   
            tabControl1.SelectedIndex = 2;
        }

        void GoToReviewConfirmTab()
        {
            pnAdditionalInfo.Enabled = false;
            pnReviewConfirm.Enabled = true;
            tabControl1.SelectedIndex = 3;
        }
        void BackToProfilePicutreTab()
        {
            pnAdditionalInfo.Enabled = false;
            pnProfilePicture.Enabled = true;
            tabControl1.SelectedIndex = 1;
        }
        void GoToAdditionalInfoTab()
        {
            pnProfilePicture.Enabled = false;
            pnAdditionalInfo.Enabled = true;
            tabControl1.SelectedIndex = 2;
        }
        void GoToProfilePictureTab()
        {
            pnPersonalInfo.Enabled = false;
            pnProfilePicture.Enabled = true;
            tabControl1.SelectedIndex = 1;
        }
        void BackToPersonalInfoTab()
        {
            pnProfilePicture.Enabled = false;
            pnPersonalInfo.Enabled = true;
            tabControl1.SelectedIndex = 0;
        }
        void CloseForm()
        {
            this.Close();
        }

        void FillInfo()
        {
            ProfileInfo.Name = txtName.Text;
            ProfileInfo.Age = Convert.ToByte(txtAge.Text);

            if (rbMale.Checked) ProfileInfo.Gender = enGender.Male;
            else ProfileInfo.Gender = enGender.Female;


            ProfileInfo.PersonImage = pbImage.Image;
            ProfileInfo.Bio = txtBio.Text;
            ProfileInfo.Hobbies = txtHobbies.Text;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
                errorProvider1.SetError(txtName, "Name is required");
            else
                errorProvider1.SetError(txtName, "");
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAge.Text))
                errorProvider1.SetError(txtAge, "Age is required");
            else
                errorProvider1.SetError(txtAge, "");
        }

    
        private void btnCancelInfo_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtAge, "Enter numbers only");
            }
            
            else
                errorProvider1.SetError(txtAge, "");

        }

    
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files |*.png;*.jpg; *.jpeg;*.bmp;*.gif; *.tif;*.ico";

            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                label4.Text = "";
                pbImage.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void btnNextToProfilePicture_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAge.Text) || (!rbMale.Checked && !rbfemale.Checked))
                MessageBox.Show("Please Enter Full Information");

            else
                GoToProfilePictureTab();
                  
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage LeavingTab = tabControl1.TabPages[e.TabPageIndex];

            if (LeavingTab ==tabControl1.TabPages[0] && pnPersonalInfo.Enabled)  
                e.Cancel = true;

            
           else if (LeavingTab == tabControl1.TabPages[1] && pnProfilePicture.Enabled)
           
                e.Cancel = true;

              

            else if (LeavingTab == tabControl1.TabPages[2] && pnAdditionalInfo.Enabled)
           
                e.Cancel = true;

           
            else if (LeavingTab == tabControl1.TabPages[3] && pnReviewConfirm.Enabled)
                e.Cancel = true;
             

        }

        private void btnBackToPersonalInfo_Click(object sender, EventArgs e)
        {
            BackToPersonalInfoTab();
        }

        private void btnNextToAdditionalInfo_Click(object sender, EventArgs e)
        {
            if (pbImage.Image == null)
                errorProvider1.SetError(btnChooseImage, "Please select a profile picture");
            else
            {
               
                errorProvider1.SetError(btnChooseImage, "");
                GoToAdditionalInfoTab();
            }
           
        }

        private void btnCancelProfilePicture_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnCancelAdditionalInfo_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnBackToProfilePicture_Click(object sender, EventArgs e)
        {
            BackToProfilePicutreTab();
        }

        private void btnNextToReviewConfirm_Click(object sender, EventArgs e)
        {
            GoToReviewConfirmTab();
            FillInfo();
            ShowFullInfo();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Profile saved successfully!");
            ResetForm();
        }

        private void btnCancelReviewConfirm_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnBackToAdditionalInfo_Click(object sender, EventArgs e)
        {
            BackToAdditionalInfoTab();
        }
    }
}
