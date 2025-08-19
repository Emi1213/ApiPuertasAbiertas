namespace ApiPuertasAbiertas.Domain.Entities;

public class ModuloPerfil
{
  public int Id { get; set; }
  public int ModuloId { get; set; }
  public int PerfilId { get; set; }

  public required Modulo Modulo { get; set; }
  public required Perfil Perfil { get; set; }
}