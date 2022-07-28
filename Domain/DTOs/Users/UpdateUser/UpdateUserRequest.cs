namespace Domain.DTOs.Users.UpdateUser
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
    }
}