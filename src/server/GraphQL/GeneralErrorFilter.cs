using HotChocolate;

namespace Server.GraphQL
{
    public class GeneralErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            // This could be improved to e.g. handle different type of exceptions in different ways if more control is needed
            return error
                .WithMessage(error.Exception == null ? error.Message : error?.Exception?.Message ?? "Unknown error")
                .RemoveExtension("stackTrace");
        }
    }
}
