using AutoMapper;
using BorrowingService.Clients;
using BorrowingService.DTO;
using BorrowingService.Models;
using BorrowingService.Repositories;

namespace BorrowingService.Services
{
    public class BorrowingsService : IBorrowingsService
    {
        private readonly IBorrowingRepository _repository;
        private readonly IBookServiceClient _bookServiceClient;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IMapper _mapper;
        public BorrowingsService(IBorrowingRepository borrowingRepository, IBookServiceClient bookServiceClient, IUserServiceClient userServiceClient, IMapper mapper)
        {
            _repository = borrowingRepository;
            _bookServiceClient = bookServiceClient;
            _userServiceClient = userServiceClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BorrowingDTO>> GetAllBorrowingsAsync()
        {
            var borrowings = await _repository.GetAllAsync();
            var borrowingDtos = new List<BorrowingDTO>();

            foreach (var borrowing in borrowings)
            {
                var borrowingDto = _mapper.Map<BorrowingDTO>(borrowing);
                borrowingDtos.Add(borrowingDto);
            }
            return borrowingDtos;
        }


        public async Task<BorrowingDTO?> GetBorrowingByIdAsync(int id) =>
            _mapper.Map<BorrowingDTO>(await _repository.GetByIdAsync(id));

        public async Task<bool> BorrowBookAsync(BorrowingDTO borrowing) {
            var a = await _userServiceClient.UserExistsAndIsNotLockedAsync(borrowing.UserId);
            var b = await _bookServiceClient.BookExistsAndIsAvailableAsync(borrowing.BookId);
            if (await _userServiceClient.UserExistsAndIsNotLockedAsync(borrowing.UserId) && await _bookServiceClient.BookExistsAndIsAvailableAsync(borrowing.BookId)) {
                await _bookServiceClient.ChangeBookAvailabilityAsync(borrowing.BookId, false);
                return await _repository.AddAsync(_mapper.Map<Borrowing>(borrowing));
            }
                
            return false;
        }
        public async Task<bool> ReturnBookAsync(BorrowingDTO borrowing)
        {
            await _bookServiceClient.ChangeBookAvailabilityAsync(borrowing.BookId, true);
            return await UpdateBorrowingAsync(borrowing);
        }

        public async Task<bool> UpdateBorrowingAsync(BorrowingDTO borrowing) =>
            await _repository.UpdateAsync(_mapper.Map<Borrowing>(borrowing));

        public async Task DeleteBorrowingAsync(int id) =>
            await _repository.DeleteAsync(id);

        public async Task<bool> DeleteBorrowingsByBookIdAsync(int bookId)
        {
            return await _repository.DeleteByBookIdAsync(bookId);
        }
    }
}
