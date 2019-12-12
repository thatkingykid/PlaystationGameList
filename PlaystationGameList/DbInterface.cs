using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaystationGameList
{
    public static class DbInterface
    {
        private static GameListDatabaseEntities context;
        public static IEnumerable<Game> ReturnAllGames()
        {
            context = new GameListDatabaseEntities();
            var games = context.Games.Include("GamePlatformMaps").Include("GamePlatformMaps.Platform");
            return games;
        }

        public static IEnumerable<Platform> GeneratePlatforms(IEnumerable<string> platformNames)
        {
            context = new GameListDatabaseEntities();
            List<Platform> platformObjects = new List<Platform>();
            foreach (var item in platformNames)
            {
                var platformModel = context.Platforms.Where(x => x.Name == item).FirstOrDefault();
                if (platformModel == null)
                {
                    var newPlatform = new Platform
                    {
                        Name = item
                    };
                    context.Platforms.Add(newPlatform);
                    context.SaveChanges();
                    platformObjects.Add(newPlatform);
                }
                else
                {
                    platformObjects.Add(platformModel);
                }
            }
            return platformObjects;
        }

        public static IEnumerable<Developer> GenerateDevelopers(IEnumerable<string> developerNames)
        {
            context = new GameListDatabaseEntities();
            List<Developer> devObjects = new List<Developer>();
            foreach (var item in developerNames)
            {
                var devModel = context.Developers.Where(x => x.Name == item).FirstOrDefault();
                if (devModel == null)
                {
                    var newDev = new Developer
                    {
                        Name = item
                    };
                    context.Developers.Add(newDev);
                    context.SaveChanges();
                    devObjects.Add(newDev);
                }
                else
                {
                    devObjects.Add(devModel);
                }
            }
            context.SaveChanges();
            return devObjects;
        }

        public static IEnumerable<Publisher> GeneratePublishers(IEnumerable<string> publisherNames)
        {
            context = new GameListDatabaseEntities();
            List<Publisher> publisherObjects = new List<Publisher>();
            foreach (var item in publisherNames)
            {
                var publisherModels = context.Publishers.Where(x => x.Name == item).FirstOrDefault();
                if (publisherModels == null)
                {
                    var newPublisher = new Publisher
                    {
                        Name = item
                    };
                    context.Publishers.Add(newPublisher);
                    context.SaveChanges();
                    publisherObjects.Add(newPublisher);
                }
                else
                {
                    publisherObjects.Add(publisherModels);
                }
            }
            context.SaveChanges();
            return publisherObjects;
        }

        public static IEnumerable<Genre> GenerateGenres(IEnumerable<string> genreNames)
        {
            context = new GameListDatabaseEntities();
            List<Genre> genreObjects = new List<Genre>();
            foreach (var item in genreNames)
            {
                var genreModel = context.Genres.Where(x => x.Name == item).FirstOrDefault();
                if (genreModel == null)
                {
                    var newGenre = new Genre
                    {
                        Name = item
                    };
                    context.Genres.Add(newGenre);
                    context.SaveChanges();
                    genreObjects.Add(newGenre);
                }
                else
                {
                    genreObjects.Add(genreModel);
                }
            }
            context.SaveChanges();
            return genreObjects;
        }

        public static Game AddGame(string name, DateTime releaseDate, int playerCount, string boxArtPath)
        {
            context = new GameListDatabaseEntities();
            var game = new Game
            {
                Name = name,
                ReleaseDate = releaseDate.Date,
                PlayerCount = playerCount,
                BoxArtPath = boxArtPath
            };
            context.Games.Add(game);
            context.SaveChanges();
            return game;
        }

        public static void CreateMappings(Game game, IEnumerable<Developer> developers, IEnumerable<Platform> platforms, IEnumerable<Publisher> publishers, IEnumerable<Genre> genres)
        {
            context = new GameListDatabaseEntities();
            foreach (var item in developers)
            {
                context.GameDevMaps.Add(new GameDevMap
                {
                    GameID = game.ID,
                    DevID = item.ID
                });
                context.SaveChanges();
            }

            foreach (var item in platforms)
            {
                context.GamePlatformMaps.Add(new GamePlatformMap
                {
                    GameID = game.ID,
                    PlatformID = item.ID
                });
                context.SaveChanges();
            }


            foreach (var item in publishers)
            {
                context.GamePublisherMaps.Add(new GamePublisherMap
                {
                    GameID = game.ID,
                    PublisherID = item.ID
                });
                context.SaveChanges();
            }

            foreach (var item in genres)
            {
                context.GameGenreMaps.Add(new GameGenreMap
                {
                    GameID = game.ID,
                    GenreID = item.ID
                });
                context.SaveChanges();
            }
            context.SaveChanges();
        }

        public static void Dispose()
        {
            context.Dispose();
        }
    }
}