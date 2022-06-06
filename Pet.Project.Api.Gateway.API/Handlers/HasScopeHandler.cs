﻿using Microsoft.AspNetCore.Authorization;
using Pet.Project.Api.Gateway.API.Dtos;

namespace Pet.Project.Api.Gateway.API.Handlers
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasScopeRequirement requirement
        )
        {
            if (!context.User.HasClaim(claim => claim.Type == "scope" && claim.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            var scopes = context.User.FindFirst(match: scope => scope.Type == "scope" && scope.Issuer == requirement.Issuer)!.Value
                .Split(' ');

            if (scopes.Any(scope => scope == requirement.Scope))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}