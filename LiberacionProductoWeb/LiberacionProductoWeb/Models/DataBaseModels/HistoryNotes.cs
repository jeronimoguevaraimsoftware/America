using LiberacionProductoWeb.Models.DataBaseModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public class HistoryNotes : Entity
    {
        public int ProductionOrderId { get; set; }
        public string Source { get; set; }
        public string Note { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

        public override IEnumerable<ReportAuditTrail> AuditTrailComparison(Entity objectToCompare, Entity objectToCompareOld = null, string DistribuitionBatch = null)
        {
            return new List<ReportAuditTrail>();
        }
    }

    public class HistoryNotesType
    {
        private HistoryNotesType(string value) { Value = value; }

        public string Value { get; private set; }

        public static HistoryNotesType OrdenProduccion { get { return new HistoryNotesType("OP"); } }
        public static HistoryNotesType OrdenAcondicionamiento { get { return new HistoryNotesType("OA"); } }
    }
}


