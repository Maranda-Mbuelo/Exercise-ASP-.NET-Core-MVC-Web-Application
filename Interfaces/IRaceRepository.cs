using ExerciseApp.Models;

namespace ExerciseApp.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetAllRaceByCity(string city);
        bool Add(Race race);
        bool Delete(Race race);
        bool Update(Race race);
        bool Save(Race race);
    }
}
