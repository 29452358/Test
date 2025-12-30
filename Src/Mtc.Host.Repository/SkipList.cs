
using Mtc.Host.Domain;

namespace Mtc.Host.Repository;
/// <summary>
/// 跳表
/// </summary>
public class SkipList<T> where T : class, ITreeNode
{
    SortedDictionary<int, AVLTree<T>> skipList = new();
    public SkipList()
    {
        skipList[0] = new AVLTree<T>();
    }
    /// <summary>
    /// 插入数据
    /// </summary>
    /// <param name="data"></param>
    public void Insert(T data)
    {
        //获取Tree
        var dto = GetTree(data.Id);
        var tree = dto.Item1;
        var key = dto.Item2;
        //插入数据
        tree.Insert(data);
        //判断是否重新分配表
        if (tree.Count > 200)
        {
            DivideData(tree, key);
        }
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="data"></param>
    public void Delete(T data)
    {
        //获取Tree
        var dto = GetTree(data.Id);
        var tree = dto.Item1;
        var key = dto.Item2;
        //删除数据
        tree.Delete(data);
        //清理空表
        if (tree.Count == 0)
        {
            skipList.Remove(key);
        }
    }
    /// <summary>
    /// 查询数据
    /// </summary>
    /// <param name="data"></param>
    public T Find(T data)
    {
        //获取Tree
        var dto = GetTree(data.Id);
        var tree = dto.Item1;
        var key = dto.Item2;
        //查询数据
        return tree.Find(data);
    }
    /// <summary>
    /// 排名
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<T> Charts(int start, int end)
    {
        //总数
        int count = 0;
        //游标
        int index = 1;
        int endIndex = end - start + 1;
        //结果
        List<T> dto = new();
        //结束标识
        bool isEnd = false;
        //倒序
        var keyDesc = skipList.Keys.OrderDescending();
        foreach (var key in keyDesc)
        {
            var model = skipList[key];
            if (isEnd == false)
            {
                count += model.Count;
                if (count >= start)
                {
                    isEnd = true;
                    index = start - (count - model.Count);
                    //index = count - start;
                    var currentData = model.InOrderTraversalDesc();
                    if (index == 0)
                    {
                        index = 1;
                    }
                    dto = currentData.Skip(index - 1).Take(endIndex).ToList();
                    if (dto.Count == endIndex)
                    {
                        return dto;
                    }
                }
            }
            else
            {
                var currentData = model.InOrderTraversalDesc();
                dto.AddRange(currentData);
                dto = dto.Skip(0).Take(endIndex).ToList();
                if (dto.Count == endIndex)
                {
                    return dto;
                }
            }
        }
        return dto;
    }
    /// <summary>
    /// 排名
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<T> ChartsById(T data, int high, int low, out int index)
    {
        //倒序
        var keyDesc = skipList.Keys.OrderDescending();
        //获取Tree
        var treeModel = GetTree(data.Id);
        var tree = treeModel.Item1;
        var key = treeModel.Item2;
        //总数
        int count = 0;
        foreach (var keyd in keyDesc)
        {
            var model = skipList[keyd];
            if (key == keyd)
            {
                break;
            }
            else
            {
                count += model.Count;
            }
        }
        var customers = tree.InOrderTraversalDesc();
        //下标
        var subscript = 0;
        foreach (var customer in customers)
        {
            subscript++;
            if (customer.Id == data.Id)
            {
                break;
            }
        }
        index = count + subscript;
        //
        int start = index - high;
        int end = index + low;
        var dto = Charts(start, end);
        index = start;
        if (index <= 0)
        {
            index = 1;
        }
        return dto;
    }
    /// <summary>
    /// 获取tree
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    private (AVLTree<T>, int) GetTree(int key)
    {
        //不需要计算
        if (skipList.Count == 0)
        {
            skipList[0] = new AVLTree<T>();
        }
        if (skipList.Count == 1)
        {
            return (skipList.FirstOrDefault().Value, skipList.Keys.Last());
        }
        else
        {
            foreach (var skey in skipList.Keys)
            {
                // skey = 30 > key = 25
                // 0  10  20  30  40  key = 25  
                // 最接近
                if (skey >= key)
                {
                    return (skipList[skey], skey);
                }
            }
        }
        return (skipList[skipList.Keys.Last()], skipList.Keys.Last());
    }
    /// <summary>
    /// 分配 一张表 拆分两张表
    /// </summary>
    /// <returns></returns>
    private void DivideData(AVLTree<T> tree, int key)
    {
        AVLTree<T> treeA = new();
        AVLTree<T> treeB = new();
        //遍历数据
        var listT = tree.InOrderTraversal();
        int i = 0;
        //重新分配
        foreach (var itemT in listT)
        {
            if (i <= 100)
            {
                treeA.Insert(itemT);
            }
            else
            {
                treeB.Insert(itemT);
            }
            i++;
        }
        //最大Id
        var keyA = treeA.FindMax().Id;
        var keyB = treeB.FindMax().Id;
        //删除旧的
        skipList.Remove(key);
        //写入新的
        skipList[keyA] = treeA;
        skipList[keyB] = treeB;
    }
}
