using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
namespace BOLayer
{
    public class ExpenseBO    {

        string connStr = "";
        public ExpenseBO()
        {
             connStr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        }
        public DataTable getExpenseCategory()
        {
            DataTable dt = new DataTable();
            MySqlConnection conn=new MySqlConnection();
            try
            {
                
                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "select * from expenseCategory;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                conn.Close();

            }
            catch(Exception e)
            {
               
            }
            finally
            {
                conn.Close();
            }
            
            return dt;

        }

        public DataTable getExpense()
        {
            DataTable dt = new DataTable();
            MySqlConnection conn = new MySqlConnection();
            try
            {

                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = @"select expense.expense_id,expense.expense_date,expenseCategory.category_name as expense_category,expense.expense_category_id,
                                expense.expense_description,expense.expense_amount,expense.expense_paidby,expense.expense_attachement
                                   from expense inner join expenseCategory on expense.expense_category_id = expenseCategory.category_id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(dt);
                conn.Close();

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;

        }



        public void insertExpense(string date,string category_id,string description,string amount,string paidby,string attachment_path)
        {
           
            MySqlConnection conn = new MySqlConnection();
            try
            {

                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = @"insert into expense (expense_date,expense_category_id,expense_description,expense_amount,expense_paidby,expense_attachement) 
                               values(@date,@category,@description,@amount,@paidby,@path);";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@category", category_id);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@paidby", paidby);
                cmd.Parameters.AddWithValue("@path", attachment_path);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

           
        }

        public void updateExpense(string id,string date, string category_id, string description, string amount, string paidby, string attachment_path)
        {

            MySqlConnection conn = new MySqlConnection();
            try
            {

                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = @"update expense set expense_date=@date,expense_category_id=@category,expense_description=@description,expense_amount=@amount,expense_paidby=@paidby";
                if (attachment_path != "")
                     sql += ",expense_attachement = @path ";
                sql += "where expense_id=@id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@category", category_id);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@paidby", paidby);
                if (attachment_path != "")
                    cmd.Parameters.AddWithValue("@path", attachment_path);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


        }


        public void deleteExpense(string id)
        {

            MySqlConnection conn = new MySqlConnection();
            try
            {

                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = @"delete from expense where expense_id=@id;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }


        }

    }
}