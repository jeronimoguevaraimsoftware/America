using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.CheckListViewModels
{
    public class CheckListReportVM
    {
        public List<RptClassCatalog> checkListCatalog { get; set; }
        public List<RptCheckListfpCatalog> checkListsfpCatalog { get; set; }
        public List<RptCheckListPipeDictumAnswers> checkListPipeDictiumAnswers { get; set; }
        public List<RptCheckListRecord> checkListRecord { get; set; }

        public string CommentIv { get; set; }


    }

    public class RptClassCatalog
    {
        public string Requeriment { get; set; }
        public string Verification { get; set; }
        public string Description { get; set; }
        public string UserNotify { get; set; }
        public string  Action { get; set; }
        public string Comment { get; set; }
    }

    public class RptCheckListfpCatalog
    {
        public string Requeriment { get; set; }
        public string  Verification { get; set; }
        public int Description { get; set; }
        public string Action { get; set; }
        public string Comment { get; set; }
    }

    public class RptCheckListPipeDictumAnswers
    {
        public string Verification  { get; set; }
        public string DictumUser { get; set; }
        public DateTime DictumDate { get; set; }
        public string DictumComment { get; set; }


    }

    public class RptCheckListRecord
    {
        public string Status { get; set; }

        public  DateTime Date { get; set; }
    }
}
