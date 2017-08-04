using System;
using System.ComponentModel;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Messages;

namespace Subscriber.Actors
{
    class Subscriber : ReceiveActor
    {
        public Subscriber()
        {
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("topic-name", Self));

            Ready();
        }

        private void Ready()
        {
            Receive<SubscribeAck>(subscribedAck =>
            {
                Become(Subscribed);
            });
        }

        public void Subscribed()
        {
            Console.WriteLine("Subscribed");

            Receive<WorkitemMessage>(wim =>
            {
                Console.WriteLine($"<< {wim.Message}");
                Console.WriteLine($"Path {Self.Path}");

                Sender.Tell($"***{wim.Message}***");
            });
        }
    }
}
