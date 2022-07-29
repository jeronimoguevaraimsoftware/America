using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Models.SechToolViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Models.CheckListViewModels
{
    public class CheckListVM: SechToolDistributionBatchVM
    {
        public int Id { get; set; }
        public string Localizate { get; set; }
        public string Pipe { get; set; }
        public string Product { get; set; }
        public int NumberOrder { get; set; }
        public String Requirement { get; set; }
        public String Verification { get; set; }
        public String VerificationTwo { get; set; }
        public String Description { get; set; }
        public String Notify { get; set; }
        public String Action { get; set; }
        public String Group { get; set; }
        public String CommentIv { get; set; }
        public String CommentFP { get; set; }
        public DateTime LastDateRecord { get; set; }
        public String LastStatusRecord { get; set; }
        public String ReasonReject { get; set; }
        public string TourNumber { get; set; }
        public string DistributionBatch { get; set; }
        public String FlagApproveSC { get; set; }
        public int checkListId { get; set; }
        public string Alias { get; set; }

        //mensajes en vista
        public String MensajeInfo { get; set; }
        public String MensajeError { get; set; }

        //helpers
        public List<CheckListVM> checkListsCatalog { get; set; }
        public List<CheckListVM> checkListsfpCatalog { get; set; }
        public List<CheckListPipeRecordAnswer> checkListsRecord { get; set; }
        public List<CheckListPipeCommentsAnswer> checkListPipeCommentsAnswers { get; set; }
        public List<CheckListPipeDictiumAnswer> checkListPipeDictiumAnswers { get; set; }
        public string Check1 { get; set; }
        public string Check2 { get; set; }
        public string Check3 { get; set; }
        public string Check4 { get; set; }
        public string Check5 { get; set; }
        public string Check6 { get; set; }
        public string Check7 { get; set; }
        public string Check8 { get; set; }
        public string Check9 { get; set; }

        public string Check10 { get; set; }
        public string Check11 { get; set; }
        public string Check12 { get; set; }

        public string Check13 { get; set; }
        public string Check14 { get; set; }
        public string Check15 { get; set; }
        public String Description1 { get; set; }
        public String Description2 { get; set; }
        public String Description3 { get; set; }
        public String Description4 { get; set; }
        public String Description5 { get; set; }
        public IEnumerable<SelectListItem> ListUserNotify { get; set; }
        public List<String> SelectedNotifyUsr1Filter { get; set; }
        public List<String> SelectedNotifyUsr2Filter { get; set; }
        public List<String> SelectedNotifyUsr3Filter { get; set; }
        public List<String> SelectedNotifyUsr4Filter { get; set; }
        public List<String> SelectedNotifyUsr5Filter { get; set; }
        public String Action1 { get; set; }
        public String Action2 { get; set; }
        public String Action3 { get; set; }
        public String Action4 { get; set; }
        public String Action5 { get; set; }


        public string Checkfp1 { get; set; }
        public string Checkfp2 { get; set; }
        public string Checkfp3 { get; set; }

        public string Checkfp4 { get; set; }
        public string Checkfp5 { get; set; }
        public string Checkfp6 { get; set; }

        public string Checkfp7 { get; set; }
        public string Checkfp8 { get; set; }
        public string Checkfp9 { get; set; }

        public string Checkfp10 { get; set; }
        public string Checkfp11 { get; set; }
        public string Checkfp12 { get; set; }

        public string Checkfp13 { get; set; }
        public string Checkfp14 { get; set; }
        public string Checkfp15 { get; set; }

        public string Checkfp16 { get; set; }
        public string Checkfp17 { get; set; }
        public string Checkfp18 { get; set; }

        public String Descriptionfp1 { get; set; }
        public String Descriptionfp2 { get; set; }
        public String Descriptionfp3 { get; set; }
        public String Descriptionfp4 { get; set; }
        public String Descriptionfp5 { get; set; }
        public String Descriptionfp6 { get; set; }

        public List<String> SelectedNotifyUsr1fpFilter { get; set; }
        public List<String> SelectedNotifyUsr2fpFilter { get; set; }
        public List<String> SelectedNotifyUsr3fpFilter { get; set; }
        public List<String> SelectedNotifyUsr4fpFilter { get; set; }
        public List<String> SelectedNotifyUsr5fpFilter { get; set; }
        public List<String> SelectedNotifyUsr6fpFilter { get; set; }
        public String Actionfp1 { get; set; }
        public String Actionfp2 { get; set; }
        public String Actionfp3 { get; set; }
        public String Actionfp4 { get; set; }
        public String Actionfp5 { get; set; }
        public String Actionfp6 { get; set; }

        public String DictumComment { get; set; }

        public String DictumUser { get; set; }

        public DateTime? DictiumDate { get; set; }

        public string CheckDictium1 { get; set; }
        public string CheckDictium2 { get; set; }
        public string Style { get; set; }

    }
    public class CheckListType
    {
        private CheckListType(string value) { Value = value; }
        public string Value { get; private set; }
        public static CheckListType Inprogress { get { return new CheckListType("CL-En proceso"); } }
        public static CheckListType InCancellation { get { return new CheckListType("CL-En cancelación"); } }
        public static CheckListType Cancelled { get { return new CheckListType("CL-Cancelado"); } }
        public static CheckListType CloseOk { get { return new CheckListType("CL-Cerrado cumple"); } }
        public static CheckListType CloseNo { get { return new CheckListType("CL-Cerrado no cumple"); } }
        public static CheckListType IsRelease { get { return new CheckListType("CL-Liberado"); } }
    }
    public class CheckListPipeDictiumAnswerViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public String Verification { get; set; }
        public DateTime Date { get; set; }
        public int NumOA { get; set; }
        public String Comment { get; set; }
        public string DistributionBatch { get; set; }
        public string TourNumber { get; set; }
        public bool? InCompliance { get; set; }
        public bool Source { get; set; }
        public string Status { get; set; }
        public string File { get; set; }
        public int CheckListId { get; set; }
        public string StatusTwo { get; set; }
        public string PipeNumber { get; set; }
    }

}
