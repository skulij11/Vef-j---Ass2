using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        
        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenAliceResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");
            var tokenBobResponse = await tokenClient.RequestResourceOwnerPasswordAsync("bob", "password", "api1");

            if (tokenAliceResponse.IsError)
            {
                Console.WriteLine(tokenAliceResponse.Error);
                return;
            }
            if (tokenBobResponse.IsError)
            {
                Console.WriteLine(tokenBobResponse.Error);
                return;
            }

            /*Console.WriteLine(tokenAliceResponse.Json);
            Console.WriteLine("\n\n");
            Console.WriteLine(tokenBobResponse.Json);
            Console.WriteLine("\n\n");*/


            // call api
            // First, /api/courses (GET) - Should be open for everyone, also non-authenticated 
            Console.WriteLine("\n/api/courses - GET:\n");

            var client = new HttpClient();
            client.SetBearerToken(tokenAliceResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/courses/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

            Console.WriteLine("Alice - Teacher:");
            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");


            client = new HttpClient();
            client.SetBearerToken(tokenBobResponse.AccessToken);

            response = await client.GetAsync("http://localhost:5001/api/courses/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            
            Console.WriteLine("Bob - Student:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");

            client = new HttpClient();

            response = await client.GetAsync("http://localhost:5001/api/courses/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            
            Console.WriteLine("Anonymous - Non-authenticated:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");

            // Now, /api/courses/{id} (GET) - Should be open for teachers and students
            Console.WriteLine("\n\n/api/courses/{id} - GET:\n");

            client = new HttpClient();
            client.SetBearerToken(tokenAliceResponse.AccessToken);

            response = await client.GetAsync("http://localhost:5001/api/courses/6");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

            Console.WriteLine("Alice - Teacher:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");


            client = new HttpClient();
            client.SetBearerToken(tokenBobResponse.AccessToken);

            response = await client.GetAsync("http://localhost:5001/api/courses/6");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            
            Console.WriteLine("Bob - Student:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");


            client = new HttpClient();

            response = await client.GetAsync("http://localhost:5001/api/courses/6");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            
            Console.WriteLine("Anonymous - Non-authenticated:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");


            // Last, /api/courses (POST) - Should be open for teachers, not students or others
            Console.WriteLine("\n\n/api/courses - POST:\n");
            client = new HttpClient();
            client.SetBearerToken(tokenAliceResponse.AccessToken);

            response = await client.PostAsync("http://localhost:5001/api/courses", new StringContent("Alice posting", Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

            Console.WriteLine("Alice - Teacher:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");


            client = new HttpClient();
            client.SetBearerToken(tokenBobResponse.AccessToken);

            response = await client.PostAsync("http://localhost:5001/api/courses", new StringContent("Bob posting", Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            
            Console.WriteLine("Bob - Student:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");

            client = new HttpClient();

            response = await client.PostAsync("http://localhost:5001/api/courses", new StringContent("Anonymous posting", Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            
            Console.WriteLine("Anonymous - Non-authenticated:");
            content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content + "\n");

        }
    }
}
