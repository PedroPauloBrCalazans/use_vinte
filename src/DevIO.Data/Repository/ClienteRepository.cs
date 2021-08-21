using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MeuDbContext context) : base(context) { }

        public async Task<Cliente> ObterCliente(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

    }
}
