using ShaTask.Models;

namespace ShaTask.Repository.CasherRepo
{
    public interface ICasherRepo
    {
        List<Cashier> getAll();
        Cashier getById(int id);
        void add(Cashier cashier);
        void remove(int id);
        void update(Cashier cashier);
        void save();
        bool any(int id);
    }
}
