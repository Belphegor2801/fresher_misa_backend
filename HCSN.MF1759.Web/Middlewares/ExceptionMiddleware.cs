using HCSN.MF1759.Domain;

namespace HCSN.MF1759
{
    /// <summary>
    /// Middleware xử lý exception
    /// </summary>
    /// Author: nxhinh (11/09/2023)  
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Xử lý các exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        /// Author: nxhinh (11/09/2023)  
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                // Không tìm thấy
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(text: new EndPointException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToJson() ?? "");
                    break;

                // Dữ liệu không hợp lệ
                case System.IO.InvalidDataException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync(text: new EndPointException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToJson() ?? "");
                    break;

                // Conflict
                case ConflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(text: new EndPointException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToJson() ?? "");
                    break;

                // Các trường hợp khác
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(text: new EndPointException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfor = exception.HelpLink,
                    }.ToJson() ?? "");
                    break;
            }
        }
    }
}
