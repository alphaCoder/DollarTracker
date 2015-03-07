using DollarTracker.Core.Infrastructure;
using DollarTracker.Core.Repository;
using DollarTracker.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DollarTracker.Core.Managers
{
	public interface IUserManager
	{
		void AddUser(Users user);
		void UpdateUser(Users user);
		Users GetUser(Guid userId);
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
			var existingUser = userRepository.Get(x => x.Email == user.Email);
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

		public Users GetUser(Guid userId)
		{
			return userRepository.Get(x => x.UserId == userId);
		}


		public void SaveUser()
		{
			unitOfWork.Save();
		}
	}
}
