using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class numismatica : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.DataBind();
            }
        }

        // Função para determinar se aparece LOGIN, REGISTER ou USERNAME no botão do topo direito da masterpage
        protected string DetermineLoginButtonText()
        {
            if (Request.Url.AbsolutePath.ToLower() == "/login.aspx" && Session["logged"] != "yes")
            {
                return "Register";
            }
            else if (Request.Url.AbsolutePath.ToLower() == "/register.aspx" && Session["logged"] != "yes")
            {
                return "Login";
            }
            else if (Session["logged"] != null)
            {
                return $"{Session["username"]}";
            }
            else
            {
                return "Login";
            }
        }

        // Função para determinar se aparece para onde irá ser feito o redirect ao carregar no botão
        protected string DetermineLoginRedirect()
        {
            if (Request.Url.AbsolutePath.ToLower() == "/login.aspx" && Session["logged"] != "yes")
            {
                return "register.aspx";
            }
            else if (Request.Url.AbsolutePath.ToLower() == "/register.aspx" && Session["logged"] != "yes")
            {
                return "login.aspx";
            }
            else if (Session["logged"] != null)
            {
                return "personal_zone.aspx";
            }
            else
            {
                return "login.aspx";
            }
        }

        protected void btn_user_Click(object sender, EventArgs e)
        {
            Response.Redirect(DetermineLoginRedirect());
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            Response.Redirect("logout.aspx");
        }
    }

}