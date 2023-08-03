using Microsoft.EntityFrameworkCore;
using ProagricaChallenge.DatabaseLayer;
using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.RepositoryLayer;

namespace ProagricaChallenge_Tests;


[TestClass]
public class TvShowTest
{
    private TvShowDbContext _db = default!;
    private TvShowsRepository _repository = default!;

    [TestInitialize]
    public async Task TestInitialize()
    {
        DbContextOptions<TvShowDbContext> dbContextOptions =
            new DbContextOptionsBuilder<TvShowDbContext>()
            .UseInMemoryDatabase("TvShows").Options;
        _db = new TvShowDbContext(dbContextOptions);
        _repository = new TvShowsRepository(_db);

        List<TvShow> tvShows = new List<TvShow>
        {
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
        };
        _db.TvShows.AddRange(tvShows);
        await _db.SaveChangesAsync();
    }

    [TestMethod]
    public async Task GetList_Success()
    {
        List<TvShow> shows = await _repository.ListTvShows();
        Assert.IsNotNull(shows);
        foreach (TvShow show in shows)
        {
            Console.WriteLine(show.Name);
        }
    }
}