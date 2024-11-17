using Microsoft.AspNetCore.Identity;

namespace DropZone_BackPanel.Data.Entity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string? roleName) : base(roleName)
        {
        }
    }
}
