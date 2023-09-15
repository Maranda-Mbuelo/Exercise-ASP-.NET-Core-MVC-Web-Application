using ExerciseApp.Data;
using ExerciseApp.Interfaces;
using ExerciseApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository RaceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            RaceRepository = raceRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await RaceRepository.GetAll();
            return View(races);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await RaceRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }
            this.RaceRepository.Add(race);
            return RedirectToAction("Index");
        }

    }
}
