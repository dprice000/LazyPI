using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
    public enum AuthenticationType
    {
        Anonymous,
        Basic,
        Kerberos
    }

    public class WebAPIConnection : LazyPI.Common.Connection
    {
        private RestSharp.RestClient _Client;
        protected AuthenticationType _AuthType;

        public RestSharp.RestClient Client
        {
            get { return _Client; }
        }

        public WebAPIConnection(AuthenticationType AuthType,string Hostname = null, string Username = null, System.Security.SecureString Password = null)
        {
            _AuthType = AuthType;
            _Hostname = Hostname;
            _Client = new RestSharp.RestClient(Hostname);

            if (AuthType == AuthenticationType.Basic)
            {
                _Client.Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(Username, Password.ToString());
                _Username = Username;
            }
            else if (AuthType == AuthenticationType.Kerberos)
            {
                //Looksup credentails of the application is using the library
                System.Net.NetworkCredential netCred = System.Net.CredentialCache.DefaultNetworkCredentials;
                _Username = netCred.UserName;

                _Client.Authenticator = new RestSharp.Authenticators.NtlmAuthenticator(netCred);
            }
        }
    }
}
