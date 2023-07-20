namespace CreamuAPI.Models.DTO
{
    public class MembersCreateDTO
    {
        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public string? Image { get; set; }

        public string? Notes { get; set; }
    }
}
