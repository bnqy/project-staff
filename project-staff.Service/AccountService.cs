using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using project_staff.Contracts;
using project_staff.Entities.Models;
using project_staff.Service.Contracts;
using project_staff.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace project_staff.Service
{
	public class AccountService : IAccountService
	{
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;


		private ApplicationUser? _user;


		public AccountService(ILoggerManager loggerManager, IMapper mapper, UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_logger = loggerManager;
			_mapper = mapper;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<string> CreateToken()
		{
			var signingCredentials = GetSigningCredentials();
			var claims = await GetClaims();
			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

			return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
		}

		public async Task<IdentityResult> RegisterUser(ApplicationUserForRegistrationDto userForRegistration)
		{
			var user = _mapper.Map<ApplicationUser>(userForRegistration);
			var result = await _userManager.CreateAsync(user, userForRegistration.Password);
			if (result.Succeeded)
				await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
			return result;
		}

		public async Task<bool> ValidateUser(ApplicationUserForAuthenticationDto userForAuth)
		{
			_user = await _userManager.FindByNameAsync(userForAuth.UserName);

			var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

			if (!result)
				_logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password."); 


	
			return result;
		}

		private SigningCredentials GetSigningCredentials()
		{
			var jwtSettings = this._configuration.GetSection("JwtSettings");
			var keyFromJWTSettins = jwtSettings["key"];
			var key = Encoding.UTF8.GetBytes(keyFromJWTSettins);
			var secret = new SymmetricSecurityKey(key);

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private async Task<List<Claim>> GetClaims()
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
				new Claim(ClaimTypes.Name, _user.UserName)
			};

			var roles = await _userManager.GetRolesAsync(_user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
		{
			var jwtSettings = _configuration.GetSection("JwtSettings");
			var tokenOptions = new JwtSecurityToken
			(
			issuer: jwtSettings["validIssuer"],
			audience: jwtSettings["validAudience"],
			claims: claims,
			expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
			signingCredentials: signingCredentials
			);
			return tokenOptions;
		}
	}
}
