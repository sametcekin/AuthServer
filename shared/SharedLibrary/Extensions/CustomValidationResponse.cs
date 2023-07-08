using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Dtos;
using SharedLibrary.Models;

namespace SharedLibrary.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Any()).SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                    var errorDto = new ErrorDto(errors, true);


                    var response = Response<NoContentResult>.Fail(errorDto, 400);
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
