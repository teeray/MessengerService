using DAL.Base;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class UserRepository : RepoBase
    {
        public UserContext _context;
        public UserRepository(UserContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<User> GetUsers()
        {
            return _context.Users.Include(i => i.Contacts).Include(i => i.Avatar);
        }
        public IQueryable<Image> GetImages()
        {
            return _context.Images;
        }
        public IQueryable<Contact> GetContacts()
        {
            return _context.Contacts;
        }
        public User PostUser(User model)
        {
            try
            {
                return Insert(_context.Users, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Image PostImage(Image model)
        {
            try
            {
                return Insert(_context.Images, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Contact PostContact(Contact model)
        {
            try
            {
                return Insert(_context.Contacts, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public User PutUser(User model)
        {
            try
            {
                return Update(_context.Users, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Image PutImage(Image model)
        {
            try
            {
                return Update(_context.Images, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Contact PutContact(Contact model)
        {
            try
            {
                return Update(_context.Contacts, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteUser(User model)
        {
            try
            {
                return Delete(_context.Users, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteImage(Image model)
        {
            try
            {
                return Delete(_context.Images, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteContact(Contact model)
        {
            try
            {
                return Delete(_context.Contacts, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
