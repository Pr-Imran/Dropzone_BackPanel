namespace DropZone_BackPanel.Areas.Auth.Models
{
    public class AspNetUsersViewModel
    {
        public string? aspnetId { get; set; }
        public string? userName { get; set; }

        public string? email { get; set; }

        public string? empCode { get; set; }

        public int? isActive { get; set; }

        public string? empName { get; set; }
        public int? employeeId { get; set; }

        public string? sectionName { get; set; }

        public string? rankName { get; set; }

        public string? unitName { get; set; }
        public string? designationName { get; set; }

        public DateTime? joiningDate { get; set; }
        public DateTime? joiningDatePresentWorkStation { get; set; }
        public string? mobileNo { get; set; }
        public int? status { get; set; }

        public string? roleId { get; set; }
        public string? roleName { get; set; }
        public string? batchName { get; set; }
        public string? branchName { get; set; }

        public int? unitId { get; set; }
        public int? rankId { get; set; }
        public string? imageUrl { get; set; }
    }
}
