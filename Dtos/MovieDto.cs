using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

       // public Genre Genre { get; set; }


        public byte GenreId { get; set; }

        [Required]

        [Range(1, 30)]
        public int AmountInStock { get; set; }

        [Required]
        public DateTime PurchasedDate { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }
    }
}