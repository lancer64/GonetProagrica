using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.Exceptions;
using ProagricaChallenge.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProagricaChallenge.ServiceLayer
{
    internal class TvShowService
    {
        private readonly TvShowsRepository _tvShowsRepository = default!;

        public TvShowService(TvShowsRepository tvShowsRepository)
        {
            _tvShowsRepository = tvShowsRepository;
        }

        public async void RunMenu()
        {
            bool execute = true;
            Console.WriteLine("Welcome.");

            while (execute)
            {
                try
                {
                    Console.Write("\n--Type a command: ");
                    string command = Console.ReadLine() ?? string.Empty; ;
                    int t;
                    if (Int32.TryParse(command, out t))
                    {
                        int commandID = Convert.ToInt32(command);

                        try
                        {
                            TvShow tvShow = await _tvShowsRepository.UpdateTvShowById(commandID);
                            if(tvShow.IsFavorite)
                            {
                                Console.WriteLine($"\"{tvShow.Name}\" was added to Favorites.");
                            } else
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
                    else
                    {
                        switch (command)
                        {
                            case "list":
                                try
                                {
                                    List<TvShow> tvShows = await _tvShowsRepository.ListTvShows();
                                    printTvShows(tvShows);
                                }
                                catch (NoTvShowSearchResults)
                                {
                                    Console.WriteLine("There are no TV shows in the database at this moment");
                                }
                                break;
                            case "favorites":
                                try
                                {
                                    List<TvShow> results = await _tvShowsRepository.GetFavoriteTvShows();
                                    printTvShows(results);
                                }
                                catch (NoTvShowSearchResults)
                                {
                                    Console.WriteLine("There are no TV shows marked as favorite.");
                                }
                                break;
                            case "quit":
                                Console.WriteLine("Goodbye.");
                                execute = false;
                                break;
                            default:
                                Console.WriteLine($"Command \"{command}\" does not exist.");
                                break;
                        }
                    }
                } catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void printTvShows(List<TvShow> tvShows)
        {
            foreach(TvShow tvShow in tvShows)
            {
                Console.WriteLine($"{tvShow.Id}: {tvShow.Name} {(tvShow.IsFavorite ? " *" : "")}");
            }
        }
    }
}
