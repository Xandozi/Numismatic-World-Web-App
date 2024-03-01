using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Numismatica.Classes
{
    public class Estatisticas
    {
        // Propriedades da classe Estatisticas
        public int total_utilizadores { get; set; }
        public int total_utilizadores_favoritos { get; set; }
        public decimal media_moedas_utilizador { get; set; }
        public int utilizador_max_moedas_quantidade { get; set; }
        public int cod_user_max_moedas { get; set; }
        public string username_max_moedas { get; set; }


        // Função para retornar uma lista de objetos da classe estatística que a sua função é apenas ler dados através de uma stored procedure e colocá-las nas propriedades correspondentes
        public static List<Estatisticas> Read_Stats()
        {
            List<Estatisticas> lst_estatisticas = new List<Estatisticas>();

            Estatisticas informacao = new Estatisticas();

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            SqlParameter total_utilizadores = new SqlParameter();
            total_utilizadores.ParameterName = "@total_utilizadores";
            total_utilizadores.Direction = ParameterDirection.Output;
            total_utilizadores.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(total_utilizadores);

            SqlParameter total_utilizadores_favoritos = new SqlParameter();
            total_utilizadores_favoritos.ParameterName = "@total_utilizadores_favoritos";
            total_utilizadores_favoritos.Direction = ParameterDirection.Output;
            total_utilizadores_favoritos.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(total_utilizadores_favoritos);

            SqlParameter media_moedas_utilizador = new SqlParameter();
            media_moedas_utilizador.ParameterName = "@media_moedas_utilizador";
            media_moedas_utilizador.Direction = ParameterDirection.Output;
            media_moedas_utilizador.SqlDbType = SqlDbType.Decimal;
            media_moedas_utilizador.Precision = 20;
            media_moedas_utilizador.Scale = 2;
            myCommand.Parameters.Add(media_moedas_utilizador);

            SqlParameter utilizador_max_moedas_quantidade = new SqlParameter();
            utilizador_max_moedas_quantidade.ParameterName = "@utilizador_max_moedas_quantidade";
            utilizador_max_moedas_quantidade.Direction = ParameterDirection.Output;
            utilizador_max_moedas_quantidade.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(utilizador_max_moedas_quantidade);

            SqlParameter cod_user_max_moedas = new SqlParameter();
            cod_user_max_moedas.ParameterName = "@cod_user_max_moedas";
            cod_user_max_moedas.Direction = ParameterDirection.Output;
            cod_user_max_moedas.SqlDbType = SqlDbType.Int;
            myCommand.Parameters.Add(cod_user_max_moedas);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "Dashboard_Stats";

            myCommand.Connection = myConn;
            myConn.Open();
            myCommand.ExecuteNonQuery();
            informacao.total_utilizadores = Convert.ToInt32(myCommand.Parameters["@total_utilizadores"].Value);
            informacao.total_utilizadores_favoritos = Convert.ToInt32(myCommand.Parameters["@total_utilizadores_favoritos"].Value);
            informacao.media_moedas_utilizador = Convert.ToDecimal(myCommand.Parameters["@media_moedas_utilizador"].Value);
            informacao.utilizador_max_moedas_quantidade = Convert.ToInt32(myCommand.Parameters["@utilizador_max_moedas_quantidade"].Value);
            informacao.cod_user_max_moedas = Convert.ToInt32(myCommand.Parameters["@cod_user_max_moedas"].Value);
            informacao.username_max_moedas = Extract.Username(Convert.ToInt32(myCommand.Parameters["@cod_user_max_moedas"].Value));
            lst_estatisticas.Add(informacao);
            myConn.Close();

            return lst_estatisticas;
        }
    }
}