using DevIO.Business.Models;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Intefaces
{
    public interface IClienteService : IDisposable
    {
        Task Adicionar(Cliente cliente);
        Task Atualizar(Cliente cliente);
        Task Remover(Guid id);
    }
}
