namespace Library.Contracts.Book
{
    public class BookResponse
    {
        public List<BookDto> Books { get; set; } = [];
    }

    public class BookDto
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }

    }
}
