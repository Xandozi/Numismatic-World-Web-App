using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class manage_catalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else if (Session["perfil"].ToString() != "Administrator")
            {
                Response.Redirect("personal_zone.aspx");
            }

            if (!Page.IsPostBack)
            {
                ddl_type.Items.Insert(0, "All");
                ddl_grade.Items.Insert(0, "All");
            }

            BindData();
        }

        protected void btn_previous_Click(object sender, EventArgs e)
        {
            PageNumber -= 1;
            BindData();
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            PageNumber += 1;
            BindData();
        }

        private void BindData()
        {
            string price = "";

            if (ddl_price.SelectedIndex == 0)
                price = "";
            else if (ddl_price.SelectedIndex != 0)
                price = ddl_price.SelectedValue;

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Moeda.Read_Coin_All(tb_search.Text, ddl_grade.SelectedIndex, ddl_type.SelectedIndex, price, Convert.ToInt32(ddl_deleted.SelectedValue));
            pagedData.AllowPaging = true;
            pagedData.PageSize = 18;
            pagedData.CurrentPageIndex = PageNumber;

            rptCoins.DataSource = pagedData;
            rptCoins.DataBind();

            btn_previous.Enabled = !pagedData.IsFirstPage;
            btn_next.Enabled = !pagedData.IsLastPage;
        }

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                    return Convert.ToInt32(ViewState["PageNumber"]);
                else
                    return 0;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }
    }
}