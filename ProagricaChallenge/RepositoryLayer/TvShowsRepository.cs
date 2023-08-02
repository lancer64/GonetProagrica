using Microsoft.EntityFrameworkCore;
using ProagricaChallenge.DatabaseLayer;
using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProagricaChallenge.RepositoryLayer;

public class TvShowsRepository
{
    private readonly TvShowDbContext _context;

    public TvShowsRepository(TvShowDbContext tvShowDbContext)
    {
        this._context = tvShowDbContext;
    }

    public virtual async Task<List<TvShow>> GetFavoriteTvShows()
    {
        List<TvShow> searchResults = await Task.
            FromResult(_context.TvShows.Where(show => show.IsFavorite).ToList());
        if (searchResults.Count > 0)
        {
            return searchResults;
        }
        throw new NoTvShowSearchResults();
    }

    public virtual async Task<List<TvShow>> ListTvShows()
    {
        List < TvShow > searchResults = await Task.FromResult(_context.TvShows.ToList());
        if(searchResults.Count > 0)
        {
            return searchResults;
        }
        throw new NoTvShowSearchResults();
    }

    public virtual async Task<TvShow> GetTvShowById(int showId)
    {
        if (showId < 0)
        {
            Console.WriteLine($"Argument exception in GetTvShowById! ShowID = {showId}");
            throw new ArgumentException("Invalid argument provided");
        }

        return await Task.FromResult(_context.TvShows.FirstOrDefault(t => t.Id == showId))
            ?? throw new TvShowNotFoundException();
    }

    public virtual async Task<TvShow> UpdateTvShowById(int showId)
    {
        TvShow tvShow = await GetTvShowById(showId);
        tvShow.IsFavorite = !tvShow.IsFavorite;
        _context.SaveChanges();

        return tvShow;
}
}
