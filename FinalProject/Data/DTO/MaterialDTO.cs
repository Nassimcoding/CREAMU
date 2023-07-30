namespace FinalProject.Data.DTO
{
    public class MaterialDTO
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; } = null!;
        public string? Info { get; set; }
        public int Price { get; set; }
    }
}
