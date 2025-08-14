using System.ComponentModel.DataAnnotations;

namespace exercise1.models.DataEntities
{
    public record CustomerModel
    {
        [Key]
        public required int GuId { get; init; }
        public string? Name { get; init; }
        public string? Email { get; init; }
    }
}
