using Server.Models;
using HotChocolate.Types;
using Server.Security;

namespace Server.GraphQL
{
    public class SystemUserType : ObjectType<SystemUser>
    {
        protected override void Configure(IObjectTypeDescriptor<SystemUser> descriptor)
        {
            descriptor.Description("A system user.");
            descriptor.BindFieldsExplicitly();

            descriptor
                .Field(n => n.Name)
                .Name("name")
                .Description("User name");

            descriptor
                .Field(n => n.Roles)
                .Name("roles")
                .Type<NonNullType<ListType<NonNullType<StringType>>>>()
                .Description("All roles this user belongs to");
        }
    }
}
