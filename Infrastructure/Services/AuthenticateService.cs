
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Domain.Exceptions;
using Application.Interfaces;
using Application.Models;


namespace Infrastructure.Services
{
    public class AuthenticateService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticateServiceOptions _options;

        public AuthenticateService(IUserRepository userRepository, IOptions<AuthenticateServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User? ValidateUser(CredentialsAuthenticateDto credentialsRequest)
        {
            if (string.IsNullOrEmpty(credentialsRequest.Email) || string.IsNullOrEmpty(credentialsRequest.Password))
                return null;

            var user = _userRepository.GetUserByEmail(credentialsRequest.Email);

            if (user == null) return null;

            if (user.Password == credentialsRequest.Password) return user;

            return null;
        }

        public string Authenticate(CredentialsAuthenticateDto credentialsRequest)
        {
            //Paso 1: Validamos las credenciales
            var user = ValidateUser(credentialsRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

            if (user == null)
            {
                throw new NotAllowedException("User authentication failed");
            }


            //Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey)); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            //Los claims son datos en clave->valor que nos permite guardar data del usuario.
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
            claimsForToken.Add(new Claim("email", user.Email)); //Lo mismo para email y username, son las convenciones para email y nombre de usuario. Ustedes pueden usar lo que quieran, pero si alguien que no conoce la app
            claimsForToken.Add(new Claim("name", user.Name)); //quiere usar la API por lo general lo que espera es que se estén usando estas keys.
            claimsForToken.Add(new Claim("role", user.UserRol)); //Debería venir del usuario
            //claimsForToken.Add(new Claim("role", credentialsRequest.UserType)); //Debería venir del usuario

            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
              _options.Issuer,
              _options.Audience,
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();

        }

        public class AuthenticateServiceOptions
        {
            public const string AuthenticateService = "AuthenticateService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
