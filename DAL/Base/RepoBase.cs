using Microsoft.EntityFrameworkCore;
using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Base
{
    public class RepoBase
    {
        private DbContext _context;

        public RepoBase(DbContext context)
        {
            _context = context;
        }
        #region Helper
        protected internal void PostHelper<T>(T entity) where T : HIDC
        {
            entity.Created = DateTimeOffset.UtcNow;
            entity.LastUpdated = DateTimeOffset.UtcNow;
        }
        protected internal void PutHelper<T>(T entity) where T : HIDC
        {
            entity.LastUpdated = DateTimeOffset.UtcNow;
        }
        #endregion

        #region Post
        public T Insert<T>(DbSet<T> dbset, T entity) where T : HIDC
        {
            try
            {
                PostHelper(entity);
                dbset.Add(entity);
                _context.Entry(entity).State = EntityState.Added;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                //handle exception
                throw ex;
            }
        }
        #endregion

        #region Put
        public T Update<T>(DbSet<T> dbSet, T entity) where T : HIDC
        {
            try
            {
                PutHelper(entity);
                dbSet.Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                //handle exception
                throw ex;
            }
        }
        #endregion

        #region Delete
        public bool Delete<T>(DbSet<T> dbset, T entity) where T : HIDC
        {
            try
            {
                dbset.Remove(entity);
                _context.Entry(entity).State = EntityState.Deleted;
                var save = _context.SaveChanges();
                return save != 0;
            }
            catch (Exception ex)
            {
                //handle exception
                throw ex;
            }
        }
        #endregion
    }

}
