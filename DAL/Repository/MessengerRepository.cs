using DAL.Base;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Models.Messengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class MessengerRepository : RepoBase
    {
        private MessengerContext _context;
        public MessengerRepository(MessengerContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Pool> GetPools()
        {
            return _context.Pools.Include(i => i.Members).Include(i => i.Messages);
        }
        public IQueryable<Member> GetMembers()
        {
            return _context.Members;
        }
        public IQueryable<Message> GetMessages()
        {
            return _context.Messages;
        }
        public Pool PostPool(Pool model)
        {
            try
            {
                return Insert(_context.Pools, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Member PostMember(Member model)
        {
            try
            {
                return Insert(_context.Members, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Message PostMessage(Message model)
        {
            try
            {
                return Insert(_context.Messages, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Pool PutPool(Pool model)
        {
            try
            {
                return Update(_context.Pools, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Member PutMember(Member model)
        {
            try
            {
                return Update(_context.Members, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Message PutMessage(Message model)
        {
            try
            {
                return Update(_context.Messages, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeletePool(Pool model)
        {
            try
            {
                return Delete(_context.Pools, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteMember(Member model)
        {
            try
            {
                return Delete(_context.Members, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteMessage(Message model)
        {
            try
            {
                return Delete(_context.Messages, model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
