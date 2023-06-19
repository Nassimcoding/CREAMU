namespace FinalProject.Models
{
    public class CCPAddToCartViewMode
    {
        //要傳給購物車的內容
        //ModelID
        public int HeadID { get; set; }
        public int BodyID { get; set; }
        public int RHID { get; set; }
        public int LHID { get; set; }
        public int RFID { get; set; }
        public int LFID { get; set; }

        //MaterialID
        
        public int HeadID_M { get; set; }
        public int BodyID_M { get; set; }
        public int RHID_M { get; set; }
        public int LHID_M { get; set; }
        public int RFID_M { get; set; }
        public int LFID_M { get; set; }

        public int count { get; set; }
        public int price { get; set; }
    }
}
