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
            Receive<string>(s =>
            {
                var mediator = DistributedPubSub.Get(Context.System).Mediator;
                
                mediator.Tell(new Publish("topic-name", new WorkitemMessage(s)));
            });
        }
    }
}
