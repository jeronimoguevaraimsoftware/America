using LiberacionProductoWeb.Models.External;
using LiberacionProductoWeb.Models.IndentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LiberacionProductoWeb.Data
{
    public class AppDbExternalContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public AppDbExternalContext(DbContextOptions<AppDbExternalContext> options, IConfiguration configuration)
          : base(options)
        {
            this._configuration = configuration;
        }
        public DbSet<VwPlantas> LPM_VW_PLANTAS { get; set; }
        public DbSet<VwTanques> LPM_VW_TANQUES { get; set; }
        public DbSet<VwProductos> LPM_VW_PRODUCTOS { get; set; }
        public DbSet<VwUsuarios> LPM_VW_HUEMPLEA_ARC { get; set; }
        public DbSet<VwDetalleXLoteTanque> LPM_VW_DETALLE_X_LOTE_TANQUE { get; set; }
        public DbSet<VwLotesProduccion> LPM_VW_LOTES_PRODUCCION { get; set; }
        public DbSet<VwLotesProduccionDetalle> LPM_VW_LOTES_PRODUCCION_DETALLE { get; set; }
        public DbSet<VwLotesTanqueXProducto> LPM_VW_LOTES_TANQUE_X_PRODUCTO { get; set; }
        public DbSet<VwLotesDistribuicionDetalle> LPM_VW_LOTES_DISTRIBUCION_DETALLE { get; set; }
        public DbSet<VwLotesDistribuicion> LPM_VW_LOTES_DISTRIBUCION { get; set; }
        public DbSet<VwLotesDistribuicionCliente> LPM_VW_LOTE_DISTRIBUCION_X_CLIENTE { get; set; }
        public DbSet<VwAnalisisCliente> LPM_VW_ANALISIS_X_CLIENTE { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<VwTanques>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_TANQUES", this._configuration["viewSchema"]);
            })
            .Entity<VwDetalleXLoteTanque>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_DETALLE_X_LOTE_TANQUE", this._configuration["viewSchema"]);
            })
            .Entity<VwLotesProduccion>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_LOTES_PRODUCCION", this._configuration["viewSchema"]);
            })
            .Entity<VwLotesProduccionDetalle>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_LOTES_PRODUCCION_DETALLE", this._configuration["viewSchema"]);
            })
            .Entity<VwLotesTanqueXProducto>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_LOTES_TANQUE_X_PRODUCTO", this._configuration["viewSchema"]);
            })
            .Entity<VwLotesDistribuicion>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_LOTES_DISTRIBUCION", this._configuration["viewSchema"]);
            })

            .Entity<VwLotesDistribuicionCliente>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_LOTE_DISTRIBUCION_X_CLIENTE", this._configuration["viewSchema"]);
            })

            .Entity<VwAnalisisCliente>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_ANALISIS_X_CLIENTE", this._configuration["viewSchema"]);
            })

            .Entity<VwLotesDistribuicionDetalle>(builder =>
            {
                builder.HasNoKey();
                builder.ToTable("LPM_VW_LOTES_DISTRIBUCION_DETALLE", this._configuration["viewSchema"]);
            })
            .Entity<VwProductos>(builder =>
            {
                builder.ToTable("LPM_VW_PRODUCTOS", this._configuration["viewSchema"]);
            })
            .Entity<VwPlantas>(builder =>
            {
                builder.ToTable("LPM_VW_PLANTAS", this._configuration["viewSchema"]);
            })
            .Entity<VwUsuarios>(builder =>
            {
                builder.ToTable("LPM_VW_HUEMPLEA_ARC", this._configuration["viewSchema"]);
            });

            base.OnModelCreating(builder);
        }
    }
}
