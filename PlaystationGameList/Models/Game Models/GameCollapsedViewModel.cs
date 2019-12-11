using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaystationGameList.Models.Game_Models
{
    public class GameCollapsedViewModel
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public List<string> Platforms { get; set; }
    }
}