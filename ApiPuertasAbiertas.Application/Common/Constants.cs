namespace ApiPuertasAbiertas.Application.Common;

public static class Messages
{
  public const string EntityNotFound = "Entidad no encontrada";
  public const string UserNotFound = "Usuario no encontrado";
  public const string CompanyNotFound = "Empresa no encontrada";
  public const string PersonalNotFound = "Personal no encontrado";
  public const string ProfileNotFound = "Perfil no encontrado";
  public const string ModuleNotFound = "Módulo no encontrado";
  public const string IngressNotFound = "Ingreso no encontrado";

  public const string EntityCreatedSuccessfully = "Entidad creada exitosamente";
  public const string EntityUpdatedSuccessfully = "Entidad actualizada exitosamente";
  public const string EntityDeletedSuccessfully = "Entidad eliminada exitosamente";

  public const string UserCreatedSuccessfully = "Usuario creado exitosamente";
  public const string UserUpdatedSuccessfully = "Usuario actualizado exitosamente";
  public const string UserDeletedSuccessfully = "Usuario eliminado exitosamente";

  public const string IdMismatch = "El ID no coincide con el proporcionado en el cuerpo de la petición";

  public const string ActiveDirectorySearchRequired = "Se requiere usuario para realizar la búsqueda en Active Directory";
  public const string ActiveDirectoryTimeout = "La búsqueda en Active Directory excedió el tiempo límite";
}

public static class ActiveDirectoryConstants
{
  public const string Domain = "otecel.com.ec";
  public const string LdapPath = "LDAP://" + Domain;
  public const int TimeoutSeconds = 5;
  public const int SearchTimeoutSeconds = 2;
  public const int MaxResults = 5;
}
