# graphql-auth
HotChocolate GraphQL implementation using Authentication and Authorization via JWT.

- User authentication is handled via GraphQL as a separate top-level query (`login`), returning JWT data in exchange for a user ApiKey
- User data is stored in `appsetting.json` which in most cases is a BAD idea, but this example is about authentication and quthorization, not user handling - that is a whole separate topic
- To provide quthorization - pass in access tokens as Bearar authentication headers to authenticate as needed, for example `Authentication: bearer my-token-goes-here`
- HotChocolate can handle authentication at various level - from individual fields up to globally for everything at once
- GraphQL server implemented using [HotChocolate](https://chillicream.com/docs/hotchocolate) by ChilliCream
- Schema visualzation available at `/voyager` via [GraphQL Voyager](https://github.com/APIs-guru/graphql-voyager)

HotChocolate uses standard ASP .Net Core authentication mechanisms and provices authorization logic in a similar way to how standard controller logic works. 
It is possible to use role-base and/or policy-based authorization - this is also using normal ASP .Net Core logic which is nice.

Overall security seems to be pretty straightforward to implement if security can ever be classified as easy or simple, but HotChocolate does not add additional complexity to the mix.

One thing I miss is a way to document security requirements, i.e. which queries/mutations/types/fields require authentication/authorization and in which way, which roles etc. 
Maybe there is a way but I cannot find it.

## Example queries:
```graphql
query login {
  login(apiKey: "abc123") 
}
```

```graphql
query getBooks {
  books {
    id
    name 
    authors {
      name
    }
  }
}
```

```graphql
query getAuthors {
  authors {
    id
    name
    nickname
    books {
      name 
    }
  }
}
```

```graphql
query currentUser {
  user {
    name 
    roles 
  }
}
```

## Example mutations:
```graphql
mutation { 
  addAuthor(input: { 
    name: "Isaac Asimov"
    nickname: "Азимов"
  }) 
  {
    author {
      id
      name
      nickname
    }
  }
}
```

```graphql
mutation { 
  addBook(input: { 
    name: "Foundation"
    authorIds: [5]
    }) 
  {
    book {
      id
      name
    }
  }
}
```
