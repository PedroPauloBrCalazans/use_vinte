using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterCliente(Guid id);
    }
}
