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
    public interface ISuggestionService
    {
        void Insert(Suggestion entity);
        void Update(Suggestion entity);
        void Delete(Suggestion entity);
        void Delete(Guid id);
        Suggestion Find(Guid id);
        IEnumerable<Suggestion> GetAll();
        IEnumerable<Suggestion> GetAll(Expression<Func<Suggestion, bool>> where);
    }

    public class SuggestionService : ISuggestionService
    {
        private readonly IRepository<Suggestion> suggestionRepository;
        private readonly IUnitOfWork unitOfWork;
        public SuggestionService(IUnitOfWork unitOfWork, IRepository<Suggestion> suggestionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.suggestionRepository = suggestionRepository;
        }
        public void Delete(Suggestion entity)
        {
            suggestionRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.Find(id);
            if (entityToDelete != null)
            {
                this.Delete(entityToDelete);
            }
        }

        public Suggestion Find(Guid id) // bu methodu constructuredan sonra yazmak gerekebilir bir üstteki delete ile alakalı find da bir sorun olursa bak?
        {
            return suggestionRepository.Find(id);
        }

        public IEnumerable<Suggestion> GetAll()
        {
            return suggestionRepository.GetAll();
        }

        public IEnumerable<Suggestion> GetAll(Expression<Func<Suggestion, bool>> where)
        {
            return suggestionRepository.GetAll(where);
        }

        public void Insert(Suggestion entity)
        {
            suggestionRepository.Insert(entity);
            entity.Assessment = Assessment.Bekleniyor;
            unitOfWork.SaveChanges();
        }

        public void Update(Suggestion entity)
        {
            var model = this.Find(entity.Id);
            model.Assessment = entity.Assessment;
            model.CurrentStatus = entity.CurrentStatus;
            model.EmployeeId = entity.EmployeeId;
            model.SuggestedStatus = entity.SuggestedStatus;
            model.Subject = entity.Subject;
            suggestionRepository.Update(model);
            unitOfWork.SaveChanges();
        }
    }
}
