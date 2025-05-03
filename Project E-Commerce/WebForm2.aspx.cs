using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.UI;
using System.Linq;
using System.Web;

namespace Project_E_Commerce
{
    public partial class WebForm2 : Page
    {
        private string ConnStr => ConfigurationManager
            .ConnectionStrings["BookBarnConn"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindDetails();
        }

        private void BindDetails()
        {
            if (!int.TryParse(Request.QueryString["BookID"], out int bookId))
            {
                Response.Redirect("WebForm1.aspx");
                return;
            }

            const string sql = @"
                SELECT
                  b.BookID,
                  b.Title,
                  b.AuthorName,
                  c.Name     AS CategoryName,
                  b.Price
                FROM dbo.Books b
                JOIN dbo.Categories c
                  ON b.CategoryID = c.CategoryID
                WHERE b.BookID = @id;";

            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", bookId);
                conn.Open();
                DetailsView1.DataSource = cmd.ExecuteReader();
                DetailsView1.DataBind();
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (DetailsView1.DataKey == null) return;
            int bookId = (int)DetailsView1.DataKey.Value;

            string title;
            decimal price;

            const string lookupSql = @"
                SELECT Title, Price
                FROM dbo.Books
                WHERE BookID = @id;";

            using (var conn = new SqlConnection(ConnStr))
            using (var cmd = new SqlCommand(lookupSql, conn))
            {
                cmd.Parameters.AddWithValue("@id", bookId);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read())
                    {
                        Response.Redirect("WebForm1.aspx");
                        return;
                    }
                    title = rdr.GetString(0);
                    price = rdr.GetDecimal(1);
                }
            }

            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var existing = cart.Find(x => x.BookID == bookId);
            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                cart.Add(new CartItem
                {
                    BookID = bookId,
                    Title = title,
                    Price = price,
                    Quantity = 1
                });
            }

            Session["Cart"] = cart;
            int count = cart.Sum(x => x.Quantity);
            var cookie = new HttpCookie("CartCount", count.ToString())
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };
            Response.Cookies.Add(cookie);
            Response.Redirect("WebForm3.aspx");
        }
    }
}
