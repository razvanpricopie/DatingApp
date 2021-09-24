using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // supply token validations parameters
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // validate the issue signing key
                        // server is going to sign the token
                        // we need tot tell it to actually validate this token is correct
                        ValidateIssuerSigningKey = true,

                        // we need to give it the issue, a signing key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),

                        // issuer is the API server
                        ValidateIssuer = false,

                        // audience is the client
                        ValidateAudience = false,
                    };
                });

            return services;
        }
    }
}