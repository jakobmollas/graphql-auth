using Server.Models;
using HotChocolate;
using Server.Repository;
using System.Collections.Generic;
using HotChocolate.Data;
using System.Security.Claims;
using HotChocolate.AspNetCore.Authorization;
using Server.Security;
using System;
using System.Linq;

namespace Server.GraphQL
{
    
    public class Query
    {
        [Authorize]
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Get authors")]
        public IList<Author> GetAuthors([Service] IInMemDataRepo repo)
        {
            return repo.Authors;
        }

        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Get books")]
        public IList<Book> GetBooks([Service] IInMemDataRepo repo)
        {
            return repo.Books;
        }

        [GraphQLDescription("Get info on the currently logged-in user")]
        public SystemUser GetUser(ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal?.Identity as ClaimsIdentity;
            if (identity?.Name == null)
                throw new Exception("No user logged in");

            var user  = new SystemUser(claimsPrincipal.Identity.Name);

            var claimsRoles = identity.Claims.Where(n => n.Type == ClaimTypes.Role).Select(n => n.Value);
            foreach (var role in claimsRoles)
                user.Roles.Add(role);

            return user;
        }

        [GraphQLDescription("Log in user and create access token.")]
        public string Login(string apiKey, [Service] IUserAuthenticationService userAuthentication)
        {
            var token = userAuthentication.Authenticate(apiKey);

            if (token == null)
                throw new Exception("Unknown user key");

            return token;
        }
    }
}
