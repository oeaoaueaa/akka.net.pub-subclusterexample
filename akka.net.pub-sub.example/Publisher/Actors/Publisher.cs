using System;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Messages;

namespace Publisher.Actors
{
    class Publisher : ReceiveActor
    {
        public Publisher()
        {
            Ready();
        }

        private void Ready()
        {
            ReceiveAsync<string>(async s =>
            {
                try
                {
                    Console.WriteLine($"Self: {Context.Self.Path}");
                    var mediator = DistributedPubSub.Get(Context.System).Mediator;

                    {
                        var currentTopics = await mediator.Ask<CurrentTopics>(GetTopics.Instance, TimeSpan.FromSeconds(1));

                        Console.WriteLine($"Current Topics {currentTopics?.Topics?.Count}");
                    }



                    var value = await mediator.Ask<string>(new Publish("topic-name", new WorkitemMessage(s)),
                        TimeSpan.FromSeconds(1));

                    Console.WriteLine($"Response : {value}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Timed out");
                }
            });
        }
    }
}
