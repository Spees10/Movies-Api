using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _GenresService;

        private List<string> _allowedExtentions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048567;

        public MoviesController(IMoviesService moviesService, IGenresService genresService)
        {
            _moviesService = moviesService;
            _GenresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound();

            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                GenreId = movie.GenreId,
                GenreName = movie.Genre.Name,
                Title = movie.Title,
                Year = movie.Year,
                Rate = movie.Rate,
                StoryLine = movie.StoryLine,
                Poster = movie.Poster
            };

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAll(genreId);

            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png & .jpg images are allowed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Only Max allowed size is 1mb!");

            var isValidGenre = await _GenresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid Genre ID!");

            using var dataStream = new MemoryStream();

            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Year = dto.Year,
                Rate = dto.Rate,
                StoryLine = dto.StoryLine,
                Poster = dataStream.ToArray()
            };

            await _moviesService.Create(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound($"No Movie was found with id{id}");

            var isValidGenre = await _GenresService.IsValidGenre(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid Genre ID!");

            if (dto.Poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png & .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Only Max allowed size is 1mb!");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }

            movie.GenreId = dto.GenreId;
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Rate = dto.Rate;
            movie.StoryLine = dto.StoryLine;

            _moviesService.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
                return NotFound($"No Movie was found with id{id}");

            _moviesService.Delete(movie);

            return Ok(movie);
        }
    }
}