using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class coins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddl_type.Items.Insert(0, "All");
                ddl_grade.Items.Insert(0, "All");
            }

            // Colocar a informação dentro do repeater para ser mostrada
            BindData();
        }

        // Função de diminuir uma página dentro do repeater
        protected void btn_previous_Click(object sender, EventArgs e)
        {
            PageNumber -= 1;
            BindData();
        }

        // Função de aumentar uma página dentro do repeater
        protected void btn_next_Click(object sender, EventArgs e)
        {
            PageNumber += 1;
            BindData();
        }

        // Função para colocar as informações das moedas todas dentro de uma lista e alimentar o repeater com essa lista
        private void BindData()
        {
            string price = "";

            if (ddl_price.SelectedIndex == 0)
                price = "";
            else if (ddl_price.SelectedIndex != 0)
                price = ddl_price.SelectedValue;

            PagedDataSource pagedData = new PagedDataSource();
            pagedData.DataSource = Moeda.Read_Coin_All(tb_search.Text, ddl_grade.SelectedIndex, ddl_type.SelectedIndex, price, 0);
            pagedData.AllowPaging = true;
            pagedData.PageSize = 18;
            pagedData.CurrentPageIndex = PageNumber;

            rptCoins.DataSource = pagedData;
            rptCoins.DataBind();

            btn_previous.Enabled = !pagedData.IsFirstPage;
            btn_next.Enabled = !pagedData.IsLastPage;
        }

        // Paginação do repeater
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

        // De acordo com o que o "e" seja dentro do repeater, o botão de favoritos irá receber um determinado command argument e
        // ai será onde se perceberá qual o item que está a ser clicado
        protected void rptCoins_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lkb_add_favorites = (LinkButton)e.Item.FindControl("lkb_add_favorites");
                Numismatica.Moeda moeda = (Numismatica.Moeda)e.Item.DataItem;
                string cod_moeda_estado = moeda.cod_moeda_estado.ToString();
                lkb_add_favorites.CommandArgument = cod_moeda_estado;

                if (Session["logged"] == "yes")
                {
                    if (Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(cod_moeda_estado)))
                    {
                        lkb_add_favorites.CssClass = "fa fa-heart";
                    }
                    else
                    {
                        lkb_add_favorites.CssClass = "fa fa-times";
                    }
                }
            }
        }

        // Adição o remoção de determinada moeda dos favoritos do utilizador
        protected void lkb_add_favorites_Click(object sender, EventArgs e)
        {
            if (Session["logged"] != "yes")
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                LinkButton lkb_add_favorites = (LinkButton)sender;
                string cod_moeda_estado = lkb_add_favorites.CommandArgument;

                if (Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(cod_moeda_estado)))
                {
                    if (Validacao.Check_Favorites(Convert.ToInt32(Session["user_code"]), Convert.ToInt32(cod_moeda_estado)))
                    {
                        Insercao.Add_Favorites(Convert.ToInt32(cod_moeda_estado), Convert.ToInt32(Session["user_code"]));
                        lbl_mensagem.Text = "Coin was inserted with success to your favorites!";
                        lbl_mensagem.CssClass = "alert alert-success";
                        BindData();
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
                        BindData();
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