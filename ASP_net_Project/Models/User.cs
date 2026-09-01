using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
 
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    
    public string Password { get; set; }
}