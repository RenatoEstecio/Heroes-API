using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeroesApi
{
    public static class RegexUtils
    {
        public static string FirstMatch(string expressao, string grupo, string texto)
        {
            string resp = string.Empty;
            try
            {
                Match m = new Regex(expressao).Match(texto);
                resp = m.Groups[grupo].Value;
            }
            catch (Exception ex) { resp = ex.Message; }
            return resp;
        }
    }
}
