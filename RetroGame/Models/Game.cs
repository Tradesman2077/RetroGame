using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Range(1970, int.MaxValue)]
        public int ReleaseYear { get; set; }
        public string Image { get; set; }

        public string InfoText{get; set;}

        [Display(Name ="Platform Type")]
        public int PlatformId { get; set; }
        [ForeignKey("PlatformId")]
        public virtual Platform Platform { get; set; }


        [Display(Name = "Developer Name")]
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }


        [Display(Name = "Publisher Name")]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }
        public string CompletePrice { get; set; }
        public string ChangeInPrice { get; set; }

    }
}
