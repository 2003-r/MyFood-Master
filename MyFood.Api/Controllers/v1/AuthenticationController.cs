using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFood.Application;
using MyFood.Application.Dtos;
using MyFood.Application.Entities;
using MyFood.Infrastructure;
using MyFood.Infrastructure.Helpers;
using MyFood.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using MyFood.Domain.Entities;

namespace MyFood.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v")]
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;
        private readonly ITokenGenerator tokenGenerator;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserRepository _userRepository, IPasswordHasher _passwordHasher, IMapper mapper, ITokenGenerator tokenGenerator)
        {
            userRepository = _userRepository;
            passwordHasher = _passwordHasher;
            _mapper = mapper;
            this.tokenGenerator = tokenGenerator;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if(registerDto == null)
            {
                return BadRequest("Need Registeration Data");
            }

            var userExist = userRepository.GetSingle(registerDto.Email);
            if(userExist != null)
            {
                return Conflict("User Email already exists.");
            }

            string hashedPassword = passwordHasher.Hash(registerDto.Password);

            var user = new UserEntity
            {
                Name = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword 
            };


            userRepository.Add(user);
            userRepository.Save();

            return CreatedAtAction(nameof(Register), "User created successfully.", user);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = userRepository.GetByEmail(loginDto.Email);
            if (user != null)
            {
                var checkuser = userRepository.Login(loginDto.Email, loginDto.Password);
                if (checkuser == null)
                {
                    return Unauthorized("email or password not valid");
                }

                string token = tokenGenerator.GenerateJwtToken(user.Email, "superdupersecretkey(hexonometric");
                return Ok(new { user, token });
            }

            return NotFound("user not found");
           
        }

    }
}
