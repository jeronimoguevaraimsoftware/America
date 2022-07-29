using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface ICheckListGeneralRepository : IRepository<CheckListGeneral>
    {
    }
    public interface ICheckListPipeAnswerRepository : IRepository<CheckListPipeAnswer>
    {
    }
    public interface ICheckListPipeRecordAnswerRepository : IRepository<CheckListPipeRecordAnswer>
    {
    }
    public interface ICheckListPipeDictiumAnswerRepository : IRepository<CheckListPipeDictiumAnswer>
    {
    }
    public interface ICheckListPipeCommentsAnswerReepository : IRepository<CheckListPipeCommentsAnswer>
    {
    }
}
