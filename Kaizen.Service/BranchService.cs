﻿using Kaizen.Data;
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
        IEnumerable<Branch> GetBranchesByCompany(Guid CompanyId);
        IEnumerable<Branch> Search(string name);

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
            var model = this.Find(entity.Id);
            model.Name = entity.Name;
            model.Address = entity.Address;
            //model.CountryId = entity.CountryId;
            //model.CityId = entity.CityId;
            //model.CountyId = entity.CountyId;
            branchRepository.Update(model);
            unitOfWork.SaveChanges();
        }
        public IEnumerable<Branch> GetBranchesByCompany(Guid CompanyId)
        {
            return this.GetAll().Where(f => f.CompanyId == CompanyId);
        }

        public IEnumerable<Branch> Search(string name)
        {
            return this.GetAll(f => f.Name.Contains(name));
        }
    }
}
