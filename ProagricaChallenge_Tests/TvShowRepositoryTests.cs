using Microsoft.EntityFrameworkCore;
using ProagricaChallenge.DatabaseLayer;
using ProagricaChallenge.DatabaseLayer.Models;
using ProagricaChallenge.Exceptions;
using ProagricaChallenge.RepositoryLayer;

namespace ProagricaChallenge_Tests;


[TestClass]
public class TvShowRepositoryTests
{
    private TvShowDbContext _context = default!;
    private TvShowsRepository _repository = default!;

    /*
     * UseInMemoryDatabase helps me to test different scenarios without affecting the
     * database of the application.
     */
    [TestInitialize]
    public async Task TestInitialize()
    {
        //the id of the database changes in order to get the ids reset
        DbContextOptions<TvShowDbContext> dbContextOptions =
            new DbContextOptionsBuilder<TvShowDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new TvShowDbContext(dbContextOptions);
        _repository = new TvShowsRepository(_context);

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

        _context.TvShows.AddRange(tvShows);
        await _context.SaveChangesAsync();
    }

    [TestCleanup]
    public void Cleanup()
    {
        // This removes the database from memory
        _context.Database.EnsureDeleted(); 
        _context.Dispose();
    }

    [TestMethod]
    public async Task ListTvShows_Success()
    {
        List<TvShow> shows = await _repository.ListTvShows();
        Assert.IsNotNull(shows);
        Assert.AreEqual(4, shows.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(NoTvShowSearchResults))]
    public async Task ListTvShows_Fail()
    {
        _context.TvShows.RemoveRange(_context.TvShows);
        await _context.SaveChangesAsync();
        List<TvShow> shows = await _repository.ListTvShows();
    }

    [TestMethod]
    public async Task GetTvShowById_Success()
    {
        TvShow show = await _repository.GetTvShowById(1);
        Assert.IsNotNull(show);
        Assert.AreEqual("Bocchi the Rock", show.Name);
        Assert.IsTrue(show.IsFavorite);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(10)]
    [ExpectedException(typeof(TvShowNotFoundException))]
    public async Task GetTvShowById_Fail_NotFound(int id)
    {
        TvShow show = await _repository.GetTvShowById(id);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(-10)]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetTvShowById_Fail_InvalidId(int id)
    {
        TvShow show = await _repository.GetTvShowById(id);
    }

    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    public async Task UpdateTvShowById_Success(int id)
    {
        TvShow show = await _repository.UpdateTvShowById(id);
        Assert.IsNotNull(show);
        Assert.IsFalse(show.IsFavorite);
    }

    [TestMethod]
    [DataRow(5)]
    [DataRow(90)]
    [ExpectedException(typeof(TvShowNotFoundException))]
    public async Task UpdateTvShowById_Fail_NotFound(int id)
    {
        TvShow show = await _repository.UpdateTvShowById(id);
    }

    [TestMethod]
    public async Task GetFavoriteTvShows_Success()
    {
        List<TvShow> tvShows = await _repository.GetFavoriteTvShows();
        Assert.IsNotNull(tvShows);
        Assert.AreEqual(2, tvShows.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(NoTvShowSearchResults))]
    public async Task GetFavoriteTvShows_Fail_NoFavorites()
    {
        TvShow show1 = await _repository.UpdateTvShowById(1);
        Assert.IsNotNull(show1);
        TvShow show2 = await _repository.UpdateTvShowById(2);
        Assert.IsNotNull(show2);
        List<TvShow> tvShows = await _repository.GetFavoriteTvShows();
    }
}