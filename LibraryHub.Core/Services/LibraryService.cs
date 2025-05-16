using LibraryHub.Core.Entity;
using LibraryHub.Core.Repository;

namespace LibraryHub.Core.Services
{
    public class LibraryService(ILibraryRepository libraryRepository)
    {
        public Task AddAsync(LibraryInfo library, CancellationToken cancellationToken = default)  => libraryRepository.AddAsync(library, cancellationToken);

        public Task ImportAsync(List<LibraryInfo> libraries, CancellationToken cancellationToken = default) => libraryRepository.ImportAsync(libraries, cancellationToken);

        public Task<List<LibraryInfo>> GetNearbyAsync(double lat, double lng, double radius, CancellationToken cancellationToken = default) => libraryRepository.GetNearbyAsync(lat, lng, radius, cancellationToken);
        
        public Task<List<LibraryInfo>> GetNearbyAsyncV2(double lat, double lng, double radius, CancellationToken cancellationToken = default) => libraryRepository.GetNearbyAsyncv2(lat, lng, radius, cancellationToken);
    }
}
