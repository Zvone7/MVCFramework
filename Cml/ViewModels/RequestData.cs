namespace Cml.ViewModels
{
    public class RequestData<T>
    {
        public T Data { get; }

        public RequestData(T data)
        {
            Data = data;
        }
}
}
