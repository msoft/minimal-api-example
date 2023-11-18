namespace MinApiExample
{
    public static class Example
    {
        public static Task Get(HttpContext context)
        {
            return Task.Run(() => "This is a GET");
        }

    }

    public interface IServiceToInject
    {
        string InnerMember { get; }
    }
    public class ServiceToInject: IServiceToInject
    {
        public string InnerMember => "Inner value";
    }


}
