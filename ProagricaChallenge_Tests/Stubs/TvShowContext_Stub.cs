using Microsoft.EntityFrameworkCore;
using ProagricaChallenge.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProagricaChallenge_Tests.Stubs
{
    public class TvShowContext_Stub: TvShowDbContext
    {
        public TvShowContext_Stub(DbContextOptions<TvShowDbContext> options) : base(options) {
            base.Database.EnsureDeleted();
        }
    }
}
