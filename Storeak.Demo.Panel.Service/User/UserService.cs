using Storeak.Models.Common;
using StoreakApiService.Core.Helper;
using System.Net.Http;
using System.Threading.Tasks;

namespace Storeak.Demo.Panel.Service.User
{
    public class UserService : IUserService
    {
        private ApiManager _apiManager;
        private ResourceMessages _resourceMessages;
        public UserService(ApiManager apiManager, ResourceMessages resourceMessages)
        {
            _apiManager = apiManager;
            _resourceMessages = resourceMessages;
        }
        public async Task<HttpResponseMessage> Login(LoginModel LoginModel)
        {
            HttpResponseMessage response = await _apiManager.PostAPI(ApisUrl.Identity.Login, LoginModel, false);
            return response;
        }
    }
}
