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
    public interface IEmployeeService
    {
        void Insert(Employee entity);
        void Update(Employee entity);
        void Delete(Guid id);
        void Delete(Employee entity);
        Employee Find(Guid id);
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetAll(Expression<Func<Employee, bool>> where);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IUnitOfWork unitOfWork;
        public EmployeeService(IRepository<Employee> employeeRepository, IUnitOfWork unitOfWork)
        {
            this.employeeRepository = employeeRepository;
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

        public void Delete(Employee entity)
        {
            employeeRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public Employee Find(Guid id)
        {
            return employeeRepository.Find(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return employeeRepository.GetAll();
        }

        public IEnumerable<Employee> GetAll(Expression<Func<Employee, bool>> where)
        {
            return employeeRepository.GetAll(where);
        }

        public void Insert(Employee entity)
        {
            employeeRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(Employee entity)
        {
            var model = this.Find(entity.Id);
            model.Email = entity.Email;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.UserName = entity.UserName;
            model.Posation = entity.Posation;
            model.Departments.Clear();
            foreach (var item in entity.Departments)
            {
                model.Departments.Add(item);
            }
            employeeRepository.Update(model);
            unitOfWork.SaveChanges();
        }
    }
}
