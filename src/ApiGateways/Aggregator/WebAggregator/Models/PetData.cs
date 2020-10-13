
public class PetData
{
    public string Gender { get; set; }
    public CityPets[] Pets { get; set; }
}

public class CityPets
{
    public string Name { get; set; }
    public string City { get; set; }
}

public enum Cities
{
    Sydney,
    Melbourne
}