using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebMotors.Data.Mapping;
using WebMotors.Domain.Entities;

namespace WebMotors.Data.Context
{
    public class Contexto : DbContext
    {

        private readonly IHostingEnvironment _ambiente;


        public Contexto(IHostingEnvironment ambiente)
        {
            this._ambiente = ambiente;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnuncioMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(this._ambiente.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            string conexaoString = config.GetConnectionString("Conexao");
            optionsBuilder.UseSqlServer(conexaoString);
        }

        #region DbSets
        public DbSet<Anuncio> Anuncios { get; set; }
        #endregion
    }
}
