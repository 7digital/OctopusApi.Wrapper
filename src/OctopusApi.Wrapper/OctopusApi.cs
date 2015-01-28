using System.Collections.Generic;
using System.Configuration;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octopus.Api
{
    public class OctopusApi
    {
        private static readonly string ApiKey = ConfigurationManager.AppSettings["OctopusApiKey"];
        private static readonly string ServerEndpoint = ConfigurationManager.AppSettings["OctopusServerEndpoint"];        
        
        private readonly IOctopusRepository _repository;

        public OctopusApi()
        {
            var endpoint = new OctopusServerEndpoint(ServerEndpoint, ApiKey);
            _repository = new OctopusRepository(endpoint);
        }


        public List<ReferenceDataItem> GetEnvironments()
        {            
            return _repository.Environments.GetAll();            
        }

        public List<MachineResource> GetMachines(string environmentId)
        {
            var environments = _repository.Environments.Get(environmentId);
            return _repository.Environments.GetMachines(environments);
        }        
    }
}