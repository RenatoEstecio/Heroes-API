
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesApi.Utils
{
    public class PdfUtils
    {
        public static string GerarPDF(List<List<string>> list,string relName)
        {
            if (list != null)
            {

                Document doc = new Document(PageSize.A4);//criando e estipulando o tipo da folha usada
                doc.SetMargins(40, 40, 40, 80);//estibulando o espaçamento das margens que queremos
                doc.AddCreationDate();//adicionando as configuracoes

                //caminho onde sera criado o pdf + nome desejado
                //OBS: o nome sempre deve ser terminado com .pdf
                string caminho = @"C:\temp\HeroesAPI_"+ relName+"_"+ DateTimeUtils.GetDateTimeNowToString() + ".pdf";

                //criando o arquivo pdf embranco, passando como parametro a variavel doc criada acima e a variavel caminho
                //tambem criada acima.
                PdfWriter writer = PdfWriter.GetInstance(doc, new
                FileStream(caminho, FileMode.Create));

                doc.Open();
              
                PdfPTable tabela = new PdfPTable(list[0].Count);               
                //Pode ser interessante colocar a primeira linha em negrito e adicionar 
                //à lista os títulos dos campos.
                for (int i = 0; i < list.Count; i++)
                    for (int j = 0; j < list[i].Count; j++)
                        tabela.AddCell(list[i][j]);

                doc.Add(tabela);

                doc.Close();
                return caminho;
            }
            return null;
                
        }
    }
}
