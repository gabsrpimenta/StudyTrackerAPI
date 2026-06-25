using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyTrackerAPI.Data;
using StudyTrackerAPI.Models;
using System.Security.Claims;

namespace StudyTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SessionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudySession>>> GetSessions()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (int.TryParse(userIdClaim, out int userId))
            {
                // CORRIGIDO AQUI: _context.Sessions em vez de _context.StudySessions
                return await _context.Sessions
                                     .Where(s => s.UserId == userId)
                                     .ToListAsync();
            }

            // CORRIGIDO AQUI: _context.Sessions
            return await _context.Sessions.ToListAsync(); 
        }

        // POST: api/Sessions
        [HttpPost]
        public async Task<ActionResult<StudySession>> CreateSession(StudySession session)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int userId))
            {
                session.UserId = userId;
                
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    string hojeStr = session.Date;

                    if (DateTime.TryParse(hojeStr, out DateTime dataSessao))
                    {
                        string ontemStr = dataSessao.AddDays(-1).ToString("yyyy-MM-dd");

                        if (user.LastStudyDate != hojeStr)
                        {
                            if (user.LastStudyDate == ontemStr)
                            {
                                user.Streak += 1; 
                            }
                            else
                            {
                                user.Streak = 1; 
                            }

                            user.LastStudyDate = hojeStr;
                        }
                    }
                }
            }

            // CORRIGIDO AQUI: _context.Sessions
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSessions), new { id = session.Id }, session);
        }
    }
}