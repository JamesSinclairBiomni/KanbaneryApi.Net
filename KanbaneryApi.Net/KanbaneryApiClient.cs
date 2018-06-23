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
        private readonly string workspace;
        private readonly string apiToken;

        public KanbaneryApiClient(string workspace, string apiToken)
        {
            this.workspace = workspace;
            this.apiToken = apiToken;
            httpClientOwned = true;

            httpClient = new HttpClient();
        }

        public KanbaneryApiClient(HttpClient httpClient, string workspace, string apiToken)
        {
            this.workspace = workspace;
            this.apiToken = apiToken;
            httpClientOwned = false;

            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); ;
        }

        /// <summary>
        /// Sets the users api token into DefaultRequestHeaders
        /// </summary>
        /// <remarks>This is in the api docs as available but I haven't been able to get it working as yet</remarks>
        /// <param name="apiToken"></param>
        public void SetApiTokenHeader(string apiToken)
        {
            if (httpClient.DefaultRequestHeaders.Contains("X-Kanbanery-ApiToken"))
            {
                httpClient.DefaultRequestHeaders.Remove("X-Kanbanery-ApiToken");
            }
            httpClient.DefaultRequestHeaders.Add("X-Kanbanery-ApiToken", apiToken);            
        }

        private string GetUrl(string path)
        {
            var queryString = $"?api_token={apiToken}";
            if (httpClient.DefaultRequestHeaders.Contains("X-Kanbanery-ApiToken"))
            {
                queryString = "";
            }
            return $"https://{this.workspace}.kanbanery.com/api/v1/{path}{queryString}";
        }

        private string GetUserWorkspacesUrl()
        {
            var queryString = $"?api_token={apiToken}";
            if (httpClient.DefaultRequestHeaders.Contains("X-Kanbanery-ApiToken"))
            {
                queryString = "";
            }
            return $"https://{this.workspace}.kanbanery.com/api/v1/user/workspaces{queryString}";
        }

        #region Workspace and Project resource

        /// <summary>
        /// Listing workspaces with projects
        /// </summary>
        /// <remarks>
        /// GET https://kanbanery.com/api/v1/user/workspaces.json
        /// </remarks>
        public async Task<IEnumerable<Workspace>> GetWorkspaces()
        {
            var str = await httpClient.GetStringAsync(GetUserWorkspacesUrl());
            return JsonConvert.DeserializeObject<IEnumerable<Workspace>>(str);            
        }

        #endregion

        #region User resource

        /// <summary>
        /// Getting the current user (the API TOKEN owner) information.
        /// </summary>
        /// <remarks>
        /// GET https://WORKSPACE.kanbanery.com/api/v1/user.json 
        /// </remarks>
        public async Task<User> GetUser()
        {
            var str = await httpClient.GetStringAsync(GetUrl("user"));
            return JsonConvert.DeserializeObject<User>(str);
        }

        /// <summary>
        /// Getting project users information
        /// </summary>
        /// <remarks>
        /// GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/users.json 
        /// </remarks>
        /// <param name="projectId"></param>
        public async Task<IEnumerable<User>> GetProjectUsers(int projectId)
        {
            var str = await httpClient.GetStringAsync(GetUrl($"projects/{projectId}/users"));
            return JsonConvert.DeserializeObject<IEnumerable<User>>(str);
        }

        #endregion

        #region ProjectMembership resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/memberships.json
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/memberships.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/memberships/PROJECT_MEMBERSHIP_ID.json 
        //PUT https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/memberships/PROJECT_MEMBERSHIP_ID.json 
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/memberships/PROJECT_MEMBERSHIP_ID.json 

        #endregion

        #region TaskType resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/task_types.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/task_types/TASK_TYPE_ID.json 

        #endregion

        #region Estimate resource

        /// <summary>
        /// Listing project's estimates
        /// </summary>
        /// <remarks>
        /// GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/estimates.json
        /// </remarks>
        /// <param name="projectId"></param>
        public async Task<IEnumerable<Estimate>> GetProjectEstimates(int projectId)
        {
            var str = await httpClient.GetStringAsync(GetUrl($"projects/{projectId}/estimates"));
            return JsonConvert.DeserializeObject<IEnumerable<Estimate>>(str);
        }

        //GET https://WORKSPACE.kanbanery.com/api/v1/estimates/ESTIMATE_ID.json

        /// <summary>
        /// Getting an estimate
        /// </summary>
        /// <remarks>
        /// GET https://WORKSPACE.kanbanery.com/api/v1/estimates/ESTIMATE_ID.json
        /// </remarks>
        /// <param name="estimateId"></param>
        public async Task<Estimate> GetEstimate(int estimateId)
        {
            var str = await httpClient.GetStringAsync(GetUrl($"estimates/{estimateId}"));
            return JsonConvert.DeserializeObject<Estimate>(str);
        }

        #endregion

        #region Column resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/columns.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/columns.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/columns/COLUMN_ID.json 
        //PUT https://WORKSPACE.kanbanery.com/api/v1/columns/COLUMN_ID.json 
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/columns/COLUMN_ID.json 

        #endregion

        #region Task resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/tasks.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/columns/COLUMN_ID/tasks.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/archive/tasks.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/icebox/tasks.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/tasks.json
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/icebox/tasks.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID.json 
        //PUT https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID.json 
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID.json
        //PUT https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/columns/last/tasks.json 

        #endregion

        #region TaskSearch resource

        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/icebox/tasks/search.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/board/tasks/search.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/archive/tasks/search.json

        #endregion

        #region TaskSubscription resource

        //POST https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/subscription.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/subscription.json
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/subscription.json 

        #endregion

        #region Comment resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/comments.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/comments.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/comments/COMMENT_ID.json 
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/comments/COMMENT_ID.json

        #endregion

        #region Subtask resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/subtasks.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/subtasks.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/subtasks/SUBTASK_ID.json 
        //PUT https://WORKSPACE.kanbanery.com/api/v1/subtasks/SUBTASK_ID.json
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/subtasks/SUBTASK_ID.json

        #endregion

        #region Issue resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/issues.json
        //POST https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/issues.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/issues/ISSUE_ID.json
        //PUT https://WORKSPACE.kanbanery.com/api/v1/issues/ISSUE_ID.json
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/issues/ISSUE_ID.json 

        #endregion

        #region Blocking resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/blockings.json
        //POST https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/blockings.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/blockings/BLOCKING_ID.json 
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/blockings/BLOCKING_ID.json

        #endregion

        #region [AddOns][Custom Fields] Project Field resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/addons/project_fields.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/projects/PROJECT_ID/addons/project_fields.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/addons/project_fields/PROJECT_FIELD_ID.json
        //PUT https://WORKSPACE.kanbanery.com/api/v1/addons/project_fields/PROJECT_FIELD_ID.json
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/addons/project_fields/PROJECT_FIELD_ID.json

        #endregion

        #region [AddOns][Custom Fields] Task Field resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/addons/task_fields.json
        //POST https://WORKSPACE.kanbanery.com/api/v1/tasks/TASK_ID/addons/task_fields.json
        //GET https://WORKSPACE.kanbanery.com/api/v1/addons/task_fields/TASK_FIELD_ID.json 
        //PUT https://WORKSPACE.kanbanery.com/api/v1/addons/task_fields/TASK_FIELD_ID.json
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/addons/task_fields/TASK_FIELD_ID.json 

        #endregion

        #region [AddOns][Custom Fields] Value Field resource

        //GET https://WORKSPACE.kanbanery.com/api/v1/addons/project_fields/PROJECT_FIELD_ID/value_fields.json 
        //POST https://WORKSPACE.kanbanery.com/api/v1/addons/project_fields/PROJECT_FIELD_ID/value_fields.json 
        //GET https://WORKSPACE.kanbanery.com/api/v1/addons/value_fields/VALUE_FIELD_ID.json 
        //PUT https://WORKSPACE.kanbanery.com/api/v1/addons/value_fields/VALUE_FIELD_ID.json 
        //DELETE https://WORKSPACE.kanbanery.com/api/v1/addons/value_fields/VALUE_FIELD_ID.json 

        #endregion

        public void Dispose()
        {
            if (httpClientOwned)
            {
                httpClient?.Dispose();
            }
        }

    }
}
