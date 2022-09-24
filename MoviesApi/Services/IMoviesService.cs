using MoviesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);

        Task<Movie> GetById(int id);

        Task<Movie> Create(Movie movie);

        Movie Update(Movie movie);

        Movie Delete(Movie movie);
    }
}