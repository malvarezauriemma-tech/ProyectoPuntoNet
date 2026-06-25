namespace SGE.Aplicacion.Expedientes;

using SGE.Aplicacion.Abstracciones;

public class ListarTodosLosExpedientesUseCase(IExpedienteRepository repo)
{
    
    public ListarTodosLosExpedientesResponse Ejecutar(ListarTodosLosExpedientesRequest request)
    {
        var expedientes = repo.ObtenerTodos();

        // mapeo de las entidades a los DTO
        var dtos = expedientes.Select(e => new ExpedienteDTO(e.Id, e.Caratula.Valor, e.Estado.ToString(), e.FechaCreacion)).ToList();

        return new ListarTodosLosExpedientesResponse(dtos);
    }
}