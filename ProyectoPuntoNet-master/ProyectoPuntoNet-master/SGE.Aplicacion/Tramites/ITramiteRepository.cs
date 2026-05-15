using System;
using SGE.Dominio.Tramites;
using System.Collections.Generic;

namespace SGE.Aplicacion;

public interface ITramiteRepository
{
    void Agregar(Tramite tramite);
    void Modificar(Tramite tramite);
    void Eliminar(Guid id);
    Tramite? ObtenerPorId(Guid id);
    List<Tramite> ObtenerPorExpedienteId(Guid expedienteId);
}
