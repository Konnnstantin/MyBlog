using MyBlog.DAL.Interface;
using MyBlog.Domains.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DAL.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _db;
        public TagRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task Create(Tag entity)
        {
            await _db.Tags.AddAsync(entity);
        }

        public async Task Delete(Guid id)
        {
           await Task.Run(()=> _db.Tags.Remove(Get(id)));
        }

        public Tag Get(Guid id)
        {
            return _db.Tags.FirstOrDefault(_ => _.Id == id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return _db.Tags.ToList();
        }

        public async Task<Tag> Update(Tag entity)
        {
            await Task.Run(() => _db.Tags.Update(entity));
            return entity;
        }
    }
}
