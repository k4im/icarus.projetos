using System.ComponentModel.DataAnnotations.Schema;

namespace projeto.domain.Entity;
public class AppUser
{   
    
    public Cliente ClienteAccount { get; set; }
    
    [ForeignKey("ClientAccount")]
    public int ClienteAccountId { get; set; }
}
