using System.DirectoryServices;
using ApiPuertasAbiertas.Application.DTOs.Usuarios;
using ApiPuertasAbiertas.Application.Interfaces;

namespace ApiPuertasAbiertas.Infrastructure.Services;

public class ActiveDirectoryServices : IActiveDirectoryServices
{
  private const string DOMINIO = "otecel.com.ec";
  private const string ACTIVE_DIRECTORY_PATH = "LDAP://" + DOMINIO;
  public bool ValidateActiveDirectoryLogin(string username, string pwd)
  {
    var domainAndUsername = DOMINIO + @"\" + username;
    var entry = new DirectoryEntry(ACTIVE_DIRECTORY_PATH, domainAndUsername, pwd);
    try
    {
      var obj = entry.NativeObject;
      var search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + username + ")" };
      search.PropertiesToLoad.Add("cn");
      var result = search.FindOne();
      return result != null;
    }
    catch (System.Exception)
    {
      return false;
    }
  }

  public List<UsuarioActiveDirectoryDto> SearchUsersTop10(string bindUser, string bindPwd, string? query)
  {
    var list = new List<UsuarioActiveDirectoryDto>();
    var upn = bindUser.Contains("@") ? bindUser : $"{bindUser}@{DOMINIO}";

    using var ad = new DirectoryEntry(ACTIVE_DIRECTORY_PATH, upn, bindPwd);

    var q = (query ?? "").Trim();
    var safe = EscapeLdap(q);

    using var searcher = new DirectorySearcher(ad)
    {
      CacheResults = false,
      ClientTimeout = TimeSpan.FromSeconds(6),
      ServerTimeLimit = TimeSpan.FromSeconds(6),
      ReferralChasing = ReferralChasingOption.None,
      SearchScope = SearchScope.Subtree
    };

    if (string.IsNullOrWhiteSpace(safe))
    {
      searcher.Filter =
        "(&(objectCategory=person)(objectClass=user)" +
        "(!(userAccountControl:1.2.840.113556.1.4.803:=2)))";
    }
    else
    {
      var prefix = safe + "*";
      searcher.Filter =
        "(&(objectCategory=person)(objectClass=user)" +
        "(!(userAccountControl:1.2.840.113556.1.4.803:=2))" +
        "(|(sAMAccountName=" + prefix + ")" +
          "(userPrincipalName=" + prefix + ")" +
          "(mail=" + prefix + ")" +
          "(displayName=" + prefix + ")))";
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

        list.Add(new UsuarioActiveDirectoryDto
        {
          SamAccountName = sam,
          NombreParaMostrar = string.IsNullOrWhiteSpace(disp) ? $"{nom} {ape}".Trim() : disp,
          UsuarioNombre = sam.Contains("@") ? sam : $"{sam}@{DOMINIO}",
          Correo = mail
        });
      }
      if (list.Count <= 10) return list;
      return list.Take(10).ToList();
    }
    catch
    {
    }
    list.Clear();
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

        list.Add(new UsuarioActiveDirectoryDto
        {
          SamAccountName = sam,
          NombreParaMostrar = string.IsNullOrWhiteSpace(disp) ? $"{nom} {ape}".Trim() : disp,
          UsuarioNombre = sam.Contains("@") ? sam : $"{sam}@{DOMINIO}",
          Correo = mail
        });
      }
    }

    return list;
  }


  private static string EscapeLdap(string s)
  {
    if (string.IsNullOrEmpty(s)) return "";
    return s.Replace("\\", "\\5c").Replace("*", "\\2a").Replace("(", "\\28").Replace(")", "\\29").Replace("\0", "\\00");
  }


}