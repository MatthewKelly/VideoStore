using System.Collections.Generic;
using VideoStore.Models;

namespace VideoStore.Repositories
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();
        int CreateMovie(Movie movie);
        void UpdateMovie(Movie movie);
    }
}