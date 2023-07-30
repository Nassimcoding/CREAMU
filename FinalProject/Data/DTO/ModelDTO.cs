namespace FinalProject.Data.DTO
{
    public class ModelDTO
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; } = null!;
        public string ModelType { get; set; } = null!;
        public string? Info { get; set; }
        public int Price { get; set; }
    }
}
