using System;
using System.Configuration;
using Akka.Actor;
using Akka.Configuration.Hocon;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var akkaConfigSection = ((AkkaConfigurationSection)ConfigurationManager.GetSection("akka")).AkkaConfig;
            var actorSystem = ActorSystem.Create("pubsubtest", akkaConfigSection);

            var publisher = actorSystem.ActorOf(Props.Create(() => new Actors.Publisher()), "publisher");

            Console.WriteLine("press [q] to exit");
            string message;

            do
            {
                Console.Write(">> ");
                message = Console.ReadLine();
                publisher.Tell(message);
            } while (!"q".Equals(message, StringComparison.CurrentCultureIgnoreCase));

            Console.ReadLine();
        }
    }
}
