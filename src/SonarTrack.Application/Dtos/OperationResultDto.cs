namespace SonarTrack.Application.Dtos
{
    public class OperationResultDto<TValue>
    {
        public bool Success { get; private set; }
        public IEnumerable<string> Errors { get; private set; } = [];
        public TValue? Value { get; set; }

        private OperationResultDto(bool success)
        {
            Success = success;
        }

        public static OperationResultDto<TValue> Ok()
        {
            return new OperationResultDto<TValue>(true);
        }

        public static OperationResultDto<TValue> Ok(TValue value)
        {
            return new OperationResultDto<TValue>(true) { Value = value };
        }

        public static OperationResultDto<TValue> Fail(IEnumerable<string> errors)
        {
            return new OperationResultDto<TValue>(false) { Errors = new List<string>(errors) };
        }

        public static OperationResultDto<TValue> Fail(string error)
        {
            return new OperationResultDto<TValue>(false) { Errors = [error] };
        }
    }
}
