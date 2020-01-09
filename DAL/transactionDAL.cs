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
    class transactionDAL
    {
        //static string method to connect to database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Insert transaction Method
        public bool InsertTransaction(transactionBLL t, out int transactionID)
        {

            //create Sql connection first 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create a boolean Value and set it default Value to false
            bool isSuccess = false;
            //set the transactionID to negative 1.
            transactionID = -1;

            try
            {
                //Create SQL quary to Insert Detail of Dea_Cust table in database
                string sql = "INSERT INTO tbl_transaction (type, dea_cust_id, grandTotal, transaction_date, tax, discount, added_by) VALUES (@type, @dea_cust_id, @grandTotal, @transaction_date, @tax, @discount, @added_by); SELECT @@IDENTITY;";

                //create SQL Command to Execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value using parameter
                cmd.Parameters.AddWithValue("@type", t.type);
                cmd.Parameters.AddWithValue("@dea_cust_id", t.dea_cus_id);
                cmd.Parameters.AddWithValue("@grandTotal", t.grandTotal);
                cmd.Parameters.AddWithValue("@transaction_date", t.transaction_date);
                cmd.Parameters.AddWithValue("@tax", t.tax);
                cmd.Parameters.AddWithValue("@discount", t.discount);
                cmd.Parameters.AddWithValue("@added_by", t.added_by);

                //opening datsbase connection
                conn.Open();

                //create Integer variable to check if the quary is executed  or not
                object o = cmd.ExecuteScalar();

                //If the quary executed Successfully the value of rows will be greater 0 else it will less than 0.
                if (o !=null )
                {
                    //Quary is Executed Successfully
                    isSuccess = true;
                    transactionID = int.Parse(o.ToString());
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
        #region Method to Display All Transactions
        public DataTable DisplayAllTransactions()
        {
            //SQL connection first
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create DataTable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Quary to displat all Transaction
                string sql = "SELECT * FROM tbl_transaction";
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
        #region Method to display transaction based on transaction 
        public DataTable DisplayTransactionByType(string type)
        {
            //SQL connection first
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create DataTable to hold the data from database temporarily
            DataTable dt = new DataTable();

            try
            {
                //Write SQL Quary to displat all Transaction
                string sql = "SELECT * FROM tbl_transaction WHERE type='"+type+"'";
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
