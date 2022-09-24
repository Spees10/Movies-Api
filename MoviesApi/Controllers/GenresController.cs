using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        //==========================================================================

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genresService.GetAll();

            return Ok(genres);
        }

        //==========================================================================

        [HttpPost]
        public async Task<IActionResult> CreatAsync([FromBody] CreateGenreDto dto)
        {
            Genre genre = new() { Name = dto.Name };
            await _genresService.Create(genre);

            return Ok(genre);
        }

        //==========================================================================

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDto dto)
        {
            var genre = await _genresService.GetById(id);

            if (genre is null) return NotFound($"NO Genre was found with id -> {id}");

            genre.Name = dto.Name;

            _genresService.Update(genre);

            return Ok(genre);
        }

        //==========================================================================

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetById(id);

            if (genre is null) return NotFound($"NO Genre was found with id -> {id}");

            _genresService.Delete(genre);

            return Ok(genre);
        }
    }
}