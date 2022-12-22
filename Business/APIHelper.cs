using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using ChromeVehicleDescriptions.Models;
using System.Security.Cryptography;
using System.Text;

namespace ChromeVehicleDescriptions.Business
{
    public class APIHelper
    {

        public class TokenBuilder
        {
            private string realm = "http://communitymanager";
            public string GenerateToken(string appId, string secret)
            {
                double val = new Random().NextDouble();
                int seed = Convert.ToInt32(val.ToString().Substring(2, 9));
                string nonce = Base36Converter(seed);

             
               //Shared Secret Security Protocol
                long timestamp = GenerateTimeStamp();
                string baseString = nonce + timestamp + secret;

                string digest = GetDigest(baseString);
                string token = "Atmosphere realm=" + "\"" + realm + "\""
                + ", chromedata_app_id=\"" + appId + "\""
                + ", chromedata_nonce=\"" + nonce + "\""
                + ", chromedata_secret_digest=\"" + digest + "\""
                + ", chromedata_version=\"1.0\", chromedata_digest_method=\"SHA1\""
                + ", chromedata_timestamp=\"" + timestamp + "\"";
                return token;
            }
            private string Base36Converter(int value)
            {
                string AllowableCharacters = "abcdefghijklmnopqrstuvwxyz0123456789";
                var bytes = new byte[10];
                using (var random = RandomNumberGenerator.Create())
                {
                    random.GetBytes(bytes);
                }
                return new string(bytes.Select(x => AllowableCharacters[x %
               AllowableCharacters.Length]).ToArray());
            }
            private string GetDigest(string input)
            {
                using (var sha1 = new SHA1CryptoServiceProvider())
                {
                    return Convert.ToBase64String(sha1.ComputeHash(Encoding.ASCII.GetBytes(input)));
                }
            }
            private long GenerateTimeStamp()
            {
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;
                return Convert.ToInt64(Math.Floor(diff.TotalMilliseconds));
            }
        }
        


        public static ChromeVehicleDescriptionModel GetVehicleDescription(string VIN)
        {

            try
            {
                var ChromeBaseURL = ConfigurationManager.AppSettings["ChromeBaseURL"].ToString();
                var ChromeAppId = ConfigurationManager.AppSettings["ChromeAppID"].ToString();
                var ChromeSecret = ConfigurationManager.AppSettings["ChromeSharedSecret"].ToString();

                var urlParameters =  VIN;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ChromeBaseURL + "/vin/");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var _TokenBuilder = new TokenBuilder();
                var token = _TokenBuilder.GenerateToken(ChromeAppId, ChromeSecret);

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("",);
                client.DefaultRequestHeaders.Add("Authorization", token);

                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var returnVehicle= response.Content.ReadAsAsync<ChromeVehicleDescriptionModel>().Result;
                    return returnVehicle;
                }
                else
                {
                    //errorMessage = errorMessage + "Vehicle Descriptions - " + VIN + ":" + response.StatusCode.ToString() + "/n";
                }
                

                return null;
            }
            catch(Exception ex)
            {
                //errorMessage = errorMessage + "Vehicle Descriptions - " + ex.Message + "/n";
                return null;
            }
        }


    }
}