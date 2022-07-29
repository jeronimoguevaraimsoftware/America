using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.LayoutCertificateViewModels
{
    public class LeyendsCertificateVM: SechToolDistributionBatchVM
    {
        public int Id { get; set; }
        public string HeaderOne { get; set; }
        public string HeaderTwo { get; set; }
        public string LeyendOne { get; set; }
        public string LeyendTwo { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string User { get; set; }
        public string PlantsId { get; set; }
        public string Footer { get; set; }
        //helper controls
        public IEnumerable<SelectListItem> SelectedPlantsFilter { get; set; }
        public string FileName { get; set; }
        public List<LeyendsFooterCertificateVM> leyendsFooterCertificateVM { get; set; }
        public bool NewFile { get; set; }

    }

    public class LeyendsCertificateHistoryVM: SechToolDistributionBatchVM
    {
        public int Id { get; set; }
        public string HeaderOne { get; set; }
        public string HeaderTwo { get; set; }
        public string LeyendOne { get; set; }
        public string LeyendTwo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string User { get; set; }
    }

    public class LeyendsFooterCertificateVM: SechToolDistributionBatchVM
    {
        public int Id { get; set; }
        public string PlantId { get; set; }
        public string Footer { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string User { get; set; }
        public string PlantName { get; set; }
    }

    public class LeyendsFooterCertificateHistoryVM : SechToolDistributionBatchVM
    {
        public int Id { get; set; }
        public string PlantId { get; set; }
        public string Footer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string User { get; set; }

    }
}
