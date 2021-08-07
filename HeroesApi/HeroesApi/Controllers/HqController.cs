using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using HeroesApi.Utils;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace HeroesApi.Controllers
{   
    [Route("api/[controller]")] 
    [ApiController]
    public class HqController : ControllerBase
    {
        static bool UsarBackUp = false;/* Caso o dominio Padrao nao esteja disponivel ou seu html/css tenha sido alterado*/

        [HttpGet("{Name}")]
        public IEnumerable<Personagem> Get (string Name)     
        {
            #region Download do Html/CSS

            string strHtml = string.Empty;
            string Url = string.Empty;

            if (!UsarBackUp)
            {
                Url = Constantes.URLMAIN + Name.Trim().Replace(" ", "_");
                strHtml = HtmlUtils.HtmlToString(Url);
            }
            else
            {
                strHtml = System.IO.File.ReadAllText(@"ArquivosBackUp/Wolv.txt", Encoding.Default) ;
                Name = "Wolverine";
            }

            #endregion

            #region Realiza a busca pela chave

            string realname = RegexUtils.FirstMatch("<b>Nome: </b>(?<Busca>.*?)\\s<", "Busca", strHtml);
            
            if(realname.Length == 0)
                realname = RegexUtils.FirstMatch("<b>Nome:&#160;</b>(?<Busca>.*?)<", "Busca", strHtml);

            #endregion

            #region Faz o processamento dos objetos para um retorno de obj Json

            Personagem p = new Personagem();
            p.RealNome = realname;
            p.Nome = StringUtils.CapitalizarNome(Name);
            p.Mensagem = realname.Length > 0 ? "Encontrado!" : "Não Encontrado!";
            return Enumerable.Range(1, 1).Select(index => p);

            #endregion
        }

        /*localhost:/Api/HQ/pdf/wolverine*/
        [HttpGet("PDF/{n}")]
        [Route("PDF/{pdf}/{a}")]
        public IActionResult Get(string pdf,string n)
        {

            #region Converte URL para String

            string strHtml = string.Empty;
            string Url = string.Empty;

            if (!UsarBackUp)
            {
                Url = Constantes.URLMAIN + n.Trim().Replace(" ", "_");
                strHtml = HtmlUtils.HtmlToString(Url);
            }
            else
            {
                strHtml = System.IO.File.ReadAllText(@"ArquivosBackUp/Wolv.txt", Encoding.Default);
                n = "Wolverine";
            }

            #endregion

            #region Realiza a busca pela chave

            string realname = RegexUtils.FirstMatch("<b>Nome: </b>(?<Busca>.*?)\\s<", "Busca", strHtml);

            if (realname.Length == 0)
                realname = RegexUtils.FirstMatch("<b>Nome:&#160;</b>(?<Busca>.*?)<", "Busca", strHtml);

            #endregion

            #region Verifica Integridade

            var valido = realname.Length > 0;

            #endregion
            if (valido)
            {               
                #region Faz a conversão para String(outro tipo qualquer)

                Personagem p = new Personagem();
                p.RealNome = realname;
                p.Nome = StringUtils.CapitalizarNome(n);

                List<List<string>> list = new List<List<string>>();
                List<string> itens = new List<string>();

                PropertyInfo[] propriedades = typeof(Personagem).GetProperties();

                // Percorre a lista, obtendo o nome de cada uma das propriedades
                foreach (PropertyInfo pi in propriedades)
                    itens.Add(pi.Name);// Obtém o nome da propriedade...

                list.Add(itens);

                itens = new List<string>();
                itens.Add(p.Nome);
                itens.Add(p.RealNome);
                itens.Add("Encontrado");
                list.Add(itens);
                #endregion

                #region Gerar Arquivo Interno e Realiza Download Cliente                      
                return new FileStreamResult(
                    new FileStream(
                        PdfUtils.GerarPDF(list, "Exemplo"),
                        FileMode.Open),
                        "application/pdf");
                #endregion
            }            
            else
            {
                #region Gerar Arquivo Parar Exibir o Erro

                dynamic obj = new ExpandoObject();

                obj.Mensagem = "Não Encontrado";
                obj.Erro = strHtml.Contains("The remote server returned an error:") ? strHtml : null;
                obj.Busca = n;

                string json = JsonConvert.SerializeObject(obj);

                return new FileStreamResult(StringUtils.StringToStream(json), "application/json");
                #endregion
            }
        }

    }
}
