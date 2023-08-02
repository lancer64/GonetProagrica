using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProagricaChallenge.DatabaseLayer.Models
{
    public class TvShow
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public bool IsFavorite { get; set; }
    }
}
