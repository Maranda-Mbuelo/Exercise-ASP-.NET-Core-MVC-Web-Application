using ExerciseApp.Data.Enum;
using ExerciseApp.Data;
using ExerciseApp.Interfaces;
using ExerciseApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;

        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRaceByCity(string city)
        {
            return await _context.Races.Include(c => c.Address).Where(r => r.Address.City == city).ToListAsync();
        }

        public async Task<Race?> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Races.CountAsync();
        }

        public async Task<int> GetCountByCategoryAsync(RaceCategory category)
        {
            return await _context.Races.CountAsync(r => r.RaceCategory == category);
        }

        public async Task<IEnumerable<Race>> GetSliceAsync(int offset, int size)
        {
            return await _context.Races.Include(a => a.Address).Skip(offset).Take(size).ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetRacesByCategoryAndSliceAsync(RaceCategory category, int offset, int size)
        {
            return await _context.Races
                .Where(r => r.RaceCategory == category)
                .Include(a => a.Address)
                .Skip(offset)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Race?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Add(Race race)
        {
            try
            {
                _context.Add(race);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Race race)
        {
            try
            {
                _context.Update(race);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return false;
            }
        }

        public bool Delete(Race race)
        {
            try
            {
                _context.Remove(race);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return false;
            }
        }

        public bool Save(Race race)
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here
                return false;
            }
        }

    }
}
