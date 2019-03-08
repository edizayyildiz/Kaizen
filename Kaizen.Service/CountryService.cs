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
    public interface ICountryService
    {
        void Insert(Country entity);
        void Update(Country entity);
        void Delete(Guid id);
        void Delete(Country entity);
        Country Find(Guid id);
        IEnumerable<Country> GetAll();
        IEnumerable<Country> GetAll(Expression<Func<Country, bool>> where);
    }
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> countryRepository;
        private readonly IUnitOfWork unitOfWork;
        public CountryService(IRepository<Country> repository, IUnitOfWork unitOfWork)
        {
            this.countryRepository = repository;
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

        public void Delete(Country entity)
        {
            this.countryRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public Country Find(Guid id)
        {
            return countryRepository.Find(id);
        }

        public IEnumerable<Country> GetAll()
        {
            return countryRepository.GetAll();
        }

        public IEnumerable<Country> GetAll(Expression<Func<Country, bool>> where)
        {
            return countryRepository.GetAll(where);
        }

        public void Insert(Country entity)
        {
            countryRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(Country entity)
        {
            var model = this.Find(entity.Id);
            model.Name = entity.Name;
            unitOfWork.SaveChanges();
        }
    }
}
