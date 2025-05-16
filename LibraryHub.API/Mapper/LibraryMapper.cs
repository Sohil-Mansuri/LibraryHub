using Library.Contracts.Library;
using LibraryHub.Core.Entity;
using MongoDB.Driver.GeoJsonObjectModel;

namespace LibraryHub.API.Mapper
{
    public static class LibraryMapper
    {
        public static LibraryInfo ToLibrary(this LibraryModel library)
        {
            return new LibraryInfo
            {
                Name = library.Name,
                Address = library.Address,
                Location = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                                 new GeoJson2DGeographicCoordinates(library.Coordinates.Lat, library.Coordinates.Long))
            };
        }

        public static LibrarySearchResponse ToLibrarySearchResponse(this LibraryInfo library)
        {
            return new LibrarySearchResponse
            {
                Name = library.Name,
                Address = library.Address,
                Coordinate = new Coordinate
                {
                    Lat = library.Location.Coordinates.Latitude,
                    Long = library.Location.Coordinates.Longitude
                }
            };
        }
    }
}
