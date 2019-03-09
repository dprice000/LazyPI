namespace LazyPI.WebAPI
{
    public enum AuthType
    {
        Anonymous,
        Basic,
        Kerberos
    }

    public class WebAPIConnection : Common.Connection
    {
        private AuthType _AuthType;

        public RestSharp.RestClient Client { get; }

        /// <summary>
        /// Creates a new webAPI connection
        /// </summary>
        /// <param name="AuthType">The form of connection to be used to connect to the webAPI.</param>
        /// <param name="Hostname">The name or IP of the machined to connect to.</param>
        /// <param name="Username">Only required for basic authentication.</param>
        /// <param name="Password">Only required for basic authentication.</param>
        public WebAPIConnection(AuthType AuthType, string Hostname = null, string Username = null, string Password = null)
        {
            _AuthType = AuthType;
            this.Hostname = Hostname;

            if (AuthType == AuthType.Basic)
            {
                Client = new RestSharp.RestClient(Hostname)
                {
                    Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(Username, Password.ToString())
                };
                this.Username = Username;
            }
            else if (AuthType == AuthType.Kerberos)
            {
                System.Net.NetworkCredential netCred = new System.Net.NetworkCredential(Username, Password);

                //Looksup credentials of the application is using the library
                Client = new RestSharp.RestClient(Hostname)
                {
                    Authenticator = new RestSharp.Authenticators.NtlmAuthenticator(netCred)
                };
                this.Username = netCred.UserName;
            }
        }
    }
}