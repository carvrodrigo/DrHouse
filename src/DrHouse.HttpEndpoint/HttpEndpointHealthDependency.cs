using DrHouse.Core;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DrHouse.HttpEndpoint
{
    public class HttpEndpointHealthDependency : IHealthDependency
    {
        private readonly string _uri;
        private readonly string _endpoint;

        public HttpEndpointHealthDependency(string uri, string endpoint = "")
        {
            _uri = uri;
            _endpoint = endpoint;
        }

        public HealthData CheckHealth()
        {
            RestClient client = new RestClient(_uri);
            RestRequest request = new RestRequest(_endpoint, Method.GET);

            IRestResponse response = client.Get(request);

            HealthData healthData = new HealthData(_uri);
            healthData.Type = "HttpEndpoint";

            if(response.StatusCode == HttpStatusCode.OK)
            {
                healthData.IsOK = true;
            }
            else
            {
                healthData.IsOK = false;
                healthData.ErrorMessage = $"StatusCode: {response.StatusCode} Message: {response.ErrorMessage}";
            }

            return healthData;
        }

        public HealthData CheckHealth(Action check)
        {
            throw new NotImplementedException();
        }
    }
}
