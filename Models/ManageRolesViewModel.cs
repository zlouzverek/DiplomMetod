namespace DiplomMetod.Models
{
    public class ManageRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<RoleViewModel> UserRoles { get; set; } = new();
    }

    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
