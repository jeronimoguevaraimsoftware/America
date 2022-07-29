using System;
using System.Collections.Generic;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LiberacionProductoWeb.Models.CatalogsViewModels
{
    public class DispositionViewModel: SechToolDistributionBatchVM
    {
        public Disposition Disposition { get; set; }
        public List<Disposition> DispositionList { get; set; }

        //messages views
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }
    }

    public class Disposition
    {
        public String Id { get; set; }
        public String DispositionType { get; set; }
        public String Status { get; set; }
    }

    public class DtoDisposition
    {
        public String id { get; set; }
        public String type { get; set; }
        public String status { get; set; }
    
    }
}
