using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using AlbumsGalore.Server.Utilities;
using System.Linq;
using System.Data;
using System.Diagnostics;
using AlbumsGalore.Server.Models;

namespace AlbumsGalore.Server.DataAccess
{
    public class UserDataAccessLayer
    {
        ArtistsContext userContext = new ArtistsContext();

        public UserDataAccessLayer()
        {

        }
        //I don't have a reason to get all users at this point
        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                List<User> users = userContext.Users.ToList();
                string userName = users[0].UserName;

                //can add collections of UserDataRoleAssociation, 2Factor and UserAudit
                //foreach (var user in users)
                //{
                //    artist.Albums.ToList();
                //    //artistObj.Songs.ToList();
                //    artist.Musicians.ToList();
                //}
                //artists.Albums.ToList();
                return users;
            }
            catch
            {
                throw;
            }
        }
        public User GetUserByID(int id)
        {
            try
            {
                User user = userContext.Users.Find(id)!;
                return user;
            }
            catch
            {
                throw;
            }

        }

        public User? AuthorizeUserByUserName(string userName)
        {
            try
            {
                IQueryable<User> user = userContext.Users.Where(m => m.UserName == userName);

                if (user.Count() > 0)
                {
                    Debug.WriteLine(user.First());
                    user.First().LastLogin = DateTime.Now;
                    userContext.Entry(user.First()).State = EntityState.Modified;
                    User userObj = new User
                    {
                        UserId = user.First().UserId,
                        UserName = user.First().UserName,
                        Password = CommonFunctions.DecodeFrom64(user.First().Password),
                        FirstName = user.First().FirstName,
                        LastName = user.First().LastName,
                        Email = user.First().Email,
                        PhoneNumber = user.First().PhoneNumber,
                        Albums = user.First().Albums
                    };

                    userContext.SaveChanges();

                    AddUserAudit(userObj.UserId, "LoginUser");
                    return userObj;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }

        public List<string> GetPriviledges(int id)
        {
            try
            {
                var results = from UserRolesAssociations in userContext.UserRolesAssociations
                              join UserRoles in userContext.UserRoles
                                on UserRolesAssociations.RoleId equals UserRoles.RoleId
                              join UserRolePermissionAssociations in userContext.UserRolePermissionAssociations
                                on UserRoles.RoleId equals UserRolePermissionAssociations.RoleId
                              join UserPermissions in userContext.UserPermissions
                                on UserRolePermissionAssociations.PermissionId equals UserPermissions.PermissionsId
                              where UserRolesAssociations.UserId == id
                              select new { UserPermissions };

                List<string> listPermissions = new List<string>();
                ; foreach (var r in results)
                {
                    listPermissions.Add(r.UserPermissions!.PermissionsName!);
                    //Debug.WriteLine(r.UserPermissions.PermissionsName);
                }
                return listPermissions;
            }
            catch
            {
                throw;
            }
        }

        public int AddUser(User userObj, int roleId)
        {
            //userContext , int userRolesAssociationsId
            try
            {
                if (userObj.UserId == 0)
                {
                    string encryted = CommonFunctions.EncodePasswordToBase64(userObj.Password);
                    userObj.Password = encryted;
                    userContext.Users.Add(userObj);
                }
                userContext.SaveChanges();
                UserRolesAssociation userRolesAssociations = new UserRolesAssociation();
                userRolesAssociations.UserId = userObj.UserId;
                userRolesAssociations.RoleId = roleId;
                List<UserRolesAssociation> rolesAssociations = [userRolesAssociations];
                foreach (UserRolesAssociation userRolesAssociation in rolesAssociations)
                {
                    if (userRolesAssociation.UserRoleId == 0)
                    {
                        userContext.UserRolesAssociations.Add(userRolesAssociation);
                    }
                }
                userContext.SaveChanges();
                AddUserAudit(userObj.UserId, "AddUser");
                return userObj.UserId;
            }
            catch
            {
                throw;
            }
        }

        //To Update the records of a User    
        public int UpdateUser(User userObj)
        {
            try
            {
                string encryted = CommonFunctions.EncodePasswordToBase64(userObj.Password);
                userObj.Password = encryted;
                userContext.Entry(userObj).State = EntityState.Modified;
                userContext.SaveChanges();

                AddUserAudit(userObj.UserId, "UpdateUser");

                return 1;
            }
            catch
            {
                throw;
            }
        }

        public void AddUserAudit(int userId, string auditType)
        {
            try
            {
                UserAudit userAudit = new UserAudit();
                userAudit.UserId = userId;
                userAudit.ActivityType = auditType;
                userAudit.Description = auditType;
                userContext.UserAudits.Add(userAudit);
                userContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
