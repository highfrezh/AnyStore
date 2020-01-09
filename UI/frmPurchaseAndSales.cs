using AnyStore.BLL;
using AnyStore.DAL;
using AnyStore.DEL;
using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Code to close the window
            this.Hide();
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();

        DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        { 
            //Getting the transactionType from user dashboard;
            string Type = frmUserDashboard.transactionType;
            lblTop.Text =Type;

            //specify coloum for our transaction table
            transactionDT.Columns.Add("productName");
            transactionDT.Columns.Add("rate");
            transactionDT.Columns.Add("qty");
            transactionDT.Columns.Add("total");
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from textBox

            string keyword = txtSearch.Text;

            if (keyword=="")
            {
                //clear all the textBox
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }
            //write the code to get the detail and set value  to textBoxs
            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);

            //now transfer the from DeaCustBLL dc to textBoxs
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void pnlDGVTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //get the value from textbox
            string value = txtDiscount.Text;

            if (value=="")
            {
                //display error message
                MessageBox.Show("please provide the Discount first");
            }
            else
            {
                //get the discount value in decimal
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //Calculate the grand total based on discount
                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //Display the grand total in textbox
                txtGrandTotal.Text = grandTotal.ToString(); 
            }
        }

        private void pnlDeaCust_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtProductSearch_TextChanged(object sender, EventArgs e)
        {
            //get the keyword from productsearch textBox
            string keyword = txtProductSearch.Text;

            //check if we have value for txtproductSearch or not
            if (keyword=="")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtQty.Text = "";
                return;
            }
            //search the products and display in respective TextBoxs
            productsBLL p = pDAL.GetProductsForTransaction(keyword);

            //set the value from productBLL P to there respective TextBoxs.
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Getting productName, rate, Qty that customer or Dealer want to buy or Supply
            string productName = txtProductName.Text;
            decimal rate = decimal.Parse(txtRate.Text);
            decimal qty = decimal.Parse(txtQty.Text);

            decimal total = qty * rate; //Total is qty x rate

            //Getting the subtotal value from textboxs
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + total;

            //Check weather the product is selected or not 
            if (productName=="")
            {
                //Display Error Message 
                MessageBox.Show("Please select the product name and Try Again");
            }
           
            else
            {
                //Add the product to data grade views
                transactionDT.Rows.Add(productName,rate,qty,total);

                //show the product in the data grid veiws
                dgvAddedProducts.DataSource = transactionDT;

                //display the subtotal in the textBox
                txtSubTotal.Text = subTotal.ToString();

                //Clear the text boxs after it showed in data grid view
                txtProductSearch.Text ="";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtQty.Text = "0";

            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            //Check if GrandTotal have value or not if it has not value calculate the discount first
            string check = txtGrandTotal.Text;
            if (check=="")
            {
                //display Error Message to calculate discount
                MessageBox.Show("Calculate the discount and set the Grand Total first");
            }
            else
            {
                //Calculate Vat.
                //Getting the VAT percent first
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVat = ((100 + vat) / 100) * previousGT;

                //display the grandTotalWithVat in textBox
                txtGrandTotal.Text = grandTotalWithVat.ToString();
            }

        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            //Get the the paid amount and grand Total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount =paidAmount- grandTotal;

            //display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //get the value from purchase form first
            transactionBLL transaction = new transactionBLL();

            transaction.type = lblTop.Text;

            //get the ID of dealer or customer here.
            //let get the name of dealer first
            string DeaCustName = txtName.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(DeaCustName);

            transaction.dea_cus_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //Get the username of logged in user
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);

            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDT;


            //lets create boolean variable and set it value to false/
            bool success = false;

            //Actual Code to insert transaction and transaction detail
            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;
                //create a boolean Value and insert transaction
                bool w = tDAL.InsertTransaction(transaction, out transactionID);

                //use for loop to insert transaction Detail
                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //Get all the detail of products
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the products name and convert ID
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;
                    //Here Increase or Decrease product Quantity based on Pyurchase or Sales
                    string transactionType = lblTop.Text;

                    //let check weather we are in purchase or sales
                    bool x = false;
                    if (transactionType=="PURCHASE")
                    {
                        x =pDAL.Increaseproduct(transactionDetail.product_id, transactionDetail.qty);
                        //Increase the product
                    }
                    else if(transactionType=="SALES")
                    {
                        //decrease the product
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);

                    }
                    //Insert transaction Detail in to DataBase
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;
                }
                if (success == true)
                {
                    //Transaction Completed
                    scope.Complete();

                    //Code to Print the Bill of products
                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n\r\n ANYSTORE PVT.LTD. \r\n\r\n";
                    printer.SubTitle = "Highfrezh, Ajoke \r\n phone: 01-41XXXXX \r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount: " + txtDiscount.Text + "% \r\n" + "VAT: " + txtVat.Text + "% \r\n" + "Grand Total: "+txtGrandTotal.Text + "\r\n\r\n" + "Thanks for doing business with us";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts);



                    MessageBox.Show("Transaction Complete successfully");
                    //Clear datagrid veiw and all the TextBoxs
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtProductSearch.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";

                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
        }

        private void lblTop_Click(object sender, EventArgs e)
        {

        }

        private void lblTop_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
