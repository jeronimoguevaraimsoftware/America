using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LiberacionProductoWeb.Models.DataBaseModels
{
    public partial class HistorianReadingsPlant
    {
        [Key]
        public int ReadingRid { get; set; }
        public int TagRid { get; set; }
        public float? ReadingValue { get; set; }
        public DateTime ReadingDateTime { get; set; }
        public string ReadingQuality { get; set; }

        public virtual HistorianTagsPlant TagR { get; set; }
    }
}
