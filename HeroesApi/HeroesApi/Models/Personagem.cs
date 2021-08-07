using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace HeroesApi
{
    [ProtoContract, Serializable, DisplayName("Personagem")]
    public class Personagem
    {
        [ProtoMember(1), DisplayName("Nome")]
        public string Nome { get; set; }
        [ProtoMember(2), DisplayName("RealNome")]
        public string RealNome { get; set; }

        [ProtoMember(3), DisplayName("Mensagem")]
        public string Mensagem { get; set; }
    }
}
