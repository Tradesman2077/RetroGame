using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetroGame.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [DisplayName ("Display Order")]
        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="Must be greater than one and not empty")]
        public int DisplayOrder { get; set; }
    }
}
