namespace Pet.Project.Api.Gateway.API.Handlers
{
    public class EncodingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.AcceptEncoding.Clear();
            return base.SendAsync(request, cancellationToken);
        }
    }
}