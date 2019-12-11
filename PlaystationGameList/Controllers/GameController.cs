using PlaystationGameList.Models.Game_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaystationGameList.Controllers
{
    public class GameController : Controller
    {
        public ActionResult GetGamesForHomePage()
        {
            using (GameListDatabaseEntities context = new GameListDatabaseEntities())
            {
                var games = context.Games;
                List<GameCollapsedViewModel> returnModels = new List<GameCollapsedViewModel>();
                foreach(var item in games)
                {
                    returnModels.Add(new GameCollapsedViewModel
                    {
                        ID = item.ID,
                        Title = item.Name,
                        ImagePath = item.BoxArtPath,
                        Platforms = item.GamePlatformMaps.Select(x => x.Platform.Name).ToList()
                    });
                }

                return PartialView("_GameList", returnModels);
            }
        }
    }
}