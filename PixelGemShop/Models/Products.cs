namespace PixelGemShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            CartItems = new HashSet<CartItems>();
            OrderDetails = new HashSet<OrderDetails>();
            Reviews = new HashSet<Reviews>();
        }

        [Key]
        public int IdProduct { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int IdCategory { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Discount percentage")]
        public int? DiscountPercentage { get; set; }
        public decimal? AverageRating { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItems> CartItems { get; set; }

        public virtual Categories Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
