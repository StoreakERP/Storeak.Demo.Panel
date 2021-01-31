using Storeak.Models.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace Storeak.Demo.Panel.Service.User
{
    public interface IUserService
    {
        Task<HttpResponseMessage> Login(LoginModel LoginModel);
    }
}
