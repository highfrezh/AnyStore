using AnyStore.BLL;
using AnyStore.DAL;
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
    public partial class frmDeaCust : Form
    {
        public frmDeaCust()
        {
            InitializeComponent();
        }
        DeaCustBLL dc = new DeaCustBLL();
        DeaCustDAL dcdal = new DeaCustDAL();

        userDAL udal = new userDAL();
       
        private void button1_Click(object sender, EventArgs e)
        {
            //Getting all value form form
            dc.type = cmbType.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;
            //Getting the Id of the logged in User
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            dc.added_by = usr.id;

            //creating a boolean variable  to check if desler and customer is added or not
            bool success = dcdal.Insert(dc);

            if (success==true) 
            {
                //dealer or customer Added successfull
                MessageBox.Show("Dealer or Customer added successfully");
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = dcdal.select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Failed to add Dealer or customer
                MessageBox.Show("Failed to add Dealer or Customer");
            }

        }
        public void Clear()
        {
            txtDeaCustID.Text = "";
            txtName.Text = "";
            cmbType.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
        }
        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDeaCust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //closing the form
            this.Close();
        }

        private void frmDeaCust_Load(object sender, EventArgs e)
        {
            //Refreshing the Data Grid view
            DataTable dt = dcdal.select();
            dgvDeaCust.DataSource = dt;
        }

        private void dgvDeaCust_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Integer varaible to get the identity of row clicked
            int rowIndex = e.RowIndex;

            txtDeaCustID.Text = dgvDeaCust.Rows[rowIndex].Cells[0].Value.ToString();
            cmbType.Text = dgvDeaCust.Rows[rowIndex].Cells[1].Value.ToString();
            txtName.Text = dgvDeaCust.Rows[rowIndex].Cells[2].Value.ToString();
            txtEmail.Text = dgvDeaCust.Rows[rowIndex].Cells[3].Value.ToString();
            txtContact.Text = dgvDeaCust.Rows[rowIndex].Cells[4].Value.ToString();
            txtAddress.Text = dgvDeaCust.Rows[rowIndex].Cells[5].Value.ToString();

            btnAdd.Enabled=false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //Getting All the value from form
            dc.id = int.Parse(txtDeaCustID.Text);
            dc.type = cmbType.Text;
            dc.name = txtName.Text;
            dc.email = txtEmail.Text;
            dc.contact = txtContact.Text;
            dc.address = txtAddress.Text;
            dc.added_date = DateTime.Now;

            //Getting the Identity of user logged in.
            string loggeduser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggeduser);

            dc.added_by = usr.id;

            //Creating a boolean variable to check if dealer or customer is Updated or not;
            bool success = dcdal.update(dc);
            //If the value the dealer or customer is update successfully the value of success will be true else it will be false;
            if (success == true)
            {
                //Dealer or customer updated successfully 
                MessageBox.Show("Dealer or Customer Updated successfully");
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = dcdal.select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //Fail to Update dealer or customer
                MessageBox.Show("Update failed try again");
            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the Id of dealer or customer to be deleted
            dc.id = int.Parse(txtDeaCustID.Text);

            //Create boolean variable to check if the dealer or customer is deleted
            bool success = dcdal.delete(dc);

            //If the dealer or customer is deleted successfully the value of success will be true else it will be false.
            if (success == true)
            {
                //dealer or customer deleted successfully
                MessageBox.Show("Dealer or Customer is Delected successfully");
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = dcdal.select();
                dgvDeaCust.DataSource = dt;
            }
            else
            {
                //failed to delete deal or customer
                MessageBox.Show("Failed to delete products");
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            //Getting the keywords from form
            string keywords = txtSearch.Text;

            if (keywords != null)
            {
                //Search the dealer or customer
                DataTable dt = dcdal.Search(keywords);
                dgvDeaCust.DataSource = dt;

            }
            else
            {
                //display All the products
                DataTable dt = dcdal.select();
                dgvDeaCust.DataSource = dt;
            }
        }
    }
}
