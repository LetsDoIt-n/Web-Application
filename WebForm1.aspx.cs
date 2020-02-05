using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BOLayer;
using System.IO;
using System.Reflection;
using ClosedXML.Excel;
namespace WebApplication2
{

    public partial class WebForm1 : System.Web.UI.Page
    {

        ExpenseBO eb = new ExpenseBO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDropDownList();
            }
            insert_button.Visible = true;
        }

        public void bindDropDownList()
        {
            DataTable dt = eb.getExpenseCategory();
            dropdown_category.DataValueField = "category_id";
            dropdown_category.DataTextField = "category_name";
            dropdown_category.DataSource = dt;
            dropdown_category.DataBind();
        }

        protected void insert_button_Click(object sender, EventArgs e)
        {

            string date = Convert.ToDateTime(textBox_calendar.Text).ToString("yyyy-MM-dd").ToString().Trim();
            string category_id = dropdown_category.SelectedValue.ToString().Trim();
            string description = textArea_description.Text.ToString().Trim();
            string amount = textBox_amount.Text.ToString().Trim();
            string paid_by = textBox_paidBy.Text.ToString().Trim();

            string path = "";
            
            if (fileupload_attachment.HasFile)
            {
                path = Server.MapPath("attachments");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file_name = fileupload_attachment.FileName.ToString();
                path+="\\" + file_name;
                fileupload_attachment.SaveAs(path);
                path = "\\attachments\\" + file_name;
            }
            
            eb.insertExpense(date, category_id, description, amount, paid_by, path);
            clearAll();
            bindGrid();




        }

        
       

        protected void excel_button_Click(object sender, EventArgs e)
        {

            

            DataTable dt = eb.getExpense();

            dt.Columns.Remove("expense_id");
            dt.Columns.Remove("expense_category_id");
            dt.Columns.Remove("expense_attachement");

            dt.Columns["expense_date"].ColumnName = "Date";
            dt.Columns["expense_category"].ColumnName = "Category";
            dt.Columns["expense_description"].ColumnName = "Description";
            dt.Columns["expense_amount"].ColumnName = "Amount";
            dt.Columns["expense_paidby"].ColumnName = "Paid By";

            string folderpath = "c:\\Expense Excel\\";
            if(!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            using(XLWorkbook wb=new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Expense Report");
                wb.SaveAs(folderpath + "Excel.xlsx");
            }


        }

        protected void generatelist_button_Click(object sender, EventArgs e)
        {
            bindGrid();

        }

        public void clearAll()
        {
            textBox_calendar.Text = "";
            dropdown_category.SelectedIndex = 0;
            textArea_description.Text = "";
            textBox_amount.Text = "";
            textBox_paidBy.Text = "";

        }

        public void bindGrid()
        {
            DataTable dt = eb.getExpense();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            string id = GridView1.DataKeys[e.RowIndex].Values["expense_id"].ToString();
            eb.deleteExpense(id);
            bindGrid();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            insert_button.Visible = false;
            excel_button.Visible = false;
            generatelist_button.Visible = false;
            GridView1.EditIndex = e.NewEditIndex;
            bindGrid();
            Label id = GridView1.Rows[e.NewEditIndex].FindControl("label1") as Label;
            Label date = GridView1.Rows[e.NewEditIndex].FindControl("label2") as Label;
            Label cat_id = GridView1.Rows[e.NewEditIndex].FindControl("label3") as Label;
            Label desc = GridView1.Rows[e.NewEditIndex].FindControl("label4") as Label;
            Label amount = GridView1.Rows[e.NewEditIndex].FindControl("label5") as Label;
            Label paidby = GridView1.Rows[e.NewEditIndex].FindControl("label6") as Label;

            textBox_calendar.Text = Convert.ToDateTime(date.Text.ToString().Trim()).ToString("dd/MM/yyyy");
            dropdown_category.SelectedValue = cat_id.Text.ToString().Trim();
            textArea_description.Text = desc.Text.ToString().Trim();
            textBox_amount.Text = amount.Text.ToString().Trim();
            textBox_paidBy.Text = paidby.Text.ToString().Trim();
            hf1.Value = id.Text.ToString().Trim();



        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string path = "";

            if (fileupload_attachment.HasFile)
            {
                path = Server.MapPath("attachments");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string file_name = fileupload_attachment.FileName.ToString();
                path += "\\" + file_name;
                fileupload_attachment.SaveAs(path);
                path = "\\attachments\\" + file_name;
            }
            string date= Convert.ToDateTime(textBox_calendar.Text).ToString("yyyy-MM-dd").ToString().Trim();
            eb.updateExpense(hf1.Value, date, dropdown_category.SelectedValue, textArea_description.Text, textBox_amount.Text, textBox_paidBy.Text, path);
            clearAll();
            GridView1.EditIndex = -1;
            bindGrid();
            insert_button.Visible = true;
            excel_button.Visible = true;
            generatelist_button.Visible = true;


        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            clearAll();
            GridView1.EditIndex = -1;
            bindGrid();
            insert_button.Visible = true;
            excel_button.Visible = true;
            generatelist_button.Visible = true;

        }
    }

}