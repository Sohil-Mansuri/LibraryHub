namespace Library.Contracts.Library
{
    public class LibraryModel
    {
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Coordinate Coordinates { get; set; } = null!;
    }

    public class Coordinate
    {
        public double Lat { get; set; }

        public double Long { get; set; }
    }
}
