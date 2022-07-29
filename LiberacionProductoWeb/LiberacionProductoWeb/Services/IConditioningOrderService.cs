using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using Microsoft.AspNetCore.Http;

namespace LiberacionProductoWeb.Services
{
    public interface IConditioningOrderService
    {
        //Task<ConditioningOrderViewModel> GetAllAsync();
        //List<EquipamentProcessConditioningViewModel> GetTable4();
        Task<ConditioningOrderViewModel> GetByIdAsync(int Id, bool refresh = false, string user = null);
        Task<ConditioningOrderViewModel> GetByProductionOrderIdAsync(int Id);
        Task<ConditioningOrderViewModel> AddAsync(ConditioningOrder entity);
        Task<List<ConditioningOrderViewModel>> GetAllAsync();
        Task<List<EquipamentProcessConditioningViewModel>> GetTable4(string tournumber, int Id, DateTime? datePipelineClearance);
        Task<List<ScalesflowsViewModel>> GetTable2(ConditioningOrderViewModel model);
        Task<List<EquipamentProcessConditioningViewModel>> GetPipesXTournumber(int Id);
        Task<string> GetBillAsignature(ConditioningOrderViewModel model);
        Task DeleteTourNumber(string number, int id);
        Task<IEnumerable<StatesHistoryViewModel>> GetLogStatusOrderByIdAsync(int Id);
        Task<List<PipeFillingControlViewModel>> GetPipeFillingControlsXChecklist(ConditioningOrderViewModel model);
        Task<StoredProcedureResult> UpdateRelChecklistTourNumberPipe(ConditioningOrderViewModel model);
        Task<StoredProcedureResult> UpdateRelChecklistTourNumberPipe(int idOA, string tourNumber, string distributionBatch, int checkListId, string pipeNumber);
        Task<StoredProcedureResult> DeleteChecklist(int checkListId);
        Task<IEnumerable<PipeFillingViewModel>> GetAnalisisCliente(int ConditioningOrderId, string ProductId, string PlantId, DateTime? CreatedDate, List<string> Tournumbers);
        Task<ConditioningOrderViewModel> GetBasicInfoConditioningOrder(int Id);
        Task <PipeFillingCustomersFiles> SaveCustomersFiles(int ConditioningOrderId, string tourNumber, string distributionBatch, string user, IFormFileCollection files, string tank, string pipeNumber);
        Task<Boolean> ValidateIsReleaseTourNumberPipe(string tourNumber, int conditioningOrderId);
    }
}
