using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;

namespace Project_E_Commerce
{
    public partial class WebForm1 : Page
    {
        private string ConnStr => ConfigurationManager
            .ConnectionStrings["BookBarnConn"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                lblMessage.Text = "";
            }
        }

        private void BindGrid(string searchTerm = "")
        {
            string sql = @"
                SELECT 
                    b.BookID,
                    b.Title,
                    b.AuthorName,
                    c.Name AS CategoryName,
                    b.Price
                FROM dbo.Books b
                JOIN dbo.Categories c 
                  ON b.CategoryID = c.CategoryID";

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                sql += @"
                WHERE b.Title      LIKE @search
                   OR b.AuthorName LIKE @search";
            }

            sql += " ORDER BY b.Title";

            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (!string.IsNullOrWhiteSpace(searchTerm))
                    cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    GridViewBooks.DataSource = dt;
                    GridViewBooks.DataBind();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid(txtSearch.Text.Trim());
            lblMessage.Text = "";
        }

        protected void GridViewBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewBooks.PageIndex = e.NewPageIndex;
            BindGrid(txtSearch.Text.Trim());
        }

        protected void GridViewBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "AddToCart") return;

            if (!int.TryParse(e.CommandArgument.ToString(), out int bookId))
                return;

            string title;
            decimal price;
            const string lookupSql = @"
        SELECT Title, Price
        FROM dbo.Books
        WHERE BookID = @id";
            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(lookupSql, conn))
            {
                cmd.Parameters.AddWithValue("@id", bookId);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return;
                    title = rdr.GetString(0);
                    price = rdr.GetDecimal(1);
                }
            }

            var cart = Session["Cart"] as List<CartItem>
                       ?? new List<CartItem>();

            var existing = cart.Find(x => x.BookID == bookId);
            if (existing != null)
                existing.Quantity++;
            else
                cart.Add(new CartItem
                {
                    BookID = bookId,
                    Title = title,
                    Price = price,
                    Quantity = 1
                });

            Session["Cart"] = cart;

            int count = cart.Sum(x => x.Quantity);
            var cookie = new HttpCookie("CartCount", count.ToString())
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
            Response.Cookies.Add(cookie);

            lblMessage.Text = $"Added \"{title}\" to cart.";
        }

    }
}
