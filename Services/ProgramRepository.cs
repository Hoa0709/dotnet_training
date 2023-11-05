using app.Connects;
using app.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace app.Repository
{
    public interface IPrograms
    {
        public Task<List<ProgramListViewDto>> GetListProgramsAsync();
        public Task<ProgramInfo> GetProgramDetailsAsync(int id);
        public Task<Boolean> AddProgramAsync(ProgramDto pr);
        public Task<Boolean> UpdateProgramAsync(int id,ProgramInfo pr);
        public Task<Boolean> DeleteProgramAsync(int id);
        public Task<Boolean> CheckProgramAsync(int id);
        public Task<Boolean> CheckTypeProgramAsync(int id);
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

        public async Task<List<ProgramListViewDto>> GetListProgramsAsync()
        {
            try
            {
                var ls = await _context.programs
                                .Select(x => new { x.Id, x.Name, x.Pathimage_list })
                                .ToListAsync();
                List<ProgramListViewDto> listview = new List<ProgramListViewDto>();
                foreach (var item in ls)
                {
                    listview.Add(new ProgramListViewDto(){Id = item.Id, Name = item.Name, Pathimage_list = item.Pathimage_list});
                }
                return listview;
            }
            catch
            {
                throw new Exception("Error! When Get Events List");
            }
        }

        public async Task<ProgramInfo> GetProgramDetailsAsync(int id)
        {
            try
            {
                return await _context.programs
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();
            }
            catch
            {
                throw new Exception("Error! When Get Event");
            }
        }

        public async Task<Boolean> AddProgramAsync(ProgramDto pr)
        {
            try
            {
                ProgramInfo addpr = _mapper.Map<ProgramInfo>(pr);
                addpr.Create_at = DateTime.Now;
                await _context.programs.AddAsync(addpr);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new Exception("Error! When Add Event");
            }
        }

        public async Task<Boolean> UpdateProgramAsync(int id,ProgramInfo pr)
        {
            try
            {   if (id == pr.Id)
                {
                    _context.Entry(pr).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception("Error! When Update Event");
            }
        }

        public async Task<Boolean> DeleteProgramAsync(int id)
        {
            try
            {
                ProgramInfo pr = await _context.programs
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();
                if (pr != null)
                {
                    _context.programs.Remove(pr);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
                }
            catch
            {
               throw new Exception("Error! When Delete Event");
            }
        }

        public async Task<Boolean> CheckProgramAsync(int id)
        {
            try
            {
                return await _context.programs.AnyAsync(e => e.Id == id);
            }
            catch
            {
               throw new Exception("Error! When Check Event");
            }
        }

        public async Task<bool> CheckTypeProgramAsync(int id)
        {
            try
            {
                return await _context.programs.AnyAsync(e => e.Id == id && e.Type_inoff == 2);
            }
            catch
            {
               throw new Exception("Error! When Check Event");
            }
        }
    }
}