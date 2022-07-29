using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface IProductionOrderRepository : IRepository<ProductionOrder>
    {
    }

    public interface IProductionEquipmentRepository : IRepository<ProductionEquipment>
    {
    }

    public interface IMonitoringEquipmentRepository : IRepository<MonitoringEquipment>
    {
    }

    public interface IPipelineClearanceRepository : IRepository<PipelineClearance>
    {
    }

    public interface IProductionOrderAttributeRepository : IRepository<ProductionOrderAttribute>
    {
    }

    public interface IBatchDetailsRepository : IRepository<BatchDetails>
    {
    }

    public interface IBatchAnalysisRepository : IRepository<BatchAnalysis>
    {
    }
    public interface IHistoryNotesRepository: IRepository<HistoryNotes>
    { 
    }
    public interface IHistoryStatesRepository: IRepository<HistoryStates>
    {
    }    
}
