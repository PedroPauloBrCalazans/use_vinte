using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Data
{
    public class ApplicationnDbContext : IdentityDbContext
    {
        public ApplicationnDbContext(DbContextOptions<ApplicationnDbContext> options) : base(options) { }
    }
}
