using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using System.Data.Entity.Infrastructure;
//added
using System.Linq.Expressions;
//added
using System.Reflection;

namespace HelpdeskDAL
{
    public class HelpdeskRepository<T> : IRepository<T> where T : EmployeeEntity
    {
        private HelpdeskContext cty = null;
        public HelpdeskRepository(HelpdeskContext context = null)
        {
            cty = context != null ? context : new HelpdeskContext();
        }
        public List<T> GetAll()
        {
            return cty.Set<T>().ToList();
        }
        public List<T> GetByExpression(Expression<Func<T, bool>> lambdaExp)
        {
            return cty.Set<T>().Where(lambdaExp).ToList();
        }
        public T Add(T entity)
        {
            cty.Set<T>().Add(entity);
            cty.SaveChanges();
            return entity;

        }
        public UpdateStatus Update(T updatedEntity)
        {
            UpdateStatus opStatus = UpdateStatus.Failed;
            try
            {
                //SchoolEntity currentEntity = GetByExpression(ent => ent.Id == updatedEntity.Id).FirstOrDefault();
                //ctx.Entry(currentEntity).OriginalValues["Timer"] = updatedEntity.Timer;
                //ctx.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
                //if (ctx.SaveChanges() == 1)
                //    opStatus = UpdateStatus.ok;
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
        public int Delete(int id)
        {
            T currentEntity = GetByExpression(ent => ent.Id == id).FirstOrDefault();
            cty.Set<T>().Remove(currentEntity);
            return cty.SaveChanges();
        }
    }
}
