using SAMAirline.DataProvider;
using SAMAirline.Logic.Interfaces;

namespace SAMAirline.Logic.Services
{
    public class CommitService : ICommitService
    {
        private readonly AirlineDB _context;

        public CommitService(AirlineDB context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
