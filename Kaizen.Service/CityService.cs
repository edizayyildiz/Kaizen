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
    public interface ICityService
    {
        void Insert(City entity);
        void Update(City entity);
        void Delete(Guid id);
        void Delete(City entity);
        City Find(Guid id);
        IEnumerable<City> GetAll();
        IEnumerable<City> GetAll(Expression<Func<City, bool>> where);
    }
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<City> cityRepository;
        public CityService(IUnitOfWork unitOfWork, IRepository<City> repository)
        {
            this.cityRepository = repository;
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

        public void Delete(City entity)
        {
            cityRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public City Find(Guid id)
        {
            return cityRepository.Find(id);
        }

        public IEnumerable<City> GetAll()
        {
            return cityRepository.GetAll();
        }

        public IEnumerable<City> GetAll(Expression<Func<City, bool>> where)
        {
            return cityRepository.GetAll(where);
        }

        public void Insert(City entity)
        {
            cityRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(City entity)
        {
            var model = this.Find(entity.Id);
            model.Name = entity.Name;
            model.CountryId = entity.CountryId;
            cityRepository.Update(model);
            unitOfWork.SaveChanges();
        }
    }
}
