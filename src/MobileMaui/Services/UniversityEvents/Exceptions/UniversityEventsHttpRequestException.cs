using System.Net;

namespace MobileMaui.Services.UniversityEvents.Exceptions;

public class UniversityEventsHttpRequestException : HttpRequestException
{
    public UniversityEventsHttpRequestException(string? message, Exception? inner, HttpStatusCode? statusCode)
        : base(message, inner, statusCode)
    {
    }
}