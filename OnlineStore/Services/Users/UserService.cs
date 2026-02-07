using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Models.Entities;
using OnlineStore.Data.Models.User;

namespace OnlineStore.Services.Users;

public class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;
    private readonly LoggedUserService loggedUserService;

    public UserService(ApplicationDbContext dbContext, LoggedUserService loggedUserService)
    {
        this.dbContext = dbContext;
        this.loggedUserService = loggedUserService;
    }

    public void Create(CreateUserDto user) 
    {
        User newUser = ToUser(user);
        newUser.Password = user.Password;
        dbContext.Users.Add(newUser);
        dbContext.SaveChanges(); //Създаване на User и запазване в базата данни
    }

   public void Login(LoginUserDto user)
    {
        User dbUser = dbContext.Users
            .FirstOrDefault(u => u.Username == user.Username); //Търсене на потребител по username

        if (dbUser == null)
        {
            throw new ArgumentException("Invalid username or password!");
        }

        if (dbUser.Password != user.Password)
        {
            throw new ArgumentException("Invalid username or password!");
        }

        this.loggedUserService.User = dbUser; //Ако потребителят е намерен, запазваме го в loggedUserService
    } 

    public void Logout()
    {
        this.loggedUserService.User = null; 
    }  
        
    private User ToUser(CreateUserDto user)
    {
        User appUser = new User();
        appUser.FullName = user.FullName;
        appUser.Username = user.Username;
        appUser.Email = user.Email;
        return appUser; //Създаване на нов User обект с цел съкващаване на повторения

    }
}