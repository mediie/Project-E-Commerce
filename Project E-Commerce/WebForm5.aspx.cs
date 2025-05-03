using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Project_E_Commerce
{
    public partial class WebForm5 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) ProcessOrder();
        }

        void ProcessOrder()
        {
            var cart = Session["Cart"] as System.Collections.Generic.List<CartItem>;
            var cust = Session["Customer"] as CustomerInfo;
            if (cart == null || cust == null || !cart.Any())
            {
                Response.Redirect("WebForm1.aspx");
                return;
            }

            int orderId = SaveOrder(cust, cart);

            lblMsg.Text = $"Thank you, {cust.Name}!<br/>Your order #{orderId} has been placed.";
            GridViewSummary.DataSource = cart;
            GridViewSummary.DataBind();
            lblTotal.Text = "Total: SAR " + cart.Sum(i => i.Subtotal).ToString("N2");

            Session.Remove("Cart");
            Session.Remove("Customer");
            if (Request.Cookies["CartCount"] != null)
            {
                var expired = new HttpCookie("CartCount") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(expired);
            }

        }


        int SaveOrder(CustomerInfo cust, System.Collections.Generic.List<CartItem> cart)
        {
            string cs = ConfigurationManager.ConnectionStrings["BookBarnConn"].ConnectionString;
            using (var conn = new SqlConnection(cs))
            {
                conn.Open();

                var cmd = new SqlCommand(@"
          INSERT INTO Customers(Name,Email,Phone,Address)
          VALUES(@n,@e,@p,@a);
          SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@n", cust.Name);
                cmd.Parameters.AddWithValue("@e", cust.Email);
                cmd.Parameters.AddWithValue("@p", cust.Phone);
                cmd.Parameters.AddWithValue("@a", cust.Address);
                int custId = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = new SqlCommand(@"
          INSERT INTO Orders(CustomerID,OrderDate,TotalAmount)
          VALUES(@cid,GETDATE(),@tot);
          SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@cid", custId);
                cmd.Parameters.AddWithValue("@tot", cart.Sum(i => i.Subtotal));
                int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in cart)
                {
                    cmd = new SqlCommand(@"
            INSERT INTO OrderItems(OrderID,BookID,Quantity,Subtotal)
            VALUES(@oid,@bid,@qty,@sub);", conn);
                    cmd.Parameters.AddWithValue("@oid", orderId);
                    cmd.Parameters.AddWithValue("@bid", item.BookID);
                    cmd.Parameters.AddWithValue("@qty", item.Quantity);
                    cmd.Parameters.AddWithValue("@sub", item.Subtotal);
                    cmd.ExecuteNonQuery();
                }

                return orderId;
            }
        }

        protected void btnContinueShopping_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm1.aspx");
        }

    }
}
