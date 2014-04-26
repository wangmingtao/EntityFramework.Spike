using System;
using System.Collections.Generic;

namespace EntityFramework.Spike
{
    public class OrderByDescriptor
    {
        public List<OrderByItem> OrderByList { get; set; }

        public OrderByDescriptor()
        {
        }

        public OrderByDescriptor(string orderby)
        {
            OrderByList = new List<OrderByItem>();

            if (!string.IsNullOrEmpty(orderby))
            {
                foreach (var item in orderby.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    var temArr = item.Split(' ');
                    if (temArr.Length == 2)
                    {
                        OrderByList.Add(new OrderByItem() { ColumnName = temArr[0], Ascending = (temArr[1].ToLower() == "ascending") });
                    }
                }
            }
        }

        public OrderByDescriptor Add(OrderByItem item)
        {
            if (OrderByList == null)
                OrderByList = new List<OrderByItem>();

            OrderByList.Add(item);

            return this;
        }

        public OrderByDescriptor Add(string columnName, OrderByType type)
        {
            if (OrderByList == null)
                OrderByList = new List<OrderByItem>();

            OrderByList.Add(new OrderByItem() { ColumnName = columnName, Ascending = (type == OrderByType.Ascending) });

            return this;
        }
    }

    public class OrderByItem
    {
        public OrderByItem() { }

        public OrderByItem(string columnName, bool ascending)
        {
            ColumnName = columnName;
            Ascending = ascending;
        }
        public string ColumnName { get; set; }
        public bool Ascending { get; set; }
    }

    public enum OrderByType
    {
        Ascending = 0,
        DeAscending = 1
    }
}