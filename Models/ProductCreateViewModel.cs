using SportProduct.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using SportProduct.Validation;

namespace SportProduct.Models
{
    /// <summary>
    /// ViewModel for creating a new product.
    /// </summary>
    public class ProductCreateViewModel
    {
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        public string? NamePro { get; set; }

        [Display(Name = "Mô tả")]
        public string? DecriptionPro { get; set; }

        [Display(Name = "Phân loại")]
        [Required(ErrorMessage = "Vui lòng chọn một phân loại.")]
        public string? Category { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Display(Name = "Đường dẫn hình ảnh")]
        public string? ImagePro { get; set; }

        [Display(Name = "Ngày sản xuất")]
        [Required(ErrorMessage = "Ngày sản xuất không được để trống.")]
        [DataType(DataType.Date)]
        [PastDate] // Custom validation attribute
        public DateTime ManufacturingDate { get; set; } = DateTime.Now.AddDays(-1);
    }
}
