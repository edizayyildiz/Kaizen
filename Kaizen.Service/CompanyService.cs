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
    public interface ICompanyService
    {
        void Insert(Company entity);
        void Update(Company entity);
        void Delete(Guid id);
        void Delete(Company entity);
        Company Find(Guid id);
        IEnumerable<Company> GetAll();
        IEnumerable<Company> GetAll(Expression<Func<Company, bool>> where);
        IEnumerable<Company> Search(string name);
    }
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> companyRepository;
        private readonly IUnitOfWork unitOfWork;
        public CompanyService(IRepository<Company> repository, IUnitOfWork unitOfWork)
        {
            this.companyRepository = repository;
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

        public void Delete(Company entity)
        {
            companyRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public Company Find(Guid id)
        {
            return companyRepository.Find(id);
        }

        public IEnumerable<Company> GetAll()
        {
            return companyRepository.GetAll();
        }

        public IEnumerable<Company> GetAll(Expression<Func<Company, bool>> where)
        {
            return companyRepository.GetAll(where);
        }

        public void Insert(Company entity)
        {
            companyRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public IEnumerable<Company> Search(string name)
        {
            return this.GetAll(f => f.Name.Contains(name));
        }

        public void Update(Company entity)
        {
            var model = this.Find(entity.Id);
            model.Description = entity.Description;
            model.HeadCount = entity.HeadCount;
            model.Name = entity.Name;
            model.Sector = entity.Sector;
            companyRepository.Update(model);
            unitOfWork.SaveChanges();
        }
    }
}
