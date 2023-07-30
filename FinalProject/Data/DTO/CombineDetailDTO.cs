namespace FinalProject.Data.DTO
{
    public class CombineDetailDTO
    {
        public string CombineId { get; set; }
        public decimal? Chead { get; set; }
        public decimal? Cbody { get; set; }
        public decimal? Clhand { get; set; }
        public decimal? Crhand { get; set; }
        public decimal? Clfoot { get; set; }
        public decimal? Crfoot { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Type { get; set; }

        public virtual Component? CbodyNavigation { get; set; }
        public virtual Component? CheadNavigation { get; set; }
        public virtual Component? ClfootNavigation { get; set; }
        public virtual Component? ClhandNavigation { get; set; }
        public virtual Component? CrfootNavigation { get; set; }
        public virtual Component? CrhandNavigation { get; set; }
    }
}
