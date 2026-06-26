using Microsoft.EntityFrameworkCore;
using System;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Infraestructura.Persistencia;

public class UnidadDeTrabajo(SgeContext context) : IUnidadDeTrabajo
{
    public void Guardar()
    {
        // lo unico que se hace es llamar al save changes
        context.SaveChanges();
    }
}