using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.DataBaseModels.Base;
using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LiberacionProductoWeb.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {  
        }

        //add below list of all entities
        public DbSet<EntityExample> entityExamples { get; set; }
        public DbSet<GeneralCatalog> generalCatalogs { get; set; }
        public DbSet<FormulaCatalog> formulaCatalogs { get; set; }
        public DbSet<ProductCatalog> productCatalogs { get; set; }
        public DbSet<StabilityCatalog> stabiltyCatalogs { get; set; }
        public DbSet<ContainerCatalog> containerCatalogs { get; set; }
        public DbSet<DispositionCatalog> dispositionCatalogs { get; set; }
        public DbSet<States> StatesProd { get; set; }
        public DbSet<Activities> ActivitiesProd { get; set; }
        public DbSet<CheckListPipeAnswer> CheckListPipeAnswer { get; set; }
        public DbSet<CheckListPipeRecordAnswer> checkListPipeRecordAnswers { get; set; }
        public DbSet<CheckListPipeCommentsAnswer> checkListPipeCommentsAnswers { get; set; }
        public DbSet<CheckListPipeDictiumAnswer> CheckListPipeDictiumAnswer { get; set; }
        public DbSet<ReportAuditTrail> reportAuditTrail { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<ProductionEquipment> ProductionEquipments { get; set; }
        public DbSet<MonitoringEquipment> MonitoringEquipments { get; set; }
        public DbSet<PipelineClearance> PipelineClearances { get; set; }
        public DbSet<ProductionOrderAttribute> ProductionOrderAttributes { get; set; }
        public DbSet<BatchDetails> BatchDetails { get; set; }
        public DbSet<BatchAnalysis> BatchAnalysis { get; set; }
        public DbSet<ConditioningOrder> ConditioningOrders { get; set; }
        public DbSet<HistoryNotes> HistoryNotes { get; set; }
        public DbSet<HistoryStates> HistoryStates { get; set; }
        public DbSet<AnalyticalEquipament> AnalyticalEquipament { get; set; }
        public DbSet<ScalesFlowMeters> ScalesFlowMeters { get; set; }
        public DbSet<EquipmentProcessConditioning> EquipmentProcessConditioning { get; set; }
        public DbSet<PerformanceProcessConditioning> PerformanceProcessConditioning { get; set; }
        public DbSet<PipeFillingControl> PipeFillingControls { get; set; }
        public DbSet<PipeFilling> PipeFillings { get; set; }
        public DbSet<PipeFillingAnalysis> PipeFillingAnalyses { get; set; }
        public DbSet<PipeFillingCustomer> PipeFillingCustomers { get; set; }
        public DbSet<PipelineClearanceOA> PipelineClearancesOA { get; set; }
        public DbSet<ComplementoTanque> ComplementoTanques { get; set; }
        public DbSet<ComplementoPipa> ComplementoPipas { get; set; }
        public DbSet<StoredProcedureResult> StoredProcedureResults { get; set; }
        public DbSet<ActivitiesReportAudit> ActivitiesReportAudit { get; set; }
        public DbSet<ProductionOrderHistorian> ProductionOrderHistorian { get; set; }
        public DbSet<HistorianInfoTagsPlant> HistorianInfoTagsPlants { get; set; }
        public DbSet<HistorianReadingsPlant> HistorianReadingsPlants { get; set; }
        public DbSet<HistorianTagsPlant> HistorianTagsPlants { get; set; }
        public DbSet<PipeFillingCustomersFiles> PipeFillingCustomersFiles { get; set; }
        public DbSet<LeyendsCertificate> LeyendsCertificate { get; set; }
        public DbSet<LeyendsCertificateHistory> LeyendsCertificateHistory { get; set; }
        public DbSet<LeyendsFooterCertificate> LeyendsFooterCertificate { get; set; }
        public DbSet<LeyendsFooterCertificateHistory> LeyendsFooterCertificateHistory { get; set; }

        public DbSet<ApplicationRole> AspNetRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StoredProcedureResult>().HasNoKey();
            builder.Entity<ReportAuditTrail>()
           .Property(b => b.DistribuitionBatch)
           .HasDefaultValueSql("NA");
            builder.Entity<ReportAuditTrail>()
           .Property(b => b.ProductionOrderId)
           .HasDefaultValueSql("0");
            builder.Entity<LeyendsCertificateHistory>()
            .Property(b => b.CreatedDate)
            .HasDefaultValueSql("getdate()");
            builder.Entity<LeyendsCertificate>()
            .Property(b => b.ModifyDate)
            .HasDefaultValueSql("getdate()");
            builder.Entity<LeyendsFooterCertificate>()
           .Property(b => b.ModifyDate)
           .HasDefaultValueSql("getdate()");
            builder.Entity<LeyendsFooterCertificateHistory>()
            .Property(b => b.CreatedDate)
            .HasDefaultValueSql("getdate()");
        }
    }
}
