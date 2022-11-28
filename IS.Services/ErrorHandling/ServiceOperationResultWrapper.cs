namespace IS.Services.ErrorHandling
{
    using System;

    public class ServiceOperationResultWrapper<TResultType> : OperationResultWrapper<TResultType>
    {
        public ServiceOperationResultWrapper()
        {
            Exception = null;
            TechnicalMessage = "TECHNICAL MESSAGE NOT ASSIGNED";
        }

        public Exception Exception { get; set; }

        public string TechnicalMessage { get; set; }

        public static ServiceOperationResultWrapper<TResultType> WrappedCall(Func<TResultType> function)
        {
            var result = new ServiceOperationResultWrapper<TResultType>();

            try
            {
                result.Result = function();
                result.Successful = true;
                result.StatusMessage = new StatusWithTraceId { Message = StandardResults.SuccessStatus };
            }
            catch (Exception exception)
            {
                result.Exception = exception;
                result.StatusMessage = new StatusWithTraceId { Message = StandardResults.StandardDisplayError };
                result.TechnicalMessage = "An exception of type - '" + exception.GetType().ToString() + "' occurred.";
            }

            return result;
        }

        public OperationResultWrapper<TResultType> ToOperationResultWrapper()
        {
            return new OperationResultWrapper<TResultType>()
            {
                Result = Result,
                StatusMessage = StatusMessage,
                Successful = Successful
            };
        }

        public OperationResultWrapper<TResultType> ToOperationResultWrapperWithTechnical()
        {
            return new OperationResultWrapper<TResultType>()
            {
                Result = Result,
                StatusMessage = new StatusWithTraceId() { Message = TechnicalMessage },
                Successful = Successful
            };
        }

        public void ComposeExceptionMessage()
        {
            TechnicalMessage = "An exception of type - '" + Exception?.GetType()?.ToString() ?? "NONE" + "' occurred.";
        }
    }
}
