using APILibrary.Endpoints;
using APILibrary.Exceptions;
using APILibrary.Models;
using Autofac;
using ChessUI.Library.AutoFac;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIConsoleTestingApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //GameEndpoint endpoint = new GameEndpoint();
            //string responseString = await endpoint.SendMove();
            //Console.WriteLine(responseString);
            Console.WriteLine(nameof(APIMoveModel.oldYCoord));

            APIResponseMoveModel model = new APIResponseMoveModel();
            model.IsAllowed = true;
            model.HasDrawn = false;
            model.HasWon = false;

            APIErrorModel modelError = new APIErrorModel();
            modelError.ErrorMessage = "ErrorMessage";
            modelError.ErrorName = "ErrorName";

            APIResponseInitModel aPIResponseInitModel = new APIResponseInitModel();
            aPIResponseInitModel.IsSuccessful = true;

            IGameEndpoint _gameEndpoint;
            using (ILifetimeScope scope = ContainerConfig.Configure().BeginLifetimeScope())
            {
                _gameEndpoint = scope.Resolve<IGameEndpoint>();
                APIInitModel initmodel = new APIInitModel();
                initmodel.AgainstComputer = false;
                initmodel.Player1Color = APIColor.White;
                initmodel.Player1GoFirst = true;

                try
                {
                    var result = await _gameEndpoint.InitializeBoard(initmodel);
                    APIMoveModel moveModel = new APIMoveModel();
                    moveModel.newXCoord = 0;
                    moveModel.newYCoord = 2;
                    moveModel.oldXCoord = 0;
                    moveModel.oldYCoord = 1;
                    var result2 = await _gameEndpoint.SendMove(moveModel);
                }
                catch (InvalidAPIDataException e)
                {
                    Console.WriteLine("Invalid API data");
                }
            }

            Console.WriteLine(JsonSerializer.Serialize(aPIResponseInitModel));
        }
    }
}
