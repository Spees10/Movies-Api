using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public float Rate { get; set; }

        public string StoryLine { get; set; }

        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }

        public string GenreName { get; set; }
    }
}