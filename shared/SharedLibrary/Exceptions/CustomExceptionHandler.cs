﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedLibrary.Dtos;
using SharedLibrary.Models;
using System.Text.Json;

namespace SharedLibrary.Exceptions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature is not null)
                    {
                        var ex = errorFeature.Error;
                        var path = errorFeature.Path;

                        ErrorDto errorDto = new();

                        if (ex is CustomException)
                            errorDto = new ErrorDto(ex.Message, path, true);
                        else
                            errorDto = new ErrorDto(ex.Message, path, false);

                        var response = Response<NoDataDto>.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}
