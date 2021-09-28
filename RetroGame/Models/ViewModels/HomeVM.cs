using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Developer> Developers { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }

    }

}
