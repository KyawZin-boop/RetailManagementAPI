using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO;

public class UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserLoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserUpdateDTO
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
