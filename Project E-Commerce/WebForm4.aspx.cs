using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Project_E_Commerce
{
    public partial class WebForm4 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindCartPreview();
        }

        private void BindCartPreview()
        {
            var cart = Session["Cart"] as List<CartItem>
                       ?? new List<CartItem>();

            GridViewCartPreview.DataSource = cart;
            GridViewCartPreview.DataBind();

            decimal total = 0;
            foreach (var item in cart)
                total += item.Subtotal;

            lblCartTotal.Text = $"Total: SAR {total:N2}";
            btnPlaceOrder.Enabled = cart.Count > 0;
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            var cust = new CustomerInfo
            {
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
            Session["Customer"] = cust;
            Response.Redirect("WebForm5.aspx");
        }
    }
}
