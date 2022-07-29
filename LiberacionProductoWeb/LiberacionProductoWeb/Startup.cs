using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using LiberacionProductoWeb.Data;
using LiberacionProductoWeb.Models.IndentityModels;
using LiberacionProductoWeb.Services;
using LiberacionProductoWeb.Helpers;
using LiberacionProductoWeb.Data.Repository.Base;
using LiberacionProductoWeb.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using LiberacionProductoWeb.Data.Repository.Base.External;
using System.Text;
using LiberacionProductoWeb.Models.DataBaseModels;
using System.Globalization;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.AspNetCore.Http.Features;


namespace LiberacionProductoWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DBContext

            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("lindeDB"), ServiceLifetime.Transient);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                                            sqlServerOptions => sqlServerOptions.CommandTimeout(180)), ServiceLifetime.Transient);
            services.AddDbContext<AppDbExternalContext>(options => options.UseSqlServer(Configuration["LProdConnection:ServiceApiKey"],
                                                            sqlServerOptions => sqlServerOptions.CommandTimeout(180)), ServiceLifetime.Transient);
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders()
               .AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ExtraUserClaimsPrincipalFactory>();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedEmail = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                //options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

           // Add configuration singleton
            var emailConfig = Configuration
               .GetSection("EmailData")
               .Get<EmailData>();
            services.AddSingleton(emailConfig);

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            //// Add application services.
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IAnalyticsCertsService, AnalyticsCertsService>();
            services.AddTransient<IProductionOrderService, ProductionOrderService>();
            services.AddTransient<IConditioningOrderService, ConditioningOrderService>();

            //MEG
            services.AddTransient<IVariablesFileReaderService, VariablesFileReaderService>();

            //Add repository injections
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IExampleRepository, ExampleRepository>(); //only example reporsitory
            services.AddScoped<IGeneralCatalogRepository, GeneralCatalogRepository>();
            services.AddScoped<IFormulaCatalogRepository, FormulaCatalogRepository>();
            services.AddScoped<IProductCatalogRepository, ProductCatalogRepository>();
            services.AddScoped<IStabilityCatalogRepository, StabilityCatalogRepository>();
            services.AddScoped<IContainerCatalogRepository, ContainerCatalogRepository>();
            services.AddScoped<IDispositionCatalogRepository, DispositionCatalogRepository>();
            //external linde
            services.AddScoped<IPlantasRepository, PlantasRepository>();
            services.AddScoped<ITanquesRepository, TanquesRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IUsuariosExtRepository, UsuariosExtRepository>();
            services.AddScoped<IDetalleXLoteTanqueRepository, DetalleXLoteTanqueRepository>();
            services.AddScoped<ILotesProduccionRepository, LotesProduccionRepository>();
            services.AddScoped<ILotesProduccionDetalleRepository, LotesProduccionDetalleRepository>();
            services.AddScoped<ILotesTanqueXProductoRepository, LotesTanqueXProductoRepository>();
            services.AddScoped<ILoteDistribuicionDetalleRepository, LoteDistribuicionDetalleRepository>();
            services.AddScoped<ILoteDistribuicionRepository, LoteDistribuicionRepository>();
            services.AddScoped<ILotesDistribuicionClienteRepository, LotesDistribuicionClienteRepository>();
            services.AddScoped<IAnalisisClienteRepository, AnalisisClienteRepository>();
            services.AddScoped<IHistorianInfoTagsPlantRepository, HistorianInfoTagsPlantRepository>();
            services.AddScoped<IHistorianReadingsPlantRepository, HistorianReadingsPlantRepository>();
            services.AddScoped<IHistorianTagsPlantRepository, HistorianTagsPlantRepository>();
            ///end external
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IPrincipalService, PrincipalService>();
            services.AddScoped<IRapService, RapService>();
            services.AddScoped<IActivitieRepository, ActivitieRepository>();
            services.AddScoped<ICheckListPipeAnswerRepository, CheckListPipeAnswerRepository>();
            services.AddScoped<ICheckListPipeRecordAnswerRepository, CheckListPipeRecordAnswerRepository>();
            services.AddScoped<ICheckListPipeCommentsAnswerReepository, CheckListPipeCommentsAnswerReepository>();
            services.AddScoped<ICheckListPipeRecordAnswerRepository, CheckListPipeRecordAnswerRepository>();
            services.AddScoped<ICheckListPipeDictiumAnswerRepository, CheckListPipeDictiumAnswerRepository>();
            services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();
            services.AddScoped<IProductionEquipmentRepository, ProductionEquipmentRepository>();
            services.AddScoped<IMonitoringEquipmentRepository, MonitoringEquipmentRepository>();
            services.AddScoped<IPipelineClearanceRepository, PipelineClearanceRepository>();
            services.AddScoped<IProductionOrderAttributeRepository, ProductionOrderAttributeRepository>();
            services.AddScoped<IBatchDetailsRepository, BatchDetailsRepository>();
            services.AddScoped<IConditioningOrderRepository, ConditioningOrderRepository>();
            services.AddScoped<IReportAuditTrailRepository, ReportAuditTrailRepository>();
            services.AddScoped<IReportAuditTrailService, ReportAuditTrailService>();
            services.AddScoped<IBatchAnalysisRepository, BatchAnalysisRepository>();
            services.AddScoped<IHistoryNotesRepository, HistoryNotesRepository>();
            services.AddScoped<IHistoryStatesRepository, HistoryStatesRepository>();
            services.AddScoped<IPipelineClearanceOARepository, PipelineClearanceOARepository>();
            services.AddScoped<IAnalyticalEquipamentRepository, AnalyticalEquipamentRepository>();
            services.AddScoped<IScalesFlowMetersRepository, ScalesFlowMetersRepository>();
            services.AddScoped<IEquipmentProcessConditioningRepository, EquipmentProcessConditioningRepository>();
            services.AddScoped<IPerformanceProcessConditioningRepository, PerformanceProcessConditioningRepository>();
            services.AddScoped<IPipeFillingControlRepository, PipeFillingControlRepository>();
            services.AddScoped<IPipeFillingRepository, PipeFillingRepository>();
            services.AddScoped<IPipeFillingAnalysisRepository, PipeFillingAnalysisRepository>();
            services.AddScoped<IPipeFillingCustomerRepository, PipeFillingCustomerRepository>();
            services.AddScoped<IComplementoTanqueRepository, ComplementoTanqueRepository>();
            services.AddScoped<IComplementoPipaRepository, ComplementoPipaRepository>();
            services.AddScoped<IActivitiesReportAuditRepository, ActivitiesReportAuditRepository>();
            services.AddScoped<IExportPDFService, ExportPDFService>();
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IHistorianRepository, HistorianRepository>();
            services.AddScoped<IProductionOrderHistorianRepository, ProductionOrderHistorianRepository>();
            services.AddScoped<IPipeFillingCustomersFilesRepository, PipeFillingCustomersFilesRepository>();
            services.AddScoped<ILeyendsCertificateRepository, LeyendsCertificateRepository>();
            services.AddScoped<ILeyendsCertificateHistoryRepository, LeyendsCertificateHistoryRepository>();
            services.AddScoped<ILeyendsFooterCertificateRepository, LeyendsFooterCertificateRepository>();
            services.AddScoped<ILeyendsFooterCertificateHistoryRepository, LeyendsFooterCertificateHistoryRepository>();
            services.AddScoped<ILayoutCertificateService, LayoutCertificateService>();
            //Add Authorization Policy
            ConfigureAuthorizationPolicies(services);
           
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("es");
                options.AddSupportedCultures(new[] { "es", "en-US" });
                options.AddSupportedUICultures(new[] { "es", "en-US" });
                options.FallBackToParentUICultures = true;

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new CustomRequestCultureProvider(context =>
                    {
                        var languages = context.Request.Headers["Accept-Language"].ToString();
                        var currentLanguage = languages.Split(',').FirstOrDefault();
                        var defaultLanguage = string.IsNullOrEmpty(currentLanguage) ? "en-US" : currentLanguage;
                        defaultLanguage = "es";
                        return Task.FromResult(new ProviderCultureResult(defaultLanguage, defaultLanguage));
                    })
                };
            });

            services.AddControllersWithViews().AddViewLocalization();
            services.AddRazorPages().AddViewLocalization();

            services.AddTransient<IUsersLogin, UsersLogin>();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.LoginPath = "/Account/Login";

           });

            services.AddDevExpressControls();
            services
               .AddMvc()
               .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.ConfigureReportingServices(configurator =>
            {
                configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
                {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services,
            ILoggerFactory loggerFactory, IPlantasRepository plantasRepository, IStateRepository status, IActivitieRepository activities)
        {
            app.UseDevExpressControls();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            //call these methods only on the initial deploy
            //CreateAdminRol(services).Wait();

            //CreateResponsableSanitarioRol(services, plantasRepository).Wait();

            //CreateAdminUsers(services, plantasRepository).Wait();

            //CreateSuperIntendenteRol(services).Wait();

            //CreateUsuarioProduccionRol(services).Wait();


            loggerFactory.AddFile(@"C:\LiberacionProd\Linde.log");

            CreateStatus(status).Wait();
            Createactivities(activities).Wait();

        }


        #region Policies

        private void ConfigureAuthorizationPolicies(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(SecurityConstants.CONSULTAR_MIS_TAREAS_PENDIENTES,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_MIS_TAREAS_PENDIENTES));
                options.AddPolicy(SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_EXPEDIENTE_DE_LOTE));
                options.AddPolicy(SecurityConstants.CONSULTA_GENERAL,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTA_GENERAL));
                options.AddPolicy(SecurityConstants.CREAR_OP,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CREAR_OP));
                options.AddPolicy(SecurityConstants.EDITAR_OP,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_OP));
                options.AddPolicy(SecurityConstants.CANCELAR_OP,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CANCELAR_OP));
                options.AddPolicy(SecurityConstants.APROBAR_CANCELACION_OP,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.APROBAR_CANCELACION_OP));
                options.AddPolicy(SecurityConstants.CONSULTAR_OP,
                    policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_OP));
                options.AddPolicy(SecurityConstants.EXPORTAR_OP,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EXPORTAR_OP));
                options.AddPolicy(SecurityConstants.EDITAR_OA,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_OA));
                options.AddPolicy(SecurityConstants.CONSULTAR_OA,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_OA));

                options.AddPolicy(SecurityConstants.EXPORTAR_OA,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EXPORTAR_OA));
                options.AddPolicy(SecurityConstants.EDITAR_VERIFICACION_DE_PIPAS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_VERIFICACION_DE_PIPAS));
                options.AddPolicy(SecurityConstants.CONSULTAR_VERIFICACION_DE_PIPAS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_VERIFICACION_DE_PIPAS));
                options.AddPolicy(SecurityConstants.EXPORTAR_CHECK_LIST_VERIFICACION_DE_PIPAS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EXPORTAR_CHECK_LIST_VERIFICACION_DE_PIPAS));
                options.AddPolicy(SecurityConstants.CANCELAR_CHECK_LIST_DE_VERIFICACION_DE_PIPAS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CANCELAR_CHECK_LIST_DE_VERIFICACION_DE_PIPAS));
                options.AddPolicy(SecurityConstants.CONSULTAR_RAP_TANQUES,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_RAP_TANQUES));
                options.AddPolicy(SecurityConstants.CONSULTAR_RAP_PIPAS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_RAP_PIPAS));
                options.AddPolicy(SecurityConstants.COMPLEMENTO_DE_RAP_PIPAS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.COMPLEMENTO_DE_RAP_PIPAS));
                options.AddPolicy(SecurityConstants.CONSULTAR_REPORTE_AUDIT_TRAIL,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_REPORTE_AUDIT_TRAIL));
                options.AddPolicy(SecurityConstants.COMPLEMENTO_DE_RAP_TANQUES,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.COMPLEMENTO_DE_RAP_TANQUES));
                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_DE_USUARIOS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_DE_USUARIOS));
                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_DE_USUARIOS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_DE_USUARIOS));
                options.AddPolicy(SecurityConstants.ELIMINAR_REGISTRO_EN_CATALOGO_DE_USUARIOS,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_REGISTRO_EN_CATALOGO_DE_USUARIOS));
                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_DE_ROLES,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_DE_ROLES));
                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_DE_ROLES,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_DE_ROLES));
                options.AddPolicy(SecurityConstants.ELIMINAR_ROL_DEL_CATALOGO_DE_ROLES,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_ROL_DEL_CATALOGO_DE_ROLES));

                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_GENERAL,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_GENERAL));

                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_GENERAL,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_GENERAL));

                options.AddPolicy(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_GENERAL,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_GENERAL));

                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_FORMULA_FARMACEUTICA,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_FORMULA_FARMACEUTICA));

                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_FORMULA_FARMACEUTICA,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_FORMULA_FARMACEUTICA));

                options.AddPolicy(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_FORMULA_FARMACEUTICA,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_FORMULA_FARMACEUTICA));

                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_PRODUCTO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_PRODUCTO));
                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_DE_PRODUCTO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_DE_PRODUCTO));
                options.AddPolicy(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_PRODUCTO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_PRODUCTO));

                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO));

                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO));

                options.AddPolicy(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO));

                options.AddPolicy(SecurityConstants.CONSULTAR_CATALOGO_DISPOSICION_DE_PNC,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_CATALOGO_DISPOSICION_DE_PNC));

                options.AddPolicy(SecurityConstants.EDITAR_CATALOGO_DISPOSICION_DE_PNC,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_CATALOGO_DISPOSICION_DE_PNC));

                options.AddPolicy(SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_DISPOSICION_DE_PNC,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_REGISTRO_DE_CATALOGO_DISPOSICION_DE_PNC));

                options.AddPolicy(SecurityConstants.CONSULTAR_ENVASE_PRIMARIO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_ENVASE_PRIMARIO));

                options.AddPolicy(SecurityConstants.EDITAR_ENVASE_PRIMARIO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_ENVASE_PRIMARIO));

                options.AddPolicy(SecurityConstants.ELIMINAR_ENVASE_PRIMARIO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.ELIMINAR_ENVASE_PRIMARIO));

                options.AddPolicy("CreateEditOP", policy => policy.RequireClaim(SecurityConstants.PERMISSION, new string[] { SecurityConstants.EDITAR_OP, SecurityConstants.CREAR_OP} ));

                options.AddPolicy(SecurityConstants.CONSULTAR_LAYOUT_CERTIFICADO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.CONSULTAR_LAYOUT_CERTIFICADO));

                options.AddPolicy(SecurityConstants.EDITAR_LAYOUT_CERTIFICADO,
                   policy => policy.RequireClaim(SecurityConstants.PERMISSION, SecurityConstants.EDITAR_LAYOUT_CERTIFICADO));
            });
        }

        #endregion

        #region User and Roles

        private async Task SetupPermissionsAsync(ApplicationRole role, ISecurityService securityService, RoleManager<ApplicationRole> roleManager)
        {
            var allPermissions = await securityService.GetAllPermissionsAsync();
            var claims = await roleManager.GetClaimsAsync(role);
            var actualPermissions = new List<String>();
            var newPermissions = new List<String>();

            if (claims != null && claims.Count() > 0)
            {
                actualPermissions = claims.Select(c => c.Value).ToList();
            }

            foreach (var section in allPermissions)
            {
                newPermissions.AddRange(section.Permissions.Select(p => p.Key).ToList());
            }

            var itemsToDelete = actualPermissions.Except(newPermissions);
            var itemsToAdd = newPermissions.Except(actualPermissions);

            if (claims != null && claims.Count() > 0)
            {
                foreach (Claim permission in claims)
                {
                    if (itemsToDelete.Contains(permission.Value))
                    {
                        await roleManager.RemoveClaimAsync(role, permission);
                    }
                }
            }

            foreach (string permission in itemsToAdd)
            {
                await roleManager.AddClaimAsync(role, new Claim(SecurityConstants.PERMISSION, permission));
            }
        }

        private async Task CreateAdminRol(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var securityService = serviceProvider.GetRequiredService<ISecurityService>();

            //Adding Admin Role
            var roleCheck = await RoleManager.FindByNameAsync(Helpers.SecurityConstants.PERFIL_ADMIN);
            if (roleCheck == null)
            {
                roleCheck = new ApplicationRole(Helpers.SecurityConstants.PERFIL_ADMIN, Helpers.SecurityConstants.PERFIL_ADMIN);
                //roleCheck.CustomName = Helpers.SecurityConstants.PERFIL_ADMIN;
                await RoleManager.CreateAsync(roleCheck);

            }
            await SetupPermissionsAsync(roleCheck, securityService, RoleManager);

            CreateAdminUser(serviceProvider).Wait();

        }



        private async Task CreateAdminUser(IServiceProvider serviceProvider)
        {

            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            var u = new ApplicationUser();
            u.UserName = Helpers.SecurityConstants.ADMIN_USER;
            u.Email = Helpers.SecurityConstants.ADMIN_EMAIL;
            u.NombreUsuario = Helpers.SecurityConstants.ADMIN_NOMBRE;
            u.EmailConfirmed = true;
            u.Rol = Helpers.SecurityConstants.PERFIL_ADMIN;

            var result = await UserManager.CreateAsync(u, Helpers.SecurityConstants.ADMIN_PASS);
            if (result.Succeeded)
            {
                var uadd = await UserManager.FindByNameAsync(u.UserName);
                await UserManager.AddToRoleAsync(uadd, Helpers.SecurityConstants.PERFIL_ADMIN);
            }

        }


        private async Task CreateResponsableSanitarioRol(IServiceProvider serviceProvider, IPlantasRepository plantasRepository)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var securityService = serviceProvider.GetRequiredService<ISecurityService>();

            //Adding Admin Role
            var roleCheck = await RoleManager.FindByNameAsync(Helpers.SecurityConstants.PERFIL_RESPONSABLE_SANITARIO);
            if (roleCheck == null)
            {
                roleCheck = new ApplicationRole(Helpers.SecurityConstants.PERFIL_RESPONSABLE_SANITARIO, Helpers.SecurityConstants.PERFIL_RESPONSABLE_SANITARIO);
                //roleCheck.CustomName = Helpers.SecurityConstants.PERFIL_RESPONSABLE_SANITARIO;
                await RoleManager.CreateAsync(roleCheck);

            }
            await SetupPermissionsAsync(roleCheck, securityService, RoleManager);

            CreateResponsableSanitarioUser(serviceProvider, plantasRepository).Wait();
        }

        private async Task CreateResponsableSanitarioUser(IServiceProvider serviceProvider, IPlantasRepository plantasRepository)
        {

            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            StringBuilder plantsObj = new StringBuilder();
            if (bool.Parse(Configuration["FlagWSMexe:ServiceApiKey"]))
            {
                var plants = plantasRepository.GetAll();
                foreach (var item in plants)
                {
                    plantsObj.Append(item.Id_Planta + " ,");
                }
            }
            var u = new ApplicationUser();
            u.UserName = "MEXTJEX1";
            u.NombreUsuario = "Juan P?rez";
            u.EmailUsuario = "jesus.guevara@imsoftware.pro";
            u.FechaCreacion = DateTime.Now;
            u.Email = "jesus.guevara@imsoftware.pro";
            u.EmailConfirmed = true;
            u.MexeUsuario = "MEXTJEX1";
            u.Rol = Helpers.SecurityConstants.PERFIL_ADMIN;
            u.PlantaUsuario = plantsObj.ToString();
            u.Activo = true;
            var result = await UserManager.CreateAsync(u, "Linde720");
            if (result.Succeeded)
            {
                var uadd = await UserManager.FindByNameAsync(u.UserName);
                await UserManager.AddToRoleAsync(uadd, Helpers.SecurityConstants.PERFIL_RESPONSABLE_SANITARIO);
            }

        }

        private async Task CreateAdminUsers(IServiceProvider serviceProvider, IPlantasRepository plantasRepository)
        {
            StringBuilder plantsObj = new StringBuilder();
            if (bool.Parse(Configuration["FlagWSMexe:ServiceApiKey"]))
            {
                var plants = plantasRepository.GetAll();
                foreach (var item in plants)
                {
                    plantsObj.Append(item.Id_Planta + " ,");
                }
            }
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            List<ApplicationUser> users = new List<ApplicationUser>();
            users.Add(new ApplicationUser
            {
                UserName = "b2bo62",
                NombreUsuario = "Juan Gilberto Zamora G?mez",
                EmailUsuario = "gilberto.zamora@linde.com",
                FechaCreacion = DateTime.Now,
                Email = "gilberto.zamora@linde.com",
                EmailConfirmed = true,
                MexeUsuario = "b2bo62",
                Rol = Helpers.SecurityConstants.PERFIL_ADMIN,
                PlantaUsuario = plantsObj.ToString(),
                Activo = true
            });
            users.Add(new ApplicationUser
            {
                UserName = "a5mz29",
                NombreUsuario = "Edith Bucio Sol?s",
                EmailUsuario = "edith.bucio@linde.com",
                FechaCreacion = DateTime.Now,
                Email = "edith.bucio@linde.com",
                EmailConfirmed = true,
                MexeUsuario = "a5mz29",
                Rol = Helpers.SecurityConstants.PERFIL_ADMIN,
                PlantaUsuario = plantsObj.ToString(),
                Activo = true
            });
            users.Add(new ApplicationUser
            {
                UserName = "f7fv31",
                NombreUsuario = "Juan Antonio AC Panti",
                EmailUsuario = "juanantonio.ac@linde.com",
                FechaCreacion = DateTime.Now,
                Email = "juanantonio.ac@linde.com",
                EmailConfirmed = true,
                MexeUsuario = "f7fv31",
                Rol = Helpers.SecurityConstants.PERFIL_ADMIN,
                PlantaUsuario = plantsObj.ToString(),
                Activo = true
            });
            foreach (ApplicationUser item in users)
            {
                var result = await UserManager.CreateAsync(item, Helpers.SecurityConstants.ADMIN_PASS);
                if (result.Succeeded)
                {
                    var uadd = await UserManager.FindByNameAsync(item.UserName);
                    await UserManager.AddToRoleAsync(uadd, Helpers.SecurityConstants.PERFIL_RESPONSABLE_SANITARIO);
                }

            }
        }


        private async Task CreateSuperIntendenteRol(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var securityService = serviceProvider.GetRequiredService<ISecurityService>();

            //Adding Admin Role
            var roleCheck = await RoleManager.FindByNameAsync(Helpers.SecurityConstants.PERFIL_SUPERINTENDENTE_DE_PLANTA);
            if (roleCheck == null)
            {
                roleCheck = new ApplicationRole(Helpers.SecurityConstants.PERFIL_SUPERINTENDENTE_DE_PLANTA, Helpers.SecurityConstants.PERFIL_SUPERINTENDENTE_DE_PLANTA);
                //roleCheck.CustomName = Helpers.SecurityConstants.PERFIL_SUPERINTENDENTE_DE_PLANTA;
                await RoleManager.CreateAsync(roleCheck);

            }
            await SetupPermissionsAsync(roleCheck, securityService, RoleManager);

        }


        private async Task CreateUsuarioProduccionRol(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var securityService = serviceProvider.GetRequiredService<ISecurityService>();

            //Adding Admin Role
            var roleCheck = await RoleManager.FindByNameAsync(Helpers.SecurityConstants.PERFIL_USUARIO_DE_PRODUCCION);
            if (roleCheck == null)
            {
                roleCheck = new ApplicationRole(Helpers.SecurityConstants.PERFIL_USUARIO_DE_PRODUCCION, Helpers.SecurityConstants.PERFIL_USUARIO_DE_PRODUCCION);
                //roleCheck.CustomName = Helpers.SecurityConstants.PERFIL_USUARIO_DE_PRODUCCION;
                await RoleManager.CreateAsync(roleCheck);

            }
            await SetupPermissionsAsync(roleCheck, securityService, RoleManager);

        }



        private async Task CreateStatus(IStateRepository statusRepository)
        {
            List<States> status = new List<States>();
            status.Add(new States { Description = "OP-En proceso", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "OP" });
            status.Add(new States { Description = "OP-Liberada", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "OP" });
            status.Add(new States { Description = "OP-En Cancelación", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "OP" });
            status.Add(new States { Description = "OP-Cancelada", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "OP" });

            status.Add(new States { Description = "OA-En proceso", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "OA" });
            status.Add(new States { Description = "OA-Liberada", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "OA" });
            foreach (var item in status)
            {
                var state = await statusRepository.GetAsync(x => x.Description == item.Description);

                if (state.Count() == 0)
                {
                    var addState = await statusRepository.AddAsync(item);
                }
            }
        }

        private async Task Createactivities(IActivitieRepository activitieRepository)
        {
            List<Activities> activities = new List<Activities>();
            activities.Add(new Activities { Description = "Equipos de producción", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Equipos de monitoreo", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Despeje de Línea", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Inicio del lote de producción", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Variables de control de proceso", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Parámetros críticos de proceso", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Atributos críticos de calidad", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Lotificación y análisis del producto", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Liberación del producto", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Equipos analáticos", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Básculas y flujómetros", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Equipos (accesorios) empleados en el proceso de acondicionamiento", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Control de llenado de pipas", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Rendimiento del proceso de acondicionamiento", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Cierre del expediente de lote", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Por lotificar", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            activities.Add(new Activities { Description = "Llenado de pipas", CreationDate = DateTime.Now, UpdateDate = DateTime.Now, Active = true, Group = "" });
            foreach (var item in activities)
            {
                var activitie = await activitieRepository.GetAsync(x => x.Description == item.Description);

                if (activitie.Count() == 0)
                {
                    var addctivitie = await activitieRepository.AddAsync(item);
                }
            }
        }
        #endregion


    }
}
