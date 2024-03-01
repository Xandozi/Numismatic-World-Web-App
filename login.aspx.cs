using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using System.IO;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using System.Configuration;

namespace Numismatica
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Procedimento de sign in com a conta Google
            GoogleConnect.ClientId = ConfigurationManager.AppSettings["GoogleClientID"];
            GoogleConnect.ClientSecret = ConfigurationManager.AppSettings["GoogleSecret"];
            GoogleConnect.RedirectUri = ConfigurationManager.AppSettings["RedirectURI_Login"];

            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["code"]))
                {
                    string code = Request.QueryString["code"];
                    string json = GoogleConnect.Fetch("me", code);
                    GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json); // Obtenção do json com a informação do utilizador

                    int valido = Insercao.Inserir_User_Google(profile.Name, profile.Email); // Retorno da função. 1 para válido e ativo ou seja pode fazer login
                    int cod_user = Extract.Code_Via_Email(profile.Email); // Extrair o código de utilizador através do email do utilizador fornecido pelo google

                    if (profile.Verified_Email == "True" && valido == 1)        // Caso autenticação dê certo e o utilizador esteja ativo
                    {
                        Session["username"] = Extract.Username(cod_user);
                        Session["user_code"] = cod_user;
                        Session["perfil"] = Validacao.Check_Perfil(Session["username"].ToString());
                        Session["email"] = profile.Email;
                        Session["logged"] = "yes";
                        Session["googlefb_log"] = "yes";
                        Response.Redirect("home.aspx", false);
                    }
                    else if (profile.Verified_Email == "True" && valido != 1)   // Caso autenticação seja correta mas o utilizador não esteja ativo
                    {
                        Email.Send(profile.Email, Extract.Username(cod_user));  // Enviar email de ativação novamente
                        Response.Redirect("login.aspx?message=Activate%20your%20account%20via%20email", false);     // Redirecionamento para a página com uma mensagem no url
                    }
                    // Else será executado pela autenticação errada do google que virá abaixo
                }
                if (Request.QueryString["error"] == "access_denied")
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Access denied.')", true);
                }

                // Caso haja uma mensagem passada pelo url, a lbl mensagem deverá mostrar isto
                if (Request.QueryString["msg"] == "yesemail")
                {
                    lbl_mensagem.Text = "Email changed successfully! Please activate your account via email before logging in again";
                    lbl_mensagem.CssClass = "alert alert-success";
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["message"]))
            {
                string message = Request.QueryString["message"];

                lbl_mensagem.Text = message;
                lbl_mensagem.CssClass = "alert alert-danger";
            }
        }

        // Procedimento ao clicar no botão login
        protected void btn_login_Click(object sender, EventArgs e)
        {
            // Validação do login bem sucedida
            if (Validacao.Check_Login(tb_username.Text, tb_pw.Text) == 1)
            {
                Session["username"] = tb_username.Text;
                Session["user_code"] = Extract.Code(tb_username.Text);
                Session["perfil"] = Validacao.Check_Perfil(tb_username.Text);
                Session["email"] = Extract.Email(tb_username.Text);
                Session["logged"] = "yes";
                Response.Redirect("home.aspx", false);
            }
            else if(Validacao.Check_Login(tb_username.Text, tb_pw.Text) == 2)   // Utilizador não ativo
            {
                lbl_mensagem.Text = "Your account is not activated. Please check your inbox for a link or just reset your password.";
            }
            else // Credenciais erradas
            {
                lbl_mensagem.Text = "Inserted credentials are wrong.";
            }
        }

        // Envio do email de pw esquecida
        protected void btn_forgot_pw_Click(object sender, EventArgs e)
        {
            if (!Validacao.Check_Email(tb_email.Text))
            {
                Email.Send_Forgot_PW(tb_email.Text);
            }
            else
            {
                lbl_mensagem.Text = "Email was not found on our database!";
            }
        }

        // Função para o procedimento Google
        protected void Login(object sender, EventArgs e)
        {
            GoogleConnect.Authorize("profile", "email");
        }
        protected void Clear(object sender, EventArgs e)
        {
            GoogleConnect.Clear(Request.QueryString["code"]);
        }

        public class GoogleProfile
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Picture { get; set; }
            public string Email { get; set; }
            public string Verified_Email { get; set; }
        }
    }
}