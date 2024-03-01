using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class coin_details_client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["message"]))
            {
                lbl_mensagem.Text = Server.UrlDecode(Request.QueryString["message"]);
                lbl_mensagem.CssClass = "alert alert-success";
            }

            if (Session["logged"] == "yes")
            {
                if (Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(Request.QueryString["id"])))
                {
                    lkb_add_favorites.CssClass = "fa fa-heart";
                }
                else
                {
                    lkb_add_favorites.CssClass = "fa fa-times";
                }
            }

            // Leitura da informação da moeda para dentro de uma lista de objetos MOEDA dea cordo com o id proveniente do coins.aspx
            List<Moeda> coin = Moeda.Read_Coin(Convert.ToInt32(Request.QueryString["id"]));

            if (coin.Count > 0)
            {
                lblNome.Text = coin[0].nome;
                lblDescricao.Text = coin[0].descricao;
                lblCunho.Text = coin[0].cunho;
                lblType.Text = coin[0].tipo;
                lblEstado.Text = coin[0].estado;
                lblValorAtual.Text = coin[0].valor_atual.ToString();

                rptFotos.DataSource = coin[0].lst_fotos;
                rptFotos.DataBind();
            }
        }

        // Função para adicionar ou remover dos favoritos, de acordo com o que retornar da função Check_Favorites
        protected void lkb_add_favorites_Click(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                int cod_moeda_estado = Convert.ToInt32(Request.QueryString["id"]);

                if (Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(cod_moeda_estado)))
                {
                    if (Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(cod_moeda_estado)))
                    {
                        Insercao.Add_Favorites(Convert.ToInt32(cod_moeda_estado), Convert.ToInt32(Session["user_code"]));
                        lbl_mensagem.Text = "Coin was inserted with success to your favorites!";
                        lbl_mensagem.CssClass = "alert alert-success";
                        string message = "Coin was inserted with success to your favorites!";
                        string url = $"coin_details_client.aspx?id={cod_moeda_estado}&message={Server.UrlEncode(message)}";
                        Response.Redirect(url, false);
                    }
                    else
                    {
                        lbl_mensagem.Text = "Coin was not inserted with success to your favorites, it already exists!";
                        lbl_mensagem.CssClass = "alert alert-danger";
                    }
                }
                else
                {
                    if (!Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(cod_moeda_estado)))
                    {
                        Insercao.Delete_Favorites(Convert.ToInt32(cod_moeda_estado), Convert.ToInt32(Session["user_code"]));
                        lbl_mensagem.Text = "Coin was deleted with success from your favorites!";
                        lbl_mensagem.CssClass = "alert alert-success";
                        string message = "Coin was deleted with success from your favorites!";
                        string url = $"coin_details_client.aspx?id={cod_moeda_estado}&message={Server.UrlEncode(message)}";
                        Response.Redirect(url, false);
                    }
                    else
                    {
                        lbl_mensagem.Text = "Coin was not deleted with from your favorites, it does not exist!";
                        lbl_mensagem.CssClass = "alert alert-danger";
                    }
                }
            }
        }
    }
}