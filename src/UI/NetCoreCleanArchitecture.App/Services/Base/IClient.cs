namespace NetCoreCleanArchitecture.App.Services;

public partial interface IClient
{
    public HttpClient HttpClient { get; }
}