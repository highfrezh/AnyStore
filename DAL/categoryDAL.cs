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
    class categoryDAL
    {
        //static string method  for database connection string.
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        #region Select Method
        public DataTable select ()
        {
            //Creating Database connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            DataTable dt = new DataTable();

            try
            {
                //writing sql quary to get all othe data from database
                string sql = "SELECT * FROM tbl_category";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //open datbase connection
                conn.Open();

                //Adding the value from adapter to Data table dt
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
        #region Insert New Category
        public bool Insert(categoryBLL c)
        {
            //Creating A boolean variable and set it default value to false
            bool isSuccess = false;

            //connecting to Database
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //sql Quary to add new category
                string sql = "INSERT INTO tbl_category (title, description, added_date, added_by) VALUES (@title, @description, @added_date, @added_by)";

                //Creating SQL commond to pass our Quary
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Passing Value through Parameter
                cmd.Parameters.AddWithValue("@title",c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.adde_by);

                //Opening Database connection
                conn.Open();

                //Creating the int Variable to excute the Quary
                int rows = cmd.ExecuteNonQuery();

                //If the quary is excuted succesfully then it value will be greater than 0 else it less than 0.

                if (rows > 0)
                {
                    //Quary excuted Successfully
                    isSuccess = true;
                }
                else
                {
                    //Failed to excute the Quary
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                //closing database
                conn.Close(); 
            }
            return isSuccess;
        }
        #endregion
        #region update Method
        public bool update(categoryBLL c)
        {
            //create a boolean  variable and set it default value to false;
            bool isSuccess = false;

            //creating SQL connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //Quary to update Category
                string sql = "UPDATE tbl_category SET title=@title, description=@description, added_date=@added_date, added_by=@added_by WHERE id=@id";

                //Sql command to pass value to Sql Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //passing value using cmd  
                cmd.Parameters.AddWithValue("@title", c.title);
                cmd.Parameters.AddWithValue("@description", c.description);
                cmd.Parameters.AddWithValue("@added_date", c.added_date);
                cmd.Parameters.AddWithValue("@added_by", c.adde_by);
                cmd.Parameters.AddWithValue("@id", c.id);

                //Open Database connection
                conn.Open();

                //Create int variable to Execute the Quary
                int rows = cmd.ExecuteNonQuery();

                //if the quary is successfully executed the value will be greater than 0
                if(rows>0)
                {
                    //Quary Executed succefully
                    isSuccess = true;

                }
                else
                {
                    //Failed to Execute Quary
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
        #region Delete Category Method
        public bool Delete(categoryBLL c)
        {
            //create a boolean variable and set it value to false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                //SQL quary to delete form database
                string sql = "DELETE FROM tbl_category WHERE id=@id ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                //passing the value using cmd
                cmd.Parameters.AddWithValue("@id", c.id);

                //open SQL connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();

                //if the quary executed successfully then the value of rows is greater than 0 else it will less than 0;
                if (rows>0)
                {
                    //quary Executed successfully
                    isSuccess = true;
                }
                else
                {
                    //failed to execute the quary
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
        #region Search Method
        public DataTable search(string keywords)
        {
            //Sql connection for Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);

            //Creating Datatable to hold Data from Database temporarily
            DataTable dt = new DataTable();
            try
            {
                //Sql Quarey to search from Database 
                string sql = "SELECT * FROM tbl_category WHERE id LIKE '%"+keywords+ "%' OR title LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%'";
                //Creating SqlCommand to Execute the Quary
                SqlCommand cmd = new SqlCommand(sql, conn);

                //Getting Data from database
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                //opening Database connection
                conn.Open();

                //passing value from adapter to DataTable dt
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
