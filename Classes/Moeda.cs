using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Numismatica
{
    public class Moeda
    {
        public int cod_moeda { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string cunho { get; set; }
        public int cod_estado { get; set; }
        public string estado { get; set; }
        public decimal valor_atual { get; set; }
        public string foto { get; set; }
        public List<string> lst_fotos { get; set; }
        public int cod_moeda_estado { get; set; }
        public int low_cod_moeda { get; set; }
        public bool deleted { get; set; }
        public string tipo { get; set; }
        public int cod_tipo { get; set; }

        // Função para ler todas as moedas do catálogo de acordo com os parâmetros que serão provenientes dos filtros
        // inseridos na página e devolver através de uma lista de objetos MOEDA
        public static List<Moeda> Read_Coin_All(string search_string, int grade, int coin_type, string sort_order, int status)
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            List<string> conditions = new List<string>();

            string query = "select moeda.cod_moeda, moeda.nome, moeda.descricao, moeda.cunho, moeda_estado.cod_estado, estado.estado, moeda_estado.valor_atual, fotos.foto, moeda_estado.cod_moeda_estado, moeda_estado.deleted, tipo.tipo from moeda " +
                "join moeda_estado on moeda.cod_moeda=moeda_estado.cod_moeda " +
                "join fotos on moeda.cod_moeda=fotos.cod_moeda " +
                "join estado on moeda_estado.cod_estado=estado.cod_estado " +
                "join tipo on moeda.cod_tipo=tipo.cod_tipo";

            // Decisões para colocar ou não os filtros dentro da string query
            if (!string.IsNullOrEmpty(search_string))
            {
                conditions.Add($"moeda.nome LIKE '%{search_string}%'");
            }
            if (grade != 0)
            {
                conditions.Add($"moeda_estado.cod_estado = {grade}");
            }
            if (coin_type != 0)
            {
                conditions.Add($"moeda.cod_tipo = {coin_type}");
            }
            if (status == 0)
            {
                conditions.Add($"moeda_estado.deleted = {status}");
            }
            else if (status == 1)
            {
                conditions.Add($"moeda_estado.deleted = {status}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY moeda_estado.valor_atual " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            // Maneira de evitar que sejam colocadas informações repetidas derivadas de cada moeda ter várias fotos.
            HashSet<int> processedCoins = new HashSet<int>();

            while (dr.Read())
            {
                int cod = dr.GetInt32(8);
                if (!processedCoins.Contains(cod))
                {
                    Moeda informacao = new Moeda();
                    informacao.cod_moeda = dr.GetInt32(0);
                    informacao.nome = dr.GetString(1);
                    informacao.descricao = dr.GetString(2);
                    informacao.cunho = dr.GetString(3);
                    informacao.cod_estado = dr.GetInt32(4);
                    informacao.estado = dr.GetString(5);
                    informacao.valor_atual = dr.GetDecimal(6);
                    informacao.foto = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                    informacao.cod_moeda_estado = dr.GetInt32(8);
                    informacao.deleted = dr.GetBoolean(9);
                    informacao.tipo = dr.GetString(10);

                    lst_moeda.Add(informacao);
                    processedCoins.Add(cod);
                }
            }
            myConn.Close();
            return lst_moeda;
        }

        // Função para ler as informações de uma certa moeda das moedas que estão na colecção de um certo utilizador
        // retornando as mesmas numa lista de objetos MOEDA com os filtros integrados
        public static List<Moeda> Read_Coin_User_Favorites(string cod_user, string search_string, int grade, int coin_type, string sort_order, int status)
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            List<string> conditions = new List<string>();

            string query = $"select moeda.cod_moeda, moeda.nome, moeda.descricao, moeda.cunho, moeda_estado.cod_estado, estado.estado, moeda_estado.valor_atual, fotos.foto, moeda_estado.cod_moeda_estado, moeda_estado.deleted, tipo.tipo " +
                $"from moeda " +
                $"join moeda_estado on moeda.cod_moeda=moeda_estado.cod_moeda " +
                $"join fotos on moeda.cod_moeda=fotos.cod_moeda " +
                $"join estado on moeda_estado.cod_estado=estado.cod_estado " +
                $"join tipo on moeda.cod_tipo=tipo.cod_tipo " +
                $"join favoritos on moeda_estado.cod_moeda_estado=favoritos.cod_moeda_estado ";

            conditions.Add($"favoritos.cod_user = {cod_user}");

            if (!string.IsNullOrEmpty(search_string))
            {
                conditions.Add($"moeda.nome LIKE '%{search_string}%'");
            }
            if (grade != 0)
            {
                conditions.Add($"moeda_estado.cod_estado = {grade}");
            }
            if (coin_type != 0)
            {
                conditions.Add($"moeda.cod_tipo = {coin_type}");
            }
            if (status == 0)
            {
                conditions.Add($"moeda_estado.deleted = {status}");
            }
            else if (status == 1)
            {
                conditions.Add($"moeda_estado.deleted = {status}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY moeda_estado.valor_atual " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            HashSet<int> processedCoins = new HashSet<int>();

            while (dr.Read())
            {
                int cod = dr.GetInt32(8);
                if (!processedCoins.Contains(cod))
                {
                    Moeda informacao = new Moeda();
                    informacao.cod_moeda = dr.GetInt32(0);
                    informacao.nome = dr.GetString(1);
                    informacao.descricao = dr.GetString(2);
                    informacao.cunho = dr.GetString(3);
                    informacao.cod_estado = dr.GetInt32(4);
                    informacao.estado = dr.GetString(5);
                    informacao.valor_atual = dr.GetDecimal(6);
                    informacao.foto = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                    informacao.cod_moeda_estado = dr.GetInt32(8);
                    informacao.deleted = dr.GetBoolean(9);
                    informacao.tipo = dr.GetString(10);

                    lst_moeda.Add(informacao);
                    processedCoins.Add(cod);
                }
            }
            myConn.Close();
            return lst_moeda;
        }

        // Função para ler as moedas todas que estão na colecção de um determinado utilizador e devolver através de lista de objetos
        public static List<Moeda> Read_User_Collection(string cod_user)
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            List<string> conditions = new List<string>();

            string query = $"select moeda.cod_moeda, moeda.nome, moeda.descricao, moeda.cunho, moeda_estado.cod_estado, estado.estado, moeda_estado.valor_atual, fotos.foto, moeda_estado.cod_moeda_estado, moeda_estado.deleted, tipo.tipo " +
                $"from moeda " +
                $"join moeda_estado on moeda.cod_moeda=moeda_estado.cod_moeda " +
                $"join fotos on moeda.cod_moeda=fotos.cod_moeda " +
                $"join estado on moeda_estado.cod_estado=estado.cod_estado " +
                $"join tipo on moeda.cod_tipo=tipo.cod_tipo " +
                $"join favoritos on moeda_estado.cod_moeda_estado=favoritos.cod_moeda_estado ";

            conditions.Add($"favoritos.cod_user = {cod_user}");

            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            HashSet<int> processedCoins = new HashSet<int>();

            while (dr.Read())
            {
                int cod = dr.GetInt32(8);
                if (!processedCoins.Contains(cod))
                {
                    Moeda informacao = new Moeda();
                    informacao.cod_moeda = dr.GetInt32(0);
                    informacao.nome = dr.GetString(1);
                    informacao.descricao = dr.GetString(2);
                    informacao.cunho = dr.GetString(3);
                    informacao.cod_estado = dr.GetInt32(4);
                    informacao.estado = dr.GetString(5);
                    informacao.valor_atual = dr.GetDecimal(6);
                    informacao.foto = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                    informacao.cod_moeda_estado = dr.GetInt32(8);
                    informacao.deleted = dr.GetBoolean(9);
                    informacao.tipo = dr.GetString(10);

                    lst_moeda.Add(informacao);
                    processedCoins.Add(cod);
                }
            }
            myConn.Close();
            return lst_moeda;
        }

        // Ler as informações de uma determianda moeda e devolvendo as informações da mesma através de uma lista de objetos
        public static List<Moeda> Read_Coin(int cod_moeda_estado)
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            string query = $"select moeda.cod_moeda, moeda.nome, moeda.descricao, moeda.cunho, moeda_estado.cod_estado, estado.estado, moeda_estado.valor_atual, fotos.foto, moeda_estado.cod_moeda_estado, moeda_estado.deleted, tipo.tipo, moeda.cod_tipo from moeda " +
                $"join moeda_estado on moeda.cod_moeda=moeda_estado.cod_moeda " +
                $"join fotos on moeda.cod_moeda=fotos.cod_moeda " +
                $"join estado on moeda_estado.cod_estado=estado.cod_estado " +
                $"join tipo on moeda.cod_tipo=tipo.cod_tipo where cod_moeda_estado = {cod_moeda_estado}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Moeda informacao = new Moeda();
                informacao.cod_moeda = dr.GetInt32(0);
                informacao.nome = dr.GetString(1);
                informacao.descricao = dr.GetString(2);
                informacao.cunho = dr.GetString(3);
                informacao.cod_estado = dr.GetInt32(4);
                informacao.estado = dr.GetString(5);
                informacao.valor_atual = dr.GetDecimal(6);
                informacao.lst_fotos = Get_Photos_Coin(informacao.cod_moeda);
                informacao.cod_moeda_estado = dr.GetInt32(8);
                informacao.deleted = dr.GetBoolean(9);
                informacao.tipo = dr.GetString(10);
                informacao.cod_tipo = dr.GetInt32(11);

                lst_moeda.Add(informacao);
            }

            myConn.Close();
            return lst_moeda;
        }

        // Função para ler as ultimas 10 moedas inseridas no catálogo para mostrá-las na página home.aspx
        public static List<Moeda> Read_Latest_Coins()
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            string query = "select top 10 " +
                            "moeda_estado.cod_moeda_estado, " +
                            "moeda_estado.valor_atual, " +
                            "estado.estado, " +
                            "moeda.nome, " +
                            "moeda_estado.deleted, " +
                            "( " +
                            "    select top 1 fotos.foto " +
                            "    from fotos " +
                            "    where fotos.cod_moeda = moeda.cod_moeda " +
                            "    order by fotos.cod_foto " +
                            ") as foto " +
                            "from " +
                            "moeda_estado " +
                            "join moeda on moeda.cod_moeda = moeda_estado.cod_moeda " +
                            "join estado on estado.cod_estado = moeda_estado.cod_estado " +
                            "where " +
                            "moeda_estado.deleted = 0 " +
                            "order by " +
                            "moeda_estado.cod_moeda_estado desc;";


            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            HashSet<int> processedCoins = new HashSet<int>();

            while (dr.Read())
            {
                int cod = dr.GetInt32(0);
                if (!processedCoins.Contains(cod))
                {
                    Moeda informacao = new Moeda();
                    informacao.cod_moeda_estado = dr.GetInt32(0);
                    informacao.valor_atual = dr.GetDecimal(1);
                    informacao.estado = dr.GetString(2);
                    informacao.nome = dr.GetString(3);
                    informacao.deleted = dr.GetBoolean(4);
                    informacao.foto = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                    lst_moeda.Add(informacao);
                    processedCoins.Add(dr.GetInt32(0));
                }
            }
            myConn.Close();
            return lst_moeda;
        }

        // Função para mostrar em home.aspx o top 10 de moedas com mais favoritos, ou seja mais vezes adicionadas a colecções
        public static List<Moeda> Read_Most_Favorite_Coins()
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            string query = "select top 10 " +
                            "moeda_estado.cod_moeda_estado, " +
                            "moeda_estado.valor_atual, " +
                            "estado.estado, " +
                            "moeda.nome, " +
                            "moeda_estado.deleted, " +
                            "(select top 1 fotos.foto from fotos where fotos.cod_moeda = moeda.cod_moeda) as foto, " +
                            "COUNT(DISTINCT favoritos.cod_user) as favorites " +
                            "from moeda_estado " +
                            "join moeda on moeda.cod_moeda = moeda_estado.cod_moeda " +
                            "join estado on estado.cod_estado = moeda_estado.cod_estado " +
                            "join favoritos on moeda_estado.cod_moeda_estado = favoritos.cod_moeda_estado " +
                            "where moeda_estado.deleted = 0 " +
                            "group by moeda_estado.cod_moeda_estado, moeda_estado.valor_atual, estado.estado, moeda.nome, moeda_estado.deleted, moeda.cod_moeda " +
                            "order by favorites desc, moeda_estado.cod_moeda_estado desc";


            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            HashSet<int> processedCoins = new HashSet<int>();

            while (dr.Read())
            {
                int cod = dr.GetInt32(0);
                if (!processedCoins.Contains(cod))
                {
                    Moeda informacao = new Moeda();
                    informacao.cod_moeda_estado = dr.GetInt32(0);
                    informacao.valor_atual = dr.GetDecimal(1);
                    informacao.estado = dr.GetString(2);
                    informacao.nome = dr.GetString(3);
                    informacao.deleted = dr.GetBoolean(4);
                    informacao.foto = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]);

                    lst_moeda.Add(informacao);
                    processedCoins.Add(dr.GetInt32(0));
                }
            }
            myConn.Close();
            return lst_moeda;
        }

        // Função para mostrar o top 10 de moedas mais valiosas
        public static List<Moeda> Read_Coin_Top10_Valuable()
        {
            List<Moeda> lst_moeda = new List<Moeda>();

            string query = $"select top 10 moeda_estado.cod_moeda_estado, moeda_estado.valor_atual, estado.estado, moeda.nome, moeda_estado.deleted from moeda_estado " +
                "join moeda on moeda.cod_moeda=moeda_estado.cod_moeda " +
                "join estado on estado.cod_estado=moeda_estado.cod_estado " +
                "where moeda_estado.deleted = 0 " +
                "order by moeda_estado.valor_atual desc";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Moeda informacao = new Moeda();
                informacao.cod_moeda_estado = dr.GetInt32(0);
                informacao.valor_atual = dr.GetDecimal(1);
                informacao.estado = dr.GetString(2);
                informacao.nome = dr.GetString(3);
                informacao.deleted = dr.GetBoolean(4);

                lst_moeda.Add(informacao);
            }
            myConn.Close();
            return lst_moeda;
        }

        // Função para ir buscar as fotos de uma determinada moeda
        public static List<string> Get_Photos_Coin(int cod_moeda_estado)
        {
            List<string> fotos = new List<string>();

            string query = $"select foto from fotos where cod_moeda = {cod_moeda_estado}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                string foto = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["foto"]);
                fotos.Add(foto);
            }

            return fotos;
        }

        // Função para ir buscar 1 foto apenas á base de dados referente á moeda em questão
        public static byte[] Get_Photo_Coin_Database(int cod_moeda_estado)
        {
            byte[] foto = null;

            string query = $"select top 1 foto from fotos where cod_moeda = {cod_moeda_estado}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            if (dr.Read())
            {
                foto = (byte[])dr["foto"];
            }

            myConn.Close();

            return foto;
        }

    }
}