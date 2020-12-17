using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Visitas.Mvc.ExternalServices
{
    public class ExternalAPIToken : IExternalAPIToken
    {
        private String tokenString;

        public ExternalAPIToken()
        {
            var httpClient = new HttpClient();

            /*Paso adicional: verificar si en la BD ya se ha guardado un token y ver si aun no expira*/

            var credential = new Dictionary<string, string>
            {
                {"grant_type", "password" },
                {"username", "usi25@apci.gob.pe" },
                {"password", "Julinho123" }
            };
            //Esta línea soluciono mi problema del llamado al webapi
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

            var response = httpClient.PostAsync("https://localhost:44329/token", new FormUrlEncodedContent(credential));
            var responseContent = response.Result.Content.ReadAsStringAsync().Result;
            var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
            tokenString = tokenDictionary["access_token"];

            /*Paso opcional: Guardar en la BD el token generado: access token, expires in y creation date*/
        }

        public String GetExternalAPIToken()
        {
            return tokenString;
        }

    }
}