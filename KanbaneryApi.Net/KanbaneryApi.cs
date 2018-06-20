using System;
using System.Net.Http;

namespace KanbaneryApi.Net
{
    public class KanbaneryApi
    {
        public HttpClient httpClient { get; set; }

        public KanbaneryApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
    }
}
