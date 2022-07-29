using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using System;
using System.Collections.Generic;

namespace LiberacionProductoWeb.Models.ProductionOrderViewModels
{
    public class ProductionOrderViewModel : SechToolDistributionBatchVM
    {
        public ProductionOrderViewModel()
        {
            ProductionEquipment = new ProductionEquipmentViewModel();
            MonitoringEquipmentList = new List<MonitoringEquipmentViewModel>();
            PipelineClearance = new PipelineClearanceViewModel();
            PipelineClearanceHistory = new List<PipelineClearanceViewModel>();
            ControlVariables = new List<ControlVariableViewModel>();
            CriticalParameters = new List<CriticalParameterViewModel>();
            CriticalQualityAttributes = new List<CriticalQualityAttributeViewModel>();
            BatchDetails = new BatchDetailsViewModel();
            HistoryNotes = new List<HistoryNotesViewModel>();
            this.HasHistorian = false;
            this.HasHistorianThree = false;
        }

        //helper controls
        public String SelectedPlantFilter { get; set; }
        public String SelectedProductFilter { get; set; }
        public String SelectedTankFilter { get; set; }
        public String SelectedPurityFilter { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

        // labels
        public String Location { get; set; }
        public String ProductName { get; set; }
        public String ProductCode { get; set; }
        public String Plant { get; set; }

        public String ShowPanelSteps { get; set; }

        // fields
        public int Id { get; set; }
        public ProductionEquipmentViewModel ProductionEquipment { get; set; }
        public List<MonitoringEquipmentViewModel> MonitoringEquipmentList { get; set; }
        public PipelineClearanceViewModel PipelineClearance { get; set; }
        public List<PipelineClearanceViewModel> PipelineClearanceHistory { get; set; }
        public List<ControlVariableViewModel> ControlVariables { get; set; }
        public List<CriticalParameterViewModel> CriticalParameters { get; set; }
        public List<CriticalQualityAttributeViewModel> CriticalQualityAttributes { get; set; }
        public BatchDetailsViewModel BatchDetails { get; set; }
        public List<HistoryNotesViewModel> HistoryNotes { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedNotes { get; set; }
        public string Status { get; set; }
        public int StepSaved { get; set; }
        public bool? InCompliance { get; set; }
        public DateTime? EndDate { get; set; }
        public string ReasonReject { get; set; }
        public bool HasHistorian { get; set; }
        public bool HasHistorianThree { get; set; }
    }

    public class ProductionEquipmentViewModel
    {
        public bool? IsAvailable { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
    }

    public class MonitoringEquipmentViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? IsCalibrated { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ProductionOrderId { get; set; }
    }

    public class PipelineClearanceViewModel
    {
        public PipelineClearanceViewModel()
        {
            this.HasPendingReview = true;
        }
        public int Id { get; set; }
        public bool? InCompliance { get; set; }
        public bool HasPendingReview { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ProductionOrderId { get; set; }
        public string Bill { get; set; }
        public string Activitie { get; set; }
        public string ReviewedBySecond { get; set; }
        public DateTime? ReviewedDateSecond { get; set; }
        public string NotesSecond { get; set; }
        public DateTime? ProductionStartedDate { get; set; }
        public string SelectedPlantFilter { get; set; }
        public string PlantId { get; set; }
        public string BatchDetailsNumber { get; set; }
        public DateTime? ProductionEndDate { get; set; }
    }

    public class ControlVariableViewModel
    {
        public ControlVariableViewModel()
        {
            DeviationReport = new DeviationReportViewModel();
            Type = this.Type;
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Variable { get; set; }
        public string Specification { get; set; }
        public string ChartPath { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string AvgValue { get; set; }
        public bool? InCompliance { get; set; }
        public DeviationReportViewModel DeviationReport { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ProductionOrderId { get; set; }
        public ProductionOrderAttributeType Type { get; set; }
        public string ReviewedByDate { get; set; }

        //MEG
        public String Historical { get; set; }

        //MEG 20211108
        public String VariableCode { get; set; }
        public String LowLimit { get; set; }
        public String TopLimit { get; set; }

        //MEG add file
        public String FileValue { get; set; }
    }

    public class CriticalParameterViewModel
    {
        public CriticalParameterViewModel()
        {
            DeviationReport = new DeviationReportViewModel();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Parameter { get; set; }
        public string Specification { get; set; }
        public string ChartPath { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string AvgValue { get; set; }
        public bool? InCompliance { get; set; }
        public DeviationReportViewModel DeviationReport { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ProductionOrderId { get; set; }
        public ProductionOrderAttributeType Type { get; set; }
        //MEG
        public String Historical { get; set; }

        //MEG 20211108
        public String VariableCode { get; set; }
        public String LowLimit { get; set; }
        public String TopLimit { get; set; }

        //MEG add file
        public String FileValue { get; set; }
        public string ReviewedByDate { get; set; }
    }

    public class CriticalQualityAttributeViewModel
    {
        public CriticalQualityAttributeViewModel()
        {
            DeviationReport = new DeviationReportViewModel();
        }

        public int Id { get; set; }
        public string Area { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Attribute { get; set; }
        public string Specification { get; set; }
        public string ChartPath { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string AvgValue { get; set; }
        public bool? InCompliance { get; set; }
        public DeviationReportViewModel DeviationReport { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ProductionOrderId { get; set; }
        public ProductionOrderAttributeType Type { get; set; }
        //MEG
        public String Historical { get; set; }
        //MEG 20211108
        public String VariableCode { get; set; }
        public String LowLimit { get; set; }
        public String TopLimit { get; set; }

        //MEG add file
        public String FileValue { get; set; }
        public string ReviewedByDate { get; set; }

    }

    public class DeviationReportViewModel
    {
        public string Folio { get; set; }
        public string Notes { get; set; }
    }

    public class BatchDetailsViewModel
    {
        public BatchDetailsViewModel()
        {
            Analysis = new List<BatchAnalysisViewModel>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Level { get; set; }
        public decimal Size { get; set; }
        public string Tank { get; set; }
        public List<BatchAnalysisViewModel> Analysis { get; set; }
        public string AnalyzedBy { get; set; }
        public DateTime? AnalyzedDate { get; set; }
        public bool? InCompliance { get; set; }
        public string NotInComplianceFolio { get; set; }
        public string NotInComplianceNotes { get; set; }
        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedNotes { get; set; }
        public int ProductionOrderId { get; set; }
        public string ReleasedByDate { get; set; }
        public string SizeString { get; set; } 
    }

    public class BatchAnalysisViewModel
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string LowerLimit { get; set; }
        public string UpperLimit { get; set; }
        public string Value { get; set; }
        public string MeasureUnit { get; set; }
    }

    public class ProductionOrderStatus
    {
        private ProductionOrderStatus(string value) { Value = value; }

        public string Value { get; private set; }

        public static ProductionOrderStatus InProgress { get { return new ProductionOrderStatus("OP-En proceso"); } }
        public static ProductionOrderStatus ToBeReleased { get { return new ProductionOrderStatus("OP-Por liberar"); } }
        public static ProductionOrderStatus Released { get { return new ProductionOrderStatus("OP-Liberada"); } }
        public static ProductionOrderStatus Denied { get { return new ProductionOrderStatus("OP-Rechazada"); } }
        public static ProductionOrderStatus InCancellation { get { return new ProductionOrderStatus("OP-En cancelación"); } }
        public static ProductionOrderStatus Cancelled { get { return new ProductionOrderStatus("OP-Cancelada"); } }
        public static ProductionOrderStatus Created { get { return new ProductionOrderStatus("OP-Creada"); } }
    }

    public class HistoryNotesViewModel
    {
        public String Note { get; set; }
        public String Source { get; set; }
    }
}