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
    public partial class frmCategories : Form
    {
        public frmCategories()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCategoryID_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        categoryBLL c = new categoryBLL();
        categoryDAL dal = new categoryDAL();
        userDAL udal = new userDAL();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //geting value form Category form
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;

            // Getting ID in added_by field;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //passing id of logged in user added by field
            c.adde_by = usr.id;

            //creating a boolean method to insert data into database.
            bool success = dal.Insert(c);

            //If the category is Inserted SuccessFully the the value of success will be true else it will be false;
            if (success==true)
            {
                //New category inserted Successfully 
                MessageBox.Show("New category Inserted Successfully.");
                Clear();
                //Refreshing the Data Grid view
                DataTable dt = dal.select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Failed to Insert new Category
                MessageBox.Show("Failed to Insert New category");
                Clear();
            }
     
        }
        public void Clear()
        {
            txtCategoryID.Text = "";
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";

        }

        private void frmCategories_Load(object sender, EventArgs e)
        {
            //Here We write code to display all Category in Data grid view
            DataTable dt = dal.select();
            dgvCategories.DataSource = dt;

        }

        private void dgvCategories_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Finding the Row index of the row Clicked on Data Grid View
            int RowIndex = e.RowIndex;
            txtCategoryID.Text = dgvCategories.Rows[RowIndex].Cells[0].Value.ToString();
            txtTitle.Text = dgvCategories.Rows[RowIndex].Cells[1].Value.ToString();
            txtDescription.Text = dgvCategories.Rows[RowIndex].Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Get the value from Category form
            c.id = int.Parse(txtCategoryID.Text); // "int.Perse" is use to covert int variable to string Variable;
            c.title = txtTitle.Text;
            c.description = txtDescription.Text;
            c.added_date = DateTime.Now;
            // Getting ID in added_by field;
            string loggedUser = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUser);
            //passing id of logged in user added by field
            c.adde_by = usr.id;

            
            //creating boolean Variable to update category and check
            bool success = dal.update(c);
            //If Category is update successfully then the value of success will be true else the value of success will be false
            if (success==true)
            {
                //Category Updated Successfully
                MessageBox.Show(" Category Update Successful");
                Clear();
                //Refresh Data Grid view
                DataTable dt = dal.select();
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Category Failed to Update
                MessageBox.Show("Failed to Update Category");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the ID of Category we want delete
            c.id = int.Parse(txtCategoryID.Text);

            //Create Boolean variable to Delete the Category
            bool success = dal.Delete(c);

            //If the Category is delete successfully the value of success will be true else the value of success will be false
            if (success==true)
            {
                //Category Delete Successfully
                MessageBox.Show("Category Delete Successfully");
                Clear();
                DataTable dt = dal.select();
                dgvCategories.DataSource = dt;

            }
            else
            {
                //Failed to Delete Category
                MessageBox.Show("Failed to Delete Category");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;

            //Filter the Category base on keyword;
            if (keywords!=null)
            {
                //Use Search method to Display Category
                DataTable dt = dal.search(keywords);
                dgvCategories.DataSource = dt;
            }
            else
            {
                //Use Select method to disply all category
                DataTable dt = dal.select();
                dgvCategories.DataSource = dt;
            }
        }
    }
}
