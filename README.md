# graphql-auth
HotChocolate GraphQL implementation using Authentication and Authorization using JWT.

- Authentication handled via GraphQL as a separate top-level query (`login`), returning JWT data in exchange for a user ApiKey
- User data is stored in `appsetting.json` which in most cases is a BAD idea, but this example is not about user handling - that is a whole separate topic
- Pass in tokens as Bearar authentication headers to authenticate as needed, for example `Authentication: bearer my-token-goes-here`
- HotChocolate can handle authentication globally or down to individual fields, in various ways
- GraphQL server implemented using [HotChocolate](https://chillicream.com/docs/hotchocolate) by ChilliCream
- Schema visualzation available at `/voyager` via [GraphQL Voyager](https://github.com/APIs-guru/graphql-voyager)
