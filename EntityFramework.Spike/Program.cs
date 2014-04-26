using System;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Spike.Entities;

namespace EntityFramework.Spike
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var uow = new UnitOfWork())
            {
                Expression<Func<Session, bool>> express = f => true;
                //express = express.And(f => f.Token == "FFF77F72-83C8-4F85-903C-83F294CC462A");

                var orderby = new OrderByDescriptor("CreatedDate Ascending,Email DeAscending");

                var result = uow.GetRepository<Session>().Query(
                    filter: express,
                    orderBy: new OrderByConstructor<Session>(orderby).GenerateOrderBy(),
                    inculudeProperties: "SubSessions");

//                var t = uow.GetRepository<SubSession>()
//                    .GetById("1A80E393-6C77-4AC2-B8A8-B085CA5D799E", "Name2");
//
//                var tt = uow.GetRepository<SubSession>().Query(inculudeProperties: "Session");

                result.First().Email = "updatedemail@g.com.cn6";

                uow.GetRepository<Session>().Update(result.First(), "1A80E393-6C77-4AC2-B8A8-B085CA5D799E");
            }
        }
    }
}
