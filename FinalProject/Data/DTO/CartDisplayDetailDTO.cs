namespace FinalProject.Data.DTO
{
    public class CartDisplayDetailDTO
    {
        //orderdetailid
        public int id { get; set; }
        //memberid
        public int? mId { get; set; }
        //member name
        public string? mName { get; set; }
        //product id
        public int? pId { get; set; }
        //product name
        public string? pName { get; set; }
        //product descript
        public string? descript { get; set; }


        public int? qty { get; set; }

        public int? unitPrice { get; set; }

        public float? discount { get; set; }

        public int? subtotal { get; set; }

        public string? notes { get; set; }

        public string? type { get; set; }
    }
}
