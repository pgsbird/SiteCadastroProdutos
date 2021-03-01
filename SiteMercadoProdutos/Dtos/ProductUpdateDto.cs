using System.ComponentModel.DataAnnotations;

namespace SiteMercadoProdutos.Dtos
{
    public class ProductUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Value { get; set; }       

        [Required]
        public string Image { get; set; }  
    }
}