using AutoMapper;
using LiberacionProductoWeb.Models.CatalogsViewModels;
using LiberacionProductoWeb.Models.ConditioningOrderViewModels;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.LayoutCertificateViewModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using System;

namespace LiberacionProductoWeb.Helpers
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<RunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class RunDtoMapper : Profile
    {
        public RunDtoMapper()
        {
            CreateMap<GeneralCatalog, General>().ReverseMap();
            CreateMap<FormulaCatalog, Formula>().ReverseMap();
            CreateMap<ProductCatalog, Models.CatalogsViewModels.Product>().ReverseMap();
            CreateMap<StabilityCatalog, StabilityStudy>().ReverseMap();
            CreateMap<ContainerCatalog, Container>().ReverseMap();
            CreateMap<DispositionCatalog, Disposition>().ReverseMap();
            CreateMap<ProductionOrderAttribute, ControlVariableViewModel>().ReverseMap();
            CreateMap<BatchDetailsViewModel, BatchDetails>().ReverseMap();
            CreateMap<BatchAnalysisViewModel, BatchAnalysis>().ReverseMap();
            CreateMap<ConditioningOrderViewModel, ConditioningOrder>().ReverseMap();
            CreateMap<ScalesflowsViewModel, ScalesFlowMeters>().ReverseMap();
            CreateMap<AnalyticEquipmentViewModel, AnalyticalEquipament>().ReverseMap();
            CreateMap<EquipamentProcessConditioningViewModel, EquipmentProcessConditioning>().ReverseMap();
            CreateMap<PerformanceProcessConditioningViewModel, PerformanceProcessConditioning>().ReverseMap();
            CreateMap<StatesHistoryViewModel, HistoryStates>().ReverseMap();
            CreateMap<PipeFillingControlViewModel, PipeFillingControl>().ReverseMap();
            CreateMap<PipeFillingViewModel, PipeFilling>().ReverseMap();
            CreateMap<PipeFillingAnalysisViewModel, PipeFillingAnalysis>().ReverseMap();
            CreateMap<PipeFillingCustomerViewModel, PipeFillingCustomer>().ReverseMap();
            CreateMap<Models.RAPModels.RapComplemento, ComplementoTanque>().ReverseMap();
            CreateMap<Models.RAPModels.RapComplemento, ComplementoPipa>().ReverseMap();
            CreateMap<VwAnalisisClienteViewModel, VwAnalisisCliente>().ReverseMap();
            CreateMap<VwAnalisisClienteViewModel, VwLotesDistribuicionDetalle>().ReverseMap();
            CreateMap<PipeFillingCustomer, PipeFillingCustomerViewModel>().ReverseMap();
            CreateMap<MonitoringEquipment, MonitoringEquipmentViewModel>().ReverseMap();
            CreateMap<LeyendsCertificate, LeyendsCertificateVM>().ReverseMap();
            CreateMap<LeyendsCertificateHistory, LeyendsCertificateHistoryVM>().ReverseMap();
            CreateMap<LeyendsFooterCertificate, LeyendsFooterCertificateVM>().ReverseMap();
            CreateMap<LeyendsCertificate , LeyendsCertificateHistory>().ReverseMap();
            CreateMap<LeyendsFooterCertificateHistoryVM, LeyendsFooterCertificateHistory>().ReverseMap();
        }
    }
}
