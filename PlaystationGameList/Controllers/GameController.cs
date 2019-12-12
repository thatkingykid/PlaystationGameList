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
            var games = DbInterface.ReturnAllGames();
            List<GameCollapsedViewModel> returnModels = new List<GameCollapsedViewModel>();
            foreach (var item in games)
            {
                returnModels.Add(new GameCollapsedViewModel
                {
                    ID = item.ID,
                    Title = item.Name,
                    ImagePath = item.BoxArtPath,
                    Platforms = item.GamePlatformMaps.Select(x => x.Platform.Name).ToList()
                });
            }
            DbInterface.Dispose();
            return PartialView("_GameList", returnModels);
        }

        public ActionResult _NewGame()
        {
            var model = new CreateGameFormModel();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AddGame(CreateGameFormModel model)
        {
            if (!ModelState.IsValid)
            {
                List<string> errorMessages = new List<string>();
                foreach (var item in ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
                Response.StatusCode = 400;
                return Json(errorMessages);
            }

            try
            {
                using (GameListDatabaseEntities context = new GameListDatabaseEntities())
                {
                    var platforms = model.Platform.Split(',');
                    var devs = model.Developer.Split(',');
                    var publishers = model.Publisher.Split(',');
                    var genre = model.Genre.Split(',');

                    var platformDbModels = DbInterface.GeneratePlatforms(platforms);
                    var devDbModels = DbInterface.GenerateDevelopers(devs);
                    var publisherDbModels = DbInterface.GeneratePublishers(publishers);
                    var genreDbModels = DbInterface.GenerateGenres(genre);
                    DbInterface.Dispose();

                    if (string.IsNullOrEmpty(model.BoxArtPath))
                    {
                        model.BoxArtPath = "/";
                    }

                    var gameDbModel = DbInterface.AddGame(model.Name, DateTime.Parse(model.ReleaseDate), model.PlayerCount, model.BoxArtPath);
                    DbInterface.Dispose();

                    DbInterface.CreateMappings(gameDbModel, devDbModels, platformDbModels, publisherDbModels, genreDbModels);
                    DbInterface.Dispose();
                    Response.StatusCode = 200;
                    return GetGamesForHomePage();
                }
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message);
            }
        }
    }
}