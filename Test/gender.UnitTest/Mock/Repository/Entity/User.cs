﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using gender.Model;

namespace gender.UnitTest.Mock
{
    public partial class MockRepository
    {
        public List<User> Users { get; set; }

        public void GenerateUsers()
        {
            Users = new List<User>();

            var admin = new User()
            {
                ID = 1,
                ActivatedDate = DateTime.Now,
                ActivatedLink = "",
               
                Password = "password",
                LastVisitDate = DateTime.Now,
            };

            var role = Roles.First(p => p.Code == "admin");
            var userRole = new UserRole()
            {
                User = admin,
                UserID = admin.ID,
                Role = role,
                RoleID = role.ID
            };

            admin.UserRoles = 
                new EntitySet<UserRole>() {
                    userRole
                };
            Users.Add(admin);

            Users.Add(new User()
            {
                ID = 2,
                ActivatedDate = DateTime.Now,
                ActivatedLink = "",
                Password = "password2",
                LastVisitDate = DateTime.Now
            });

            this.Setup(p => p.Users).Returns(Users.AsQueryable());
            this.Setup(p => p.GetUser(It.IsAny<string>())).Returns((string email) => 
                Users.FirstOrDefault(p => string.Compare(p.Email, email, 0) == 0));
        }
    }
}
