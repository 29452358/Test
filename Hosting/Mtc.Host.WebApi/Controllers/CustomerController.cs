
using Microsoft.AspNetCore.Mvc;
using Mtc.Framwork.FramworkBase.Utility;
using Mtc.Host.IService;
using Mtc.Host.IService.Dto;
using Mtc.Host.Service;
using System.ComponentModel.DataAnnotations;

namespace Mtc.Host.WebApi.Controllers;
/// <summary>
/// 客户
/// </summary>
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class CustomerController : ControllerBase
{
    CustomerService _customer = new();
    /// <summary>
    /// 3.1更新分数
    /// </summary>
    [HttpPost("{customerid}/score/{score}")]
    public async Task<WebApiResultModel> UpdateScore(Int64 customerid, int score)
    {
        CustomerVm vm = new();
        vm.CustomerID = customerid;
        vm.Score = score;
        return _customer.UpdateScore(vm);
    }
    /// <summary>
    /// 3.2按排名获取客户
    /// </summary>
    /// <returns></returns>
    [HttpGet("Rank")]
    public async Task<WebApiResultModel> GetCustomerRank(int start, int end)
    {
        return _customer.GetCustomerRank(start, end);
    }
    /// <summary>
    /// 3.2按排名获取客户
    /// </summary>
    /// <returns></returns>
    [HttpGet("Rank/{customerid}")]
    public async Task<WebApiResultModel> GetCustomerRankById(Int64 customerid, int high, int low)
    {
        return _customer.GetCustomerRankById(customerid, high, low);
    }
    /// <summary>
    /// 验证
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private bool Validation(CustomerVm vm)
    {
        var validationContext = new ValidationContext(vm);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(vm, validationContext, validationResults, true);
        if (!isValid)
        {
            foreach (var error in validationResults)
            {
                // 处理错误
                throw new Exception(error.ErrorMessage);
            }
        }
        return isValid;
    }
}
