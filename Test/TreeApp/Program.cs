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
    /// 测试树
    /// </summary>
    private static void TestTree()
    {
        //
        AVLTree<Customer> avlTree = new AVLTree<Customer>();
        avlTree.Insert(AddCustomer(15514665, 124, 1));
        avlTree.Insert(AddCustomer(81546541, 113, 2));
        avlTree.Insert(AddCustomer(1745431, 100, 3));
        avlTree.Insert(AddCustomer(76786448, 100, 4));
        avlTree.Insert(AddCustomer(254814111, 96, 5));
        avlTree.Insert(AddCustomer(53274324, 95, 6));
        avlTree.Insert(AddCustomer(6144320, 93, 7));
        avlTree.Insert(AddCustomer(8009471, 93, 8));
        avlTree.Insert(AddCustomer(38819, 92, 10));
        avlTree.Insert(AddCustomer(38818, 92, 10));
        avlTree.Insert(AddCustomer(81546542, 113, 2));
        // 中序遍历（有序输出）
        Console.WriteLine("In-order traversal:");
        foreach (var item in avlTree.InOrderTraversal())
        {
            Console.Write(item.CustomerID + " ");
        }
        // 查找最小值
        Console.WriteLine($"\nMin value: {avlTree.FindMin().CustomerID}");
        // 查找最大值
        Console.WriteLine($"Max value: {avlTree.FindMax().CustomerID}");
        // 检查平衡
        Console.WriteLine($"Is balanced: {avlTree.IsBalanced()}");
        // 获取高度
        Console.WriteLine($"Tree height: {avlTree.GetHeight()}");
        //avlTrees.Delete(AddCustomer(38816, 92, 10));
        //avlTrees.Delete(AddCustomer(15514665, 124, 2));
        //avlTrees.Delete(AddCustomer(8009471, 93, 2));
        foreach (var item in avlTree.InOrderTraversalDesc())
        {
            Console.Write(item.Score + " ");
        }
        //查询范围
        FindRangeQuery(avlTree);
        //范围
        //RangeQuery(avlTree);
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
            for (int i = 1; i < 10000; i++)
            {
                CustomerRepository.UpdateScore(AddCustomer(10000 + i, i, i));
            }
        });
        var task2 = Task.Run(() =>
        {
            for (int i = 10000; i < 20000; i++)
            {
                CustomerRepository.UpdateScore(AddCustomer(20000 + i, i, i));
            }
        });
        var task3 = Task.Run(() =>
        {
            for (int i = 20000; i < 30000; i++)
            {
                CustomerRepository.UpdateScore(AddCustomer(30000 + i, i, i));
            }
        });
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
