using API.Dominio;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestrutura
{
    public class CidadesContext : DbContext
    {
        public DbSet<Cidade> Cidades { get; set; }
        public CidadesContext(DbContextOptions<CidadesContext> options) 
            : base(options){}
    }

    
}