using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingPlanner.Models;

namespace MeetingPlanner.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly MeetingPlannerContext _context;

        public SpeakersController(MeetingPlannerContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index(string filterString)
        {
            //ViewData["CurrentFilter"] = filterString;

            var speakers = from s in _context.Speaker
                           select s;
            if (!String.IsNullOrEmpty(filterString))
            {
                int meetingId = Convert.ToInt32(RouteData.Values[filterString]);
                speakers = speakers.Where(s => s.MeetingId.Equals(meetingId));
            }

            //var meetingPlannerContext = _context.Speaker.Include(s => s.SpeakerNavigation);
            return View(await speakers.ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.SpeakerNavigation)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            ViewData["SpeakerId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingHymn");
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerId,FirstName,LastName,Subject,MeetingId")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                speaker.MeetingId = Convert.ToInt32(RouteData.Values["id"]);

                _context.Add(speaker);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Speakers", new { id = speaker.MeetingId });
            }
            ViewData["SpeakerId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingHymn", speaker.SpeakerId);
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }
            ViewData["SpeakerId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingHymn", speaker.SpeakerId);
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerId,FirstName,LastName,Subject,MeetingId")] Speaker speaker)
        {
            if (id != speaker.SpeakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.SpeakerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeakerId"] = new SelectList(_context.Meeting, "MeetingId", "ClosingHymn", speaker.SpeakerId);
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .Include(s => s.SpeakerNavigation)
                .SingleOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }
            string stringID = id.ToString();

            return RedirectToAction("View", "Speakers", new { id });


            //return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speaker = await _context.Speaker.SingleOrDefaultAsync(m => m.SpeakerId == id);
            _context.Speaker.Remove(speaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speaker.Any(e => e.SpeakerId == id);
        }

    }
}
