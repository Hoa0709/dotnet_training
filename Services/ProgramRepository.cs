using System.Text;
using app.Connects;
using app.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public interface IPrograms
    {
        public List<ProgramListViewDto> GetListPrograms();
        public ProgramInfo GetProgramDetails(int id);
        public Boolean AddProgram(ProgramDto pr);
        public Boolean UpdateProgram(int id,ProgramDto pr);
        public Boolean DeleteProgram(int id);
        public Boolean CheckProgram(int id);
    }

    public class ProgramRepository : IPrograms
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProgramRepository(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //string random
        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string ComputeHash()
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(RandomString(30)));
                var sb = new StringBuilder();
                foreach (var c in data) {
                    sb.Append(c.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public List<ProgramListViewDto> GetListPrograms()
        {
            try
            {
                var ls = _context.programs
                                        .Select(x => new { x.pid, x.name, x.pathimage_list })
                                        .ToList();
                List<ProgramListViewDto> listview = new List<ProgramListViewDto>();
                foreach (var item in ls)
                {
                    listview.Add(new ProgramListViewDto(){pid = item.pid, name = item.name, pathimage_list = item.pathimage_list});
                }
                return listview;
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
                return pr;
            }
            catch
            {
                throw;
            }
        }

        public Boolean AddProgram(ProgramDto pr)
        {
            try
            {
                ProgramInfo addpr = _mapper.Map<ProgramInfo>(pr);
                addpr.create_at = DateTime.Now;
                addpr.md5 = ComputeHash();
                _context.programs.Add(addpr);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public Boolean UpdateProgram(int id,ProgramDto pr)
        {
            try
            {
                ProgramInfo p = _context.programs
                                    .Where(x => x.pid == id)
                                    .FirstOrDefault();            
                if (p!=null){
                    p.name = pr.name;
                    p.content = pr.content;
                    p.type_inoff = pr.type_inoff;
                    p.type_program = pr.type_program;
                    p.price = pr.price;
                    p.pathimage_list = pr.pathimage_list;
                    p.held_on = pr.held_on;
                    p.l_id = pr.l_id;
                    p.g_id = pr.g_id;
                    p.u_id = pr.u_id;
                    _context.Entry(p).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
                else return false;
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

        public Boolean CheckProgram(int id)
        {
            return _context.programs.Any(e => e.pid == id);
        }
    }
}