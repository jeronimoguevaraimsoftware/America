using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public partial class HistorianServer
    {
        public HistorianServer()
        {
            HistorianTagsPlants = new HashSet<HistorianTagsPlant>();
        }
        [Key]
        public int ServerRid { get; set; }
        public string ServerName { get; set; }

        public virtual ICollection<HistorianTagsPlant> HistorianTagsPlants { get; set; }
    }
}
