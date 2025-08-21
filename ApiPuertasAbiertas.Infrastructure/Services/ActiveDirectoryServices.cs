using System.DirectoryServices;
using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Application.Interfaces;
using ApiPuertasAbiertas.Application.Common;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class ActiveDirectoryServices : IActiveDirectoryServices
{
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

  public List<UsuarioActiveDirectoryDto> SearchUsersTop10(string usuarioConexion, string contraseniaConexion, string? consulta)
  {
    var listaUsuarios = new List<UsuarioActiveDirectoryDto>();
    var upn = usuarioConexion.Contains("@") ? usuarioConexion : $"{usuarioConexion}@{ActiveDirectoryConstants.Domain}";

    using var ad = new DirectoryEntry(ActiveDirectoryConstants.LdapPath, upn, contraseniaConexion);

    var consultaLimpia = (consulta ?? "").Trim();
    var consultaSegura = EscapeLdap(consultaLimpia);

    using var searcher = new DirectorySearcher(ad)
    {
      CacheResults = false,
      ClientTimeout = TimeSpan.FromSeconds(6),
      ServerTimeLimit = TimeSpan.FromSeconds(6),
      ReferralChasing = ReferralChasingOption.None,
      SearchScope = SearchScope.Subtree
    };

    if (string.IsNullOrWhiteSpace(consultaSegura))
    {
      searcher.Filter =
        "(&(objectCategory=person)(objectClass=user)" +
        "(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
    }
    else
    {
      var prefijo = consultaSegura + "*";
      searcher.Filter =
        "(&(objectCategory=person)(objectClass=user)" +
        "(!(userAccountControl:1.2.840.113556.1.4.803:=2))" +
        "(|(sAMAccountName=" + prefijo + ")" +
          "(userPrincipalName=" + prefijo + ")" +
          "(mail=" + prefijo + ")" +
          "(displayName=" + prefijo + ")))";
    }


    searcher.PropertiesToLoad.Add("sAMAccountName");
    searcher.PropertiesToLoad.Add("displayName");
    searcher.PropertiesToLoad.Add("givenName");
    searcher.PropertiesToLoad.Add("sn");
    searcher.PropertiesToLoad.Add("mail");

    try
    {
      searcher.Sort = new SortOption("sAMAccountName", SortDirection.Ascending);

      var vlv = new DirectoryVirtualListView
      {
        BeforeCount = 0,
        AfterCount = 9,
        Offset = 1
      };
      searcher.VirtualListView = vlv;

      using var results = searcher.FindAll();
      foreach (SearchResult r in results)
      {
        string Get(string a) =>
          r.Properties.Contains(a) && r.Properties[a].Count > 0 ? r.Properties[a][0]?.ToString() ?? "" : "";

        var sam = Get("sAMAccountName");
        var disp = Get("displayName");
        var nom = Get("givenName");
        var ape = Get("sn");
        var mail = Get("mail");

        listaUsuarios.Add(new UsuarioActiveDirectoryDto
        {
          SamAccountName = sam,
          NombreParaMostrar = string.IsNullOrWhiteSpace(disp) ? $"{nom} {ape}".Trim() : disp,
          UsuarioNombre = sam.Contains("@") ? sam : $"{sam}@{ActiveDirectoryConstants.Domain}",
          Correo = mail
        });
      }
      if (listaUsuarios.Count <= 10) return listaUsuarios;
      return listaUsuarios.Take(10).ToList();
    }
    catch
    {
    }
    listaUsuarios.Clear();
    searcher.VirtualListView = null;
    searcher.PageSize = 10;
    searcher.SizeLimit = 10;

    using (var results = searcher.FindAll())
    {
      int count = Math.Min(results.Count, 10);
      for (int i = 0; i < count; i++)
      {
        var r = results[i];
        string Get(string a) =>
          r.Properties.Contains(a) && r.Properties[a].Count > 0 ? r.Properties[a][0]?.ToString() ?? "" : "";

        var sam = Get("sAMAccountName");
        var disp = Get("displayName");
        var nom = Get("givenName");
        var ape = Get("sn");
        var mail = Get("mail");

        listaUsuarios.Add(new UsuarioActiveDirectoryDto
        {
          SamAccountName = sam,
          NombreParaMostrar = string.IsNullOrWhiteSpace(disp) ? $"{nom} {ape}".Trim() : disp,
          UsuarioNombre = sam.Contains("@") ? sam : $"{sam}@{ActiveDirectoryConstants.Domain}",
          Correo = mail
        });
      }
    }

    return listaUsuarios;
  }


  private static string EscapeLdap(string s)
  {
    if (string.IsNullOrEmpty(s)) return "";
    return s.Replace("\\", "\\5c").Replace("*", "\\2a").Replace("(", "\\28").Replace(")", "\\29").Replace("\0", "\\00");
  }


}
