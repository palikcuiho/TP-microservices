using BookService.Kafka;
using BookService.Models;
using BookService.Repositories;

namespace BookService.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBookRepository _repository;
        private readonly KafkaProducer _kafkaproducer;

        public BooksService(IBookRepository bookRepository, KafkaProducer kafkaProducer)
        {
            _repository = bookRepository;
            _kafkaproducer = kafkaProducer;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync() =>
            await _repository.GetAllAsync();

        public async Task<Book?> GetBookByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddBookAsync(Book book) =>
            await _repository.AddAsync(book);

        public async Task UpdateBookAsync(Book book) =>
            await _repository.UpdateAsync(book);

        public async Task DeleteBookAsync(int id)
        {
            await _kafkaproducer.sendMessageAsync("book-deleted", id.ToString(), $"Book {id} deleted");
            await _repository.DeleteAsync(id);
        }
            

    }
}
