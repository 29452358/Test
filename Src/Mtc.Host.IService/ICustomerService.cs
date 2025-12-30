
using Mtc.Framwork.FramworkBase.Utility;
using Mtc.Host.IService.Dto;

namespace Mtc.Host.IService;
public interface ICustomerService
{
    //查询客服由Id
    WebApiResultModel GetCustomerById(Int64 customerid);
    //3.1更新分数
    WebApiResultModel UpdateScore(CustomerVm vm);
    //3.2按排名获取客户
    WebApiResultModel GetCustomerRank(int start, int end);
    //3.3Get通过CustomerId获得客户
    WebApiResultModel GetCustomerRankById(Int64 customerid, int high, int low);
}
