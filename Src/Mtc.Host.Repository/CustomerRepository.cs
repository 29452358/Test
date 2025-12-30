
using Mtc.Host.Domain;

namespace Mtc.Host.Repository;
/// <summary>
/// 存储
/// </summary>
public class CustomerRepository
{
    //读写锁
    private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
    //根据Id查询 值 
    private static Dictionary<Int64, Customer> customers = new();
    //跳表
    private static SkipList<Customer> avlTree = new();
    /// <summary>
    /// 获取客户由Id
    /// </summary>
    public static Customer GetCustomerById(Int64 customerID)
    {
        try
        {
            _lock.EnterReadLock();
            //查找
            if (customers.TryGetValue(customerID, out var value))
            {
                //拷贝
                return new Customer(value);
            }
            else
            {
                return value;
            }
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
    /// <summary>
    /// 获取客户
    /// </summary>
    public static Customer GetCustomer(Customer customer)
    {
        try
        {
            _lock.EnterReadLock();
            customer.Id = customer.Score;
            //拷贝
            return new Customer(avlTree.Find(customer));
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
    /// <summary>
    /// 3.1 更新分数 客户已经存在，则更新其分数 不存在特定客户，则会将该客户添加到排行榜中并存储其分数
    /// </summary>
    public static void UpdateScore(Customer customer)
    {
        try
        {
            _lock.EnterWriteLock();
            //客户已经存在则更新其分数
            if (customers.TryGetValue(customer.CustomerID, out Customer value))
            {
                value.Id = value.Score;
                customer.Id = customer.Score;
                EditCustomer(value, customer);
            }
            //不存在特定客户,添加到排行榜中并存储其分数
            else
            {
                customer.Id = customer.Score;
                AddCustomer(customer);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
    /// <summary>
    /// 3.2 获取客户
    /// </summary>
    public static List<Customer> GetCustomerRank(int start, int end)
    {
        try
        {
            _lock.EnterReadLock();
            //游标
            int index = start;
            //拷贝
            var dto = avlTree.Rank(start, end).Select(t => new Customer(t)).ToList();
            //返回 Rank 赋值
            foreach (var customer in dto)
            {
                customer.Rank = index;
                index++;
            }
            return dto;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
    /// <summary>
    /// 3.3通过CustomerId获得客户
    /// </summary>
    public static List<Customer> GetCustomerRankById(Int64 customerid, int high, int low)
    {
        try
        {
            _lock.EnterReadLock();
            //游标
            int index = 0;
            //查找
            if (customers.TryGetValue(customerid, out var value))
            {
                //拷贝
                var dto = avlTree.RankById(value, high, low, out index).Select(t => new Customer(t)).ToList();
                //返回 Rank 赋值
                foreach (var customer in dto)
                {
                    customer.Rank = index;
                    index++;
                }
                return dto;
            }
            //拷贝
            return new List<Customer>();
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
    /// <summary>
    /// 添加客户
    /// </summary>
    private static void AddCustomer(Customer customer)
    {
        customers[customer.CustomerID] = customer;
        avlTree.Insert(customer);
    }
    /// <summary>
    /// 修改客户
    /// </summary>
    private static void EditCustomer(Customer value, Customer customer)
    {
        avlTree.Delete(value);
        avlTree.Insert(customer);
        value.CustomerID = customer.CustomerID;
        value.Score = customer.Score;
        value.Rank = customer.Rank;
    }
    /// <summary>
    /// 删除客服
    /// </summary>
    public static void DeleteCustomer(Customer customer)
    {
        try
        {
            _lock.EnterWriteLock();
            customers.Remove(customer.CustomerID);
            avlTree.Delete(customer);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
}
