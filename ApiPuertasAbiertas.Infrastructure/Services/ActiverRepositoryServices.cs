using System.DirectoryServices;
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
}