using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class productsDAL
    {
        //Creating Static string method for database connection
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select method for products modules
        public DataTable select()
        {
            //create sql conection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Datatable to hold dat from database
            DataTable dt = new DataTable();

            try
            {
                //writing sql quary to select all product from database
                string sql = "SELECT * FROM tbl_products";

                //Create sql command to execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Create Data adapter to hold the value from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //Open database connection
                conn.Open();
                //passing that from adapter to dt;
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;

        }
        #endregion
        #region Method to Insert products in Database
        public bool Insert(productsBLL p)
        {
            //Creating a boolean varaible and set it default value to false
            bool isSuccess = false;

            //Sql connection for database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Sql Quary to Insert Products into Database
                string sql = "INSERT INTO tbl_products (name, category, description, rate, qty, added_date, added_by) VALUES (@name, @category, @description, @rate, @qty, @added_date, @added_by)";

                //Sql command to pass the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value through parameters
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@category", p.category);
                cmd.Parameters.AddWithValue("@description", p.description);
                cmd.Parameters.AddWithValue("@rate", p.rate);
                cmd.Parameters.AddWithValue("@qty", p.qty);
                cmd.Parameters.AddWithValue("@added_date", p.added_date);
                cmd.Parameters.AddWithValue("@added_by", p.added_by);

                //Opening database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the quary executed successfully the value of rows will be greater than 0 else it will be less than 0
                if (rows>0)
                {
                    //Executed Successfully
                    isSuccess = true;

                }
                else
                {
                    //Failed to Execute
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        #endregion
        #region Method to update product in database
        public bool update(productsBLL p)
        {
            //Creating A Boollean Variable and set it default value to false;
            bool isSuccess = false;

            //Create Sql connection for database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Create Sql Quary to update in database
                string sql = "UPDATE tbl_products SET name=@name, category=@category, description=@description, rate=@rate, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //creating Sql command to pass the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value through parameter and cmd;
                cmd.Parameters.AddWithValue("name", p.name);
                cmd.Parameters.AddWithValue("category", p.category);
                cmd.Parameters.AddWithValue("description", p.description);
                cmd.Parameters.AddWithValue("rate", p.rate);
                cmd.Parameters.AddWithValue("qty", p.qty);
                cmd.Parameters.AddWithValue("added_date", p.added_date);
                cmd.Parameters.AddWithValue("added_by", p.added_by);
                cmd.Parameters.AddWithValue("id", p.id);

                //open the database connection
                conn.Open();

                //create Int variable to check if the quary is executed Successfully
                int rows = cmd.ExecuteNonQuery();

                //if the quary is executed successfully then the value of rows is greater than 0 else it will be less than 0.
                if (rows>0)
                {
                    //Quary executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to execute Quary
                    isSuccess = false;
                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return isSuccess;
        }

        #endregion
        #region Method to Delect product from database
        public bool Delete(productsBLL p)
        {
            //Create A Boolean variable and set it default value to false,
            bool isSuccess = false;

            //SQL connection to connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write Quary to Delete products from Database
                string sql = "DELETE FROM tbl_products WHERE id=@id";

                //Sql command to pass the Value 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value using cmd.
                cmd.Parameters.AddWithValue("@id", p.id);

                //Open the Database 
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //If the Quary is executed successfully the value of rows will be greater than 0 else it will less than 0.
                if (rows>0)
                {
                    //Quary executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to execute quary
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }
        #endregion
        #region Search method for product Modules
        public DataTable search(string keywords)
        {
            //SQL connectionfor database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Creating Datatable to hold data from database
            DataTable dt = new DataTable();

            try
            {
                //Sql quary to search the products from Database
                string sql = "SELECT * FROM tbl_products WHERE id LIKE '%"+keywords+ "%' OR name LIKE '%" + keywords + "%' OR category LIKE '%" + keywords + "%'";
                //create sql command to execute the quary 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //sql Adapter to hold the data from database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open database connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        #endregion
        #region Methods to search products in the transaction module 
        public productsBLL GetProductsForTransaction(string keywords)
        {
            //Creating an object for ProductsBLL class and return it;
            productsBLL p = new productsBLL();

            //Sql connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Creating Datatable to hold Data from Database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql Quarey to search from Database 
                string sql = "SELECT name, rate, qty FROM tbl_products WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%'";

                //Creating SqlCommand to Execute the Quary
                //SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data from database
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //opening Database connection
                conn.Open();

                //passing value from adapter to DataTable dt
                adapter.Fill(dt);

                //if we have the value of dt we need to save int dealercustmer BLL
                if (dt.Rows.Count > 0)
                {
                    p.name = dt.Rows[0]["name"].ToString();
                    p.qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                    p.rate = decimal.Parse(dt.Rows[0]["rate"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return p;
        }
        #endregion
        #region Method To get the ID of dealer or Customer based on Name
        public productsBLL GetProductIDFromName(string productName)
        {
            //first create object of DeaCustBLL and return it.
            productsBLL p = new productsBLL();

            //SQL connection here
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create data table to hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql Quary to get customer or dealer ID based on NAme. 
                string sql = "SELECT id FROM tbl_products WHERE name='" + productName + "'";

                //Create sql Adapter to Execute the Quary
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //opening Database connection
                conn.Open();

                //passing value from adapter to DataTable dt
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    //passing the value from dt to DeaCust dc.
                    p.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return p;
         
        }
        #endregion
        #region Method to get current quantity from the DAtabasebase on products ID
        public decimal GetProductQty(int ProductID)
        {
            //SQL connection first
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create decimal variable and set it default value to zero
            decimal qty = 0;

            //create a data table to hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //create SQL Quary to get quantity from database
                string sql = "SELECT qty FROM tbl_products WHERE id = "+ProductID;

                //create SQL command to execute the quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //create SQL adapter lto Execute the QUary
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open database connection
                conn.Open();

                //passing the value from adapter to data table
                adapter.Fill(dt);

                //let check if the data table has value or not
                if (dt.Rows.Count>0)
                {
                    qty = decimal.Parse(dt.Rows[0]["qty"].ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return qty;

        }
        #endregion
        #region Method to Update Quantity
        public bool UpdateQuantity(int ProductID, decimal Qty)
        {
            //Create Boolean Variable And set it Default value to false.
            bool success = false;

            //Create SQL connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Write SQL Quary to Update the database
                string sql = "UPDATE tbl_products SET qty=@qty WHERE id=@id";

                //Create SQll command 
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value to parameter
                cmd.Parameters.AddWithValue("@qty", Qty);
                cmd.Parameters.AddWithValue("@id", ProductID);

                //open database connection
                conn.Open();

                //create in variable and check if the quar is executed or not
                int row = cmd.ExecuteNonQuery();
                //let check if the quary is executed successfull or not 
                if (row>0)
                {
                    //quary is executed Successfully
                    success = true;
                }
                else
                {
                    //failed to execute the Quary
                    success = false;
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region Method to increase Products
        public bool Increaseproduct(int ProductID, decimal IncreaseQty)
        {
            //Create Boolean Variable And set it Default value to false.
            bool success = false;

            //Create SQL connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Get the current Qty from database based on ID
                decimal currentQty = GetProductQty(ProductID);

                //Increase the current Quantity by the Qty purchase by dealer
                decimal NewQty = currentQty + IncreaseQty;

                //Update the product quantity now
                success = UpdateQuantity(ProductID, NewQty);
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region Method to Decrease Products
        public bool DecreaseProduct(int ProductID, decimal Qty)
        {
            //Create Boolean Variable And set it Default value to false.
            bool success = false;

            //Create SQL connection to connect database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Get the current  product Qty from database based on ID
                decimal currentQty = GetProductQty(ProductID);

                //decrease the product Quantity based on product sales.
                decimal NewQty = currentQty - Qty;

                //Update the product in database.
                success = UpdateQuantity(ProductID, NewQty);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return success;
        }
        #endregion
        #region Method to display Product based on catagories
        public DataTable DisplayProductByCategory(string category)
        {
            //SQL connection first
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create DataTable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Quary to displat Products based on Category
                string sql = "SELECT * FROM tbl_products WHERE category='"+category+"'";
                //sql command to execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Sql Adapter to hold the data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open database connection
                conn.Open();

                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        #endregion

    }
}
 