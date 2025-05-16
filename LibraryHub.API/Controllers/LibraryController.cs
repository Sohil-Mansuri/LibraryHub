using Library.Contracts.Library;
using LibraryHub.API.Mapper;
using LibraryHub.API.Model;
using LibraryHub.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.API.Controllers
{
    [Route("api/library/v1")]
    [ApiController]
    public class LibraryController(LibraryService libraryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add(LibraryModel libraryModel, CancellationToken cancellationToken)
        {
            await libraryService.AddAsync(libraryModel.ToLibrary(), cancellationToken);
            return Ok(ApiResponse<string>.Success("library added"));
        }

        [HttpPost("import")]
        public async Task<IActionResult> BuikImport(List<LibraryModel> libraries, CancellationToken cancellationToken)
        {
            await libraryService.ImportAsync([.. libraries.Select(l => l.ToLibrary())], cancellationToken);
            return Ok(ApiResponse<string>.Success("libraries imported"));
        }

        [HttpPost("nearby")]
        public async Task<IActionResult> GetNearby(LibrarySearchRequest request, CancellationToken cancellationToken)
        {
            var libraries = await libraryService.GetNearbyAsync(request.Let, request.Long, request.Radius, cancellationToken);
            return Ok(ApiResponse<List<LibrarySearchResponse>>.Success([.. libraries.Select(l => l.ToLibrarySearchResponse())]));
        }
    }
}
