using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Rate;
using GSP.Domain.Games;
using GSP.Domain.Orders;

namespace GSP.BLL.Mapper
{
    public class GameAutoMapperProfile : Profile
    {
        public GameAutoMapperProfile()
        {
            CreateMap<GamePhoto, GamePhotoDto>()
                .ForMember(x => x.Photo, p => p.MapFrom(t => $"data:image/png;base64,{Convert.ToBase64String(t.Content)}"))
                .ForMember(x => x.Content, p => p.MapFrom(t => Convert.ToBase64String(t.Content))); ;

            CreateMap<GamePhotoDto, GamePhoto>()
                .ForMember(x => x.Content, p => p.MapFrom(t => Convert.FromBase64String(t.Content)))
                .ForMember(x => x.Game, p => p.Ignore());

            CreateMap<CreateGamePhotoDto, GamePhoto>()
                .ForMember(x => x.GameId, p => p.Ignore())
                .ForMember(x => x.GamePhotoId, p => p.Ignore())
                .ForMember(x => x.Game, p => p.Ignore())
                .ForMember(x => x.Content, p => p.MapFrom(t => Convert.FromBase64String(t.Photo)));

            CreateMap<GameDto, Game>()
                .ForMember(x => x.Photo, p => p.MapFrom(t => Convert.FromBase64String(t.PhotoContent)))
                .ForMember(x => x.Icon, p => p.MapFrom(t => Convert.FromBase64String(t.IconContent)))
                .ForMember(x => x.FileContent, p => p.MapFrom(t => Convert.FromBase64String(t.FileContent)))
                .ForMember(x => x.Rates, p => p.MapFrom(t => AutoMapper.Mapper.Map<List<RateDto>, List<Rate>>(t.Rates)))
                .ForMember(x => x.Photos, p => p.MapFrom(t => AutoMapper.Mapper.Map<List<GamePhotoDto>, List<GamePhoto>>(t.Photos)))
                .ForMember(x => x.Category, p => p.Ignore())
                .ForMember(x => x.Orders, p => p.Ignore())
                .ForMember(x => x.IsDeleted, p => p.Ignore());

            CreateMap<Game, GameDto>()
                .ForMember(x => x.Photo, p => p.MapFrom(t => $"data:image/png;base64,{Convert.ToBase64String(t.Photo)}"))
                .ForMember(x => x.Icon, p => p.MapFrom(t => $"data:image/png;base64,{Convert.ToBase64String(t.Icon)}"))
                .ForMember(x => x.File, p => p.MapFrom(t => $"{t.FileExtinction}base64,{Convert.ToBase64String(t.FileContent)}"))
                .ForMember(x => x.PhotoContent, p => p.MapFrom(t => Convert.ToBase64String(t.Photo)))
                .ForMember(x => x.IconContent, p => p.MapFrom(t => Convert.ToBase64String(t.Icon)))
                .ForMember(x => x.FileContent, p => p.MapFrom(t => Convert.ToBase64String(t.FileContent)))
                .ForMember(x => x.Rates, p => p.MapFrom(t => AutoMapper.Mapper.Map<ICollection<Rate>, List<RateDto>>(t.Rates)))
                .ForMember(x => x.Photos, p => p.MapFrom(t => AutoMapper.Mapper.Map<ICollection<GamePhoto>, List<GamePhotoDto>>(t.Photos)))
                .ForMember(x => x.AverageRate, p => p.MapFrom(t => t.Rates.Any() ? t.Rates.Average(x => x.Rating) : 0))
                .ForMember(x => x.CategoryName, p => p.MapFrom(t => t.Category.Name));

            CreateMap<CreateGameDto, Game>()
                .ForMember(x => x.Rates, p => p.Ignore())
                .ForMember(x => x.Orders, p => p.Ignore())
                .ForMember(x => x.Category, p => p.Ignore())
                .ForMember(x => x.IsDeleted, p => p.UseValue(false))
                .ForMember(x => x.Icon, p => p.MapFrom(t => Convert.FromBase64String(t.Icon)))
                .ForMember(x => x.Photo, p => p.MapFrom(t => Convert.FromBase64String(t.Photo)))
                .ForMember(x => x.FileContent, p => p.MapFrom(t => Convert.FromBase64String(t.File)));

            CreateMap<AddGameToBucketDto, OrderGame>()
                .ForMember(x => x.Order, p => p.Ignore())
                .ForMember(x => x.Game, p => p.Ignore());
        }
    }
}