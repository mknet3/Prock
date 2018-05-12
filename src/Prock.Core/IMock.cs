namespace Prock.Core
{
    public interface IMock
    {
        string ContentType { get; }

        string Json { get; }

        string Route { get; }

        int StatusCode { get; }
    }
}