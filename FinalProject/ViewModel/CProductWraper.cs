using FinalProject.Data;
using System.ComponentModel;

namespace FinalProject.ViewModel
{
    public class CProductWraper
    {
        private Product _product;

        public CProductWraper()
        {
            if (_product == null)
                _product = new Product();
        }

        public Product product
        {
            get { return _product; }
            set { _product = value; }
        }

        public string? ProductImage
        {
            get { return _product.ProductImage; }
            set { _product.ProductImage = value; }
        }

        public int ProductId
        {
            get { return _product.ProductId; }
            set { _product.ProductId = value; }
        }

        [DisplayName("名稱")]
        public string? ProductName
        {
            get { return _product.ProductName; }
            set { _product.ProductName = value; }
        }

        [DisplayName("敘述")]
        public string? Descript
        {
            get { return _product.Descript; }
            set { _product.Descript = value; }
        }

        public int? CategoryId
        {
            get { return _product.CategoryId; }
            set { _product.CategoryId = value; }
        }

        [DisplayName("售價")]
        public int? Price
        {
            get { return _product.Price; }
            set { _product.Price = value; }
        }

        [DisplayName("狀態")]
        public string? ProductStatus
        {
            get { return _product.ProductStatus; }
            set { _product.ProductStatus = value; }
        }

        [DisplayName("庫存")]
        public int? ProductStock
        {
            get { return _product.ProductStock; }
            set { _product.ProductStock = value; }
        }

        [DisplayName("上架日期")]
        public string? ReleaseDate
        {
            get { return _product.ReleaseDate; }
            set { _product.ReleaseDate = value; }
        }

        [DisplayName("更新日期")]
        public string? UpdatedDate
        {
            get { return _product.UpdatedDate; }
            set { _product.UpdatedDate = value; }
        }

        [DisplayName("類型")]
        public string? Type
        {
            get { return _product.Type; }
            set { _product.Type = value; }
        }

        public IFormFile photo { get; set; }
    }
}
