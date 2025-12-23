using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookManagement.Views
{
    public partial class Books : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBSC"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getBooks();
            }               
        }

        void getBooks()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetBooks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvBooks.DataSource = dt;
                        gvBooks.DataBind();
                    }
                }
            }
        }

        protected void gvBooks_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int bookId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditBook")
            {
                LoadBookById(bookId);
            }
            else if (e.CommandName == "DeleteBook")
            {
                deleteBook(bookId);
                getBooks();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            clearForm(); 
            lblModalTitle.Text = "Add Book";
            ScriptManager.RegisterStartupScript(
    this, GetType(), "show", "showModal();", true);
        }

        void clearForm()
        {
            hfBookId.Value = "";
            txtTitle.Text = txtAuthor.Text = txtPrice.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           

            using (SqlConnection con = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    if (string.IsNullOrEmpty(hfBookId.Value))
                    {
                        cmd.CommandText = "sp_AddBook";
                        cmd.Parameters.AddWithValue("@ISBN", TxtBookName.Text.Trim());
                    }
                    else
                    {
                        cmd.CommandText = "sp_UpdateBook"; 
                        cmd.Parameters.AddWithValue("@BookId", Convert.ToInt32(hfBookId.Value));
                    }

                  
                    
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@Author", txtAuthor.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtPrice.Text.Trim()));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblMessage.Text = "Book saved successfully!";
                    hfBookId.Value = "";
                    ClearForm();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        lblMessage.Text = "Book with this Title or ISBN already exists!";
                        return;
                    }
                    else
                    {
                        lblMessage.Text = "Database error: " + ex.Message;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    return;
                }
            }
            getBooks();

            ScriptManager.RegisterStartupScript(this, GetType(), "hide", "$('#bookModal').modal('hide');", true);
        }

        private void ClearForm()
        {
            TxtBookName.Text = "";
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtPrice.Text = "";
        }



        void LoadBookById(int bookId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("sp_GetBookById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", bookId);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        hfBookId.Value = dt.Rows[0]["BookId"].ToString();
                        TxtBookName.Text = dt.Rows[0]["ISBN"].ToString();
                        txtTitle.Text = dt.Rows[0]["Title"].ToString();
                        txtAuthor.Text = dt.Rows[0]["Author"].ToString();
                        txtPrice.Text = dt.Rows[0]["Price"].ToString();
                    }
                }
            }

            lblModalTitle.Text = "Edit Book";
            ScriptManager.RegisterStartupScript(this, GetType(), "show", "showModal();", true);
        }
        void deleteBook(int bookId) {
            using(SqlConnection conn = new SqlConnection(cs))
            {
                using(SqlCommand cmd = new SqlCommand("sp_DeleteBook", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", bookId);                    

                    try
                    {
                        conn.Open();
                        int row = cmd.ExecuteNonQuery();
                        if (row > 0)
                        {
                            lblMessage.Text = "book deleted sucessfully";
                        }
                        else
                        {
                            lblMessage.Text = "record not found";
                        }
                        

                    }
                    catch(Exception ex)
                    {
                        lblMessage.Text = "An error occurred: " + ex.Message;
                    }
                   
                }
            }
        }
    }
}