using RequestHandler.Interfaces;
using RequestHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.xTest.FakeDate
{
    public class FakeStatusRepository : IStatusRepository
    {
        private readonly List<Status> _fakeStatuses;
        public FakeStatusRepository() 
        {
            _fakeStatuses = new List<Status>()
            {
                new Status()
                {
                    StatusId = 1,
                    Title = "Рассматривается",
                },
                new Status()
                {
                    StatusId = 2,
                    Title = "Рассмотрена",
                },
                new Status()
                {
                    StatusId = 3,
                    Title = "Исправлена",
                }
            };
        }

        public async Task<ICollection<Status>> GetStatuses()
        {
            return _fakeStatuses.OrderBy(x => x.StatusId).ToList();
        }
    }
}
