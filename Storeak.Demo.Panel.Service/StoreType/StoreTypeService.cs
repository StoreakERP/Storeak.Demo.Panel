using Microsoft.AspNetCore.Mvc;
using Storeak.Models.Store.BusinessUseCases.Type;
using Storeak.Models.Store.Queries.Type;
using Storeak.Demo.Panel.Service;
using StoreakApiService.Core.Helper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Storeak.Demo.Panel.StoreType
{
    public class StoreTypeService : IStoreTypeService
    {
        private ApiManager _apiManager;
        private ResourceMessages _resourceMessages;
        public StoreTypeService(ApiManager apiManager, ResourceMessages resourceMessages)
        {
            _apiManager = apiManager;
            _resourceMessages = resourceMessages;
        }
        public async Task<List<GetAllTypeModel>> GetAllStoreTypes()
        {
            List<GetAllTypeModel> storeTypes = new List<GetAllTypeModel>();
            HttpResponseMessage response = await _apiManager.GetAllAPI(ApisUrl.Store.GetAllStoreTypes);
            if (response.IsSuccessStatusCode)
            {
                storeTypes = response.Content.ReadAsAsync<List<GetAllTypeModel>>().Result;
            }
            return storeTypes;
        }
        public async Task<GetTypeModel> GetStoreTypeDetail(Guid Id)
        {
            GetTypeModel storeType = new GetTypeModel();
            HttpResponseMessage response = await _apiManager.GetByIdAPI(ApisUrl.Store.GetStoreTypeById, Id);
            if (response.IsSuccessStatusCode)
            {
                storeType = response.Content.ReadAsAsync<GetTypeModel>().Result;
            }
            return storeType;
        }
        public async Task<JsonResult> CreateStoreType(CreateTypeModel CreateTypeModel)
        {
            HttpResponseMessage response = await _apiManager.PostAPI(ApisUrl.Store.CreateStoreType, CreateTypeModel);
            if (response.IsSuccessStatusCode)
            {
                return response.CreateJsonSuccessResponse(_resourceMessages.StoreTypeCreatedSuccessfully);
            }
            else
            {
                return response.CreateJsonErrorResponse();
            }
        }
        public async Task<JsonResult> UpdateStoreType(Guid Id,UpdateTypeModel UpdateTypeModel)
        {
            HttpResponseMessage response = await _apiManager.PutAPI(ApisUrl.Store.UpdateStoreType, UpdateTypeModel, Id);
            return response.CreateJsonResponse();
        }
        public async Task<JsonResult> DeleteStoreType(Guid Id)
        {
            HttpResponseMessage response = await _apiManager.DeleteAPI(ApisUrl.Store.DeleteStoreType, Id);
            return response.CreateJsonResponse();
        }
    }
}
