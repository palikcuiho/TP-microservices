using Confluent.Kafka;
using UserService.Services;

namespace UserService.Kafka
{
    public class KafkaConsumer
    {


        private readonly IUsersService _iorderService;
        private readonly string _topic = "user-locked";
        private readonly string _groupId = "user-locked-group";
        private readonly string _bootstrapServers = "localhost:9092";

        public KafkaConsumer(IUsersService ordersService)
        {
            _iorderService = ordersService;
        }


        public void StartConsuming()
        {
            var config = new ConsumerConfig
            {
                GroupId = _groupId,
                BootstrapServers = _bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };


            using var consumer = new ConsumerBuilder<string,string>(config).Build();

            consumer.Subscribe(_topic);

            Console.WriteLine($"Listening for messages ont topic : {_topic} ");


            while (true)
            {

                var consumeResult = consumer.Consume();

                if(consumeResult != null)
                {
                    //var customerId = int.Parse(consumeResult.Message.Key);


                    //_iorderService.DeleteUsersByCustomerIdAsync(customerId).Wait();


                }



            }




        }




    }
}
