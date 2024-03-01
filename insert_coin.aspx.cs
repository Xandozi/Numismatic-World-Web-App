using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class insert_coin : System.Web.UI.Page
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
                ddl_state.Items.Insert(0, "---");
                ddl_type.Items.Insert(0, "---");
            }
        }

        // Inserção da informação que está no formulário á base de dados
        protected void btn_submit_Click(object sender, EventArgs e)
        {
            if (fu_photos.PostedFiles.Count < 2)
            {
                lbl_mensagem.Text = "You have to insert at least 2 photos (front and back).";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else if (ddl_state.SelectedIndex == 0)
            {
                lbl_mensagem.Text = "You have to select a grade before proceeding.";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else if (ddl_type.SelectedIndex == 0)
            {
                lbl_mensagem.Text = "You have to select a type before proceeding";
                lbl_mensagem.CssClass = "alert alert-danger";
            }
            else
            {
                List<byte[]> photos = new List<byte[]>();

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

                Insercao.Inserir_Moeda(tb_name.Text, tb_description.Text, tb_cunho.Text, Convert.ToInt32(ddl_type.SelectedValue), Convert.ToInt32(ddl_state.SelectedValue), Convert.ToDouble(tb_value.Text), photos);
                lbl_mensagem.Text = "Coin was inserted with success on the database!";
                lbl_mensagem.CssClass = "alert alert-success";
            }
        }
    }
}