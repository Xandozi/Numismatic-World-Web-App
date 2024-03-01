using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class user_details : System.Web.UI.Page
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
            else
            {
                // Carregamento da informação do user para dentro da lista de objetos USER
                List<Users> user = Users.Read_User(Convert.ToInt32(Request.QueryString["id"]));

                if (user.Count > 0)
                {
                    lbl_nome.Text = user[0].username;
                    lbl_email.Text = user[0].email;
                    lbl_perfil.Text = user[0].perfil;
                    if (user[0].ativo)
                    {
                        lbl_ativo.Text = "Active";
                        lbl_ativo.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lbl_ativo.Text = "Inactive";
                        lbl_ativo.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void btn_modal_delete_Click(object sender, EventArgs e)
        {
            int cod_user = Convert.ToInt32(Request.QueryString["id"]);
            Insercao.Update_User(cod_user, 1, 1);

            Response.Redirect("manage_users.aspx", false);
        }

        protected void btn_modal_activate_Click(object sender, EventArgs e)
        {
            int cod_user = Convert.ToInt32(Request.QueryString["id"]);
            Insercao.Update_User(cod_user, 1, 0);

            string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect(currentPageUrl);
        }

        protected void btn_deactivation_Click(object sender, EventArgs e)
        {
            int cod_user = Convert.ToInt32(Request.QueryString["id"]);
            Insercao.Update_User(cod_user, 0, 0);

            string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect(currentPageUrl);
        }
    }
}