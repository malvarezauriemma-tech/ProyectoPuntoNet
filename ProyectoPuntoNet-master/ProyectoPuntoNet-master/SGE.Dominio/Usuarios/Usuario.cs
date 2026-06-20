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

    // metodos publicos para gestionar la asignacion de permisos
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

    // método para actualizar los datos del usuario con los casos de uso
    public void ActualizarDatos(string nombre, string contrasenaHash)
    {
        if (string.IsNullOrWhiteSpace(nombre)) {
            throw new DominioException("El nombre es obligatorio.");
        }
        if (string.IsNullOrWhiteSpace(contrasenaHash)) {
            throw new DominioException("La contraseña es obligatoria.");
        }

        Nombre = nombre;
        ContrasenaHash = contrasenaHash;
    }

}