using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class LibraryService(ILibraryRepository libraryRepository)
    {
        public Task AddAsync(LibraryInfo library)  => libraryRepository.AddAsync(library);

        public Task ImportAsync(List<LibraryInfo> libraries) => libraryRepository.ImportAsync(libraries);

        public Task<List<LibraryInfo>> GetNearbyAsync(double lat, double lng, double radius) => libraryRepository.GetNearbyAsync(lat, lng, radius);
        public Task<List<LibraryInfo>> GetNearbyAsyncV2(double lat, double lng, double radius) => libraryRepository.GetNearbyAsyncv2(lat, lng, radius);
    }
}
