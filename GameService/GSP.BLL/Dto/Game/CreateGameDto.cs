using System.Collections.Generic;

namespace GSP.BLL.Dto.Game
{
    public class CreateGameDto
    {
        public int GameId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Photo { get; set; }

        public string Icon { get; set; }

        public string File { get; set; }

        public string FileExtinction { get; set; }

        public List<CreateGamePhotoDto> Photos { get; set; }
    }
}