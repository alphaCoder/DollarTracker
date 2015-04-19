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
		void AddUser(User user);
		void UpdateUser(User user);
		User GetUserViaUserId(string userId);
		User GetUserViaEmail(string email);
		User GetUserViaUsername(string username);
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
		public void AddUser(User user)
		{
			if (!userRepository.Any(x => x.Email == user.Email || x.Username == user.Username))
			{
				userRepository.Add(user);
				SaveUser();
			}
		}

		public void UpdateUser(User user)
		{
			var existingUser = userRepository.Get(x => x.Email == user.Email);
			if (existingUser != null)
			{
				existingUser.Status = user.Status;
				userRepository.Add(existingUser);
				SaveUser();
			};
		}

		public User GetUserViaUserId(string userId)
		{
			return userRepository.GetById(userId);
		}

		public User GetUserViaEmail(string email)
		{
			return userRepository.Get(user => user.Email == email);
		}

		public User GetUserViaUsername(string username)
		{
			return userRepository.Get(user => user.Username == username);
		}

		public void SaveUser()
		{
			unitOfWork.Save();
		}
	}
}
