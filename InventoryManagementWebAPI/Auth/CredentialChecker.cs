using InventoryManagementWebAPI.DBContext;
using InventoryManagementWebAPI.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace InventoryManagementWebAPI.Auth
{
    public class CredentialChecker
    {
        public User CheckCredential(string username, string password)
        {
            DataContext dataContext = new DataContext();
            User user = dataContext.Users.Where(un => un.Id == username).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            if (Hash.Verify(user.Password, password))
            {
                return user;
            }
            return null;
        }

        public class AuthenticationHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                try
                {
                    var tokens = request.Headers.GetValues("Authorization").FirstOrDefault();
                    if (tokens != null)
                    {
                        string[] base64encoded = tokens.Split(' ');
                        byte[] data = Convert.FromBase64String(base64encoded[1]);
                        string decodedString = Encoding.UTF8.GetString(data);
                        string[] tokensValues = decodedString.Split(':');

                        User ObjUser = new CredentialChecker().CheckCredential(tokensValues[0], tokensValues[1]);
                        if (ObjUser != null)
                        {
                            IPrincipal principal = new GenericPrincipal(new GenericIdentity(ObjUser.Id), new string[] { ObjUser.Type.ToString() });
                            Thread.CurrentPrincipal = principal;
                            HttpContext.Current.User = principal;
                        }
                    }
                    return base.SendAsync(request, cancellationToken);
                }
                catch
                {
                    return base.SendAsync(request, cancellationToken);
                }
            }

        }
    }
}