namespace CreamuAPI.Models.DTO
{
    public class MembersDTO
    {
        public int MemberId { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public int Level { get; set; }

        public DateTime JoinDate { get; set; }

        public string? Image { get; set; }

        public string? Notes { get; set; }
    }
}
