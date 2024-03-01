using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class manage_users : System.Web.UI.Page
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
            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Users.Read_User_All(tb_search.Text, tb_email.Text, Convert.ToInt32(ddl_ativo.SelectedValue), ddl_username.SelectedValue);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 18;
            pagedData.CurrentPageIndex = PageNumber;

            rptUsers.DataSource = pagedData;
            rptUsers.DataBind();

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