﻿using DisputenPWA.Application.Users.Shared;
using DisputenPWA.Domain.Aggregates.UserAggregate;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands;
using DisputenPWA.Domain.Aggregates.UserAggregate.Commands.Results;
using DisputenPWA.Domain.Aggregates.UserAggregate.DalObject;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace DisputenPWA.Application.Users.Handlers.Commands
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public RegisterUserHandler(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterUserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                // domain error?
                return new RegisterUserCommandResult(new User { JWTToken = null });
            }

            var token = JwtTokenGenerator.GenerateJwtToken(user, _configuration.GetValue<string>("JWT:Secret"));
            return new RegisterUserCommandResult(new User { JWTToken = token });
        }
    }
}