using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndCat.Models
{
    public class ProdAndCat
    {
        [Key]
        public int ProdAndCatId { get; set; }
        //foreign Key
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        //Nav Props
        public Product ListOfProducts { get; set; }
        public Category ListOfCategories { get; set; }
    }
}
