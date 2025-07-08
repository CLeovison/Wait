using FluentValidation;

namespace Wait.Extensions;


public static class ValidationExtension
{
    /// <summary>
    /// Custom Extension for Endpoint Filter Validation
    /// </summary>
    public static RouteHandlerBuilder WithValidation<TRequest>(this RouteHandlerBuilder builder)
    {

        return builder.AddEndpointFilter(async (invocationContext, next) =>
        {
            //Grab the first argument passed into the route handler(usually the request DTO)
            var request = invocationContext.GetArgument<TRequest>(0);

            //Resolve IValidator<TRequest> from Dependency Injection Container. It may be null if not registered
            var validator = invocationContext.HttpContext.RequestServices.GetService<IValidator<TRequest>>();

            //Only proceed with validation if a validator exists for the request type
            if (validator is not null)
            {

                //Run an async validation since when requesting from an endpoint it is an async operation
                var validationResult = await validator.ValidateAsync(request);

                //If request data is invalid, return structured problem response
                if (!validationResult.IsValid)
                {
                    //Flatten the validation errors into key-value pairs.
                    var errors = validationResult.Errors.Select(e => KeyValuePair.Create(e.PropertyName, e.ErrorMessage));

                    //Log Validation failure for Tracing/Debuging
                    var loggerFactory = invocationContext.HttpContext.RequestServices.GetService<ILoggerFactory>();
                    var logger = loggerFactory?.CreateLogger("FluentValidation");

                    logger?.LogWarning("Validation failed for {RequestType} : {ErrorDetails}",
                     typeof(TRequest).Name, string.Join("|", errors.Select(e => $"{e.Key} : {e.Value}")));

                    // Group errors by property and return as a structured 400 response
                    return Results.ValidationProblem(
                        new Dictionary<string, string[]>
                        (errors.GroupBy(e => e.Key)
                        .ToDictionary(g => g.Key,
                        g => g.Select(x => x.Value).ToArray())));
                }
            }
            // If valid or no validator found, continue to the original request handler

            return await next(invocationContext);
        });
    }
}