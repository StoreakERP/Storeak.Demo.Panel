using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Storeak.Models.Common;
using Storeak.Models.Identity.Queries.User;
using Storeak.Demo.Panel.Service;
using Storeak.Demo.Panel.Service.User;
using StoreakApiService.Core;
using StoreakApiService.Core.Helper;
using StoreakApiService.Core.Responses;

namespace Storeak.Demo.Panel.Controllers
{
    public class AccountController : Controller
    {
        private SessionHandler _sessionHandler;
        private ApiManager _apiManager;
        private IConfiguration _config;
        private ResourceMessages _resourceMessages;
        private IUserService _userService;
        private IMapper _mapper;
        private CustomModelValidator _modelValidator;
        public AccountController(IMapper mapper, SessionHandler sessionHandler, IUserService userService, ApiManager apiManager, IConfiguration config, ResourceMessages resourceMessages, CustomModelValidator modelValidator)
        {
            _sessionHandler = sessionHandler;
            _apiManager = apiManager;
            _config = config;
            _resourceMessages = resourceMessages;
            _userService = userService;
            _mapper = mapper;
            _modelValidator = modelValidator;
        }
        public IActionResult Login()
        {
            if (!string.IsNullOrEmpty(_sessionHandler.Token) && _sessionHandler.UserProfile != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel LoginModel)
        {
            LoginModel.clientId = _config["Client_Id"];
            LoginModel.clientSecret = _config["Client_Secret"];
            GetTokenModel token = null;
            HttpResponseMessage response = await _userService.Login(LoginModel);
            if (response.IsSuccessStatusCode)

            {
                token = response.Content.ReadAsAsync<GetTokenModel>().Result;
            }
            if (token != null && token.User != null)
            {
                UserProfileModel User = _mapper.Map<UserProfileModel>(token.User);
                User.Language = "en";
                User.Roles = token.roles;

                _sessionHandler.Token = token.access_token;
                _sessionHandler.UserProfile = User;
                _sessionHandler.Language = User.Language;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    TempData["Message"] = response.Content.ReadAsAsync<CustomResponse>().Result.Value;
                }
                catch (Exception)
                {
                    TempData["Message"] = response.Content.ReadAsStringAsync().Result;
                }
                return View(LoginModel);
            }
        }
       
        public IActionResult Signout()
        {
            _sessionHandler.Token = "";
            _sessionHandler.Language = "";
            return RedirectToAction("Login", "Account");
        }
    }
}