using System.Security.Principal;

namespace OnlinerNotifier.Filters
{
    public class Principal : IPrincipal
    {
        public int Id { get; set; }

        public Principal(int id)
        {
            Id = id;
            Identity = null;
        }

        public bool IsInRole(string role)
        {
           return true;
        } 

        public IIdentity Identity { get; }
    }
}