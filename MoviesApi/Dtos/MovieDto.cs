using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Dtos
{
    public class MovieDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public float Rate { get; set; }

        [MaxLength(2500)]
        public string StoryLine { get; set; }

        public IFormFile Poster { get; set; }

        public byte GenreId { get; set; }
    }
}