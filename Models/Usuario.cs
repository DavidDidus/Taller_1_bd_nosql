public class Usuario
{
    public required string Nombre { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class UsuarioDTOLogin
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
