using System;

namespace TechNotes.domain.User;

public interface IUserRepository
{
    Task<IUser?> GetUserByIdAsync(string userId);
    
}