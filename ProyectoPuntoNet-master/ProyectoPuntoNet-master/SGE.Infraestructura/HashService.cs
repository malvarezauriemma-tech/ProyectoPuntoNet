using System.Security.Cryptography;
using System.Text;
using SGE.Aplicacion.Abstracciones;

namespace SGE.Infraestructura;

public class HashService : IHashService
{
    public string ObtenerHash(string textoPlano)
    {
        var bytes = Encoding.UTF8.GetBytes(textoPlano); // tomo contraseña que ingreso usuario, y lo traduce a una secuencia de bytes
        var hash = SHA256.HashData(bytes);  // toma los bytes y les aplica una serie de operaciones matematicas y genera un resumen de longitud fija de 256 bits 
        return Convert.ToHexString(hash); // convierte estos bytes recientes en una cadena de texto hexadecimal, asi el hash transforma un string legible que se asigna a la propiedad ContrasenaHash
    }
}