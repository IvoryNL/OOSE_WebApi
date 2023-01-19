using System.Net;
using System.Web.Http;

namespace WebAPI.ActionResults
{
    public class ConflictWithMessageResult : IHttpActionResult
    {
        private string message;

        public ConflictWithMessageResult(string message)
        {
            this.message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            response.Content = new StringContent(message);
            return Task.FromResult(response);
        }
    }
}
