using System.Runtime.InteropServices.JavaScript;
using kol1Oficial.DTOs;
using kol1Oficial.Exceptions;
using Microsoft.Data.SqlClient;

namespace kol1Oficial.Services;

public class DbService:IDbService
{
    private readonly string _connectionString;
    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default") ?? string.Empty;
    }

    public async Task<GetDeliverisDTO> GetDeliveries(int deliveryId)
    {
        var wholedel = new GetDeliverisDTO();
        
        var query =
                @"  Select del.date, C.first_name, C.last_name, c.date_of_birth, dri.first_name, dri.last_name,dri.licence_number,prod.name,prod.price,pd.amount
                    From Delivery del
                    Join Customer C ON del.customer_id = C.customer_id
                    Join Driver dri ON del.driver_id = dri.driver_id
                    JOIN Product_Delivery pd on del.delivery_id = pd.delivery_id
                    Join Product prod ON prod.product_id = pd.product_id
                    Where del.delivery_id = @deliveryId
                    ";
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@deliveryId", deliveryId);
        var reader = await command.ExecuteReaderAsync();
        var deliveryClient = new DeliveryClientGetDto();

        
        while (await reader.ReadAsync())
        {
            if (!await reader.IsDBNullAsync(4))
            {
                var deliveryDate = reader.GetDateTime(0);
                deliveryClient = new DeliveryClientGetDto()
                {
                    first_name = reader.GetString(1),
                    last_name = reader.GetString(2),
                    date_of_birth = reader.GetDateTime(3),
                };
                var deliveryDriver = new DeliveryDriverGetDto()
                {
                    first_name = reader.GetString(4),
                    last_name = reader.GetString(5),
                    license_number = reader.GetString(6),
                };
                var deliveryProduct = new DeliveryProductsDto()
                {
                    name = reader.GetString(7),
                    price = reader.GetDecimal(8),
                    amount = reader.GetInt32(9),
                };
                wholedel = new GetDeliverisDTO()
                {
                    Date = deliveryDate,
                    DeliveryClient = deliveryClient,
                    DeliveryDriver = deliveryDriver,
                    DeliveryProducts = deliveryProduct,
                };
            }

            if (wholedel.Date == DateTime.Parse("0001-01-01T00:00:00"))
            {
                throw new NotFoundException("Delivery not found");
            }

        }
        return wholedel;
    }
}