using Kaizen.Data;
using Kaizen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kaizen.Service
{
    public interface ICountyService
    {
        void Insert(County entity);
        void Update(County entity);
        void Delete(Guid id);
        void Delete(County entity);
        County Find(Guid id);
        IEnumerable<County> GetAll();
        IEnumerable<County> GetAll(Expression<Func<County, bool>> where);
    }
    public class CountyService : ICountyService
    {
        private readonly IRepository<County> countyRepository;
        private readonly IUnitOfWork unitOfWork;
        public CountyService(IRepository<County> countyRepository, IUnitOfWork unitOfWork)
        {
            this.countyRepository = countyRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Delete(Guid id)
        {
            var entityToDelete = this.Find(id);
            if (entityToDelete != null)
            {
                this.Delete(entityToDelete);
            }
        }

        public void Delete(County entity)
        {
            this.countyRepository.Delete(entity);
            this.unitOfWork.SaveChanges();
        }

        public County Find(Guid id)
        {
            return countyRepository.Find(id);
        }

        public IEnumerable<County> GetAll()
        {
            return countyRepository.GetAll();
        }

        public IEnumerable<County> GetAll(Expression<Func<County, bool>> where)
        {
            return countyRepository.GetAll(where);
        }

        public void Insert(County entity)
        {
            this.countyRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(County entity)
        {
            var model = this.Find(entity.Id);
            model.CityId = entity.CityId;
            model.Name = entity.Name;
            countyRepository.Update(model);
            unitOfWork.SaveChanges();
        }
    }
}
