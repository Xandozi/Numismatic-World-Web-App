using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class dashboard : System.Web.UI.Page
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
                // Leitura para dentro da lista de objetos e consequente representação da informação na página
                List<Estatisticas> stats = Estatisticas.Read_Stats();
                if (stats != null)
                {
                    lblTotalUsers.Text = stats[0].total_utilizadores.ToString();
                    lblTotalUsersWithFavorites.Text = string.Format("{0:F2}", stats[0].total_utilizadores_favoritos.ToString());
                    lblAvgCoinsPerUser.Text = stats[0].media_moedas_utilizador.ToString();
                    lblUserWithMostCoins.Text = stats[0].username_max_moedas;
                    lblMaxCoinsQuantity.Text = stats[0].utilizador_max_moedas_quantidade.ToString();
                }

                rptMostValuableCoins.DataSource = Moeda.Read_Coin_Top10_Valuable();
            }
        }
    }
}