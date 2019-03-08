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
    public interface IDepartmentService
    {
        void Insert(Department entity);
        void Update(Department entity);
        void Delete(Guid id);
        void Delete(Department entity);
        Department Find(Guid id);
        IEnumerable<Department> GetAll();
        IEnumerable<Department> GetAll(Expression<Func<Department, bool>> where);
    }
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> departmentRepository;
        private readonly IUnitOfWork unitOfWork;
        public DepartmentService(IRepository<Department> departmentRepository, IUnitOfWork unitOfWork)
        {
            this.departmentRepository = departmentRepository;
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

        public void Delete(Department entity)
        {
            departmentRepository.Delete(entity);
            this.unitOfWork.SaveChanges();
        }

        public Department Find(Guid id)
        {
            return departmentRepository.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return departmentRepository.GetAll();
        }

        public IEnumerable<Department> GetAll(Expression<Func<Department, bool>> where)
        {
            return departmentRepository.GetAll(where);
        }

        public void Insert(Department entity)
        {
            this.departmentRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(Department entity)
        {
            var model = this.Find(entity.Id);
            model.CompanyId = entity.CompanyId;
            model.Name = entity.Name;
            departmentRepository.Update(model);
            unitOfWork.SaveChanges();
        }
    }
}
