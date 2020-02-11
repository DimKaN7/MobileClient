using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using MobileClient.Classes;

namespace MobileClient
{
    public class PersonsService
    {
        const string URL = "http://192.168.0.37:63729/api/Persons/";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        
        public async Task<List<string>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(URL);
            return JsonConvert.DeserializeObject<List<string>>(result);
        }

        public async Task<Person> AddPerson(Person person)
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "0");
            values.Add("pName", person.Name);
            values.Add("pAge", person.Age);
            var response = await client.PostAsync(URL, new StringContent(
                JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<Person>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<MarriedPerson> AddMarriedPerson(MarriedPerson mPerson)
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "1");
            values.Add("pName", mPerson.Name);
            values.Add("pAge", mPerson.Age);
            values.Add("partnerName", mPerson.PartnerName);
            values.Add("partnerAge", mPerson.PartnerAge);

            var response = await client.PostAsync(URL, new StringContent(
               JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<MarriedPerson>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Parent> AddParent(Parent pPerson)
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "2");
            values.Add("pName", pPerson.Name);
            values.Add("pAge", pPerson.Age);
            values.Add("childCount", pPerson.ChildCount.ToString());

            var response = await client.PostAsync(URL, new StringContent(
               JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<Parent>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "0");
            values.Add("id", person.Id.ToString());
            values.Add("pName", person.Name);
            values.Add("pAge", person.Age);

            var response = await client.PutAsync(URL + "/" + person.Id,
                new StringContent(JsonConvert.SerializeObject(values),
                Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<Person>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<MarriedPerson> UpdateMarriedPerson(MarriedPerson mPerson)
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "1");
            values.Add("id", mPerson.Id.ToString());
            values.Add("pName", mPerson.Name);
            values.Add("pAge", mPerson.Age);
            values.Add("partnerName", mPerson.PartnerName);
            values.Add("partnerAge", mPerson.PartnerAge);

            var response = await client.PutAsync(URL + "/" + mPerson.Id,
                new StringContent(JsonConvert.SerializeObject(values),
                Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<MarriedPerson>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Parent> UpdateParent(Parent pPerson)
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "2");
            values.Add("id", pPerson.Id.ToString());
            values.Add("pName", pPerson.Name);
            values.Add("pAge", pPerson.Age);
            values.Add("childCount", pPerson.ChildCount.ToString());

            var response = await client.PutAsync(URL + "/" + pPerson.Id,
                new StringContent(JsonConvert.SerializeObject(values),
                Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<Parent>(
                await response.Content.ReadAsStringAsync());
        }

        public async void Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(URL + "/" + id);
        }

        public async Task<string> PerformRequest1()
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "r_1");

            var response = await client.PostAsync(URL, new StringContent(
               JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<string>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<string> PerformRequest2()
        {
            HttpClient client = GetClient();
            var values = new Dictionary<string, string>();
            values.Add("type", "r_2");

            var response = await client.PostAsync(URL, new StringContent(
               JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json"));
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            return JsonConvert.DeserializeObject<string>(
                await response.Content.ReadAsStringAsync());
        }
    }
}
