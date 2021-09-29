using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Models.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<SelectListItem> PlatformSelectList { get; set; }
        public IEnumerable<SelectListItem> DeveloperSelectList { get; set; }
        public IEnumerable<SelectListItem> PublisherSelectList { get; set; }
    }
}
