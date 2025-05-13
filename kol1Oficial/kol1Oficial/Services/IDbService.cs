using kol1Oficial.DTOs;

namespace kol1Oficial.Services;

public interface IDbService
{
    public Task<GetDeliverisDTO> GetDeliveries(int deliveryId);
    public Task PostDelivery(PostDelivery delivery);
    
}