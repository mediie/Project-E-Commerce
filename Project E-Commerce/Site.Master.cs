using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_E_Commerce
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadCartCount();
        }

        private void LoadCartCount()
        {
            var cookie = Request.Cookies["CartCount"];
            if (cookie != null && int.TryParse(cookie.Value, out int count))
                lblCartCount.Text = $"Cart: {count}";
            else
                lblCartCount.Text = "Cart: 0";
        }
    }
}
