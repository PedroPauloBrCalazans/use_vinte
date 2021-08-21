using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

//bibliotecas apenas System nao estou indo para o ASP.NET somente na camada de negocio C#.NET
namespace DevIO.Business.Intefaces
{
    // uma interface que representa apenas o usuario. sera simples.
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
