using UserService.Kafka;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _repository;
        private readonly KafkaProducer _kafkaproducer;

        public UsersService(IUserRepository userRepository, KafkaProducer kafkaProducer)
        {
            _repository = userRepository;
            _kafkaproducer = kafkaProducer;
        }
//Un emprunteur ne peut emprunter que 5 livres maximum quand il est Regular et 7 quand il est Premium.
//Quand il atteint le nombre maximum de livre, isLocked est à true. Il ne peut plus emprunter.

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _repository.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddUserAsync(User user) =>
            await _repository.AddAsync(user);

        public async Task UpdateUserAsync(User user) =>
            await _repository.UpdateAsync(user);

        public async Task DeleteUserAsync(int id)
        {
            await _kafkaproducer.sendMessageAsync("user-deleted", id.ToString(), $"User {id} deleted");
            await _repository.DeleteAsync(id);
        }
    }
}
