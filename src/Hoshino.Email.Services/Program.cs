using System;
using Topshelf;

namespace Hoshino.Email.Services
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            MainServices s = new MainServices();
            s.Start(null);
            Console.Read();
            return;
#endif
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
