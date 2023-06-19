using FinalProject.Data;
using FinalProject.ViewModel;

namespace FinalProject.ViewModel
{
    public class CCustomizedProdViewModel
    {
        //用於顯示客製化頁面的產品們
        public IEnumerable<Material> Materials { get; set; }
        public IEnumerable<Model> Models { get; set; }
        public IEnumerable<Model> Head { get; set; }
        public IEnumerable<Model> Body { get; set; }
        public IEnumerable<Model> RH { get; set; }
        public IEnumerable<Model> LH { get; set; }
        public IEnumerable<Model> RF { get; set; }
        public IEnumerable<Model> LF { get; set; }
    }
}
