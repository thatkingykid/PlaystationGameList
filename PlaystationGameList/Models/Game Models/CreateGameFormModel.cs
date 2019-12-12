using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlaystationGameList.Models.Game_Models
{
    public class CreateGameFormModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int PlayerCount { get; set; }
        public string BoxArtPath { get; set; }
    }
}