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
                    filter: f => f.Token == "1A80E393-6C77-4AC2-B8A8-B085CA5D799E",
                    orderBy: o => o.OrderBy(or => or.CreatedDate).ThenByDescending(or => or.Email),
                    inculudeProperties: "SubSessions");

//                var t = uow.GetRepository<SubSession>()
//                    .GetById("1A80E393-6C77-4AC2-B8A8-B085CA5D799E", "Name2");
//
//                var tt = uow.GetRepository<SubSession>().Query(inculudeProperties: "Session");

                result.First().Email = "updatedemail@g.com.cn";

                uow.GetRepository<Session>().Update(result.First(), "1A80E393-6C77-4AC2-B8A8-B085CA5D799E");

                uow.Save();

            }

            Console.ReadLine();
        }
    }
}
