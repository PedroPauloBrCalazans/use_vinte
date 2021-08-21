using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Business.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public TipoSexo TipoSexo { get; set; }
        public bool Ativo { get; set; }




       
    }
}
