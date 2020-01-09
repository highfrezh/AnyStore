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
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Add code to Hide this form
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        categoryDAL cdal = new categoryDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        private void frmProducts_Load(object sender, EventArgs e)
        {
            //Create a DataTable to Hold data from database
            DataTable categoryDT = cdal.select();
            //Specify Datasource for category comboBox
            cmbCategory.DataSource = categoryDT;
            //Specify Display member and Value member for ComboBox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            //Code to Load all products in Data grid view.
            DataTable dt = pdal.select();
            dgvProducts.DataSource = dt;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Getting all the value from products form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            //Getting the Usernae of logged in user
            string loggeduser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggeduser);

            p.added_by = usr.id;

            //Create a boolean variable to check if the products is added successfully or NOT
            bool success = pdal.Insert(p);
            //If the products is added successfully the value of success will be true else it will be false.
            if (success==true)
            {
                //products added successfully
                MessageBox.Show("Products added successfully");
                //Calling the Clear Method
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = pdal.select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Fail to Add products
                MessageBox.Show("Fail to add new  product try again");
            }
                
        }
        public void Clear()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            cmbCategory.Text = "";
            txtID.Text ="";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //create Inter Variable to know which product was clicked
            int rowIndex = e.RowIndex;
            //Display the value on respective boxes;
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Getting the value from UI or product form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.added_date = DateTime.Now;

            //Getting the username of logged user for added_by
            string loggeduser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggeduser);

            p.added_by = usr.id;

            //Creating a boolean variable to check if products is Updated or not;
            bool success = pdal.update(p);
            //If the value the products is update successfully the value of success will be true else it will be false;
            if (success==true)
            {
                //products updated successfully 
                MessageBox.Show("Products Updated successfully");
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = pdal.select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Fail to Update products
                MessageBox.Show("Update failed try again");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the Id of products to be deleted
            p.id = int.Parse(txtID.Text);

            //Create boolean variable to check if the Products is deleted
            bool success = pdal.Delete(p);

            //If the products is deleted successfully the value of success will be true else it will be false.
            if (success==true)
            {
                //products deleted successfully
                MessageBox.Show("Products is Delected successfully");
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = pdal.select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //failed to deleted products
                MessageBox.Show("Failed to delete products");
            }
           
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Getting the keywords from form
            string keywords = txtSearch.Text;

            if (keywords!=null)
            {
                //Search the products
                DataTable dt = pdal.search(keywords);
                dgvProducts.DataSource = dt;

            }
            else
            {
                //display All the products
                DataTable dt = pdal.select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}
