namespace PixelGemShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartItems
    {
        [Key]
        public int IdCartItem { get; set; }

        public int IdCart { get; set; }

        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        public virtual Carts Carts { get; set; }

        public virtual Products Products { get; set; }
    }
}
