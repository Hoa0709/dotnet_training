using app.Models;
namespace app.Interfaces
{
    public interface IPrograms
    {
        public List<ProgramInfo> GetListPrograms();
        public ProgramInfo GetProgramDetails(int id);
        public void AddProgram(ProgramInfo pr);
        public void UpdateProgram(ProgramInfo pr);
        public Boolean DeleteProgram(int id);
        public bool CheckProgram(int id);
    }
}