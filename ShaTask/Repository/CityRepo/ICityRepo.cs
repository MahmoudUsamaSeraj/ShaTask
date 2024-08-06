using ShaTask.Models;

namespace ShaTask.Repository.CityRepo
{
    public interface ICityRepo
    {
        List<City> getAll();
        City getById(int id);
        void add(City city);
        void remove(int id);
        void update(City city);
        void save();
    }
}
