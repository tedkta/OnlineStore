using OnlineStore.Data.Models.PayOut;

namespace OnlineStore.Services.PayOut;

public interface IPayOutService
{
    PayOutViewModel BuildPayoutModel(int userId);
    void PlacePayout(int userId, PayOutViewModel model);
}