using FinalProject.Data;
using FinalProject.Models;
using FinalProject.ViewModel;

namespace FinalProject.ViewModel
{
    public class CCustomizedProdViewModel
    {
        //用於顯示客製化頁面的產品們
        public IEnumerable<ReadMaterial> Materials { get; set; }
        public IEnumerable<ReadModel> Models { get; set; }
        public IEnumerable<ReadModel> Head { get; set; }
        public IEnumerable<ReadModel> Body { get; set; }
        public IEnumerable<ReadModel> RH { get; set; }
        public IEnumerable<ReadModel> LH { get; set; }
        public IEnumerable<ReadModel> RF { get; set; }
        public IEnumerable<ReadModel> LF { get; set; }
    }
}
