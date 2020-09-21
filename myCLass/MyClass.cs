using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace EK_Home_POS.myCLass
{
    public class MyClass
    {
        public string username;
        public SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-PND5I06;Initial Catalog=EK;Integrated Security=True");

        //used to check if the username and password is correct, if true, user can login
        public bool CheckUser(string uname, string pw)
        {
            username = uname;

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Count(*) FROM users WHERE username=@uname AND password=@pw", con);
                cmd.Parameters.AddWithValue("@uname", uname);
                cmd.Parameters.AddWithValue("@pw", pw);
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        //sends a datatable back to the main program
        public DataTable ShowUsers()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM users", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        //Datatable didn't work well therefor the BindingSource
        public BindingSource ShowStock()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM stock", con);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //SqlCommand sql = new SqlCommand()

            sda.Fill(dt);
            //sda.Fill(ds, "All");
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            return bSource;
        }

        //Datatable didn't work well therefor the BindingSource
        public BindingSource ShowPackaging()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM packaging", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BindingSource bSource = new BindingSource();
            bSource.DataSource = dt;
            return bSource;

        }

        //Method to update the stock quantity
        public bool UpdateStock(int ID, int quantity)
        {
            SqlCommand cmd = new SqlCommand("UPDATE [stock] SET quantity = @quantity WHERE SID=@ID ", con);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@quantity", quantity);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
            
        }

        //Method to update the packaging quantity
        public bool UpdatePackaging(int ID, int quantity)
        {
            //Sal later klaar maak
            SqlCommand cmd = new SqlCommand("UPDATE [packaging] SET quantity = @quantity WHERE PID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
                
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }

        }

        //Method to add a user and password to the database
        public bool AddUser(string uname, string pw)
        {
            string sql = "INSERT INTO users(username, password) VALUES(@uname, @pw)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@uname", uname);
            cmd.Parameters.AddWithValue("@pw", pw);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        //method to add new stock to the database
        public bool AddStock(string product, string description, string kode, int quantity)
        {
            string sql = "INSERT INTO stock(product, description, kode, quantity) VALUES(@product, @description, @kode, @quantity)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@product", product);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@kode", kode);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        //Method to add packaging to the database
        public bool AddPackaging(string product, string description, int quantity)
        {
            string sql = "INSERT INTO packaging(product, description, quantity) VALUES(@product, @description, @quantity)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@product", product);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }

        //Method to delete a user from the database
        public bool DeleteUser(string uname)
        {
            string sql = "DELETE FROM users WHERE username=@uname";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@uname", uname);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                con.Close();
                return false;
            }
        }
    }
}
