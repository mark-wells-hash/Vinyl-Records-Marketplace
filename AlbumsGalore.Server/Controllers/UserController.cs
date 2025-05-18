using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AlbumsGalore.Server.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System;
using System.Linq;
using AlbumsGalore.Server.DataAccess;

namespace AlbumsGalore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserDataAccessLayer objUser = new UserDataAccessLayer();
        [HttpGet]
        [Route("UserIndex")]
        public IEnumerable<User> GetUsers()
        {
            var allUsers = objUser.GetAllUsers();
            return allUsers; 
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public User GetUser(int id)
        {
            var user = objUser.GetUserByID(id);
           
            return user;
        }

        [HttpPost]
        [Route("Create")]
        public int Create(User user)
        {
            //TODO: Check to see if artist already exists. Do the same for the other entity creations-
            return objUser.AddUser(user, 2);
            //return 5;
        }

        [HttpPut]
        [Route("UpdateUser")]
        public int UpdateUser(User user)
        {
            //var artist1 = objArtist.GetArtistData(artist.ArtistId);
            //artist1.Albums[0].
            return objUser.UpdateUser(user);
            //return 1;
        }

        [HttpGet]
        [Route("GetPriviledges/{id}")]
        public List<String> GetPriviledges(int userId)
        {
            List<String> permissions = objUser.GetPriviledges(userId);
            return permissions;
        }

        [HttpGet]
        [Route("HasPriviledge/{id}")]
        public bool HasPriviledge(int userId, string priviledge)
        {
            //TODO: make this better in the data access layer if I decide to use this method
            List<String> permissions = objUser.GetPriviledges(userId);
            if (permissions.Contains(priviledge))
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("Authorize/{userName}/{password}")]
        public User Authorize(String userName, String password)
        {
            Debug.WriteLine(userName + ": " + password);
            User? user = objUser.AuthorizeUserByUserName(userName);
            if (user != null)
            {
                if (user.Password == password) {
                    return user;
                }
            }
            return new Server.Models.User();
        }

        [HttpGet]
        [Route("AddUserAudit/{userId}/{auditType}")]
        public int AddUserAudit(int userId, string auditType)
        {
            objUser.AddUserAudit(userId, auditType);
            return userId;
        }
    }
}
