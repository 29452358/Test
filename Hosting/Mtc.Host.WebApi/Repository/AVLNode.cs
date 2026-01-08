
namespace Mtc.Host.Repository;
/// <summary>
/// 节点
/// </summary>
/// <typeparam name="T"></typeparam>
public class AVLNode<T> where T : class
{
    /// <summary>
    /// 实体
    /// </summary>
    public T Data { get; set; }
    /// <summary>
    /// 左
    /// </summary>
    public AVLNode<T> Left { get; set; }
    /// <summary>
    /// 右
    /// </summary>
    public AVLNode<T> Right { get; set; }
    /// <summary>
    /// 高
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// 重复
    /// </summary>
    public List<T> Repeat { get; set; }
    public AVLNode(T data)
    {
        Data = data;
        Height = 1;
        Left = null;
        Right = null;
        Repeat = new List<T>();
    }
}
