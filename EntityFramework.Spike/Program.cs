using System;
using System.Linq;
using EntityFramework.Spike.Entities;

namespace EntityFramework.Spike
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var uow = new UnitOfWork())
            {
                var result = uow.GetRepository<Session>().Query(
                    filter: f => f.Email.Contains("tommy"),
                    orderBy: o => o.OrderBy(or => or.CreatedDate).ThenByDescending(or => or.Email));
            }

            Console.ReadLine();
        }
    }
}
