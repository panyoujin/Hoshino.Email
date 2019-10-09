using Topshelf;

namespace Hoshino.Email.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<MainServices>();
                x.SetDescription("HoshinoEmail");
                x.SetDisplayName("HoshinoEmail");
                x.SetServiceName("HoshinoEmail");
            });
        }
    }
}
