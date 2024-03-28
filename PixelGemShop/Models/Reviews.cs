namespace PixelGemShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reviews
    {
        [Key]
        public int IdReview { get; set; }

        public int IdUser { get; set; }

        public decimal Rate { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int IdProduct { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }
    }
}
