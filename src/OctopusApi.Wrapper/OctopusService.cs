using System.Collections.Generic;
using System.Linq;

namespace Octopus.Api
{
    public class OctopusService
    {
        private static OctopusApi _octopusApi = new OctopusApi();        

        public OctopusService(OctopusApi octopusApi)
        {
            _octopusApi = octopusApi;
        }

        public static string GetEnvironmentId(string environmentName)
        {        
            var environments = _octopusApi.GetEnvironments();
            return environments.Where(x => x.Name == environmentName).Select(y => y.Id).First();
        }

        public static IList<string> GetServerUrisBy(string environmentName, string role)
        {            
            var environmentId = GetEnvironmentId(environmentName);
            return _octopusApi.GetMachines(environmentId)
                .Where(x => x.Roles.Any(y => y.Equals(role)))
                .Select(x => x.Uri).ToList();
        }
    }
}