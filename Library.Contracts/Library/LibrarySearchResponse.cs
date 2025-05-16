namespace Library.Contracts.Library
{
    public class LibrarySearchResponse
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Coordinate Coordinate { get; set; } = null!;
    }
}
