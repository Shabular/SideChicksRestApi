using System.Runtime.Serialization;

namespace SideChicksRestApi.Models;

[DataContract]
public class Show
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public bool Accepted {get; set; }
}