using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Data.Repository
{
    public interface ILeyendsCertificateRepository: IRepository<LeyendsCertificate>
    {
    }
    public interface ILeyendsCertificateHistoryRepository : IRepository<LeyendsCertificateHistory>
    {
    }
    public interface ILeyendsFooterCertificateRepository : IRepository<LeyendsFooterCertificate>
    {
    }
    public interface ILeyendsFooterCertificateHistoryRepository: IRepository<LeyendsFooterCertificateHistory>
    {
    }
}
