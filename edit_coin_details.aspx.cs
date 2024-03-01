using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class edit_coin_details : System.Web.UI.Page
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
                if (!Page.IsPostBack)
                {
                    // Leitura das informações presentes na BD relativa a determinada cod_moeda_estado
                    List<Moeda> coin = Moeda.Read_Coin(Convert.ToInt32(Request.QueryString["id"]));

                    if (coin.Count > 0)
                    {
                        lbl_cod_moeda.Text = coin[0].cod_moeda.ToString();
                        lbl_cod_moeda_estado.Text = coin[0].cod_moeda_estado.ToString();
                        tb_nome.Text = coin[0].nome;
                        tb_descricao.Text = coin[0].descricao;
                        tb_cunho.Text = coin[0].cunho;
                        ddl_tipo.SelectedValue = coin[0].cod_tipo.ToString();
                        ddl_estado.SelectedValue = coin[0].cod_estado.ToString();
                        tb_valor_atual.Text = coin[0].valor_atual.ToString();

                        rptFotos.DataSource = coin[0].lst_fotos;
                        rptFotos.DataBind();
                    }
                }
            }
            if (!Page.IsPostBack)
            {
                ddl_estado.Items.Insert(0, "---");
                ddl_tipo.Items.Insert(0, "---");
            }
        }

        // Edição das informações dentro da moeda ao clicar no botão editar
        protected void btn_editar_Click(object sender, EventArgs e)
        {
            if (fu_photos.HasFile && fu_photos.PostedFiles.Count < 2)
            {
                lbl_mensagem.Text = "You have to insert at least 2 photos (front and back).";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else if (ddl_estado.SelectedIndex == 0)
            {
                lbl_mensagem.Text = "You have to select a grade before proceeding.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else if (ddl_tipo.SelectedIndex == 0)
            {
                lbl_mensagem.Text = "You have to select a type before proceeding";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else
            {
                List<byte[]> photos = new List<byte[]>();

                // Caso haja imagens colocadas, carregá-las para dentro da lista photos, caso contrário apenas adicionar a lista photos vazia á função de edição de moeda
                if (fu_photos.HasFiles)
                {
                    foreach (HttpPostedFile uploaded_photo in fu_photos.PostedFiles)
                    {
                        Stream imgStream = uploaded_photo.InputStream;
                        int file_size = uploaded_photo.ContentLength;
                        byte[] imgBinaryData = new byte[file_size];
                        imgStream.Read(imgBinaryData, 0, file_size);

                        photos.Add(imgBinaryData);
                    }
                }

                Insercao.Edit_Moeda(Convert.ToInt32(lbl_cod_moeda.Text), Convert.ToInt32(lbl_cod_moeda_estado.Text), tb_nome.Text, tb_descricao.Text, tb_cunho.Text, Convert.ToInt32(ddl_tipo.SelectedValue), Convert.ToInt32(ddl_estado.SelectedValue), Convert.ToDouble(tb_valor_atual.Text), photos);

                int cod_moeda_estado = Convert.ToInt32(Request.QueryString["id"]);
                Response.Redirect($"coin_details.aspx?id={cod_moeda_estado}");
            }
        }
    }
}