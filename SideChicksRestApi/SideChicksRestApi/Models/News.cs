using System.Runtime.Serialization;

namespace SideChicksRestApi.Models;

[DataContract]

public class News
{
    
    public int Id { get; set; }
    public string Userid { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public string Details { get; set; }
}