using System.ComponentModel.DataAnnotations;

namespace GameForge.Models;

public class User
{
    public int ID { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime CreationDate{ get; set; }
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    //hello
    
}