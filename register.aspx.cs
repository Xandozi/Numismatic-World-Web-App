using ASPSnippets.GoogleAPI;
using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Numismatica.login;

namespace Numismatica
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Procedimento de sign in com a conta Google
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GoogleClientID"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["RedirectURI_Register"];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);

                    int valido = Insercao.Inserir_User_Google(profile.Name, profile.Email);
                    int cod_user = Extract.Code_Via_Email(profile.Email);

                    if (profile.Verified_Email == "True" && valido == 1)
                    {
                        Session["username"] = Extract.Username(cod_user);
                        Session["user_code"] = cod_user;
                        Session["perfil"] = Validacao.Check_Perfil(Session["username"].ToString());
                        Session["email"] = profile.Email;
                        Session["logged"] = "yes";
                        Response.Redirect("home.aspx", false);
                    }
                    else if (profile.Verified_Email == "True" && valido != 1)
                    {
                        Email.Send(profile.Email, Extract.Username(cod_user));
                        Response.Redirect("login.aspx?message=Activate%20your%20account%20via%20email", false);
                    }
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["message"]))
            {
                string message = Request.QueryString["message"];

                lbl_mensagem.Text = message;
                lbl_mensagem.CssClass = "alert alert-danger";
            }
        }

        // Registar o utilizador na base de dados de acordo com as validações necessárias
        protected void btn_register_Click(object sender, EventArgs e)
        {
            if (tb_pw.Text != tb_repeat_pw.Text)
            {
                lbl_mensagem.Text = "Passwords do not match!";
            }
            else
            {
                if (Validacao.Check_Username(tb_username.Text) && Validacao.Check_Email(tb_email.Text))
                {
                    Insercao.Inserir_User(tb_username.Text, tb_pw.Text, tb_email.Text);
                    Email.Send(tb_email.Text, tb_username.Text);
                    lbl_mensagem.Text = "User registered with success! Check your inbox for your account activation link.";
                }
                else if (!Validacao.Check_Username(tb_username.Text))
                    lbl_mensagem.Text = "Username already exists!";
                else if (!Validacao.Check_Email(tb_email.Text))
                    lbl_mensagem.Text = "Email already exists!";
                else
                    lbl_mensagem.Text = "Username and email already exist!";
            }
        }
        protected void Login(object sender, EventArgs e)
        {
            GoogleConnect.Authorize("profile", "email");
        }
        protected void Clear(object sender, EventArgs e)
        {
            GoogleConnect.Clear(Request.QueryString["code"]);
        }
    }
}