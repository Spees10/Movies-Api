using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Models
{
    public class Movie
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public float Rate { get; set; }

        [MaxLength(2500)]
        public string StoryLine { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}