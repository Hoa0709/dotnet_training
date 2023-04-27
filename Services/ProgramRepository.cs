using app.Connects;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public class ProgramRepository : IPrograms
    {
        private readonly AppDbContext _context;
        public ProgramRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ProgramInfo> GetListPrograms()
        {
            try
            {
                return _context.programs.ToList();
            }
            catch
            {
                throw;
            }
        }

        public ProgramInfo GetProgramDetails(int id)
        {
            try
            {
                ProgramInfo pr = _context.programs
                                    .Where(x => x.pid == id)
                                    .FirstOrDefault();
                if (pr != null)
                {
                    return pr;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddProgram(ProgramInfo pr)
        {
            try
            {
                _context.programs.Add(pr);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateProgram(ProgramInfo pr)
        {
            try
            {
                _context.Entry(pr).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Boolean DeleteProgram(int id)
        {
            try
            {
                ProgramInfo pr = _context.programs
                                    .Where(x => x.pid == id)
                                    .FirstOrDefault();
                if (pr != null)
                {
                    _context.programs.Remove(pr);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
                throw;
            }
        }

        public bool CheckProgram(int id)
        {
            return _context.programs.Any(e => e.pid == id);
        }
    }
}