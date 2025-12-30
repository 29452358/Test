
using Mtc.Host.Domain;

namespace Mtc.Host.Repository;
/// <summary>
/// 平衡树
/// </summary>
/// <typeparam name="T"></typeparam>
public class AVLTree<T> where T : class, ITreeNode
{
    /// <summary>
    /// 入口
    /// </summary>
    private AVLNode<T> root;
    /// <summary>
    /// 总数
    /// </summary>
    private int count;
    public int Count => count;
    public bool IsEmpty => root == null;
    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="data"></param>
    public void Insert(T data)
    {
        root = Insert(root, data, null);
        count++;
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="data"></param>
    public void Delete(T data)
    {
        if (Contains(data))
        {
            root = Delete(root, data);
            count--;
        }
    }
    /// <summary>
    /// 包含
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool Contains(T data)
    {
        //搜索
        return Search(root, data) != null;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T Find(T data)
    {
        if (IsEmpty)
            throw new InvalidOperationException("Tree is empty");
        var list = Search(root, data);
        if (list != null)
        {
            if (list.Data.CustomerID == data.CustomerID)
            {
                return list.Data;
            }
            else
            {
                return list.Repeat.Where(t => t.CustomerID == data.CustomerID).FirstOrDefault();
            }
        }
        return default(T);
    }
    public List<T> FindRangeQuery(T data, int high, int low)
    {
        if (IsEmpty)
            throw new InvalidOperationException("Tree is empty");
        List<AVLNode<T>> parent = new();
        var node = Search(root, data, parent);
        List<T> dto = new();
        if (node != null)
        {
            for (int i = parent.Count - 1; i >= 0; i--)
            {
                dto.Clear();
                dto.AddRange(InOrderTraversalDesc(parent[i]));
                int highs = dto.Where(t => t.Id > data.Id).ToList().Count;
                int lows = dto.Where(t => t.Id <= data.Id).ToList().Count;
                if (highs >= high && lows >= low)
                {
                    return dto;
                }
            }
        }
        return dto;
    }
    /// <summary>
    /// 查询最小值
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T FindMin()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Tree is empty");
        return FindMin(root).Data;
    }
    /// <summary>
    /// 查询最大值
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T FindMax()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Tree is empty");
        return FindMax(root).Data;
    }

    #region 私有辅助方法
    // 计算高度
    private int GetHeight(AVLNode<T> node) => node?.Height ?? 0;
    // 获取平衡因子
    private int GetBalanceFactor(AVLNode<T> node) => node == null ? 0 :
        GetHeight(node.Left) - GetHeight(node.Right);
    /// <summary>
    /// 跟新高度
    /// </summary>
    /// <param name="node"></param>
    private void UpdateHeight(AVLNode<T> node)
    {
        if (node != null)
        {
            //返回最大
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }
    }
    #endregion

    #region 旋转操作
    /// <summary>
    /// 旋转操作右
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    private AVLNode<T> RotateRight(AVLNode<T> y)
    {
        AVLNode<T> x = y.Left;
        AVLNode<T> T2 = x.Right;
        //执行旋转
        x.Right = y;
        y.Left = T2;
        //更新高度
        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }
    /// <summary>
    /// 旋转操作左
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    private AVLNode<T> RotateLeft(AVLNode<T> x)
    {
        AVLNode<T> y = x.Right;
        AVLNode<T> T2 = y.Left;
        // 执行旋转
        y.Left = x;
        x.Right = T2;
        // 更新高度
        UpdateHeight(x);
        UpdateHeight(y);
        return y;
    }
    #endregion

    #region 插入操作
    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="node"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private AVLNode<T> Insert(AVLNode<T> node, T data, AVLNode<T> parent)
    {
        // 1. 执行标准BST插入
        if (node == null)
        {
            return new AVLNode<T>(data);
        }
        //data.Id 大于 node.Data.Id 返回 1 ; 小于返回 -1 ; 等于 0 不允许重复值
        int compareResult = data.Id.CompareTo(node.Data.Id);
        if (compareResult < 0)
        {
            node.Left = Insert(node.Left, data, node);
        }
        else if (compareResult > 0)
        {
            node.Right = Insert(node.Right, data, node);
        }
        else
        {
            //重复值
            node.Repeat.Add(data);
            return node;
        }
        // 2. 更新节点高度
        UpdateHeight(node);
        // 3. 获取平衡因子
        int balance = GetBalanceFactor(node);
        // 4. 如果不平衡，有四种情况
        // 左左情况
        if (balance > 1 && data.Id.CompareTo(node.Left.Data.Id) < 0)
            return RotateRight(node);
        // 右右情况
        if (balance < -1 && data.Id.CompareTo(node.Right.Data.Id) > 0)
            return RotateLeft(node);
        // 左右情况
        if (balance > 1 && data.Id.CompareTo(node.Left.Data.Id) > 0)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        // 右左情况
        if (balance < -1 && data.Id.CompareTo(node.Right.Data.Id) < 0)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }
    #endregion

    #region 删除操作
    /// <summary>
    /// 删除操作
    /// </summary>
    /// <param name="node"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private AVLNode<T> Delete(AVLNode<T> node, T data)
    {
        if (node == null) return node;
        //比较结果
        int compareResult = data.Id.CompareTo(node.Data.Id);
        //查找要删除的节点
        if (compareResult < 0)
        {
            node.Left = Delete(node.Left, data);
        }
        else if (compareResult > 0)
        {
            node.Right = Delete(node.Right, data);
        }
        //找到要删除的节点
        else
        {
            //节点有一个子节点或没有子节点
            if (node.Left == null || node.Right == null)
            {
                AVLNode<T> temp = node.Left ?? node.Right;
                //没有子节点的情况
                if (temp == null)
                {
                    //没有重复键
                    if (node.Repeat.Count == 0)
                    {
                        temp = node;
                        node = null;
                    }
                    else
                    {
                        if (node.Data.CustomerID == data.CustomerID)
                        {
                            node.Data = node.Repeat[0];
                            node.Repeat.RemoveAt(0);
                        }
                        else
                        {
                            var model = node.Repeat.Where(t => t.CustomerID == data.CustomerID).FirstOrDefault();
                            node.Repeat.Remove(model);
                        }
                    }
                }
                //一个子节点的情况
                else
                {
                    //没有重复键
                    if (node.Repeat.Count == 0)
                    {
                        node = temp;
                    }
                    else
                    {
                        if (node.Data.CustomerID == data.CustomerID)
                        {
                            node.Data = node.Repeat[0];
                            node.Repeat.RemoveAt(0);
                        }
                        else
                        {
                            var model = node.Repeat.Where(t => t.CustomerID == data.CustomerID).FirstOrDefault();
                            node.Repeat.Remove(model);
                        }
                    }
                }
            }
            else
            {
                //没有重复键
                if (node.Repeat.Count == 0)
                {
                    // 节点有两个子节点：获取中序后继（右子树的最小值）
                    AVLNode<T> temp = FindMin(node.Right);
                    node.Data = temp.Data;
                    node.Right = Delete(node.Right, temp.Data);
                }
                else
                {
                    if (node.Data.CustomerID == data.CustomerID)
                    {
                        node.Data = node.Repeat[0];
                        node.Repeat.RemoveAt(0);
                    }
                    else
                    {
                        var model = node.Repeat.Where(t => t.CustomerID == data.CustomerID).FirstOrDefault();
                        node.Repeat.Remove(model);
                    }
                }
            }
        }
        // 如果树只有一个节点
        if (node == null) return node;
        // 更新高度
        UpdateHeight(node);
        // 获取平衡因子
        int balance = GetBalanceFactor(node);
        // 平衡树
        // 左左情况
        if (balance > 1 && GetBalanceFactor(node.Left) >= 0)
            return RotateRight(node);
        // 左右情况
        if (balance > 1 && GetBalanceFactor(node.Left) < 0)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        // 右右情况
        if (balance < -1 && GetBalanceFactor(node.Right) <= 0)
            return RotateLeft(node);
        // 右左情况
        if (balance < -1 && GetBalanceFactor(node.Right) > 0)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }
    #endregion

    #region 查找方法
    /// <summary>
    /// 查找方法
    /// </summary>
    /// <param name="node"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private AVLNode<T> Search(AVLNode<T> node, T data)
    {
        if (node == null || data.Id.CompareTo(node.Data.Id) == 0)
            return node;
        if (data.Id.CompareTo(node.Data.Id) < 0)
            return Search(node.Left, data);
        else
            return Search(node.Right, data);
    }
    private AVLNode<T> Search(AVLNode<T> node, T data, List<AVLNode<T>> parent)
    {
        parent.Add(node);
        if (node == null || data.Id.CompareTo(node.Data.Id) == 0)
            return node;
        if (data.Id.CompareTo(node.Data.Id) < 0)
            return Search(node.Left, data, parent);
        else
            return Search(node.Right, data, parent);
    }
    private AVLNode<T> FindMin(AVLNode<T> node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }
    private AVLNode<T> FindMax(AVLNode<T> node)
    {
        while (node.Right != null)
            node = node.Right;
        return node;
    }
    #endregion

    #region 遍历方法
    /// <summary>
    /// 中序遍历
    /// </summary>
    /// <returns></returns>
    public List<T> InOrderTraversal()
    {
        List<T> result = new List<T>();
        InOrderTraversal(root, result);
        return result;
    }
    /// <summary>
    /// 中序遍历Desc
    /// </summary>
    /// <returns></returns>
    public List<T> InOrderTraversalDesc()
    {
        List<T> result = new List<T>();
        InOrderTraversalDesc(root, result);
        return result;
    }
    /// <summary>
    /// 中序遍历Desc
    /// </summary>
    /// <returns></returns>
    public List<T> InOrderTraversalDesc(AVLNode<T> node)
    {
        List<T> result = new List<T>();
        InOrderTraversalDesc(node, result);
        return result;
    }
    /// <summary>
    /// 中序遍历
    /// </summary>
    /// <param name="node"></param>
    /// <param name="result"></param>
    private void InOrderTraversal(AVLNode<T> node, List<T> result)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left, result);
            result.Add(node.Data);
            //重复节点
            result.AddRange(node.Repeat);
            InOrderTraversal(node.Right, result);
        }
    }
    /// <summary>
    /// 中序遍历
    /// </summary>
    /// <param name="node"></param>
    /// <param name="result"></param>
    private void InOrderTraversalDesc(AVLNode<T> node, List<T> result)
    {
        if (node != null)
        {
            InOrderTraversalDesc(node.Right, result);
            result.Add(node.Data);
            //重复节点
            result.AddRange(node.Repeat);
            InOrderTraversalDesc(node.Left, result);
        }
    }
    /// <summary>
    /// 前序遍历
    /// </summary>
    /// <returns></returns>
    public List<T> PreOrderTraversal()
    {
        List<T> result = new List<T>();
        PreOrderTraversal(root, result);
        return result;
    }
    /// <summary>
    /// 前序遍历
    /// </summary>
    /// <returns></returns>
    private void PreOrderTraversal(AVLNode<T> node, List<T> result)
    {
        if (node != null)
        {
            result.Add(node.Data);
            //重复节点
            result.AddRange(node.Repeat);
            PreOrderTraversal(node.Left, result);
            PreOrderTraversal(node.Right, result);
        }
    }
    /// <summary>
    /// 后序遍历
    /// </summary>
    /// <returns></returns>
    public List<T> PostOrderTraversal()
    {
        List<T> result = new List<T>();
        PostOrderTraversal(root, result);
        return result;
    }
    /// <summary>
    /// 后序遍历
    /// </summary>
    /// <returns></returns>
    private void PostOrderTraversal(AVLNode<T> node, List<T> result)
    {
        if (node != null)
        {
            PostOrderTraversal(node.Left, result);
            PostOrderTraversal(node.Right, result);
            result.Add(node.Data);
            //重复节点
            result.AddRange(node.Repeat);
        }
    }
    #endregion

    #region 验证和调试方法
    /// <summary>
    /// 不平衡
    /// </summary>
    /// <returns></returns>
    public bool IsBalanced()
    {
        return CheckBalance(root);
    }
    /// <summary>
    /// 平衡检验
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool CheckBalance(AVLNode<T> node)
    {
        if (node == null) return true;
        int balance = GetBalanceFactor(node);
        //去除符号
        if (Math.Abs(balance) > 1) return false;
        //平衡检验
        return CheckBalance(node.Left) && CheckBalance(node.Right);
    }
    /// <summary>
    /// 查询高度
    /// </summary>
    /// <returns></returns>
    public int GetHeight()
    {
        return GetHeight(root);
    }
    #endregion

    #region 查询范围
    /// <summary>
    /// 查询范围
    /// </summary>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <returns></returns>
    public List<T> RangeQuery(T lowerBound, T upperBound)
    {
        //结果
        List<T> result = new List<T>();
        //查找具体方法
        RangeQuery(root, lowerBound, upperBound, result);
        //返回
        return result;
    }
    /// <summary>
    /// 查询范围
    /// </summary>
    /// <param name="node"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    /// <param name="result"></param>
    private void RangeQuery(AVLNode<T> node, T lowerBound, T upperBound, List<T> result)
    {
        //节点不为空
        if (node == null) return;
        //比对
        int lowerCompare = node.Data.Id.CompareTo(lowerBound.Id);
        int upperCompare = node.Data.Id.CompareTo(upperBound.Id);
        //大 递归左 左边小
        if (lowerCompare > 0)
            RangeQuery(node.Left, lowerBound, upperBound, result);
        //范围正确
        if (lowerCompare >= 0 && upperCompare <= 0)
        {
            result.Add(node.Data);
            result.AddRange(node.Repeat);
        }
        //小 递归右 右边大
        if (upperCompare < 0)
            RangeQuery(node.Right, lowerBound, upperBound, result);
    }
    #endregion
}
