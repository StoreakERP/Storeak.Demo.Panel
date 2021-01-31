using Microsoft.AspNetCore.Mvc;
using Storeak.Models.Accounting.VoucherTypes;
using Storeak.Models.Common;
using Storeak.Models.Store.BusinessUseCases.Type;
using Storeak.Models.Store.Queries.Type;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storeak.Demo.Panel.StoreType
{
   public interface IStoreTypeService
    {
        Task<List<GetAllTypeModel>> GetAllStoreTypes();
        Task<GetTypeModel> GetStoreTypeDetail(Guid Id);
        Task<JsonResult> CreateStoreType(CreateTypeModel CreateTypeModel);
        Task<JsonResult> UpdateStoreType(Guid Id,UpdateTypeModel UpdateTypeModel);
        Task<JsonResult> DeleteStoreType(Guid Id);
    }
}
