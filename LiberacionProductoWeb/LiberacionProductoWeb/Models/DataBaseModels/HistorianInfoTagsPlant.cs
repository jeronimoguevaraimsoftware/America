#nullable disable

using System.ComponentModel.DataAnnotations;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public partial class HistorianInfoTagsPlant
    {
        [Key]
        public int TagInfoRid { get; set; }
        public string TagName { get; set; }
        public string Uomcode { get; set; }
        public int? Uomsid { get; set; }
        public float? LowerBound { get; set; }
        public float? UpperBound { get; set; }
        public string Stage { get; set; }
        public string Variable { get; set; }
        public string Plant { get; set; }
        public string PlantId { get; set; }
        public string Product { get; set; }
        public string Type { get; set; }
    }
}
