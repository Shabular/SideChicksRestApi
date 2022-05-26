using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace SideChicksRestApi.Models;

[DataContract]
public class Show
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Fee { get; set; }
    [ForeignKey("LocationId")]
    public int LocationId { get; set; }
    public DateTime Date { get; set; }
    public bool Accepted {get; set; }
    
}