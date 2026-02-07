using OnlineStore.Data.Models.Entities;

namespace OnlineStore.Services.Users;

public class LoggedUserService
{
    private User user;

    public User User 
    {
        get => this.user;
        set
        {
            user = value;      //Тук задаваме стойност на user след като я вземе
            IsLogged = user != null;
            IsAdmin = user?.Role == "administrator";
        }
        //Тук проверяваме дали user е null и задаваме IsLogged на true или false 
    }


    public bool IsAdmin { get; private set; }
    public bool IsLogged {get; private set; } 

} 
