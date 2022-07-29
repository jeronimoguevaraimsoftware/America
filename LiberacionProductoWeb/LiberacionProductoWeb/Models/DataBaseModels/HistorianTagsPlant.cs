using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public partial class HistorianTagsPlant
    {
        public HistorianTagsPlant()
        {
            HistorianReadingsPlants = new HashSet<HistorianReadingsPlant>();
        }
        [Key]
        public int TagRid { get; set; }
        public string TagName { get; set; }
        public string TagPhysical { get; set; }
        public int ServerRid { get; set; }
        public string IsActive { get; set; }

        public virtual HistorianServer ServerR { get; set; }
        public virtual ICollection<HistorianReadingsPlant> HistorianReadingsPlants { get; set; }
    }
}
