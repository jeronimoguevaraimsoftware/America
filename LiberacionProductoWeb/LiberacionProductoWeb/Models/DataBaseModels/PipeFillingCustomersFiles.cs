using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class PipeFillingCustomersFiles: Entity
    {
		public string ReviewedBy { get; set; }
		public DateTime ReviewedDate { get; set; }
		public string DistributionBatch { get; set; }
		public string TourNumber { get; set; }
		public string PipeNumber { get; set; }
		public int ConditioningOrderId { get; set; }
		public bool? State { get; set; }
		public string Tank { get; set; }
		public string FileName { get; set; }
		public string FileNameOrigin { get; set; }
		public ConditioningOrder ConditioningOrder { get; set; }

		public static PipeFillingCustomersFiles Create(string reviewedBy, string distributionBatch, 
			string tourNumber, string pipeNumber, int conditioningOrderId, string tank,
			string filename, string fileOrigin)
        {
			var entity = new PipeFillingCustomersFiles { 
				ReviewedBy = reviewedBy,
				ReviewedDate = DateTime.Now,
				DistributionBatch = distributionBatch,
				TourNumber = tourNumber,
				PipeNumber = pipeNumber,
				ConditioningOrderId = conditioningOrderId,
				Tank = tank,
				FileName = filename,
				FileNameOrigin = fileOrigin,
				State = true
			};
			return entity;


		}
		public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            throw new NotImplementedException();
        }
    }
}
