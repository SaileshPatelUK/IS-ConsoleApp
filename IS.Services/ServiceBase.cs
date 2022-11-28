using IS.Services.ErrorHandling;

namespace IS.Services
{
    public abstract class ServiceBase
    {
        public ServiceBase()
        {
        }

        protected ServiceOperationResultWrapper<T> ProvideFailureWrapper<T>(string message, string traceId)
        {
            return new ServiceOperationResultWrapper<T>
            {
                StatusMessage = new StatusWithTraceId { Message = StandardResults.StandardDisplayError, TraceId = traceId },
                Result = default,
                Successful = false,
                TechnicalMessage = message
            };
        }

        protected ServiceOperationResultWrapper<T> ProvideFailureWrapper<T>(Exception exception, string message, string traceId)
        {
            return new ServiceOperationResultWrapper<T>
            {
                Exception = exception,
                StatusMessage = new StatusWithTraceId { Message = StandardResults.StandardDisplayError, TraceId = traceId },
                Result = default,
                Successful = false,
                TechnicalMessage = message
            };
        }
    }
}
