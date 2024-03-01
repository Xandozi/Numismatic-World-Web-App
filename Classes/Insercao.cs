using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Numismatica
{
    public class Insercao
    {
        // Função para inserir o user na BD
        public static void Inserir_User(string username, string password, string email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@password", EncryptString(password));
                myCommand.Parameters.AddWithValue("@email", email);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_User";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para inserir o user que faz registo/login através do Google
        public static int Inserir_User_Google(string username, string email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@email", email);

                SqlParameter valido = new SqlParameter();
                valido.ParameterName = "@valido";
                valido.Direction = ParameterDirection.Output;
                valido.SqlDbType = SqlDbType.Bit;
                myCommand.Parameters.Add(valido);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_User_Google";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int valido_SP = Convert.ToInt32(myCommand.Parameters["@valido"].Value);
                myConn.Close();

                return valido_SP;
            }
        }

        // Função para inserir a informação da moeda passada com diversos parâmetros de acordo com a BD
        public static void Inserir_Moeda(string nome, string descricao, string cunho, int cod_estado, int estado, double valor_atual, List<byte[]> fotos)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@nome", nome);
                myCommand.Parameters.AddWithValue("@descricao", descricao);
                myCommand.Parameters.AddWithValue("@cunho", cunho);
                myCommand.Parameters.AddWithValue("@cod_tipo", cod_estado);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@cod_moeda";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Moeda";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int cod_moeda = Convert.ToInt32(myCommand.Parameters["@cod_moeda"].Value);
                myConn.Close();

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@cod_moeda", cod_moeda);
                myCommand.Parameters.AddWithValue("@cod_estado", estado);
                myCommand.Parameters.AddWithValue("@valor_atual", valor_atual);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Moeda_Estado";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();

                // Ciclo para inserir cada foto que o utilizador colocar na moeda dentro da tabela fotos na BD
                foreach (byte[] foto in fotos)
                {
                    myCommand.Parameters.Clear();
                    myCommand.Parameters.AddWithValue("@cod_moeda", cod_moeda);
                    myCommand.Parameters.AddWithValue("@foto", foto);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Insert_Fotos_Moeda";

                    myCommand.Connection = myConn;
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    myConn.Close();
                }
            }
        }

        // Função para dar update ao username
        public static void Update_Username(string old_username, string new_username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@new_username", new_username);
                myCommand.Parameters.AddWithValue("@old_username", old_username);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Username";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para dar update ao Email
        public static void Update_Email(string old_email, string new_email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@new_email", new_email);
                myCommand.Parameters.AddWithValue("@old_email", old_email);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Email";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para dar update á password
        public static void Update_Password(string email, string password)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@email", email);
                myCommand.Parameters.AddWithValue("@password", password);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Password";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para dar update á moeda_estado, neste caso para colocá-la apagada/desativada
        public static int Update_Moeda_Estado(int cod_moeda_estado, int deleted)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_moeda_estado", cod_moeda_estado);
                myCommand.Parameters.AddWithValue("@deleted", deleted);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@return";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Update_Moeda_Estado";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                int return_SP = Convert.ToInt32(myCommand.Parameters["@return"].Value);
                myConn.Close();

                return return_SP;
            }
        }

        // Função para ativar/desativar ou apagar user conforme o parâmetro passado em INT DELETED (!= 0 será para apagar e o == 0 será para apenas dar activate/deactivate
        public static void Update_User(int cod_user, int status, int deleted)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);
                myCommand.Parameters.AddWithValue("@status", status);

                if (deleted == 0)
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Update_User";
                }
                else
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Delete_User";
                }

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para editar as informações referentes a uma determinada moeda
        public static void Edit_Moeda(int cod_moeda, int cod_moeda_estado, string nome, string descricao, string cunho, int cod_estado, int estado, double valor_atual, List<byte[]> fotos)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_moeda", cod_moeda);
                myCommand.Parameters.AddWithValue("@nome", nome);
                myCommand.Parameters.AddWithValue("@descricao", descricao);
                myCommand.Parameters.AddWithValue("@cunho", cunho);
                myCommand.Parameters.AddWithValue("@cod_tipo", cod_estado);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Edit_Moeda";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();

                myCommand.Parameters.Clear();
                myCommand.Parameters.AddWithValue("@cod_moeda_estado", cod_moeda_estado);
                myCommand.Parameters.AddWithValue("@cod_moeda", cod_moeda);
                myCommand.Parameters.AddWithValue("@cod_estado", estado);
                myCommand.Parameters.AddWithValue("@valor_atual", valor_atual);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Edit_Moeda_Estado";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();

                // Decisão para perceber se o user colocou novas fotos na moeda ou não. Caso tenha colocado, apaga todas e volta a introduzir. Caso não, apenas passa ao proximo passo
                if (fotos.Count != 0)
                {
                    myCommand.Parameters.Clear();
                    myCommand.Parameters.AddWithValue("@cod_moeda", cod_moeda);

                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.CommandText = "Delete_Fotos";

                    myCommand.Connection = myConn;
                    myConn.Open();
                    myCommand.ExecuteNonQuery();
                    myConn.Close();

                    foreach (byte[] foto in fotos)
                    {
                        myCommand.Parameters.Clear();
                        myCommand.Parameters.AddWithValue("@cod_moeda", cod_moeda);
                        myCommand.Parameters.AddWithValue("@foto", foto);

                        myCommand.CommandType = CommandType.StoredProcedure;
                        myCommand.CommandText = "Insert_Fotos_Moeda";

                        myCommand.Connection = myConn;
                        myConn.Open();
                        myCommand.ExecuteNonQuery();
                        myConn.Close();
                    }
                }
            }
        }

        // Função para inserir moeda numa colecção de um utilizador
        public static void Add_Favorites(int cod_moeda_estado, int cod_user)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_moeda_estado", cod_moeda_estado);
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Insert_Favorites";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        // Função para apagar dos favoritos de um utilizador uma moeda
        public static void Delete_Favorites(int cod_moeda_estado, int cod_user)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_moeda_estado", cod_moeda_estado);
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Delete_Favorites";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();
                myConn.Close();
            }
        }

        public static string EncryptString(string Message)
        {
            string Passphrase = "batatascomarroz";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below

            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the encrypted string as a base64 encoded string

            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KKK");
            enc = enc.Replace("/", "JJJ");
            enc = enc.Replace("\\", "III");
            return enc;
        }
    }
}