namespace Car;
public class Car
{
    public Guid Id {get; set;}
    public string Plate {get; set;}
    public bool IsParked {get; set;}
    public DateTime Entry {get; set;}
    public DateTime Exit {get; set;}
}