public class Usuario
{
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime? FechaRegistro { get; set; }
}

public class UsuarioDTOLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
}
