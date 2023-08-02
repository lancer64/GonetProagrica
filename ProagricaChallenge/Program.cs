using ProagricaChallenge.DatabaseLayer;
using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.RepositoryLayer;
using ProagricaChallenge.ServiceLayer;
using System;


namespace ProagricaChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            TvShowDbContext db = new TvShowDbContext();
            TvShowsRepository rep = new TvShowsRepository(db);
            PopulateDatabase(rep, db);

            TvShowService _service = new TvShowService(rep);
            _service.RunMenu();
        }

        private async static void PopulateDatabase(TvShowsRepository _repository, TvShowDbContext _context)
        {
            List<TvShow> shows = await _repository.ListTvShows();
            if (shows.Count == 0)
            {
                List<TvShow> newShows = new List<TvShow> {
                    new TvShow()
                    {
                        Name = "Bocchi the Rock",
                        IsFavorite = true,
                    },
                    new TvShow()
                    {
                        Name = "Attack On Titan",
                        IsFavorite = true,
                    },
                    new TvShow()
                    {
                        Name = "Community",
                        IsFavorite = false,
                    },
                    new TvShow()
                    {
                        Name = "Breaking Bad",
                        IsFavorite = false,
                    },
                    new TvShow()
                    {
                        Name = "Gravity Falls",
                        IsFavorite = false,
                    },
                    new TvShow()
                    {
                        Name = "The Crown",
                        IsFavorite = true,
                    },
                    new TvShow()
                    {
                        Name = "Cobra Kai",
                        IsFavorite = false,
                    },
                };
                foreach (TvShow show in newShows)
                {
                    _context.TvShows.Add(show);
                };
                _context.SaveChanges();
            }
        }
    }
}
