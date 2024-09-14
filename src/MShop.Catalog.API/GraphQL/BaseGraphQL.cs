using HotChocolate;
using HotChocolate.Execution;
using MShop.Core.Message;
using System.Linq;

public abstract class BaseGraphQL
{
    protected IError[] ConvertToGraphQLErrors(INotification notifications)
    {
        return notifications.Errors().Select(error =>
            ErrorBuilder.New()
                .SetMessage(error.Message)
                .SetCode("CUSTOM_ERROR_CODE") // Opcionalmente, defina um código de erro
                .Build()
        ).ToArray();
    }

    public void Notify(string message, INotification notifications)
    {
        notifications.AddNotifications(message);
    }

    public void NotifyWithException(string message, INotification notifications)
    {
        notifications.AddNotifications(message);
        throw new GraphQLException(ConvertToGraphQLErrors(notifications));
    }

    public void Error(INotification notifications)
    {
        throw new GraphQLException(ConvertToGraphQLErrors(notifications));
    }

    public void RequestIsValid(INotification notifications)
    {
        if (notifications.HasErrors())
        {
            throw new GraphQLException(ConvertToGraphQLErrors(notifications));
        }
    }
}