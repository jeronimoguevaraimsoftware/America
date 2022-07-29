using LiberacionProductoWeb.Models.LayoutCertificateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public interface ILayoutCertificateService
    {
        Task AddLayout(LeyendsCertificateVM leyendsCertificateVM);
        Task<LeyendsCertificateVM> GetAllAsync();
        Task<LeyendsCertificateVM> GetByIdAsync();
        Task<int> GetLastFooter(string PlantId);
        Task<LeyendsCertificateVM> GetFooter(string PlantId);
        Task<int> GetLastCertificate();
    }
}
