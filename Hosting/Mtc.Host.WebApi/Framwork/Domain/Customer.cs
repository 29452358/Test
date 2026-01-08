namespace Mtc.Host.WebApi.Framwork.Domain;
public class Customer : ITreeNode
{
    public Customer()
    {

    }
    public Customer(Customer model)
    {
        Id = model.Id;
        CustomerID = model.CustomerID;
        Score = model.Score;
        Rank = model.Rank;
    }
    public int Id { get; set; }
    public long CustomerID { get; set; }
    public int Score { get; set; }
    public int Rank { get; set; }
}
