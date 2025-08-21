using System.DirectoryServices;
using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Application.Common;
using Microsoft.Extensions.Options;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class ActiveDirectoryServices : IActiveDirectoryServices
{
  private readonly ActiveDirectorySettings _adSettings;

  public ActiveDirectoryServices(IOptions<ActiveDirectorySettings> adSettings)
  {
    _adSettings = adSettings.Value;
  }
  public bool ValidateActiveDirectoryLogin(string nombreUsuario, string contrasenia)
  {
    var dominioYUsuario = ActiveDirectoryConstants.Domain + @"\" + nombreUsuario;
    var entry = new DirectoryEntry(ActiveDirectoryConstants.LdapPath, dominioYUsuario, contrasenia);
    try
    {
      var obj = entry.NativeObject;
      var busquedaAd = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + nombreUsuario + ")" };
      busquedaAd.PropertiesToLoad.Add("cn");
      var resultado = busquedaAd.FindOne();
      return resultado != null;
    }
    catch (System.Exception)
    {
      return false;
    }
  }

  public List<UsuarioActiveDirectoryDto> SearchUsersTop10(string? consulta)
  {
    var upn = _adSettings.Usuario.Contains("@")
      ? _adSettings.Usuario
      : $"{_adSettings.Usuario}@{ActiveDirectoryConstants.Domain}";

    using var ad = new DirectoryEntry(ActiveDirectoryConstants.LdapPath, upn, _adSettings.Contrasenia);

    string BuildFilter(string? q)
    {
      const string baseFiltro =
        "(&(objectCategory=person)(objectClass=user)" +
        "(!(userAccountControl:1.2.840.113556.1.4.803:=2))";

      q = (q ?? "").Trim();
      if (q.Length == 0) return baseFiltro + ")";

      var safe = EscapeLdap(q);
      var term = $"*{safe}*";

      return baseFiltro +
            $"(|(anr={term})(displayName={term})(sAMAccountName={term})(userPrincipalName={term})(mail={term}))" +
            ")";
    }
    using var searcher = new DirectorySearcher(ad)
    {
      CacheResults = false,
      ClientTimeout = TimeSpan.FromSeconds(6),
      ServerTimeLimit = TimeSpan.FromSeconds(6),
      ReferralChasing = ReferralChasingOption.None,
      SearchScope = SearchScope.Subtree,
      PageSize = 10,
      SizeLimit = 10,
      Sort = new SortOption("sAMAccountName", SortDirection.Ascending),
      Filter = BuildFilter(consulta)
    };
    searcher.PropertiesToLoad.Add("sAMAccountName");
    searcher.PropertiesToLoad.Add("displayName");
    searcher.PropertiesToLoad.Add("givenName");
    searcher.PropertiesToLoad.Add("sn");
    searcher.PropertiesToLoad.Add("mail");

    try
    {
      using var results = searcher.FindAll();
      string Get(SearchResult r, string p) =>
        r.Properties.Contains(p) && r.Properties[p].Count > 0 ? r.Properties[p][0]?.ToString() ?? "" : "";

      return results.Cast<SearchResult>()
                    .Take(10)
                    .Select(r =>
                    {
                      var sam = Get(r, "sAMAccountName");
                      var disp = Get(r, "displayName");
                      var nom = Get(r, "givenName");
                      var ape = Get(r, "sn");
                      var mail = Get(r, "mail");

                      return new UsuarioActiveDirectoryDto
                      {
                        SamAccountName = sam,
                        NombreParaMostrar = string.IsNullOrWhiteSpace(disp) ? $"{nom} {ape}".Trim() : disp,
                        UsuarioNombre = sam.Contains("@") ? sam : $"{sam}@{ActiveDirectoryConstants.Domain}",
                        Correo = mail
                      };
                    })
                    .ToList();
    }
    catch
    {
      return new List<UsuarioActiveDirectoryDto>();
    }
  }



  private static string EscapeLdap(string s)
  {
    if (string.IsNullOrEmpty(s)) return "";
    return s.Replace("\\", "\\5c").Replace("*", "\\2a").Replace("(", "\\28").Replace(")", "\\29").Replace("\0", "\\00");
  }


}
