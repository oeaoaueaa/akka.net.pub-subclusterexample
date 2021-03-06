﻿using System;
using System.Configuration;
using Akka.Actor;
using Akka.Configuration.Hocon;
using Akka.Routing;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var akkaConfigSection = ((AkkaConfigurationSection)ConfigurationManager.GetSection("akka")).AkkaConfig;
            var actorSystem = ActorSystem.Create("pubsubtest", akkaConfigSection);

            var subscribers = actorSystem.ActorOf(Props.Create(() => new Actors.Subscriber()).WithRouter(FromConfig.Instance), "subscriber");

            Console.ReadLine();
        }
    }
}
