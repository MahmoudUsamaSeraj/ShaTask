using ShaTask.Models;

namespace ShaTask.Repository.BranchRepo
{
    public class BranchRepo : IBranchRepo
    {

        ShaTaskContext db;
        public BranchRepo(ShaTaskContext db)
        {
            this.db = db;   
        }
        public void add(Branch branch)
        {
            db.Branches.Add(branch);
        }

        public List<Branch> getAll()
        {
            return db.Branches.ToList();
        }

        public Branch getById(int id)
        {
            return db.Branches.FirstOrDefault(b=>b.ID == id);
        }

        public void remove(int id)
        {
            Branch b = getById(id);
            db.Branches.Remove(b);
        }

        public void save()
        {
           db.SaveChanges();
        }

        public void update(Branch branch)
        {
            db.Entry(branch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
