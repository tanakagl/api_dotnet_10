using Domain.Entities;

namespace WebApi.Graphql.Types.Outputs;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Description("Represents a user in the system");

        descriptor.Field(u => u.Id)
            .Description("The unique identifier of the user");

        descriptor.Field(u => u.Name)
            .Description("The name of the user");

        descriptor.Field(u => u.Email)
            .Description("The email address of the user");

        descriptor.Field(u => u.CreatedAt)
            .Description("When the user was created");

        descriptor.Field(u => u.UpdatedAt)
            .Description("When the user was last updated");
    }
}
