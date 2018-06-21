using KanbaneryApi.Net.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KanbaneryApi.Net
{
    public class KanbaneryApiClient : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly bool httpClientOwned;

        public KanbaneryApiClient(string workspace)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri($"https://{workspace}.kanbanery.com/api/v1");
            httpClientOwned = true;
        }

        public KanbaneryApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); ;
            this.httpClientOwned = false;
        }

        /// <summary>
        /// Sets the users api token into DefaultRequestHeaders
        /// </summary>
        /// <param name="apiToken"></param>
        public void SetApiTokenHeader(string apiToken)
        {
            if (httpClient.DefaultRequestHeaders.Contains("X-Kanbanery-ApiToken"))
            {
                httpClient.DefaultRequestHeaders.Remove("X-Kanbanery-ApiToken");
            }
            httpClient.DefaultRequestHeaders.Add("X-Kanbanery-ApiToken", apiToken);            
        }

        /// <summary>
        /// Getting the current user (the API TOKEN owner) information.
        /// </summary>
        /// <returns></returns>
        public async Task<User> GetUser()
        {
            var str = await httpClient.GetStringAsync($"/user");
            if (str == null)
                return null;

            var user = JsonConvert.DeserializeObject<User>(str);
            if (user == null)
                return null;

            return user;
        }

        /// <summary>
        /// Getting project users information
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetProjectUsers(int projectId)
        {
            var str = await httpClient.GetStringAsync($"/projects/{projectId}/users");
            if (str == null)
                return null;

            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(str);
            if (users == null)
                return null;

            return users;
        }

        /// <summary>
        /// Listing workspaces with projects
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Workspace>> GetWorkspaces()
        {
            var str = await httpClient.GetStringAsync($"/user/workspaces");
            if (str == null)
                return null;

            var workspaces = JsonConvert.DeserializeObject<IEnumerable<Workspace>>(str);
            if (workspaces == null)
                return null;

            return workspaces;
        }

        public void Dispose()
        {
            if (httpClientOwned)
            {
                httpClient?.Dispose();
            }
        }

    }
}
