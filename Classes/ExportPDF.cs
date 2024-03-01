using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Numismatica.Classes
{
    public class ExportPDF
    {
        // Função para retornar um array de byte para formar o PDF de exportação da colecção
        public byte[] MyCollection(int cod_user)
        {
            // Ir buscar informação sobre o utilizador e as moedas recorrendo a funções criadas em outras classes
            List<Users> User_Info = Users.Read_User(cod_user);
            List<Moeda> Coin_Collection = Moeda.Read_User_Collection(cod_user.ToString());

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Adicionar o header
                Paragraph header = new Paragraph($"Numismatic World - {User_Info[0].username}'s Collection\n\n", new Font(Font.FontFamily.HELVETICA, 18));
                header.Alignment = Element.ALIGN_CENTER;
                document.Add(header);

                // Adicionar a informação relativa ao utilizador
                PdfPTable userTable = new PdfPTable(2);
                PdfPCell usernameCell = new PdfPCell(new Phrase($"Username: {User_Info[0].username}"));
                usernameCell.Border = Rectangle.BOTTOM_BORDER;
                userTable.AddCell(usernameCell);
                PdfPCell emailCell = new PdfPCell(new Phrase($"Email: {User_Info[0].email}"));
                emailCell.Border = Rectangle.BOTTOM_BORDER;
                userTable.AddCell(emailCell);
                document.Add(userTable);

                document.Add(new Paragraph("\n\n"));

                // Criar uma tabela para inserção das moedas favoritas do utilizador
                PdfPTable coinCollectionTable = new PdfPTable(2);

                // Adicionar cada moeda á tabela
                int coinCount = 0;
                foreach (Moeda coin in Coin_Collection)
                {
                    // Ir buscar a fotografia da moeda em questão á base de dados
                    byte[] photo = Moeda.Get_Photo_Coin_Database(coin.cod_moeda);

                    // Adicionar a fotografia ao PDF
                    iTextSharp.text.Image coinImage = iTextSharp.text.Image.GetInstance(photo);
                    coinImage.ScaleAbsolute(50, 50);

                    // Criar uma cell para cada a informação de cada moeda á medidade que são carregadas da lista
                    PdfPTable coinTable = new PdfPTable(1);
                    PdfPCell coinCell = new PdfPCell();
                    coinCell.Border = Rectangle.BOTTOM_BORDER;
                    coinCell.PaddingTop = 10f;
                    coinCell.PaddingBottom = 10f;
                    coinCell.AddElement(coinImage);

                    // Adicionar a informação da moeda ao PDF
                    Paragraph coinInfo = new Paragraph($"\nCoin Name: {coin.nome}\nDescription: {coin.descricao}\nGrade: {coin.estado}\nMint: {coin.cunho}\nType: {coin.tipo}\nCurrent Value: {coin.valor_atual}€\n");
                    coinInfo.KeepTogether = true; // Evitar que a informação de uma moeda seja separada aquando da mudança de página
                    coinCell.AddElement(coinInfo);

                    coinTable.AddCell(coinCell);

                    // Adicionar a cell referente a esta moeda á tabela de colecção
                    PdfPCell collectionCell = new PdfPCell(coinTable);
                    collectionCell.Border = Rectangle.NO_BORDER;
                    coinCollectionTable.AddCell(collectionCell);

                    coinCount++;
                }

                // Caso o número de moedas total for ímpar, colocar uma linha vazia final para evitar que sejam "perdidas" moedas
                if (coinCount % 2 != 0)
                {
                    PdfPCell emptyCell = new PdfPCell();
                    emptyCell.Border = Rectangle.NO_BORDER;
                    coinCollectionTable.AddCell(emptyCell);
                }

                document.Add(coinCollectionTable);

                document.Close();

                return memoryStream.ToArray();
            }
        }

    }
}