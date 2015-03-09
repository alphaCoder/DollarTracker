using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Managers
{
	public interface IUserManager
	{
		void AddUser(Users user);
		void UpdateUser(Users user);
		Users GetUserViaUserId(Guid userId);
		Users GetUserViaEmail(string email);
		Users GetUserViaUsername(string username);
		void SaveUser();
	}
	public class UserManager : IUserManager
	{
		private readonly IUserRepository userRepository;
		private readonly IUnitOfWork unitOfWork;
		public UserManager(IUserRepository userRepository, IUnitOfWork unitOfWork)
		{
			this.userRepository = userRepository;
			this.unitOfWork = unitOfWork;
		}
		public void AddUser(Users user)
		{
			var existingUser = userRepository.Get(x => x.Email == user.Email || x.Username == user.Username);
			if (existingUser == null)
			{
				userRepository.Add(user);
				SaveUser();
			}
		}

		public void UpdateUser(Users user)
		{
			var existingUser = userRepository.Get(x => x.Email == user.Email);
			if (existingUser != null)
			{
				existingUser.Password = user.Password;
				existingUser.Status = user.Status;
				userRepository.Add(existingUser);
				SaveUser();
			};
		}

		public Users GetUserViaUserId(Guid userId)
		{
			return userRepository.GetById(userId);
		}

		public Users GetUserViaEmail(string email)
		{
			return userRepository.Get(user => user.Email == email);
		}

		public Users GetUserViaUsername(string username)
		{
			return userRepository.Get(user => user.Username == username);
		}

		public void SaveUser()
		{
			unitOfWork.Save();
		}
	}
}
