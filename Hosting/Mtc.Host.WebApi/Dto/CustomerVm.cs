
using System.ComponentModel.DataAnnotations;

namespace Mtc.Host.IService.Dto;
public class CustomerVm
{
    /// <summary>
    /// 客户Id
    /// </summary>
    [Required(ErrorMessage = "客户Id 不能为空")]
    public Int64 CustomerID { get; set; }
    /// <summary>
    /// 分数
    /// </summary>
    [Range(typeof(int), "-1000", "1000", ErrorMessage = "范围为[-1000，+1000]")]
    public int Score { get; set; }
}
