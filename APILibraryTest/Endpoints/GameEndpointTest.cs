using APILibrary.Endpoints;
using APILibrary.Models;
using Autofac;
using ChessUI.Library.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APILibraryTest.Endpoints
{
    public class GameEndpointTest
    {
        private IGameEndpoint _gameEndpoint;

        public GameEndpointTest()
        {
            using (ILifetimeScope scope = ContainerConfig.Configure().BeginLifetimeScope())
            {
                _gameEndpoint = scope.Resolve<IGameEndpoint>();
            }
        }

        [Fact]
        //Works
        public async void InitializeBoardTest()
        {
            APIInitModel model = new APIInitModel();
            model.AgainstComputer = false;
            model.Player1Color = APIColor.White;
            model.Player1GoFirst = true;

            var actual = await _gameEndpoint.InitializeBoard(model);
            Assert.True(actual);
        }

        [Theory]
        [InlineData(0, 1, 0, 3, true)]
        [InlineData(1, 1, 0, 5, false)]
        //Works
        public async void SendMoveTest(int x1, int y1, int x2, int y2, bool expected)
        {
            APIMoveModel model = new APIMoveModel();
            model.newXCoord = x2;
            model.newYCoord = y2;
            model.oldXCoord = x1;
            model.oldYCoord = y1;

            APIInitModel initModel = new APIInitModel();
            initModel.AgainstComputer = false;
            initModel.Player1Color = APIColor.White;
            initModel.Player1GoFirst = true;

            var actual = await _gameEndpoint.InitializeBoard(initModel);

            if(actual)
            {
                var result = await _gameEndpoint.SendMove(model);
                Assert.Equal(expected, result);
            }
        }
    }
}
