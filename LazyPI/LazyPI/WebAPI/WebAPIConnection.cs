using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyPI.WebAPI
{
    public enum AuthType
    {
        Anonymous,
        Basic,
        Kerberos
    }

    public class WebAPIConnection : LazyPI.Common.Connection
    {
        private RestSharp.RestClient _Client;
        protected AuthType _AuthType;

        public RestSharp.RestClient Client
        {
            get { return _Client; }
        }

        /// <summary>
        /// Creates a new webAPI connection
        /// </summary>
        /// <param name="AuthType">The form of connection to be used to connect to the webAPI.</param>
        /// <param name="Hostname">The name or IP of the machined to connect to.</param>
        /// <param name="Username">Only required for basic authentication.</param>
        /// <param name="Password">Only required for basic authentication.</param>
        public WebAPIConnection(AuthType AuthType,string Hostname = null, string Username = null, string Password = null)
        {
            _AuthType = AuthType;
            _Hostname = Hostname;

            if (AuthType == AuthType.Basic)
            {
                _Client = new RestSharp.RestClient(Hostname)
                    {
                        Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(Username, Password.ToString())
                    };
                _Username = Username;
            }
            else if (AuthType == AuthType.Kerberos)
            {

                System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(Username, Password);

                //Looksup credentails of the application is using the library
                _Client = new RestSharp.RestClient(Hostname)
                {
                    Authenticator = new RestSharp.Authenticators.NtlmAuthenticator(netCred)
                };
                _Username = netCred.UserName;

            }
        }
    }
}
