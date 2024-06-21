namespace HandlerService.DataAccess.Repositories.MessageHandler;

public class MyHttpMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(await request.Content.ReadAsStringAsync());
        return await base.SendAsync(request, cancellationToken);
    }
}