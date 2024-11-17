using System.Collections.Generic;

namespace DropZone_BackPanel.Areas.Auth.Models
{
    public class UserListViewModel
    {
        public IEnumerable<AspNetUsersViewModel>? aspNetUsersViewModels { get; set; }
        public IEnumerable<ApplicationRoleViewModel>? userRoles { get; set; }
    }
}
