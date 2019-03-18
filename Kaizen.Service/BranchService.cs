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
    public interface IBranchService
    {
        void Insert(Branch entity);
        void Update(Branch entity);
        void Delete(Guid id);
        void Delete(Branch entity);
        Branch Find(Guid id);
        IEnumerable<Branch> GetAll();
        IEnumerable<Branch> GetAll(Expression<Func<Branch, bool>> where);

    }
    public class BranchService : IBranchService
    {
        private readonly IRepository<Branch> branchRepository;
        private readonly IUnitOfWork unitOfWork;
        public BranchService(IUnitOfWork unitOfWork, IRepository<Branch> branchRepository)
        {
            this.unitOfWork = unitOfWork;
            this.branchRepository = branchRepository;
        }
        public void Delete(Guid id)
        {
            var entityToDelete = this.Find(id);
            if (entityToDelete != null)
            {
                this.Delete(entityToDelete);
            }
        }

        public void Delete(Branch entity)
        {
            branchRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public Branch Find(Guid id)
        {
            return branchRepository.Find(id);
        }

        public IEnumerable<Branch> GetAll()
        {
            return branchRepository.GetAll();
        }

        public IEnumerable<Branch> GetAll(Expression<Func<Branch, bool>> where)
        {
            return branchRepository.GetAll(where);
        }

        public void Insert(Branch entity)
        {

            branchRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(Branch entity)
        {
            branchRepository.Update(entity);
            unitOfWork.SaveChanges();
        }
    }
}
