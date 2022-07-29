using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{

    public class LeyendsCertificateRepository : Repository<LeyendsCertificate>, ILeyendsCertificateRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsCertificateRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class LeyendsCertificateHistoryRepository : Repository<LeyendsCertificateHistory>, ILeyendsCertificateHistoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsCertificateHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class LeyendsFooterCertificateRepository : Repository<LeyendsFooterCertificate>, ILeyendsFooterCertificateRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsFooterCertificateRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }

    public class LeyendsFooterCertificateHistoryRepository : Repository<LeyendsFooterCertificateHistory>, ILeyendsFooterCertificateHistoryRepository
    {
        private readonly AppDbContext _appDbContext;
        public LeyendsFooterCertificateHistoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }
    }
}
