using PropertyExchange.Core.Domain.Exceptions;

namespace PropertyExchange.Core.Application.Common
{
   public abstract class ExceptionHelper
    {
        protected void ThrowValidationError(string message)
        {
            throw new AppValidationException(message);
        }
    }
}
