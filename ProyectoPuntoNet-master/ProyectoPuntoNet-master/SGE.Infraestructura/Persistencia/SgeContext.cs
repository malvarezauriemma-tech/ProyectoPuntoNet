using Microsoft.EntityFrameworkCore;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Infraestructura.Persistencia;

public class SgeContext : DbContext
{
    // usamos null! para evitar warnings, el EF los inicializa por reflexion
    public DbSet<Expediente> Expedientes { get; set; } = null!;
    public DbSet<Tramite> Tramites { get; set; } = null!;
    public DbSet<Usuario> Usuarios { get; set; } = null!;

    public SgeContext(DbContextOptions<SgeContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // mapeo de value objects como propiedades complejas
        modelBuilder.Entity<Expediente>().ComplexProperty(e => e.Caratula);
        modelBuilder.Entity<Tramite>().ComplexProperty(t => t.Contenido);

        // configuracion del usuario
        modelBuilder.Entity<Usuario>().HasIndex(u => u.CorreoElectronico).IsUnique();

        // si la lista de permisos es un List<Permiso>, EF core necesita saber que debe tratarla como una coleccion de elementos
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Permisos)
            .HasConversion(
                p => String.Join(',', p), // guardamos como string separado con comas
                p => p.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(Enum.Parse<Permiso>).ToList() // lo recuperamos como lista
            );
    }
}