namespace ApiPuertasAbiertas.Shared.Interfaces;

public interface IRbacNotifier
{
  Task NotificarCambioModulosAsync(int perfilId);
}
