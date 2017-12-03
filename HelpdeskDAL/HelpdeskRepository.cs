using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Reflection;

namespace HelpdeskDAL
{
    public class HelpdeskRepository<T> : IRepository<T> where T : EmployeeEntity
    {

        // create HelpdeskContext to get data
        private HelpdeskContext cty = null;
        public HelpdeskRepository(HelpdeskContext context = null)
        {
            cty = context != null ? context : new HelpdeskContext();
        }

        // create getall
        public List<T> GetAll()
        {
            return cty.Set<T>().ToList();
        }

        // create GetByExpression
        public List<T> GetByExpression(Expression<Func<T, bool>> lambdaExp)
        {
            return cty.Set<T>().Where(lambdaExp).ToList();
        }

     //create   Add
        public T Add(T entity)
        {
            cty.Set<T>().Add(entity);
            cty.SaveChanges();
            return entity;

        }

        //create Update
        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                EmployeeEntity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                cty.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                cty.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
                if (cty.SaveChanges() == 1)
                    opStatus = UpdateStatus.Ok;
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                opStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + MethodBase.GetCurrentMethod().Name + ex.Message);

            }
            return opStatus;
        }

        // create delete
        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            cty.Set<T>().Remove(currentEntity);
            return cty.SaveChanges();
        }
    }
}
