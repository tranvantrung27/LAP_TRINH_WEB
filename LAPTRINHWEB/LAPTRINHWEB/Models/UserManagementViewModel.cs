using System.ComponentModel.DataAnnotations;

namespace LAPTRINHWEB.Models
{
    public class UserManagementViewModel
    {
        public UserCreateModel NewUser { get; set; }
        public List<User> Users { get; set; }
    }

}
