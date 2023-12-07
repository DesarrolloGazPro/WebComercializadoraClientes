using System.Data;

/// <summary>
/// Summary description for cUsuario
/// </summary>
public class cUsuario
{
    public DataTable Perfiles { get; set; }

    public string Perfil { get; set; }
    public int prospectoId { get; set; }

    public int AlumnoId { get; set; }

    public string Nombre { get; set; }
    public string NombreCompleto { get; set; }

    public int usuarioId { get; set; }

    public string Correo { get; set; }

    public int esAdmin { get; set; }

    public int Modificar { get; set; }

    public int allDestinos { get; set; }

    public string primerLogin { get; set; }
}