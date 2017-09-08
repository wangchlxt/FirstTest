using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CSharpUtils
{
    public class PrincipalUtil : IPrincipal
    {
        private string roles;
        private IdentityUtil identity;

        public PrincipalUtil(string userid, string authenticationType,string role)
        {
            roles = role;
            identity = new IdentityUtil(userid, authenticationType);
        }

        public IIdentity Identity
        {
            get
            {
                return identity;
            }
        }

        public bool IsInRole(string role)
        {
            return (role == roles);
        }
    }
}
