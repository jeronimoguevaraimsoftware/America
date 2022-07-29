using AutoMapper;
using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.LayoutCertificateViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using LiberacionProductoWeb.Localize;
using LiberacionProductoWeb.Data;

namespace LiberacionProductoWeb.Services
{
    public class LayoutCertificateService : ILayoutCertificateService
    {
        private readonly ILeyendsCertificateRepository _leyendsCertificateRepository;
        private readonly ILeyendsCertificateHistoryRepository _leyendsCertificateHistoryRepository;
        private readonly ILeyendsFooterCertificateRepository _leyendsFooterCertificateRepository;
        private readonly ILeyendsFooterCertificateHistoryRepository _leyendsFooterCertificateHistoryRepository;
        private readonly ILogger<LayoutCertificateService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<Resource> _resource;
        private readonly AppDbExternalContext _external;
        public LayoutCertificateService(ILeyendsCertificateRepository leyendsCertificateRepository, 
        ILeyendsFooterCertificateRepository leyendsFooterCertificateRepository, ILeyendsCertificateHistoryRepository leyendsCertificateHistoryRepository,
        ILogger<LayoutCertificateService> logger, ILeyendsFooterCertificateHistoryRepository leyendsFooterCertificateHistoryRepository,
        IConfiguration configuration, IStringLocalizer<Resource> resource, AppDbExternalContext external)
        {
            _leyendsCertificateRepository = leyendsCertificateRepository;
            _leyendsCertificateHistoryRepository = leyendsCertificateHistoryRepository;
            _leyendsFooterCertificateRepository = leyendsFooterCertificateRepository;
            _leyendsFooterCertificateHistoryRepository = leyendsFooterCertificateHistoryRepository;
            _logger = logger;
            _configuration = configuration;
            _resource = resource;
            _external = external;
        }
        public async Task AddLayout(LeyendsCertificateVM leyendsCertificateVM)
        {
            var LeyendsCertificateHistoryDB = new LeyendsCertificateHistory();
            var LeyendsFooterInsert = new List<LeyendsFooterCertificate>();
            var LeyendsFooterUpdated = new List<LeyendsFooterCertificateVM>();
            var FooderHistInsert = new List<LeyendsFooterCertificateHistory>();
            var mappedHist = new LeyendsCertificateHistory();
            var HeaderOne = string.Empty;
            var HeaderTwo = string.Empty;
            var LeyendOne = string.Empty;
            var LeyendTwo = string.Empty;
            var Fooder = string.Empty;
            try
            {
                var MappedLeyendsCertificate = ObjectMapper.Mapper.Map<LeyendsCertificate>(leyendsCertificateVM);
                var infoDB = (await _leyendsCertificateRepository.GetAllAsync()).FirstOrDefault();
                var infoFooder = (await _leyendsFooterCertificateRepository.GetAllAsync());
                ///general
                if (infoDB == null)
                {
                    await _leyendsCertificateRepository.AddAsync(MappedLeyendsCertificate);
                    ///history
                    mappedHist.HeaderOne = leyendsCertificateVM.HeaderOne;
                    mappedHist.HeaderTwo = leyendsCertificateVM.HeaderTwo;
                    mappedHist.LeyendOne = leyendsCertificateVM.LeyendOne;
                    mappedHist.LeyendTwo = leyendsCertificateVM.LeyendTwo;
                    mappedHist.User = leyendsCertificateVM.User;
                    await _leyendsCertificateHistoryRepository.AddAsync(mappedHist);
                }
                else
                {
                    var entityClone = (LeyendsCertificate)infoDB.Clone();
                    HeaderOne = MappedLeyendsCertificate.HeaderOne;
                    LeyendOne = MappedLeyendsCertificate.LeyendOne;
                    LeyendTwo = MappedLeyendsCertificate.LeyendTwo;
                    if (HeaderOne != infoDB.HeaderOne || LeyendOne != infoDB.LeyendOne || LeyendTwo != infoDB.LeyendTwo || leyendsCertificateVM.NewFile)
                    {
                        infoDB.HeaderOne = MappedLeyendsCertificate.HeaderOne;
                        infoDB.HeaderTwo = MappedLeyendsCertificate.HeaderTwo;
                        infoDB.LeyendOne = MappedLeyendsCertificate.LeyendOne;
                        infoDB.LeyendTwo = MappedLeyendsCertificate.LeyendTwo;
                        infoDB.ModifyDate = DateTime.Now;
                        await _leyendsCertificateRepository.UpdateAsync(infoDB, entityClone);
                        ///history
                        mappedHist.HeaderOne = leyendsCertificateVM.HeaderOne;
                        mappedHist.HeaderTwo = leyendsCertificateVM.HeaderTwo;
                        mappedHist.LeyendOne = leyendsCertificateVM.LeyendOne;
                        mappedHist.LeyendTwo = leyendsCertificateVM.LeyendTwo;
                        mappedHist.User = leyendsCertificateVM.User;
                        await _leyendsCertificateHistoryRepository.AddAsync(mappedHist);
                    }
                }

                ///footer
                var sourcePlants = leyendsCertificateVM.PlantsId?.Split(",")?.ToList();
                if (sourcePlants != null)
                {
                    foreach (var item in sourcePlants)
                    {
                        if (!infoFooder.Where(x => x.PlantId.Equals(item)).Any())
                        {
                            LeyendsFooterInsert.Add(new LeyendsFooterCertificate
                            {
                                Footer = leyendsCertificateVM.Footer,
                                PlantId = item.ToString(),
                                User = leyendsCertificateVM.User
                            });
                            FooderHistInsert.Add(new LeyendsFooterCertificateHistory
                            {
                                Footer = leyendsCertificateVM.Footer,
                                User = leyendsCertificateVM.User,
                                PlantId = item.ToString(),
                                CreatedDate = DateTime.Now
                            });
                        }
                        else
                        {
                            if (!infoFooder.Where(x => x.Footer.Equals(leyendsCertificateVM.Footer) && x.PlantId.Equals(item)).Any())
                            {
                                LeyendsFooterUpdated.Add(new LeyendsFooterCertificateVM
                                {
                                    Footer = leyendsCertificateVM.Footer,
                                    PlantId = item.ToString(),
                                    User = leyendsCertificateVM.User,
                                    Id = infoFooder.Where(x => x.PlantId.Equals(item)).Select(x => x.Id).FirstOrDefault(),
                                    ModifyDate = DateTime.Now
                                });
                                FooderHistInsert.Add(new LeyendsFooterCertificateHistory
                                {
                                    Footer = leyendsCertificateVM.Footer,
                                    User = leyendsCertificateVM.User,
                                    PlantId = item.ToString(),
                                    CreatedDate = DateTime.Now
                                });
                            }
                        }
                    }
                    await _leyendsFooterCertificateRepository.AddAsync(LeyendsFooterInsert);
                    var mappedUpdFooter = ObjectMapper.Mapper.Map<List<LeyendsFooterCertificate>>(LeyendsFooterUpdated);
                    await _leyendsFooterCertificateRepository.UpdateAsync(mappedUpdFooter);
                    await _leyendsFooterCertificateHistoryRepository.AddAsync(FooderHistInsert);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en LayoutCertificateService AddLayout " + ex);
            }
        }

        public async Task<LeyendsCertificateVM> GetAllAsync()
        {
            var model = new LeyendsCertificateVM();
            try
            {
                var infoDb = (await _leyendsCertificateRepository.GetAllAsync()).LastOrDefault();
                if (infoDb != null)
                {
                    var FileName = infoDb.HeaderTwo;
                    infoDb.HeaderTwo = infoDb.HeaderTwo;
                    model = ObjectMapper.Mapper.Map<LeyendsCertificateVM>(infoDb);
                    model.FileName = FileName;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en LayoutCertificateService AddLayout " + ex);
            }

            return model;
        }

        public Task<LeyendsCertificateVM> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<LeyendsCertificateVM> GetFooter(string PlantId)
        {
            var model = new LeyendsCertificateVM();
            try
            {
                var footer =  (await  _leyendsFooterCertificateRepository.GetAsync(x => x.PlantId.Equals(PlantId))).LastOrDefault();
                if (footer != null)
                {
                    var mapped = ObjectMapper.Mapper.Map<LeyendsFooterCertificateVM>(footer);
                    model.leyendsFooterCertificateVM = new List<LeyendsFooterCertificateVM>();
                    model.leyendsFooterCertificateVM.Add(new LeyendsFooterCertificateVM
                    {
                        Footer = mapped.Footer,
                        PlantId = mapped.PlantId,
                        PlantName = _external.LPM_VW_PLANTAS.Where(x => x.Id_Planta == Convert.ToInt32(mapped.PlantId)).FirstOrDefault().Descripcion,
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en LayoutCertificateService GetFooter " + ex);
            }
          
            return model;
        }

        public async Task<int> GetLastCertificate()
        {
            var Id = 0;
            try
            {
                var HistoryList = await _leyendsCertificateHistoryRepository.GetAllAsync();
                if (HistoryList.Any())
                    Id = HistoryList.OrderByDescending(x => x.CreatedDate).FirstOrDefault().Id;

            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en LayoutCertificateService GetLastOn " + ex);
            }
            return Id;
        }

        public async Task<int> GetLastFooter(string PlantId)
        {
            var Id = 0;
            try
            {
                var HistoryList = await _leyendsFooterCertificateHistoryRepository.GetAsync(x => x.PlantId.Equals(PlantId));
                if(HistoryList.Any())
                    Id = HistoryList.OrderByDescending(x=>x.CreatedDate).FirstOrDefault().Id;
     
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Ocurrio un error en LayoutCertificateService GetLastOn " + ex);
            }
            return Id;
        }
    }
}
