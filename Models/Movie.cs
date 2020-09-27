using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public Genre Genre { get; set; }

        [Display(Name="Genre")]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name ="Number in Stock")]
        [Range(1,30)]
        public int AmountInStock { get; set; }

        [Required]
        public DateTime PurchasedDate { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }

    }
}