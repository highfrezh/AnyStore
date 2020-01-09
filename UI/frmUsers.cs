using AnyStore.BLL;
using AnyStore.DEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }

        private void lblUserID_Click(object sender, EventArgs e)
        {

        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Getting Data from UI
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;

            //Getting the username of the loggedin user
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = dal.GetIDFromUsername(loggedUser);
            u.added_by = usr.id;

            // Inserting in to database
            bool success = dal.Insert(u);
            // If the data is successfully inserted then the value of success will be true else it will be false
            if (success==true)
            {
                //Data successfully inserted
                MessageBox.Show("User successfully Created.");
                clear();
            }
            else
            {
                //Fail to insert Data
                MessageBox.Show("Fail to Create New user.");
            }
            //Refresh Data Grid View
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }
        private void clear()
        {
            txtUserID.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Get the value from user UI
            u.id = Convert.ToInt32(txtUserID.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEmail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;

            //update data into database
            bool success = dal.update(u);
            // if data is updated successfully then the valu of success is true else it will be false
            if (success==true)
            {
                //Data updated successfully
                MessageBox.Show("User successfully updated");
                clear();

            }
            else
            {
                //Data update fail
                MessageBox.Show("Failed to Update User");
                clear();

            }
            //refreshing Data Grid view
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;


        }

        private void dgvUser_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get the index of partitular Row
            int rowIndex = e.RowIndex;
            txtUserID.Text = dgvUser.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dgvUser.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dgvUser.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvUser.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dgvUser.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dgvUser.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dgvUser.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dgvUser.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dgvUser.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dgvUser.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Getting User ID from form 
            u.id = Convert.ToInt32(txtUserID.Text);

            bool success = dal.Delete(u);
            // if Data is deleted then the value of  success will be true else the value of success will be false
            if (success==true)
            {
                // user deleted successfully
                MessageBox.Show("User Successfully Deleted");
                clear();
            }
            else
            {
                // failed to delete user
                MessageBox.Show("Failed to Delete User");
            }
            // Refreshing Data Grid View
            DataTable dt = dal.Select();
            dgvUser.DataSource = dt;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // get keywords from text box 
            string keywords = txtSearch.Text;

            //check if keywords have value or not 
            if (keywords !=null)
            {
                //show user base on keyword
                DataTable dt = dal.search(keywords);
                dgvUser.DataSource = dt;

            }
            else
            {
                // show all user from database
                DataTable dt = dal.Select();
                dgvUser.DataSource = dt;
            }
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
