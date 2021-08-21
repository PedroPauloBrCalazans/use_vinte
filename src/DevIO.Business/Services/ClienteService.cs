using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository,
                              INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
        }


        public async Task Adicionar(Cliente cliente)
        {
            if(!ExecutarValidacao(new ClienteValidation(), cliente)) return;

            await _clienteRepository.Adicionar(cliente);
        }

        public async Task Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)) return;

            await _clienteRepository.Atualizar(cliente);
        }

        public async Task Remover(Guid id)
        {
            await _clienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}
