namespace FinalProject.Models
{
    public class CCPShoppingCartItem
    {
        public int HeadCom { get; set; }
        public int BodyCom { get; set; }
        public int RHCom { get; set; }
        public int LHCom { get; set; }
        public int RFCom { get; set; }
        public int LFCom { get; set; }
        public int SubTota { get; set; }//一組公仔的價格//還沒算出來
        public int count { get; set; }//買幾個公仔
        public int TotalP { get; set; }//買幾個公仔*單價//還沒算出來

    }
}
