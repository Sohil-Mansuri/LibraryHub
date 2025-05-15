using FluentValidation;
using LibraryHub.API.Middleware;
using LibraryHub.API.Model;
using LibraryHub.API.Validator;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateBookValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateBookValidator>();

            return services;
        }

        public static void CustomizeValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                                  .Where(e => e.Value!.Errors.Count > 0)
                                  .GroupBy(e => e.Key)
                                  .ToDictionary(
                                      g => g.Key,
                                      g => string.Join(", ", g.SelectMany(e => e.Value!.Errors.Select(err => err.ErrorMessage)))
                                  );

                    var response = ApiResponse<string>.Fail(new ErrorResponse(errors, "Validation failed", "400"));

                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
