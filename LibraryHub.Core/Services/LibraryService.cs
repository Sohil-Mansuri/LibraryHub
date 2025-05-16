using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class LibraryService(ILibraryRepository libraryRepository)
    {
        public Task AddLibraryAsync(LibraryInfo library)  => libraryRepository.AddLibrary(library);

        public Task ImportLibrariesAsync(List<LibraryInfo> libraries) => libraryRepository.ImportLibraries(libraries);

        public Task<List<LibraryInfo>> GetNearbyLibrariesAsync(double lat, double lng, double radius) => libraryRepository.GetNearbyLibrariesAsync(lat, lng, radius);
        public Task<List<LibraryInfo>> GetNearbyLibrariesAsyncV2(double lat, double lng, double radius) => libraryRepository.GetNearbyLibrariesAsyncV2(lat, lng, radius);
    }
}
