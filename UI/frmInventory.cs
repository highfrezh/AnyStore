using AnyStore.DAL;
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
    public partial class frmInventory : Form
    {
        public frmInventory()
        {
            InitializeComponent();
        }
        categoryDAL cdal = new categoryDAL();
        productsDAL pdal = new productsDAL();
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Funtionality to  close form 
            this.Close();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            //Display the category in the comboBox
            DataTable cDT = cdal.select();

            cmbCategory.DataSource = cDT;

            //Give the value member and diplay member for combobox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            //Display all the product in data grid veiw the form is loaded 
            DataTable pdt = pdal.select();
            dgvProducts.DataSource = pdt;

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display All product based on categoty
            string category = cmbCategory.Text;
            DataTable dt = pdal.DisplayProductByCategory(category);
            dgvProducts.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            //Display All products when this button is clicked
            DataTable dt = pdal.select();
            dgvProducts.DataSource = dt;
        }
    }
}