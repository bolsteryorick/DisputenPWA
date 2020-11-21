using DisputenPWA.DAL.Models;
using DisputenPWA.Domain.Hierarchy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisputenPWA.DAL.Repositories.Base
{
    public interface IRepository<TModel>
        where TModel : class, new()
    {
        IQueryable<TModel> GetQueryable();
        Task Add(TModel item);
        Task Add(IEnumerable<TModel> items);
        Task DeleteByQuery(IQueryable<TModel> toBeDeleted);
        Task DeleteByObject(TModel toBeDeleted);
        Task Update(TModel item);
        Task<TModel> UpdateProperties(TModel item, Dictionary<string, object> properties);
    }

    public class Repository<TModel> : IRepository<TModel>
        where TModel : class, new()
    {
        private readonly DisputenAppContext _context;

        public Repository(
            DisputenAppContext context
            )
        {
            _context = context;
        }

        public IQueryable<TModel> GetQueryable()
        {
            return _context.Set<TModel>().AsNoTracking().AsQueryable();
        }

        public async Task Add(TModel item)
        {
            var itemInList = new List<TModel> { item };
            await Add(itemInList);
        }

        public async Task Add(IEnumerable<TModel> items)
        {
            _context.Set<TModel>().AddRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByQuery(IQueryable<TModel> toBeDeleted)
        {
            _context.Set<TModel>().RemoveRange(toBeDeleted);
            await _context.SaveChangesAsync();
        }

        // todo check if this works
        public async Task DeleteByObject(TModel toBeDeleted)
        {
            _context.Set<TModel>().Remove(toBeDeleted);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TModel item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TModel> UpdateProperties(TModel item, Dictionary<string, object> properties)
        {
            _context.Attach(item);
            UpdateItemProperties(item, properties);
            await _context.SaveChangesAsync();
            return item;
        }

        private void UpdateItemProperties(TModel item, Dictionary<string, object> properties)
        {
            foreach (var property in properties)
            {
                UpdateItemProperty(item, property.Key, property.Value);
            }
        }

        private void UpdateItemProperty(TModel item, string propertyName, object newValue)
        {
            var propertyInfo = typeof(TModel).GetProperty(propertyName);
            propertyInfo.SetValue(item, newValue);
        }
    }
}
