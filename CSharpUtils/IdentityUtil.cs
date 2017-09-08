using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace CSharpUtils
{
    public class IdentityUtil : IIdentity
    {
        private string userID;
        private string type;

        public IdentityUtil(string userid,string authenticationType)
        {
            type = authenticationType;
            userID = userid;
        }

        public string AuthenticationType
        {
            get
            {
                return type;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return string.IsNullOrEmpty(userID);
            }
        }

        public string Name
        {
            get
            {
                return userID;
            }
        }
    }
}
