using DataContext;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspWeb.pageResponse
{
    public partial class ProductdeDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                var id = Request.QueryString["id"];
                var product = new StuDBContext().DepartMents.SingleOrDefault(x => x.SortCode == id);
                lbSn.Text = product.SortCode;
                lbName.Text = product.Name;
                lb3.Text = "挺好的!";
            }else
            {
                Response.Redirect("~/productlist.aspx");
            }
        }
    }
}