using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from tblStudent";
                cmd.Connection = con;
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
                con.Close();

                cmd.CommandText = "Select AVG(Marks) from tblStudent";
                con.Open();
                Label8.Text = cmd.ExecuteScalar().ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("tblStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StudentName", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Gender", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@Marks", TextBox2.Text);

                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@StudentId";
                outputParameter.SqlDbType = System.Data.SqlDbType.Int;
                outputParameter.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outputParameter);

                con.Open();
                cmd.ExecuteNonQuery();

                string StuId = outputParameter.Value.ToString();

                Label6.Text = "Student Id : " + StuId + " has been inserted";
            }
        }
    }
}