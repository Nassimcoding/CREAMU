using System;
using System.Collections.Generic;

namespace Product_new.Models
{
    public partial class Member
    {
        public Member()
        {
            CreditcardInfos = new HashSet<CreditcardInfo>();
            Messages = new HashSet<Message>();
            Orders = new HashSet<Order>();
            PostAddresses = new HashSet<PostAddress>();
            TempOrderDetails = new HashSet<TempOrderDetail>();
            TrackingLists = new HashSet<TrackingList>();
        }

        public int MemberId { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public string? Password { get; set; }
        public int? EmailId { get; set; }
        public string? Address { get; set; }
        public DateOnly? Birthday { get; set; }
        public int? Level { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? Image { get; set; }
        public string? Notes { get; set; }

        public virtual Account? Email { get; set; }
        public virtual ICollection<CreditcardInfo> CreditcardInfos { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PostAddress> PostAddresses { get; set; }
        public virtual ICollection<TempOrderDetail> TempOrderDetails { get; set; }
        public virtual ICollection<TrackingList> TrackingLists { get; set; }
    }
}
