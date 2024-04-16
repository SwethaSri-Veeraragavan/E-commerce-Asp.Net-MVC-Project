using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bigbasket.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Id")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        public double Mrp { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double Offer { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Required]
        [Display(Name = "About Products")]
        public string AboutProduct { get; set; }

        [Required]
        public string Benefit { get; set; }

        [Required]
        public string Uses { get; set; }

        [Required]        
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
