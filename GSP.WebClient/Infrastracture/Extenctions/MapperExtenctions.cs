using System;
using System.Collections.Generic;
using System.Linq;
using GSP.Domain.Games;
using GSP.Domain.Orders;
using GSP.WebClient.ViewModels;

namespace GSP.WebClient.Infrastracture.Extenctions
{
    public class MapperExtenctions
    {
        public static GameViewModel ToGameViewModel(Game game)
        {
            var gameViewModel = new GameViewModel();

            AutoMapper.Mapper.Map(game, gameViewModel);

            if (game.Photo != null)
            {
                var data = Convert.ToBase64String(game.Photo);
                gameViewModel.PhotoData = $"data:image/png;base64,{data}";
            }

            return gameViewModel;
        }

        public static OrderViewModel ToOrderViewModel(Order order)
        {
            var orderViewModel = new OrderViewModel();

            AutoMapper.Mapper.Map(order, orderViewModel);

            return orderViewModel;
        }

        public static IEnumerable<GameViewModel> ToGameViewModels(IEnumerable<Game> games)
        {
            var result = new List<GameViewModel>();

            foreach (var game in games)
            {
                result.Add(ToGameViewModel(game));
            }

            return result;
        }

        public static IEnumerable<OrderViewModel> ToOrderViewModels(IEnumerable<Order> orders)
        {
            var result = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var orderViewModel = ToOrderViewModel(order);
                orderViewModel.Games = ToGameViewModels(order.Games.Select(g=> g.Game).ToList()).ToList();
                result.Add(orderViewModel);                
            }

            return result;
        }
    }
}
