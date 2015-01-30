using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareHolderCore
{
    public interface IMembershipRepository<T>
    {
        void Save(T entity);
        void Update(T entity);
        void Delete(T entiy);
        T GetById(Guid id);
        T GetById(int id);
        T GetByLoginId(string loginId);
        T GetByLoginId(string loginId,string password);
        T GetByLoginId(string loginId, string password, string type);
        T GetByEmail(string email);
        bool ChangePassword(string loginId, string oldPassword, string newPassword);
        bool ResetPassword(string email, string newPassword);
        IList<T> GetAll();
        T GetByMemberId(int id);
    }
}
