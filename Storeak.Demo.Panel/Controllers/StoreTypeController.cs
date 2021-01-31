using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Storeak.Models.Store.BusinessUseCases.Type;
using Storeak.Models.Store.Queries.Type;
using Storeak.Demo.Panel.Service;
using Storeak.Demo.Panel.StoreType;
using StoreakApiService.Core;
using StoreakApiService.Core.Helper;
namespace Storeak.Demo.Panel.Controllers
{
    [CustomAuthorize(Roles = "Admin,Administrator")]
    public class StoreTypeController : Controller
    {
        private SessionHandler _sessionHandler;
        private ApiManager _apiManager;
        private IStoreTypeService _storeTypeService;
        private ResourceMessages _resourceMessages;
        private CustomModelValidator _modelValidator;
        private IMapper _mapper;
        public StoreTypeController(IMapper mapper, SessionHandler sessionHandler, ApiManager apiManager, IStoreTypeService storeTypeService, ResourceMessages resourceMessages, CustomModelValidator modelValidator)
        {
            _sessionHandler = sessionHandler;
            _apiManager = apiManager;
            _resourceMessages = resourceMessages;
            _modelValidator = modelValidator;
            _storeTypeService = storeTypeService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> StoreTypeList()
        {
            List<GetAllTypeModel> storeTypes = await _storeTypeService.GetAllStoreTypes();
            return PartialView(storeTypes);
        }
        public IActionResult CreateStoreType()
        {
            return PartialView(new CreateTypeModel());
        }
        [HttpPost]
        public async Task<IActionResult> CreateStoreType(CreateTypeModel CreateTypeModel)
        {
            return await _storeTypeService.CreateStoreType(CreateTypeModel);
        }
        public async Task<IActionResult> UpdateStoreType(Guid Id)
        {
            TempData["Id"] = Id;
            GetTypeModel storeTypeDetail = await _storeTypeService.GetStoreTypeDetail(Id);
            UpdateTypeModel storeType = _mapper.Map<UpdateTypeModel>(storeTypeDetail);
            return PartialView(storeType);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStoreType(UpdateTypeModel UpdateTypeModel)
        {
            return await _storeTypeService.UpdateStoreType((Guid)TempData.Peek("Id"), UpdateTypeModel);
        }
        public IActionResult DeleteStoreType(Guid Id, string Name)
        {
            TempData["Id"] = Id;
            ViewBag.Name = Name;
            return PartialView();
        }
        [HttpPost]
        public async Task<JsonResult> DeleteStoreType()
        {
            return await _storeTypeService.DeleteStoreType((Guid)TempData.Peek("Id"));
        }
    }
}