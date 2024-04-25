using Microsoft.AspNetCore.Identity;
using Notes.Models;
using Notes.Repositories;

namespace Notes.Services
{
	public class UserStore : IUserStore<User>, IUserEmailStore<User>, IUserPasswordStore<User>
	{
		private readonly IRepositoryUser _repositoryUser;

		public UserStore(IRepositoryUser repository)
		{
			_repositoryUser = repository;
		}

		#region Delete, create, update, dispose

		public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
		{
			user.Id = await _repositoryUser.Create(user);
			return IdentityResult.Success;
		}

		public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public void Dispose()
		{
			//throw new NotImplementedException();
		}

		#endregion

		#region Email
		public async Task<User?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			return await _repositoryUser.GetByEmail(normalizedEmail);
		}
		
		public Task<string?> GetEmailAsync(User user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Email);
		}
		public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public Task<string?> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public Task SetEmailAsync(User user, string? email, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public Task SetNormalizedEmailAsync(User user, string? normalizedEmail, CancellationToken cancellationToken)
		{
			//Sure?
			return Task.CompletedTask;
		}

		#endregion

		#region UserName
		public Task<string?> GetUserNameAsync(User user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.UserName);
		}
		public Task<string?> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public Task SetUserNameAsync(User user, string? userName, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
		public Task SetNormalizedUserNameAsync(User user, string? normalizedName, CancellationToken cancellationToken)
		{
			//Sure?
			return Task.CompletedTask;
		}

		#endregion

		#region Name

		//In fact, find by user name
		public async Task<User?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			return await _repositoryUser.GetByUserName(normalizedUserName);
		}

		#endregion

		#region UserId
		public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.Id.ToString());
		}
		public Task<User?> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Password

		public Task<string?> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
		{
			return Task.FromResult(user.PasswordHash);
		}

		public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public Task SetPasswordHashAsync(User user, string? passwordHash, CancellationToken cancellationToken)
		{
			user.PasswordHash = passwordHash;
			return Task.CompletedTask;
		}

		#endregion

	}
}
