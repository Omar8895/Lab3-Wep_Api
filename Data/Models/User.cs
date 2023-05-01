using Microsoft.AspNetCore.Identity;

namespace Lab3.Data.Models;

public class User : IdentityUser
{
    public string Department { get; set; } = string.Empty;
}
