
using Microsoft.AspNetCore.Mvc;
using Mtc.Framwork.FramworkBase.Utility;
using Mtc.Host.IService;
using Mtc.Host.IService.Dto;

namespace Mtc.Host.WebApi.Controllers;
/// <summary>
/// 客户
/// </summary>
[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class CustomerController : ControllerBase
{
    ICustomerService _customer;
    public CustomerController(ICustomerService customer)
    {
        _customer = customer;
    }
    /// <summary>
    /// 查询 测试
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCustomer/{customerid}")]
    public async Task<WebApiResultModel> GetCustomer(Int64 customerid)
    {
        return _customer.GetCustomerById(customerid);
    }
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
    [HttpGet("Charts")]
    public async Task<WebApiResultModel> GetCustomerCharts(int start, int end)
    {
        return _customer.GetCustomerCharts(start, end);
    }
    /// <summary>
    /// 3.2按排名获取客户
    /// </summary>
    /// <returns></returns>
    [HttpGet("Charts/{customerid}")]
    public async Task<WebApiResultModel> GetCustomerChartsById(Int64 customerid, int high, int low)
    {
        return _customer.GetCustomerChartsById(customerid, high, low);
    }
}
