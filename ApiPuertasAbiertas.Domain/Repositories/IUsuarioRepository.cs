using ApiPuertasAbiertas.Domain.Entities;

namespace ApiPuertasAbiertas.Domain.Repositories;
public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorCredencialesAsync(string usuario, string contrasenia);
}