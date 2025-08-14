namespace exercise1.models.Dto
{
    public class CustomerDto(int guId, string? name, string? email)
    {
        public required int GuId { get; init; } = guId;
        public string? Name { get; init; }
        public string? Email { get; init; }
    }
}
