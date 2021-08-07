using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesApi.Utils
{
    static public class StringUtils
    {
        public static string CapitalizarNome(string nome)
        {
            try
            {
                nome = RemoveUnderlines(nome);
                var palavras = new Queue<string>();
                foreach (var palavra in nome.Split(' '))
                {
                    if (!string.IsNullOrEmpty(palavra))
                    {
                        var emMinusculo = palavra.ToLower();
                        var letras = emMinusculo.ToCharArray();
                        letras[0] = char.ToUpper(letras[0]);
                        palavras.Enqueue(new string(letras));
                    }
                }
                return string.Join(" ", palavras);
            }
            catch(Exception e) { return nome;}
            
        }

        static public Stream StringToStream(string s, string encoder = null)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = encoder == null ?
                new StreamWriter(stream) :
                new StreamWriter(stream, Encoding.GetEncoding(encoder));
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string RemoveUnderlines(string nome)
        {
            return (nome.Replace("_", " ")).Trim();
        }

       
    }
}
