namespace kol1Oficial.DTOs;

public class PostDelivery
{
    public int deliveryId { get; set; }
    public int customerId { get; set; }
    public int licenceNumber { get; set; }
    public List<PostProductDto> posts { get; set; }
}