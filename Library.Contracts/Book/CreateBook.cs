namespace Library.Contracts.Book
{
    public class CreateBook
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Year { get; set; }
        public int CopiesAvailable { get; set; }
    }
}
