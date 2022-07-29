using System;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Data.Repository.Base;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface IConditioningOrderRepository : IRepository<ConditioningOrder>
    {
    }
    public interface IPipelineClearanceOARepository : IRepository<PipelineClearanceOA>
    {
    }
    public interface IScalesFlowMetersRepository : IRepository<ScalesFlowMeters>
    {
    }
    public interface IAnalyticalEquipamentRepository : IRepository<AnalyticalEquipament>
    {
    }
    public interface IEquipmentProcessConditioningRepository : IRepository<EquipmentProcessConditioning>
    {
    }
    public interface IPerformanceProcessConditioningRepository : IRepository<PerformanceProcessConditioning>
    {
    }
    public interface IPipeFillingControlRepository : IRepository<PipeFillingControl>
    {
    }
    public interface IPipeFillingRepository : IRepository<PipeFilling>
    {
    }
    public interface IPipeFillingAnalysisRepository : IRepository<PipeFillingAnalysis>
    {
    }
    public interface IPipeFillingCustomerRepository : IRepository<PipeFillingCustomer>
    {
        Task<IEnumerable<string>> FindLastFolioByParametersAsync(string plant, string product, string tank);
        Task<PipeFillingCustomer> GetByParametersAsync(int ConditionOrderId, string tournumber, string distributionBatch, string tank);
    }
}
