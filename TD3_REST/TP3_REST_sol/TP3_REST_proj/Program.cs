using System.Text.Json;

namespace Program
{
    class Program
    {
        static string apiKey = "e942015b46b5489071f63256a043e94895fd7909";
        static void Main(string[] args)
        {
            string response = ReadResponseFromUrl(preparedUrl("https://api.jcdecaux.com/vls/v3/stations"));
            List<Station> stations = JsonSerializer.Deserialize<List<Station>>(response);
            foreach(Station station in stations)
                Console.WriteLine("- "+station.ToString());
        }

        static string preparedUrl(string baseUrl)
        {
            return baseUrl + "/?apiKey=" + apiKey;
        }

        static string ReadResponseFromUrl(string url)
        {
            var httpClientHandler = new HttpClientHandler();
            var httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(url)
            };
            using var response = httpClient.GetAsync(url);
            string responseBody = response.Result.Content.ReadAsStringAsync().Result;
            return responseBody;
        }
    }

    internal class Station
    {
        public int number { get; set; }
        public string contract_name { get; set; }
        public string name;
        public string address;
        public Position position;
        public bool banking;
        public bool bonus;
        public string status;
        public DateTime last_update;
        public bool connected;
        public bool overflow;
        
        public string ToString()
        {
            return this.number + " | " + this.contract_name;
        }
    }

    internal class Position
    {
            public double latitude;
            public double longitude;
    }
}