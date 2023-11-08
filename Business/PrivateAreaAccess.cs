using ClinicApp.Models;

namespace ClinicApp.Business;

public class PrivateAreaAccess
{
    public bool Authentication(string userName, string password)
    {
        if (userName == "admin" && password == "admin")
        {
            return true;
        }
        return false;
    }
}


