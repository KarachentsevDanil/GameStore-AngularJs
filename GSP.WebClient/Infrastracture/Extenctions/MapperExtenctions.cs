using System;
using GSP.Domain.Games;
using GSP.WebClient.ViewModels;

namespace GSP.WebClient.Infrastracture.Extenctions
{
    public class MapperExtenctions
    {
        public static GameViewModel ToGameViewModel(Game game)
        {
            var gameViewModel = new GameViewModel();

            AutoMapper.Mapper.Map(game, gameViewModel);

            var data = Convert.ToBase64String(game.Photo);
            gameViewModel.PhotoData = $"data:image/png;base64,{data}";

            return gameViewModel;
        }
    }
}
