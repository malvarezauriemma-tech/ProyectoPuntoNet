namespace SGE.Dominio.Usuarios;

using SGE.Dominio.Comun;

public class Usuario
{
    public Guid Id {get ; private set;}
    public string Nombre {get; private set;}
    public string CorreoElectronico {get; private set;}
    public string ContrasenaHash {get; private set;}
    public bool EsAdministrador {get; private set;}

    // coleccion interna de permisos
    private List<Permiso> _permisos = new List<Permiso>();
    public IEnumerable<Permiso> Permisos => _permisos.AsReadOnly();

    // constructor para creacion de nuevo usuario
    public Usuario(string nombre, string correo, string contrasenaHash, bool esAdmin = false)
    { 
        // todos los datos son obligatorios
        if (string.IsNullOrWhiteSpace(nombre)) throw new DominioException("Nombre obligatorio");
        if (string.IsNullOrWhiteSpace(correo)) throw new DominioException("Correo obligatorio");
        if (string.IsNullOrWhiteSpace(contrasenaHash)) throw new DominioException("Contraseña obligatoria");

        Id = Guid.NewGuid();
        Nombre = nombre;
        CorreoElectronico = correo;
        ContrasenaHash = contrasenaHash;
        EsAdministrador = esAdmin;
    }

    protected Usuario() {} // constructor protegido para la Entity Framework

    // preguntar: "el dominio debe proveer métodos públicos para gestionar la asignación de permisos de forma segura"
    public void AsignarPermiso(Permiso permiso)
    {
        if (!_permisos.Contains(permiso))
        {
            _permisos.Add(permiso);
        }
    }

    public void QuitarPermiso(Permiso permiso)
    {
        if (_permisos.Contains(permiso))
        {
            _permisos.Remove(permiso);
        }
    }

}