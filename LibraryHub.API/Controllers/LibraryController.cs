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
        public async Task<IActionResult> AddLibrary(LibraryModel libraryModel)
        {
            await libraryService.AddAsync(libraryModel.ToLibrary());
            return Ok(ApiResponse<string>.Success("library added"));
        }

        [HttpPost("import")]
        public async Task<IActionResult> BuikImport(List<LibraryModel> libraries)
        {
            await libraryService.ImportAsync([.. libraries.Select(l => l.ToLibrary())]);
            return Ok(ApiResponse<string>.Success("libraries imported"));
        }

        [HttpPost("nearby")]
        public async Task<IActionResult> GetNearby(LibrarySearchRequest request)
        {
            var libraries = await libraryService.GetNearbyAsync(request.Let, request.Long, request.Radius);
            return Ok(ApiResponse<List<LibrarySearchResponse>>.Success([.. libraries.Select(l => l.ToLibrarySearchResponse())]));
        }
    }
}
