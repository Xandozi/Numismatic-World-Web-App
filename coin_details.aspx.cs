using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class coin_details : System.Web.UI.Page
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
                    if (coin[0].deleted)
                    {
                        lbl_deleted.Text = "Inactive";
                        lbl_deleted.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lbl_deleted.Text = "Active";
                        lbl_deleted.BackColor = System.Drawing.Color.Green;
                    }

                    rptFotos.DataSource = coin[0].lst_fotos;
                    rptFotos.DataBind();
                }
            }
        }

        protected void btn_modal_delete_Click(object sender, EventArgs e)
        {
            int cod_moeda_estado = Convert.ToInt32(Request.QueryString["id"]);
            Insercao.Update_Moeda_Estado(cod_moeda_estado, 1);

            string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect(currentPageUrl);
        }

        protected void btn_modal_activate_Click(object sender, EventArgs e)
        {
            int cod_moeda_estado = Convert.ToInt32(Request.QueryString["id"]);
            Insercao.Update_Moeda_Estado(cod_moeda_estado, 0);

            string currentPageUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect(currentPageUrl);
        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            int cod_moeda_estado = Convert.ToInt32(Request.QueryString["id"]);
            Response.Redirect($"edit_coin_details.aspx?id={cod_moeda_estado}");
        }
    }
}