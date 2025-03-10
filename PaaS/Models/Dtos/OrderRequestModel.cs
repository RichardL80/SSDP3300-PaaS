namespace PaaS.Models.Dtos;

public class OrderRequestModel
{
    public int DeliveryMethodId { get; set; }
    public int PaymentMethodId { get; set; }
    public string SelectedAddress { get; set; }
}