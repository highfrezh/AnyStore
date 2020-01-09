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
    class DeaCustDAL
    {
        //static string method to connect to database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method for Dealer and Customer
        public DataTable select()
        {
            //Sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create a DataTable to hold data from database and return it
            DataTable dt = new DataTable();

            try
            {
                //Creating SQL quary to select all data from database
                string sql = "SELECT * FROM tbl_dea_cust";

                //Creating Sql command to execute the quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Creating Sql Adapter to hold the data form database temporarily
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open database connection to connect to database
                conn.Open();

                //passing the data form adapter to Datatable 
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
        #region Insert method for Dealer and customer
        public bool Insert(DeaCustBLL dc)
        {

            //create Sql connection first 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create a boolean Value and set it default Value to false
            bool isSuccess = false;

            try
            {
                //Create SQL quary to Insert Detail of Dea_Cust table in database
                string sql = "INSERT INTO tbl_dea_cust (type, name, email, contact, address, added_date, added_by) VALUES (@type, @name, @email, @contact, @address, @added_date, @added_by)";

                //create SQL Command to Execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value using parameter
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);

                //opening datsbase connection
                conn.Open();

                //create Integer variable to check if the quary is executed  or not
                int rows = cmd.ExecuteNonQuery();

                //If the quary executed Successfully the value of rows will be greater 0 else it will less than 0.
                if (rows>0)
                {
                    //Quary is Executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to execute the Quary
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
        #region Method for Update Dealer and Customer
        public bool update(DeaCustBLL dc)
        {
            //create Sql connection for Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create a boolean variable and set it default value to false
            bool isSuccess = false;

            try
            {
                //Create SQL Quary to Update data to database
                string sql = "UPDATE tbl_dea_cust SET type=@type, name=@name, email=@email, contact=@contact, address=@address, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //create Sql Commqnd to execute the quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the Value using parameter
                cmd.Parameters.AddWithValue("@type", dc.type);
                cmd.Parameters.AddWithValue("@name", dc.name);
                cmd.Parameters.AddWithValue("@email", dc.email);
                cmd.Parameters.AddWithValue("@contact", dc.contact);
                cmd.Parameters.AddWithValue("@address", dc.address);
                cmd.Parameters.AddWithValue("@added_date", dc.added_date);
                cmd.Parameters.AddWithValue("@added_by", dc.added_by);
                cmd.Parameters.AddWithValue("@id", dc.id);

                //opening the database connection
                conn.Open();

                //create Integer variable to check if the quary is executed successfully
                int rows = cmd.ExecuteNonQuery();

                //If the Quary is executed Successfully the value of rows will be greater 0 else it will less than 0
                if (rows>0)
                {
                    //Quary executed Successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to execute Quary
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
        #region Delete Method for Dealer and Customer
        public bool delete(DeaCustBLL dc)
        {
            //Sql connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Create a boolean variable and set it default value to false
            bool isSuccess = false;

            try
            {
                //Create Sql Quary to delete form data base
                string sql = "DELETE FROM tbl_dea_cust WHERE id=@id";

                //create Sql command to Execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing the value through parameter
                cmd.Parameters.AddWithValue("@id", dc.id);

                //open database connection
                conn.Open();

                //creating Integer variable to check if the quary is execute successfully
                int rows = cmd.ExecuteNonQuery();

                //If the quary is executed successfully the value of row will greater than 0 else it will be less than 0;
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
        #region Search method for Dealer or customer Modules
        public DataTable Search(string keywords)
        {
            //SQL connection for database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            //Creating Datatable to hold data from database
            DataTable dt = new DataTable();

            try
            {
                //Sql quary to search the dealer or customer from Database
                string sql = "SELECT * FROM tbl_Dea_Cust WHERE id LIKE '%" + keywords + "%' OR type LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%'";
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
        #region Methods to search dealer and customer in the transaction module 
        public DeaCustBLL SearchDealerCustomerForTransaction(string keywords)
        {
            //Creating an object for deacust class
            DeaCustBLL dc = new DeaCustBLL();

            //Sql connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Creating Datatable to hold Data from Database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql Quarey to search from Database 
                string sql = "SELECT name, email, contact, address FROM tbl_dea_cust WHERE id LIKE '%" + keywords + "%' OR name LIKE '%" + keywords + "%'";
                //Creating SqlCommand to Execute the Quary
                //SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data from database
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);

                //opening Database connection
                conn.Open();

                //passing value from adapter to DataTable dt
                adapter.Fill(dt);

                //if we have the value of dt we need to save int dealercustmer BLL
                if (dt.Rows.Count>0)
                {
                    dc.name = dt.Rows[0]["name"].ToString();
                    dc.email = dt.Rows[0]["email"].ToString();
                    dc.contact = dt.Rows[0]["contact"].ToString();
                    dc.address = dt.Rows[0]["address"].ToString();
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
            return dc;
        }
        #endregion
        #region Method To get the ID of dealer or Customer based on Name
        public DeaCustBLL GetDeaCustIDFromName(string Name)
        {
            //first create object of DeaCustBLL and return it.
            DeaCustBLL dc = new DeaCustBLL();

            //SQL connection here
            SqlConnection conn = new SqlConnection(myconnstrng);
            //create data table to hold the data temporarily
            DataTable dt = new DataTable();

            try
            {
                //Sql Quary to get customer or dealer ID based on NAme. 
                string sql = "SELECT id FROM tbl_dea_cust WHERE name='" + Name + "'";
          
                //Create sql Adapter to Execute the Quary
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

                //opening Database connection
                conn.Open();

                //passing value from adapter to DataTable dt
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    //passing the value from dt to DeaCust dc.
                    dc.id = int.Parse(dt.Rows[0]["id"].ToString());
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
            return dc;
            return dc;
        }
        #endregion

    }
}