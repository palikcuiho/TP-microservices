using UserService.Models;

namespace BorrowingService.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public MembershipType MembershipType { get; set; } // ToString
        public bool IsLocked { get; set; }
        public int NombreMaxEmprunt { get; set; }
    }
}
