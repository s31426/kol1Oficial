namespace kol1Oficial.DTOs;

public class GetDeliverisDTO
{
    public DateTime Date { get; set; }
    public DeliveryClientGetDto DeliveryClient { get; set; }
    public DeliveryDriverGetDto DeliveryDriver { get; set; }
    public DeliveryProductsDto DeliveryProducts { get; set; }
}