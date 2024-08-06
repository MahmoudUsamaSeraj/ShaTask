using ShaTask.Models;

namespace ShaTask.Repository.CasherRepo
{
    public class CasherRepo : ICasherRepo
    {
        ShaTaskContext db;
        public CasherRepo(ShaTaskContext db )
        {
            this.db = db;
        }

        public void add(Cashier cashier)
        {
            db.Cashiers.Add( cashier );
        }

        public List<Cashier> getAll()
        {
            return db.Cashiers.ToList();
        }

        public Cashier getById(int id)
        {
            return db.Cashiers.Find(id);
        }

        public void remove(int id)
        {
            Cashier c = getById(id);
            db.Cashiers.Remove(c);
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void update(Cashier cashier)
        {
            db.Entry(cashier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
