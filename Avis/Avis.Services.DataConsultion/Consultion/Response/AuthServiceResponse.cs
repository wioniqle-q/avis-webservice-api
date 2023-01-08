namespace Avis.Services.DataConsultion.Consultion.Response;

public class AuthServiceResponse
{
    public virtual string Response
    {
        get => response;
        set => response = value;
    }
    private volatile string response;

    public AuthServiceResponse()
    {

    }

    public AuthServiceResponse(string response)
    {
        Response = response;
    }
}