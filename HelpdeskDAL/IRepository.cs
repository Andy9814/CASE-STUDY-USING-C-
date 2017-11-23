using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using System.Linq.Expressions;
namespace HelpdeskDAL
{
    
        public interface IRepository<T>
        {
            List<T> GetAll();
            List<T> GetByExpression(Expression<Func<T, bool>> lambdaExp);
            T Add(T entity);
            UpdateStatus Update(T entity);
            int Delete(int i);

        }
    
}
