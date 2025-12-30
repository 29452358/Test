
using Mtc.Framwork.FramworkBase.Utility;
using Mtc.Host.Domain;
using Mtc.Host.IService;
using Mtc.Host.IService.Dto;
using Mtc.Host.Repository;
using System.ComponentModel.DataAnnotations;

namespace Mtc.Host.Service;
public class CustomerService : ICustomerService
{
    /// <summary>
    /// 3.1更新分数
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    public WebApiResultModel UpdateScore(CustomerVm vm)
    {
        //验证
        Validation(vm);
        //查询
        var customer = CustomerRepository.GetCustomerById(vm.CustomerID);
        if (customer != null)
        {
            Customer newcustomer = new();
            newcustomer.CustomerID = customer.CustomerID;
            newcustomer.Score = customer.Score + vm.Score;
            //更新
            CustomerRepository.UpdateScore(newcustomer);
        }
        else
        {
            Customer newcustomer = new();
            newcustomer.CustomerID = vm.CustomerID;
            newcustomer.Score = vm.Score;
            //更新
            CustomerRepository.UpdateScore(newcustomer);
        }
        //返回成功
        return WebApiResult.Success();
    }
    /// <summary>
    /// 3.2按排名获取客户
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public WebApiResultModel GetCustomerCharts(int start, int end)
    {
        return WebApiResult.Success(CustomerRepository.GetCustomerCharts(start, end).Select(t => new CustomerDto()
        {
            CustomerID = t.CustomerID,
            Score = t.Score,
            Rank = t.Rank
        }).ToList());
    }
    /// <summary>
    /// 3.3Get通过CustomerId获得客户
    /// </summary>
    /// <param name="customerid"></param>
    /// <param name="high"></param>
    /// <param name="low"></param>
    /// <returns></returns>
    public WebApiResultModel GetCustomerChartsById(Int64 customerid, int high, int low)
    {
        return WebApiResult.Success(CustomerRepository.GetCustomerChartsById(customerid, high, low).Select(t => new CustomerDto()
        {
            CustomerID = t.CustomerID,
            Score = t.Score,
            Rank = t.Rank
        }).ToList());
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="vm"></param>
    /// <returns></returns>
    public WebApiResultModel GetCustomerById(Int64 customerid)
    {
        return WebApiResult.Success(CustomerRepository.GetCustomerById(customerid));
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
