using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;


namespace LiberacionProductoWeb.Data.Repository
{
    public class CheckListGeneralRepository : Repository<CheckListGeneral>, ICheckListGeneralRepository
    {
        private readonly AppDbContext _appDbContext;
        public CheckListGeneralRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class CheckListPipeAnswerRepository : Repository<CheckListPipeAnswer>, ICheckListPipeAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CheckListPipeAnswerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class CheckListPipeRecordAnswerRepository : Repository<CheckListPipeRecordAnswer>, ICheckListPipeRecordAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CheckListPipeRecordAnswerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class CheckListPipeCommentsAnswerReepository : Repository<CheckListPipeCommentsAnswer>, ICheckListPipeCommentsAnswerReepository
    {
        private readonly AppDbContext _appDbContext;
        public CheckListPipeCommentsAnswerReepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class CheckListPipeDictiumAnswerRepository : Repository<CheckListPipeDictiumAnswer>, ICheckListPipeDictiumAnswerRepository
    {
        private readonly AppDbContext _appDbContext;
        public CheckListPipeDictiumAnswerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
