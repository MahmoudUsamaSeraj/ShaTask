using ShaTask.Models;

namespace ShaTask.Repository.CityRepo
{
    public class CityRepo:ICityRepo
    {
        ShaTaskContext db;
        public CityRepo(ShaTaskContext db)
        {
            this.db = db;
        }

        public void add(City city)
        {
            db.Cities.Add(city);
        }

        public List<City> getAll()
        {
            return db.Cities.ToList();
        }

        public City getById(int id)
        {
            return db.Cities.Find(id);
        }

        public void remove(int id)
        {
            City c = getById(id);
            db.Cities.Remove(c);
        }

        public void save()
        {
            db.SaveChanges();
        }

        public void update(City city)
        {
            db.Entry(city).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
