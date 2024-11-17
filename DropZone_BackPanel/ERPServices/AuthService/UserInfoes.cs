
using DropZone_BackPanel.Areas.Auth.Models;
using DropZone_BackPanel.Context;
using DropZone_BackPanel.Data.Entity;
using DropZone_BackPanel.ERPService.AuthService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DropZone_BackPanel.ERPServices.AuthService
{
    public class UserInfoes : IUserInfoes
    {
        private readonly DropZoneDbContext _context;

        public UserInfoes(DropZoneDbContext context)
        {
            _context = context;
        }

       
        public async Task<ApplicationUser> GetAspnetuserByUser(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<ApplicationUser> GetUserInfoByUser(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetRoleListByUserId(string Id)
        {
            return  await _context.UserRoles.Where(x=>x.UserId==Id).Select(x=>x.RoleId).ToListAsync();
        }
          public async Task<string> GetRoleNameByUserId(string Id)
        {
            var data =  await _context.UserRoles.Where(x=>x.UserId==Id).FirstOrDefaultAsync();
            var roleName = await _context.Roles.Where(x => x.Id == data.RoleId).Select(x=>x.Name).FirstOrDefaultAsync();
            
            return roleName;
        }

        


        public async Task<string> GetRoleNameByUserIdNew(string Id)
        {
            var data = await _context.UserRoles.Where(x => x.UserId == Id).LastOrDefaultAsync();
            var roleName = await _context.Roles.Where(x => x.Id == data.RoleId).Select(x => x.Name).FirstOrDefaultAsync();

            return roleName;
        }


        public async Task<bool> DeleteUserRoleListByUserId(string Id)
        {
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(x => x.UserId == Id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRoleById(string Id)
        {
            _context.Roles.Remove(_context.Roles.Where(x => x.Id == Id).First());
            return 1 == await _context.SaveChangesAsync();
        }

        
        public async Task<ApplicationUser> GetUserBasicInfoes(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ApplicationUser>> GetUserInfoListByUser(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).ToListAsync();
        }

       
       
       
        public async Task<bool> DeleteUserById(string id)
        {
            _context.Users.Remove(await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync());
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<ApplicationUser> UpdateAspNetUser(string userId)
        {
            try
            {
                var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> UpdateAspNetUser(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }
        

    }
}
