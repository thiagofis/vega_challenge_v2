using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recruitment_challenge.Interfaces
{
    internal interface IApiService
    {
        Task<string> AuthenticateAsync(string login, string password);
        Task<string> GetUnitsAsync(string token);
    }
}
