using Confluent.Kafka;
using BorrowingService.Services;

namespace BorrowingService.Kafka
{
    public class KafkaConsumer
    {


        private readonly IBorrowingsService _borrowingService;
        private readonly string _topic = "book-deleted";
        private readonly string _topic2 = "user-deleted"; //TODO : refactor
        private readonly string _groupId = "book-deleted-group";
        private readonly string _bootstrapServers = "localhost:9092";

        public KafkaConsumer(IBorrowingsService ordersService)
        {
            _borrowingService = ordersService;
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

            //consumer.Subscribe(_topic);
            consumer.Subscribe([_topic, _topic2]);

            Console.WriteLine($"Listening for messages on topics : {_topic}, {_topic2} ");


            while (true)
            {

                var consumeResult = consumer.Consume();

                if(consumeResult != null)
                {
                    if (consumeResult.Topic == _topic) { 
                    var bookId = int.Parse(consumeResult.Message.Key);

                    Console.WriteLine($"Book {bookId} deleted, deleting borrowings...");
                    _borrowingService.DeleteBorrowingsByBookIdAsync(bookId).Wait();
                    }
                    else if (consumeResult.Topic == _topic2)
                    {
                        var userId = int.Parse(consumeResult.Message.Key);

                        Console.WriteLine($"User{userId} deleted, deleting borrowings...");
                        _borrowingService.DeleteBorrowingsByBookIdAsync(userId).Wait();
                    }


                }



            }




        }




    }
}
