using Numismatica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Numismatica
{
    public partial class home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Bind da informação aos repeaters
            rptCoins.DataSource = Moeda.Read_Latest_Coins();
            rpt2.DataSource = Moeda.Read_Most_Favorite_Coins();
        }
    }
}