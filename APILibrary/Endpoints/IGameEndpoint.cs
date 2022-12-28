using APILibrary.Models;
using System.Threading.Tasks;

namespace APILibrary.Endpoints
{
    public interface IGameEndpoint
    {
        Task<bool> InitializeBoard(APIInitModel model);
        Task<bool> SendMove(APIMoveModel model);
        APIResponseInitModel ResponseInitModel { get; }
        APIResponseMoveModel ResponseMoveModel { get; }
        APIErrorModel ErrorModel { get; }
    }
}