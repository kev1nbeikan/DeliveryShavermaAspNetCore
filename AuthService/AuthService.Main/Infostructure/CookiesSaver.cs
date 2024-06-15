namespace AuthService.Main.Infostructure;

public class CookiesSaver : IDisposable
{
    IResponseCookies _cookies;
    private List<string> _domains = new();
    
    
    public CookiesSaver(IResponseCookies cookies)
    {
        _cookies = cookies;
    }

    public CookiesSaver SetDomains(List<string> domains)
    {
        _domains.AddRange(domains);
        return this;
    }

    public CookiesSaver SetDomain(string domain)
    {
        _domains.Add(domain);
        return this;
    }

    public CookiesSaver Append(string key, string value)
    {
        _cookies.Append(key, value);
        foreach (var domain in _domains)
        {
            _cookies.Append(key, value, new CookieOptions
            {
                Domain = domain
            });
        }

        return this;
    }


    public void Dispose()
    {
        _domains.Clear();
    }
    
}