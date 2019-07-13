using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspWeb.pageResponse
{
    public partial class D : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["str"] != null)
            {
                Label1.Text = Session["str"].ToString();
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //手动过期
            //Session.Remove("str");
            //立即过期
            Session.Abandon();
            Response.Redirect("D.aspx");
        }
    }
}