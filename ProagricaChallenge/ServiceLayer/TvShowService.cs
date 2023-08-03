using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.Exceptions;
using ProagricaChallenge.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProagricaChallenge.ServiceLayer
{
    /*
     * Services process the output of the repository in order to return more useful
     * and understandable information to the user.
     */
    public class TvShowService
    {
        private readonly TvShowsRepository _tvShowsRepository = default!;

        public TvShowService(TvShowsRepository tvShowsRepository)
        {
            _tvShowsRepository = tvShowsRepository;
        }

        public async void ShowTvShows()
        {
            try
            {
                List<TvShow> tvShows = await _tvShowsRepository.ListTvShows();
                PrintTvShows(tvShows);
            }
            catch (NoTvShowSearchResults)
            {
                Console.WriteLine("There are no TV shows in the database at this moment");
            }
        }

        public async void ShowFavorites()
        {
            try
            {
                List<TvShow> results = await _tvShowsRepository.GetFavoriteTvShows();
                PrintTvShows(results);
            }
            catch (NoTvShowSearchResults)
            {
                Console.WriteLine("There are no TV shows marked as favorite.");
            }
        }

        public async void UpdateTvShow(int id)
        {
            try{
                TvShow tvShow = await _tvShowsRepository.UpdateTvShowById(id);
                if (tvShow.IsFavorite)
                {
                    Console.WriteLine($"\"{tvShow.Name}\" was added to Favorites.");
                }
                else
                {
                    Console.WriteLine($"\"{tvShow.Name}\" was removed from Favorites.");
                }
            }
                        catch (TvShowNotFoundException)
            {
                Console.WriteLine("This show does not exist");
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to update the TV Show");
            }
        }

        public void PrintTvShows(List<TvShow> tvShows)
        {
            foreach(TvShow tvShow in tvShows)
            {
                Console.WriteLine($"{tvShow.Id}: {tvShow.Name} {(tvShow.IsFavorite ? " *" : "")}");
            }
        }
    }
}
