namespace ApiPuertasAbiertas.Application.Interfaces;

public interface IActiveDirectoryServices
{
  bool ValidateActiveDirectoryLogin(string username, string pwd);
}