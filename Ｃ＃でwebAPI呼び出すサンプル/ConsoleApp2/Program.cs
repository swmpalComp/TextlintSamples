using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8081/api/textlint/";

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                string jsonText = "{\"text\":\"私は花は好きです\"}";
                var requestContent = new System.Net.Http.StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");

                using (System.Net.Http.HttpResponseMessage response = client.PostAsync(url, requestContent).Result)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine(responseBody);
                }
            }
        }
    }
}
