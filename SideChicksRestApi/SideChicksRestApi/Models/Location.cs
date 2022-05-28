using System.Runtime.Serialization;

namespace SideChicksRestApi.Models;

[DataContract]
public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string PostalNumber { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
}