using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_E_Commerce
{
    public partial class WebForm3 : Page
    {
        System.Collections.Generic.List<CartItem> Cart =>
          Session["Cart"] as System.Collections.Generic.List<CartItem>
          ?? new System.Collections.Generic.List<CartItem>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindCart();
        }

        void BindCart()
        {
            var cart = Session["Cart"] as List<CartItem>
               ?? new List<CartItem>();
            GridViewCart.DataSource = cart;
            GridViewCart.DataBind();
        }

        protected void GridViewCart_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewCart.EditIndex = e.NewEditIndex;
            BindCart();
        }
        protected void GridViewCart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewCart.EditIndex = -1;
            BindCart();
        }
        protected void GridViewCart_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int idx = e.RowIndex;
            var txt = GridViewCart.Rows[idx].FindControl("txtQty") as TextBox;
            if (int.TryParse(txt.Text, out int qty) && qty > 0)
            {
                Cart[idx].Quantity = qty;
            }
            GridViewCart.EditIndex = -1;
            BindCart();
        }
        protected void GridViewCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Cart.RemoveAt(e.RowIndex);
            BindCart();
        }

        protected void cvCartNotEmpty_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Cart.Any();
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;
            Response.Redirect("WebForm4.aspx");
        }
    }
}
