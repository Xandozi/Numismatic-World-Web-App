using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Numismatica.Classes
{
    public class Users
    {
        public int cod_user { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public int cod_perfil { get; set; }
        public bool ativo { get; set; }
        public string perfil { get; set; }

        // Função para lêr as informações de todos os users na BD que tem já integrado os filtros da página caso sejam aplicados
        public static List<Users> Read_User_All(string search_string, string email, int active, string sort_order)
        {
            List<Users> lst_user = new List<Users>();

            List<Users> cod_user = new List<Users>();

            List<string> conditions = new List<string>();

            string query = "select users.username, users.email, perfil.perfil, users.ativo, users.cod_user from users join perfil on perfil.cod_perfil=users.cod_perfil";

            if (!string.IsNullOrEmpty(search_string))
            {
                conditions.Add($"users.username LIKE '%{search_string}%'");
            }
            if (!string.IsNullOrEmpty(email))
            {
                conditions.Add($"users.email LIKE '%{email}%'");
            }
            if (active == 0)
            {
                conditions.Add($"users.ativo = {active}");
            }
            else if (active == 1)
            {
                conditions.Add($"users.ativo = {active}");
            }
            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }
            if (!string.IsNullOrEmpty(sort_order))
            {
                query += " ORDER BY users.username " + sort_order;
            }

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            HashSet<int> processedUsers = new HashSet<int>();

            while (dr.Read())
            {
                int cod = dr.GetInt32(4);
                if (!processedUsers.Contains(cod))
                {
                    Users informacao = new Users();
                    informacao.username = dr.GetString(0);
                    informacao.email = dr.GetString(1);
                    informacao.perfil = dr.GetString(2);
                    informacao.ativo = dr.GetBoolean(3);
                    informacao.cod_user = dr.GetInt32(4);

                    lst_user.Add(informacao);
                    processedUsers.Add(cod);
                }
            }
            myConn.Close();
            return lst_user;
        }

        // Função para ler a informação de um certo utilizador
        public static List<Users> Read_User(int cod_user)
        {
            List<Users> lst_user = new List<Users>();

            string query = $"select users.username, users.email, users.ativo, perfil.perfil from users join perfil on perfil.cod_perfil=users.cod_perfil where cod_user={cod_user}";

            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand(query, myConn);

            myConn.Open();

            SqlDataReader dr = myCommand.ExecuteReader();

            while (dr.Read())
            {
                Users informacao = new Users();
                informacao.username = dr.GetString(0);
                informacao.email = dr.GetString(1);
                informacao.ativo = dr.GetBoolean(2);
                informacao.perfil = dr.GetString(3);

                lst_user.Add(informacao);
            }

            myConn.Close();
            return lst_user;
        }
    }
}