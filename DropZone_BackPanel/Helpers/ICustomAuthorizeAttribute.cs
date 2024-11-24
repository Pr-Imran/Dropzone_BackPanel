namespace DropZone_BackPanel.Helpers
{
    public interface ICustomAuthorizeAttribute
    {
        Task<bool> GetAuthorizations();
        Task<bool> GetRoleWisePageAuthorizations();
    }
}
