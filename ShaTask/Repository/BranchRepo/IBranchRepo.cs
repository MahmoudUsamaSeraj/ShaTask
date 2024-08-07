using ShaTask.Models;

namespace ShaTask.Repository.BranchRepo
{
    public interface IBranchRepo
    {
        List<Branch> getAll();
        Branch getById(int? id);
        void add(Branch branch);
        void remove(int id);
        void update(Branch branch);
        void save();

    }
}
