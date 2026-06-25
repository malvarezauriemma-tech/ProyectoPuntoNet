using System;

namespace SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Autorizacion;
using SGE.Dominio.Expedientes;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Excepcion;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Abstracciones;

public class ModificarCaratulaExpedienteUseCase(
    IExpedienteRepository repositorio, 
    IAutorizacionService autorizacionService,
    IUnidadDeTrabajo uow)
{
    public ModificarCaratulaResponse Ejecutar(ModificarCaratulaRequest request)
    {
        // valido autorización 
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("No tiene permisos para modificar expedientes.");
        }

        // recuperar el expediente del repositorio 
        var expediente = repositorio.ObtenerPorId(request.Id);
        
        // validar que exista (si es null, lanzamos excepción)
        if (expediente == null)
        {
            throw new RepositorioException($"No se encontró el expediente con ID {request.Id}");
        }

        // crear el Objeto de Valor Caratula (esto valida el texto)
        var nuevaCaratula = new Caratula(request.NuevaCaratula);

        // llamar al método de comportamiento rico de la entidad 
        expediente.ModificarCaratula(nuevaCaratula, request.UsuarioId);

        // persistir los cambios 
        uow.Guardar();

        return new ModificarCaratulaResponse();
    }
}