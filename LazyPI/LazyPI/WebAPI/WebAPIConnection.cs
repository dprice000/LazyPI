using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
    public class WebAPIConnection : LazyPI.Common.Connection
    {
        private RestSharp.RestClient _Client;

        public override RestSharp.RestClient Client
        {
            get { return _Client; }
        }

        public WebAPIConnection(LazyPI.Common.AuthenticationType AuthType,string Hostname, string Username, System.Security.SecureString Password)
        {
            _Hostname = Hostname;
            _Username = Username;
            _AuthType = AuthType;
            _Client = new RestSharp.RestClient(Hostname);

            if (AuthType == Common.AuthenticationType.Basic)
                _Client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(Username, Password.ToString());
            else if (AuthType == Common.AuthenticationType.Kerberos)
            {
                System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(Username, Password.ToString());
                _Client.Authenticator = new RestSharp.Authenticators.NtlmAuthenticator(netCred);
            }
        }
    }
}
