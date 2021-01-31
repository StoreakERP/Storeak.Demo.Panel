using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreakApiService.Core.Helper;
using Storeak.Models.ApiWrapper;
using Storeak.Models.Setting.Enums;
using Storeak.Models.Inventory.Queries.Item;

namespace StoreakAdmin.Controllers
{
    public class SharedAjaxController : Controller
    {
        private ApiManager _apiManager;
        private SessionHandler _sessionHandler;
       

        public SharedAjaxController(ApiManager apiManager, SessionHandler sessionHandler)
        {
            _apiManager = apiManager;
            _sessionHandler = sessionHandler;
        }     
    }
}