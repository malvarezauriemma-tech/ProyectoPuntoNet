using Microsoft.EntityFrameworkCore;
using System;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Infraestructura.Persistencia;

public class UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly SgeContext _context;
    public UnidadDeTrabajo(SgeContext context) => _context = context;

    public void Guardar()
    {
        _context.Database.ExecuteSqlRaw("PRAGMA journal_mode=DELETE;");
        _context.SaveChanges();
    }
}