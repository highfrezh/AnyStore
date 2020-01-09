using AnyStore.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.DAL
{
    class transactionDetailDAL
    {
        //static string method to connect to database
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        #region Insert transaction Method
        public bool InsertTransactionDetail(transactionDetailBLL td)
        {

            //create Sql connection first 
            SqlConnection conn = new SqlConnection(myconnstrng);

            //create a boolean Value and set it default Value to false
            bool isSuccess = false;

            try
            {
                //Create SQL quary to Insert Detail of Dea_Cust table in database
                string sql = "INSERT INTO tbl_transaction_detail (product_id, rate, qty, total, dea_cust_id, added_date, added_by) VALUES (@product_id, @rate, @qty, @total, @dea_cust_id, @added_date, @added_by)";

                //create SQL Command to Execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Passing the value using parameter
                cmd.Parameters.AddWithValue("@product_id", td.product_id);
                cmd.Parameters.AddWithValue("@rate", td.rate);
                cmd.Parameters.AddWithValue("@qty", td.qty);
                cmd.Parameters.AddWithValue("@total", td.total);
                cmd.Parameters.AddWithValue("@dea_cust_id", td.dea_cust_id);
                cmd.Parameters.AddWithValue("@added_date", td.added_date);
                cmd.Parameters.AddWithValue("@added_by", td.added_by);

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
    }
}
