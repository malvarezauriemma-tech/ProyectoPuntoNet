public record class Caratula
{
    public String Valor {get;}

    public Caratula(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new DominioException("La caratula no puede estar vacía o ser nula");
        }
        Valor = valor;
    }
}