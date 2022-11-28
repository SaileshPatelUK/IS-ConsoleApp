namespace IS.Services.ErrorHandling
{
    public class OperationResultWrapper<TResultType>
    {
        public OperationResultWrapper()
        {
            Result = default(TResultType);
            StatusMessage = new StatusWithTraceId { Message = "STATUS NOT ASSIGNED" };
            Successful = false;
        }

        // Private result field.
        private TResultType resultObject;

        public TResultType Result
        {
            get
            {
                if (typeof(TResultType) == typeof(object))
                {
                    return default(TResultType);
                }

                return resultObject;
            }
            set
            {
                resultObject = value;
            }
        }

        public StatusWithTraceId StatusMessage { get; set; }

        public bool Successful { get; set; }
    }
}
