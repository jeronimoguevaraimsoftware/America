using LiberacionProductoWeb.Models.DataBaseModels.Base;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class ProductionOrderHistorian : Entity
    {
        public int ProductionOrderId { get; set; }
        public string JsonFile { get; set; }
        [NotMapped]
        public IEnumerable<ProductionOrderHistorianDto> productionOrderHistorianDtos
        {
            get
            {
                return JsonConvert.DeserializeObject<IEnumerable<ProductionOrderHistorianDto>>(this.JsonFile);
            }
            set
            {
                this.JsonFile = JsonConvert.SerializeObject(value);
            }
        }
        public ProductionOrder ProductionOrder { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            throw new NotImplementedException();
        }
    }
}
