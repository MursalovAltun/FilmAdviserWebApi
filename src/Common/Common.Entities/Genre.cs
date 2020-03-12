using Common.Entities.Enums;

namespace Common.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public string ExternalId { get; set; }

        public GenreType Type { get; set; }
    }
}