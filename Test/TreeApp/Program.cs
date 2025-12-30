using Mtc.Host.Domain;
using Mtc.Host.Repository;

namespace TreeApp;
internal class Program
{
    static async Task Main(string[] args)
    {
        //Repository();
        await TaskRepository();
        //GetCustomerById();
        //await GetCustomerCharts();
        await TaskGetCustomerChartsById();
        var dtos = CustomerRepository.GetCustomerCharts(900, 999);
        foreach (var item in dtos)
        {
            Console.WriteLine(item.CustomerID + "-" + item.Score + "-" + item.Rank);
        }
        var dtoss = CustomerRepository.GetCustomerCharts(898, 918);
        foreach (var item in dtoss)
        {
            Console.WriteLine(item.CustomerID + "-" + item.Score + "-" + item.Rank);
        }
        var dto = CustomerRepository.GetCustomerChartsById(92, 10, 10);
        foreach (var item in dto)
        {
            Console.WriteLine(item.CustomerID + "-" + item.Score + "-" + item.Rank);
        }
        //
        Console.ReadLine();
    }
    public static Customer AddCustomer(Int64 customerID, int score, int rank)
    {
        return new Customer()
        {
            Id = score,
            CustomerID = customerID,
            Score = score,
            Rank = rank
        };
    }
    /// <summary>
    /// 范围
    /// </summary>
    /// <param name="avlTree"></param>
    private static void RangeQuery(AVLTree<Customer> avlTree)
    {
        //范围排序
        var list = avlTree.RangeQuery(AddCustomer(38818, 92, 10), AddCustomer(53274324, 95, 6));
        Console.WriteLine("开始范围");
        foreach (var item in list)
        {
            Console.WriteLine(item.CustomerID);
        }
        Console.WriteLine("结束范围");
    }
    /// <summary>
    /// 查询范围
    /// </summary>
    private static void FindRangeQuery(AVLTree<Customer> avlTree)
    {
        var list = avlTree.FindRangeQuery(AddCustomer(53274324, 95, 6), 3, 3);
        int i = 0;
        foreach (var item in list)
        {
            i++;
            if (item.Id == 95)
            {
                break;
            }
        }
        List<Customer> dto = new();
        int high = i - 4;
        int low = i + 3;
        for (int j = high; j < low; j++)
        {
            if (j >= 0 && j < list.Count)
            {
                dto.Add(list[j]);
            }
        }
        Console.WriteLine("\n开始范围");
        foreach (var item in dto)
        {
            Console.WriteLine(item.Id);
        }
        Console.WriteLine("结束范围");
    }
    /// <summary>
    /// 调表
    /// </summary>
    private static void SkipList()
    {
        SkipList<Customer> avlTree = new SkipList<Customer>();

        for (int i = 0; i < 1000; i++)
        {
            avlTree.Insert(AddCustomer(53274324, i, 6));
        }
        for (int i = 0; i < 1000; i++)
        {
            var val = avlTree.Find(AddCustomer(53274324, i, 6));
            Console.WriteLine($"Max value: {val.Id}");
        }
        return;
    }
    /// <summary>
    /// 存储并发
    /// </summary>
    /// <returns></returns>
    private static async Task TaskRepository()
    {
        DateTime t1 = DateTime.Now;
        var task1 = Task.Run(() =>
        {
            for (int i = 1; i < 1000; i++)
            {
                CustomerRepository.UpdateScore(AddCustomer(i, i, i));
            }
        });
        //var task2 = Task.Run(() =>
        //{
        //    for (int i = 10000; i < 20000; i++)
        //    {
        //        CustomerRepository.UpdateScore(AddCustomer(20000 + i, i, i));
        //    }
        //});
        //var task3 = Task.Run(() =>
        //{
        //    for (int i = 20000; i < 30000; i++)
        //    {
        //        CustomerRepository.UpdateScore(AddCustomer(30000 + i, i, i));
        //    }
        //});
        await Task.WhenAll(task1);
        DateTime t2 = DateTime.Now;
        Console.WriteLine($"Max value: {t2 - t1}");
    }
    private static void GetCustomerById()
    {
        DateTime t1 = DateTime.Now;
        for (int i = 1; i < 1000000; i++)
        {
            var b = CustomerRepository.GetCustomerById(1);
        }
        DateTime t2 = DateTime.Now;
        Console.WriteLine($"Max value: {t2 - t1}");
    }
    private static async Task GetCustomerCharts()
    {
        DateTime t1 = DateTime.Now;
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            var task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    var dto = CustomerRepository.GetCustomerCharts(10, 20);
                }
            });
            tasks.Add(task1);
        }
        await Task.WhenAll(tasks);
        DateTime t2 = DateTime.Now;
        Console.WriteLine($"Max value: {t2 - t1}");
    }
    private static async Task TaskGetCustomerChartsById()
    {
        DateTime t1 = DateTime.Now;
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 10; i++)
        {
            var task1 = Task.Run(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    var dto = CustomerRepository.GetCustomerChartsById(10050, 10, 10);
                }
            });
            tasks.Add(task1);
        }
        await Task.WhenAll(tasks);
        DateTime t2 = DateTime.Now;
        Console.WriteLine($"Max value: {t2 - t1}");
    }
}
