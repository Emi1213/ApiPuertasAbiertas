namespace ApiPuertasAbiertas.Application.DTOs.Usuarios;

public class BusquedaActiveDirectoryRequestDto
{
  public string? Query { get; set; }
  public string? Usuario { get; set; }
  public string? Contrasenia { get; set; }
}
