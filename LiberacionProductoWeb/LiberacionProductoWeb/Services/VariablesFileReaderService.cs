using LiberacionProductoWeb.Data.Repository;
using LiberacionProductoWeb.Data.Repository.Base.External;
using LiberacionProductoWeb.Models.DataBaseModels;
using LiberacionProductoWeb.Models.ProductionOrderViewModels;
using LiberacionProductoWeb.Models.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberacionProductoWeb.Services
{
    public class VariablesFileReaderService : IVariablesFileReaderService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHistorianReadingsPlantRepository _historianReadingsPlantRepository;
        private readonly IHistorianRepository _historianRepository;
        private readonly IProductionOrderHistorianRepository _productionOrderHistorianRepository;

        public VariablesFileReaderService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IHistorianReadingsPlantRepository historianReadingsPlantRepository, IHistorianRepository historianRepository,
            IProductionOrderHistorianRepository productionOrderHistorianRepository)
        {
            this._configuration = configuration;
            this._webHostEnvironment = webHostEnvironment;
            this._historianReadingsPlantRepository = historianReadingsPlantRepository;
            this._historianRepository = historianRepository;
            this._productionOrderHistorianRepository = productionOrderHistorianRepository;
        }

        public async Task<List<CriticalQualityAttributeViewModel>> FillCriticalQualityAttributes(int productionOrderId, List<CriticalQualityAttributeViewModel> criticalQualityAttributesModel)
        {
            var parameters = await this.GetVariablesAsync(productionOrderId);
            if (!(parameters?.Any() ?? false))
                return criticalQualityAttributesModel;
            var criticalQualityAttributes = parameters.Where(x => x.Type == "Atributo critico de calidad").Select(x => x.Variables).FirstOrDefault();
            foreach (var item in criticalQualityAttributesModel)
            {
                var variable = criticalQualityAttributes.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                if (variable != null && variable.Any())
                {
                    item.Historical = JsonConvert.SerializeObject(variable.OrderBy(x => x.Period));
                    item.AvgValue = decimal.Round((decimal)variable.Average(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                    item.MaxValue = decimal.Round((decimal)variable.Max(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                    item.MinValue = decimal.Round((decimal)variable.Min(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                }
            }
            return criticalQualityAttributesModel;
        }

        public async Task<ProductionOrderViewModel> FillVariablesAsync(ProductionOrderViewModel model)
        {
            var parameters = await this.GetVariablesAsync(model.Id);
            if (!(parameters?.Any() ?? false))
                return model;
            model.HasHistorian = parameters != null && parameters.Any() && model.StepSaved > 3;
            model.HasHistorianThree = parameters != null && parameters.Any();
            if (parameters != null && parameters.Any())
            {
                var controlVariables = parameters.Where(x => x.Type == "Variable control de proceso").Select(x => x.Variables).FirstOrDefault();
                foreach (var item in model.ControlVariables)
                {
                    var variable = controlVariables.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                    if (variable != null && variable.Any())
                    {
                        item.Historical = JsonConvert.SerializeObject(variable.OrderBy(x=>x.Period));
                    }
                }
                var criticalQualityAttributes = parameters.Where(x => x.Type == "Atributo critico de calidad").Select(x => x.Variables).FirstOrDefault();
                foreach (var item in model.CriticalQualityAttributes)
                {
                    var variable = criticalQualityAttributes.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                    if (variable != null && variable.Any())
                    {
                        item.Historical = JsonConvert.SerializeObject(variable.OrderBy(x => x.Period));
                    }
                }
                var criticalParameters = parameters.Where(x => x.Type == "Parametro critico de proceso").Select(x => x.Variables).FirstOrDefault();
                foreach (var item in model.CriticalParameters)
                {
                    var variable = criticalParameters.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                    if (variable != null && variable.Any())
                    {
                        item.Historical = JsonConvert.SerializeObject(variable.OrderBy(x => x.Period));
                    }
                }
            }
            return model;
        }

        public async Task<FilterParameters> GetFilterParameterByProductKeyAsync(string ProductKey)
        {
            var pathFile = $"{this._webHostEnvironment.ContentRootPath}{this._configuration["PathFilterParameter"]}";
            var jsonFile = await System.IO.File.ReadAllTextAsync(pathFile, Encoding.Latin1);
            var parameters = JsonConvert.DeserializeObject<List<FilterParameters>>(jsonFile);
            return parameters.FirstOrDefault(x => x.ProductKey == ProductKey);
        }

        private async Task<IEnumerable<ProductionOrderHistorianDto>> GetVariablesAsync(int productionOrderId, string plant, string product, DateTime start, DateTime end)
        {
            var result = await this._historianReadingsPlantRepository.GetVariablesAsync(plant, product, start, end);
            var variablesGroup = result.GroupBy(x => x.Type).ToList();
            var variables = variablesGroup.Select(x => new ProductionOrderHistorianDto
            {
                Type = x.Key,
                Variables = x.GroupBy(y => y.Name).Select(y => new ProductionOrderVariableDto()
                {
                    Name = y.Key,
                    Values = y.Select(z => new VariableDto()
                    {
                        Period = z.Period,
                        Value = z.Value
                    }).OrderBy(x=>x.Period).ToList()
                }).ToList()
            }).ToList();
            return variables;
        }

        private async Task<IEnumerable<ProductionOrderHistorianDto>> GetVariablesAsync(int productionOrderId)
        {
            var historian = (await this._historianRepository.GetAsync(x=>x.ProductionOrderId == productionOrderId)).LastOrDefault();
            return historian?.productionOrderHistorianDtos;
        }

        private async Task SaveVariablesAsync(IEnumerable<ProductionOrderHistorianDto> variables, int productionOrderId)
        {
            if (variables != null && variables.Any())
            {
                var historian = new ProductionOrderHistorian()
                {
                    productionOrderHistorianDtos = variables,
                    ProductionOrderId = productionOrderId
                };
                await this._productionOrderHistorianRepository.AddAsync(historian);
            }
        }

        public async Task<ProductionOrderViewModel> StorageVariablesAsync(ProductionOrderViewModel model)
        {
            var PlantId = model.Location?.Split(" ")[0];
            PlantId = PlantId?.Split("-")[0];
            var ProductId = model.SelectedTankFilter?.Split("-")[0];
            var parameters = await this.GetVariablesAsync(model.Id, PlantId, ProductId, (DateTime)model.PipelineClearance.ProductionStartedDate, (DateTime)model.PipelineClearance.ProductionEndDate);
            if (!(parameters?.Any() ?? false))
                return model;
            var parametersSelected = new List<ProductionOrderHistorianDto>();
            var variablesSelected = new List<ProductionOrderVariableDto>();

            var controlVariables = parameters.Where(x => x.Type == "Variable control de proceso").Select(x => x.Variables).FirstOrDefault();
            var controlSelected = parameters.Where(x => x.Type == "Variable control de proceso").FirstOrDefault();
            foreach (var item in model.ControlVariables)
            {
                var variable = controlVariables?.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                var variableSelected = controlVariables?.Where(x => x.Name == item.ChartPath).FirstOrDefault();
                if (variable != null && variable.Any())
                {
                    variable = variable.Select(x => { x.Value = x.Value.HasValue ? x.Value : 0; return x; }).ToList();
                    variableSelected.Values = variable;
                    variablesSelected.Add(variableSelected);
                    item.Historical = JsonConvert.SerializeObject(variable);
                    item.AvgValue = decimal.Round((decimal)variable.Average(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                    item.MaxValue = decimal.Round((decimal)variable.Max(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                    item.MinValue = decimal.Round((decimal)variable.Min(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                }
            }
            controlSelected.Variables = variablesSelected;
            parametersSelected.Add(controlSelected);

            var criticalParameters = parameters.Where(x => x.Type == "Parametro critico de proceso").Select(x => x.Variables).FirstOrDefault();
            controlSelected = parameters.Where(x => x.Type == "Parametro critico de proceso").FirstOrDefault();
            variablesSelected = new List<ProductionOrderVariableDto>();
            foreach (var item in model.CriticalParameters)
            {
                var variable = criticalParameters?.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                var variableSelected = criticalParameters?.Where(x => x.Name == item.ChartPath).FirstOrDefault();
                if (variable != null && variable.Any())
                {
                    variable = variable.Select(x => { x.Value = x.Value.HasValue ? x.Value : 0; return x; }).ToList();
                    variableSelected.Values = variable;
                    variablesSelected.Add(variableSelected);
                    item.Historical = JsonConvert.SerializeObject(variable);
                    item.AvgValue = decimal.Round((decimal)variable.Average(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                    item.MaxValue = decimal.Round((decimal)variable.Max(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                    item.MinValue = decimal.Round((decimal)variable.Min(x => x.Value), 2, MidpointRounding.AwayFromZero).ToString("G", CultureInfo.InvariantCulture);
                }
            }
            controlSelected.Variables = variablesSelected;
            parametersSelected.Add(controlSelected);

            var criticalQualityAttributes = parameters.Where(x => x.Type == "Atributo critico de calidad").Select(x => x.Variables).FirstOrDefault();
            controlSelected = parameters.Where(x => x.Type == "Atributo critico de calidad").FirstOrDefault();
            variablesSelected = new List<ProductionOrderVariableDto>();
            foreach (var item in model.CriticalQualityAttributes)
            {
                var variable = criticalQualityAttributes?.Where(x => x.Name == item.ChartPath).Select(x => x.Values).FirstOrDefault();
                var variableSelected = criticalQualityAttributes?.Where(x => x.Name == item.ChartPath).FirstOrDefault();
                if (variable != null && variable.Any())
                {
                    variable = variable.Select(x => { x.Value = x.Value.HasValue ? x.Value : 0; return x; }).ToList();
                    variableSelected.Values = variable;
                    variablesSelected.Add(variableSelected);
                }
            }
            controlSelected.Variables = variablesSelected;
            parametersSelected.Add(controlSelected);

            await this.SaveVariablesAsync(parametersSelected, model.Id);
            return model;
        }

        public async Task<ControlVariableViewModel> GetMaxMin(String variables, string max, string min)
        {
            ControlVariableViewModel control = new ControlVariableViewModel();
            float MaxValue;
            float MinValue;
            float.TryParse(max, NumberStyles.Float, CultureInfo.InvariantCulture, out MaxValue);
            float.TryParse(min, NumberStyles.Float, CultureInfo.InvariantCulture, out MinValue);

            var JsonList = JsonConvert.DeserializeObject<List<VariableDto>>(variables);
            float MaxJson = (float)JsonList.Max(x => x.Value);
            float MinJson = (float)JsonList.Min(x => x.Value);

            control.MaxValue = Math.Max(MaxJson, MaxValue).ToString();
            control.MinValue = Math.Min(MinJson, MinValue).ToString();

            return control;
        }
    }
}
