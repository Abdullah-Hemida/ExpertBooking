
namespace ExpertBooking.Application.Helper
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccessed { get; set; } = true;
        public string Message { get; set; }

        public static ServiceResponse<T> Success(T data, string message = "") =>
            new() { Data = data, IsSuccessed = true, Message = message };

        public static ServiceResponse<T> Fail(string message) =>
            new() { IsSuccessed = false, Message = message };
    }
}
