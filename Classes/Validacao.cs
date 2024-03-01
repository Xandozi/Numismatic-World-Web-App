using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Numismatica
{
    public class Validacao
    {
        // Função para verificar se o username existe na base de dados e que retorna true ou false
        public static bool Check_Username(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Username";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
            }
        }

        // Função para verificar se o email existe na base de dados e retornar true ou false
        public static bool Check_Email(string email)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@email", email);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Email";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
            }
        }

        // Função para verificar se o user está ativo ou não e retornar true ou false
        public static bool Check_Ative(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Active";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
            }
        }

        // Função para ativar ou desativar o utilizador de acordo com o que estiver na BD
        public static void Ativacao(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            SqlCommand myCommand = new SqlCommand();

            myCommand.Parameters.AddWithValue("@username", username);

            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.CommandText = "User_Activation";

            myCommand.Connection = myConn;
            myConn.Open();
            myCommand.ExecuteNonQuery();
            myConn.Close();
        }

        // Função para verificar se as credenciais de login estão corretas ou não e assim retornar um int de acordo com as opções
        public static int Check_Login(string username, string password)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);
                myCommand.Parameters.AddWithValue("@password", EncryptString(password));

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Login";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido;
            }
        }

        // Função para verificar o perfil do utilizador e retornar em string o mesmo
        public static string Check_Perfil(string username)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@username", username);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@perfil";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.VarChar;
                resposta_SP.Size = 50;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Perfil";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                string perfil = myCommand.Parameters["@perfil"].Value.ToString();

                myConn.Close();

                return perfil;
            }
        }

        // Função para verificar se o utilizador tem o perfil de administrador na BD
        public static bool Check_IsAdmin(string username)
        {
            if (Validacao.Check_Perfil(username) == "Administrador")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Função para verificar se a moeda está nos favoritos de um determinado utilizador
        public static bool Check_Favorites(int cod_user, int cod_moeda_estado)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["numismaticaConnectionString"].ConnectionString);

            using (SqlCommand myCommand = new SqlCommand())
            {
                myCommand.Parameters.AddWithValue("@cod_moeda_estado", cod_moeda_estado);
                myCommand.Parameters.AddWithValue("@cod_user", cod_user);

                SqlParameter resposta_SP = new SqlParameter();
                resposta_SP.ParameterName = "@valido";
                resposta_SP.Direction = ParameterDirection.Output;
                resposta_SP.SqlDbType = SqlDbType.Int;
                myCommand.Parameters.Add(resposta_SP);

                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Check_Favorites";

                myCommand.Connection = myConn;
                myConn.Open();
                myCommand.ExecuteNonQuery();

                int valido = Convert.ToInt32(myCommand.Parameters["@valido"].Value);

                myConn.Close();

                return valido == 1;
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

        public static string DecryptString(string Message)
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

            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]

            Message = Message.Replace("KKK", "+");
            Message = Message.Replace("JJJ", "/");
            Message = Message.Replace("III", "\\");


            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5. Attempt to decrypt the string
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                // Clear the TripleDes and Hashprovider services of any sensitive information
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6. Return the decrypted string in UTF8 format
            return UTF8.GetString(Results);
        }
    }
}