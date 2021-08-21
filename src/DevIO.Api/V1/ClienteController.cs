using AutoMapper;
using DevIO.Api.Controllers;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clientes")]
    public class ClienteController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;


        public ClienteController(INotificador notificador,
                                 IClienteRepository clienteRepository,
                                 IClienteService clienteService,
                                 IMapper mapper) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _clienteService = clienteService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
        }


        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteViewModel));

            return CustomResponse(clienteViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(Guid id, ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteViewModel));

            return CustomResponse(clienteViewModel);
        }


        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var cliente = await ObterClientes(id);

            if (cliente == null) return NotFound();

            await _clienteService.Remover(id);

            return CustomResponse(cliente);
        }

        private async Task<ClienteViewModel> ObterClientes(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterCliente(id));
        }

    }
}
