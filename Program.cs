using System;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DoIt().GetAwaiter().GetResult();
        }

        static async Task DoIt()
        {
            Rootobject rootobject = new Rootobject();
            HttpClient client = new HttpClient();
            string url = "https://swapi.dev/api/people/";
            List<People> peopleList = new List<People>();

            while( !String.IsNullOrEmpty(url))
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                rootobject = (Rootobject)System.Text.Json.JsonSerializer.Deserialize(json, typeof(Rootobject));
                People[] peoples = rootobject.results;
                peopleList.AddRange(peoples);
                url = rootobject.next;
            }

            List<string> names = peopleList.Select<People, string>(p => p.name).OrderBy<string,string>(s=>s).ToList();
            string delimitedNames = string.Join(Environment.NewLine, names);
            Console.WriteLine(delimitedNames);


        }
    }


    public class Rootobject
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public People[] results { get; set; }
    }

    public class People
    {
        public string name { get; set; }
        public string height { get; set; }
        public string mass { get; set; }
        public string hair_color { get; set; }
        public string skin_color { get; set; }
        public string eye_color { get; set; }
        public string birth_year { get; set; }
        public string gender { get; set; }
        public string homeworld { get; set; }
        public string[] films { get; set; }
        public string[] species { get; set; }
        public string[] vehicles { get; set; }
        public string[] starships { get; set; }
        public DateTime created { get; set; }
        public DateTime edited { get; set; }
        public string url { get; set; }
    }

}