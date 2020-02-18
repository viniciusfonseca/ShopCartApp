using System.Net;

namespace ShopCart.Core
{
    public abstract class ServerException : System.Exception
    {
        public abstract HttpStatusCode Status { get; set; }
        public abstract object Payload { get; set; }
    }

    public class NotFoundError : ServerException
    {
        public override HttpStatusCode Status { get; set; } = HttpStatusCode.NotFound;
        public override object Payload { get; set; }

        public NotFoundError()
        {
            Payload = new { message = "Entity not found." };
        }

        public NotFoundError(string message)
        {
            Payload = new { message };
        }
    }

    public class BadRequestError : ServerException
    {
        public override HttpStatusCode Status { get; set; } = HttpStatusCode.BadRequest;
        public override object Payload { get; set; } = "";
    }
}