using System;
using System.Collections.Generic;
using System.Text;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.ConditioningOrderViewModels
{
    public class ConditioningOrderViewModel : SechToolDistributionBatchVM
    {
        public ConditioningOrderViewModel()
        {
            AnalyticEquipmentList = new List<AnalyticEquipmentViewModel>();
            PipeFillingControl = new List<PipeFillingControlViewModel>();
            PipelineClearanceHistory = new List<PipeClearanceOAViewModel>();
            ScalesflowsList = new List<ScalesflowsViewModel>();
            PipelineClearanceOA = new PipeClearanceOAViewModel();
            EquipamentProcessesList = new List<EquipamentProcessConditioningViewModel>();
            PipeClearancesList = new List<PipeClearanceOAViewModel>();
            PerformanceList = new List<PerformanceProcessConditioningViewModel>();
            ObservationsList = new List<ObservationHistoryViewModel>();
            StatesList = new List<StatesHistoryViewModel>();
            CheckListGeneralList = new List<CheckListGeneralViewModel>();
            checkListFile = new CheckListFileViewModel();
            IsCancelled = false;
            PipeFillingCustomersFiles = new PipeFillingCustomersFilesViewModel();
        }

        // fields
        public int ProductionOrderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string State { get; set; }
        public int StepSaved { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public string Plant { get; set; }
        public string Product { get; set; }
        public string Tank { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string LotProd { get; set; }
        public string Presentation { get; set; }
        public string ContainerPrimary { get; set; }
        public string NumberLot { get; set; }

        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public string ReleasedNotes { get; set; }
        public List<AnalyticEquipmentViewModel> AnalyticEquipmentList { get; set; }
        public List<ScalesflowsViewModel> ScalesflowsList { get; set; }
        public PipeClearanceOAViewModel PipelineClearanceOA { get; set; }
        public List<PipeClearanceOAViewModel> PipelineClearanceHistory { get; set; }
        public List<EquipamentProcessConditioningViewModel> EquipamentProcessesList { get; set; }
        public string EquipamentProcessesError { get; set; }
        public List<PipeClearanceOAViewModel> PipeClearancesList { get; set; }
        public List<PipeFillingControlViewModel> PipeFillingControl { get; set; }
        public List<PerformanceProcessConditioningViewModel> PerformanceList { get; set; }
        public List<ObservationHistoryViewModel> ObservationsList { get; set; }
        public List<StatesHistoryViewModel> StatesList { get; set; }
        public List<CheckListGeneralViewModel> CheckListGeneralList { get; set; }
        public String ShowPanelSteps { get; set; }
        public string ReasonReject { get; set; }
        public CheckListFileViewModel checkListFile { get; set; }
        // labels
        public String Location { get; set; }
        public String ProductName { get; set; }
        public String ProductCode { get; set; }
        public string DeliveryNumber { get; set; }
        public string TourNumber { get; set; }
        public string Name { get; set; }

        // catalogs
        public List<SelectListItem> BayAreaFilter { get; set; }
        public List<BayAreaItem> BayAreaList { get; set; }
        public List<SelectListItem> ScalesFlowMeters { get; set; }
        public List<String> SelectedScalesFlowMeters { get; set; }
        public string PlantId { get; set; }
        public string ProductId { get; set; }
        public List<CheckListRelationShip> CheckListRelationShip { get; set; }
        public List<String> SelectedPipeFillingControl { get; set; }

        public List<SelectListItem> ListPipeFillingControl { get; set; }
        public List<String> SelectedPipeFilling { get; set; }
        public List<SelectListItem> ListPipeFilling { get; set; }

        public CheckListRelationShip CheckListRelation { get; set; }
        public bool IsCancelled { get; set; }
        public PipeFillingCustomersFilesViewModel PipeFillingCustomersFiles { get; set; }
        public int? CertificateId { get; set; }
        public int? FooterCertificateId { get; set; }

    }

    public class BayAreaItem
    {
        public int Index { get; set; }
        public string BayArea { get; set; }
        public string FillingPump { get; set; }
        public string FillingHose { get; set; }
    }

    public class AnalyticEquipmentViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? IsCalibrated { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ConditioningOrderId { get; set; }
    }

    public class ScalesflowsViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public bool? IsCalibrated { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ConditioningOrderId { get; set; }
    }

    public class EquipamentProcessConditioningViewModel
    {
        public int Id { get; set; }
        public string TourNumber { get; set; }
        public string PipeNumber { get; set; }
        public DateTime? DistributionBatchDate { get; set; }
        public string Bay { get; set; }
        public string Bomb { get; set; }
        public string Hosefill { get; set; }
        public string HoseDownload { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string Notes { get; set; }
        public int ConditioningOrderId { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PipeClearanceOAViewModel
    {
        public int Id { get; set; }
        public bool? InCompliance { get; set; }
        public bool HasPendingReview { get; set; }
        public string Notes { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public int ConditioningOrderId { get; set; }
        public string Bill { get; set; }
        public string Activitie { get; set; }
        public string ReviewedBySecond { get; set; }
        public DateTime? ReviewedDateSecond { get; set; }
        public string NotesSecond { get; set; }
        public DateTime? ProductionStartedDate { get; set; }
        public string SelectedPlantFilter { get; set; }
        public int ProductionOrderId { get; set; }
        public string PlantId { get; set; }
        public string LotProd { get; set; }
    }

    public class PerformanceProcessConditioningViewModel
    {
        public int Id { get; set; }
        public string SizeLote { get; set; }
        public string TotalTons { get; set; }
        public string DifTons { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string Notes { get; set; }
        public int ConditioningOrderId { get; set; }
        public string TourNumber { get; set; }
        public string PipeNumber { get; set; }
    }

    public class ObservationHistoryViewModel
    {
        public string Section { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string NumberOA { get; set; }
    }

    public class StatesHistoryViewModel
    {
        public int ProductionOrderId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
    }

    public class ControlFillPipeViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int PipeNumber { get; set; }
        public int WeightInit { get; set; }
        public int WeightEnd { get; set; }
        public string TotalTons { get; set; }
        public string ReportPNCFolio { get; set; }
        public string ReportPNCNotes { get; set; }
        public string Dictium { get; set; }
        public string ReleaseBy { get; set; }
        public string TankId { get; set; }
        public string NameClient { get; set; }
        public string DeliveryNumber { get; set; }
        public string ReviewedBy { get; set; }
        public string AnalysisReport { get; set; }
        public string Notes { get; set; }
        public int ConditioningOrderId { get; set; }

    }

    public class ConditioningOrderStatus
    {
        private ConditioningOrderStatus(string value) { Value = value; }

        public string Value { get; private set; }

        public static ConditioningOrderStatus InProgress { get { return new ConditioningOrderStatus("OA-En proceso"); } }
        public static ConditioningOrderStatus ToBeReleased { get { return new ConditioningOrderStatus("OA-Por liberar"); } }
        public static ConditioningOrderStatus Released { get { return new ConditioningOrderStatus("OA-Liberada"); } }
        public static ConditioningOrderStatus Denied { get { return new ConditioningOrderStatus("OA-Rechazada"); } }
        public static ConditioningOrderStatus InCancellation { get { return new ConditioningOrderStatus("OA-En cancelación"); } }
        public static ConditioningOrderStatus Cancelled { get { return new ConditioningOrderStatus("OA-Cancelada"); } }
        public static ConditioningOrderStatus WithoutPipes { get { return new ConditioningOrderStatus("OA-En proceso sin pipas"); } }
        public static ConditioningOrderStatus Created { get { return new ConditioningOrderStatus("OA-Creada"); } }
    }

    public class PipeFillingControlViewModel
    {
        public int Id { get; set; }
        public string TourNumber { get; set; }
        public List<PipeFillingViewModel> PipesList { get; set; }
        public int ConditioningOrderId { get; set; }
        public ConditioningOrder ConditioningOrder { get; set; }
    }

    public class PipeFillingViewModel
    {
        public int Id { get; set; }
        public string PipeNumber { get; set; }
        public int CheckListId { get; set; }
        public int ConditioningOrderId { get; set; }
        public string TourNumber { get; set; }
        public string CheckListStatus { get; set; }
        public bool? CheckListIncompliance { get; set; }
        public bool? CheckListSource { get; set; }

        public DateTime? Date { get; set; }
        public string InitialWeight { get; set; }
        public string FinalWeight { get; set; }
        public decimal DiffWeight { get; set; }
        public List<PipeFillingAnalysisViewModel> InitialAnalysis { get; set; }
        public List<PipeFillingAnalysisViewModel> FinalAnalysis { get; set; }
        public string AnalyzedBy { get; set; }
        public DateTime? AnalyzedDate { get; set; }
        public DateTime? InitialAnalyzedDate { get; set; }
        public DateTime? FinalAnalyzedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string DistributionBatch { get; set; }
        public bool? InCompliance { get; set; }
        public string ReportPNCFolio { get; set; }
        public string ReportPNCNotes { get; set; }
        public bool? IsReleased { get; set; }
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public int PipeFillingControlId { get; set; }
        public List<PipeFillingCustomerViewModel> Customers { get; set; }
        public List<PipeFillingCustomersFilesViewModel> CustomersFiles { get; set; }
    }

    public class PipeFillingAnalysisViewModel
    {
        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string ValueExpected { get; set; }
        public string ValueReal { get; set; }
        public string MeasureUnit { get; set; }
        public string Type { get; set; }
        public string DistributionBatch { get; set; }
        public string PipeNumber { get; set; }
        public int ConditioningOrderId { get; set; }
        public string Unique { get; set; }
        public string PathFile { get; set; }    //AHF
        public string File { get; set; }    //AHF
        public string InputId { get; set; } //AHF
    }


    public class PipeFillingCustomerViewModel
    {
        public int Id { get; set; }
        public string Tank { get; set; }
        public string Name { get; set; }
        public string DeliveryNumber { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string AnalysisReport { get; set; }
        public string EmailsList { get; set; }
        public string Notes { get; set; }
        public bool? InCompliance { get; set; }
        public string DistributionBatch { get; set; }
        public string TourNumber { get; set; }
        public int ConditioningOrderId { get; set; }
        public string PlantIdentificador { get; set; }
        public string ProductId { get; set; }
        public string TankId { get; set; }
        public string Folio { get; set; }
        public int PipeFillingControlId { get; set; }
        public bool? EmailsListSend { get; set; }
        public bool? state { get; set; }
    }

    public class CheckListGeneralViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ConditioningOrderId { get; set; }
        public string DistributionBatch { get; set; }
        public string TourNumber { get; set; }
        public bool? InCompliance { get; set; }
        public string Verification { get; set; }
        public string Notes { get; set; }
        public bool Source { get; set; }
        public string Alias { get; set; }
    }

    public class CheckListGeneralViewModelStatus
    {
        private CheckListGeneralViewModelStatus(bool value) { Value = value; }

        public bool Value { get; private set; }

        public static CheckListGeneralViewModelStatus Web { get { return new CheckListGeneralViewModelStatus(true); } }
        public static CheckListGeneralViewModelStatus Manual { get { return new CheckListGeneralViewModelStatus(false); } }
    }

    public class CheckListFileViewModel
    {
        public int ConditioningOrderId { get; set; }
        public string TourNumber { get; set; }
        public string DistributionBatch { get; set; }
        public bool? CheckListIncompliance { get; set; }
    }

    public class FilesDto
    {
        public string FileName { get; set; }
        public string InputId { get; set; }
        public string Type { get; set; }
    }

    public class CheckListRelationShip
    {
        public int Id { get; set; }
        public int ConditioningOrderId { get; set; }
        public string TourNumber { get; set; }
        public string DistributionBatch { get; set; }
        public string Source { get; set; }
        public string PipeNumber { get; set; }
        public bool? Complete { get; set; }
        public bool? RelationShip { get; set; }
        public string File { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }
    }

    public class CheckListGeneralViewModelCheckListStep
    {
        private CheckListGeneralViewModelCheckListStep(int value) { Value = value; }

        public int Value { get; private set; }

        public static CheckListGeneralViewModelCheckListStep One { get { return new CheckListGeneralViewModelCheckListStep(1); } }
        public static CheckListGeneralViewModelCheckListStep Two { get { return new CheckListGeneralViewModelCheckListStep(2); } }
    }
    public class SendNotificationViewModel
    {
        public int Id { get; set; }
        public int ProductionOrderId { get; set; }
        public string Emails { get; set; }
        public string TourNumber { get; set; }
        public string LotProd { get; set; }
        public string Tank { get; set; }
        public string Product { get; set; }
        public string DistributionBatch { get; set; }
        public string PlantId { get; set; }
        public string ProductId { get; set; }
        public string Bill { get; set; }
    }

    public class PipeCustomerToUpdate
    {
        public int ConditioningOrderId { get; set; }
        public string TourNumber { get; set; }
        public string DistributionBatch { get; set; }
        public StringBuilder Values { get; set; }
    }

    public class PipeFillingCustomersFilesViewModel
    {
        public int Id { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime ReviewedDate { get; set; }
        public string DistributionBatch { get; set; }
        public string TourNumber { get; set; }
        public string PipeNumber{ get; set; }
        public int ConditioningOrderId { get; set; }
        public bool? state { get; set; }
        public string Tank { get; set; }
        public string FileName { get; set; }
        public string FileNameOrigin { get; set; }
    }
}