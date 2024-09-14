namespace MShop.Catalog.API.GraphQL.Common
{
    public class GraphiQLPayload<T>
    {
        public GraphiQLPayload(T data, bool success)
        {
            Data = data;
            IsSuccess = success;
        }

        public T Data { get; private set; }    
        public bool IsSuccess {get; private set; }

        public static GraphiQLPayload<T> Success(T data, bool success = true) => new GraphiQLPayload<T>(data, success);
        public static GraphiQLPayload<T> Error(T data, bool success = false) => new GraphiQLPayload<T>(data, success);
    }
}
