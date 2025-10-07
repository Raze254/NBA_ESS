using System.Security.Principal;

namespace Latest_Staff_Portal.CustomSecurity
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string userName)
        {
            Identity = new GenericIdentity(userName);
        }

        public string UserID { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }

        public IIdentity Identity { get; }

        public bool IsInRole(string role)
        {
            if (role == RoleName)
                return true;
            return false;
        }
    }
}