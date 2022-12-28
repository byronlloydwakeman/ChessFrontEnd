using APILibrary.Exceptions;
using APILibrary.Models;
using APILibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APILibrary.Endpoints
{
    public class GameEndpoint : IGameEndpoint
    {
        private APIHelper _apiHelper = new APIHelper();
        private APIErrorModel errorModel;
        public APIErrorModel ErrorModel
        {
            get { return errorModel; }
        }

        private APIResponseMoveModel responseMoveModel;
        public APIResponseMoveModel ResponseMoveModel
        {
            get { return responseMoveModel; }
        }

        private APIResponseInitModel responseInitModel;
        public APIResponseInitModel ResponseInitModel
        {
            get { return responseInitModel; }
        }

        private string address {
            get
            {
                return "http://localhost:1234";
            }
        }

        public async Task<bool> SendMove(APIMoveModel model)
        {
            //Reset the move model fo the new move
            responseMoveModel = new APIResponseMoveModel();
            var values = new Dictionary<string, string>
            {
                { nameof(model.newXCoord).ToString(), model.newXCoord.ToString() },
                { nameof(model.newYCoord).ToString(), model.newYCoord.ToString() },
                { nameof(model.oldXCoord).ToString(), model.oldXCoord.ToString() },
                { nameof(model.oldYCoord).ToString(), model.oldYCoord.ToString() }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await _apiHelper._apiClient.PostAsync(String.Format(address + "/api/ChessAPI/Move"), content);
            var responseString = await response.Content.ReadAsStringAsync();

            if(APIValidation.IsJsonAPIResponseMoveModel(responseString))
            {
                this.responseMoveModel = JsonSerializer.Deserialize<APIResponseMoveModel>(responseString);
                return true;
            }
            else if(APIValidation.IsJsonAPIErrorModel(responseString))
            {
                this.errorModel = JsonSerializer.Deserialize<APIErrorModel>(responseString);
                return false;
            }

            //If its neither, there must be something wrong with the data
            throw new InvalidAPIDataException();
        }
        
        public async Task<bool> InitializeBoard(APIInitModel model)
        {
            this.errorModel = new APIErrorModel();

            var values = new Dictionary<string, string>
            {
                { nameof(model.AgainstComputer), model.AgainstComputer.ToString() },
                { nameof(model.Player1Color), model.Player1Color.ToString() },
                { nameof(model.Player1GoFirst), model.Player1GoFirst.ToString() }
            };

            var content = new FormUrlEncodedContent(values);

            //The response will either be an error, or a APIResponseInitModel in json form
            var response = await _apiHelper._apiClient.PostAsync(String.Format(address + "/api/ChessAPI/Initialize"), content);
            var responseString = await response.Content.ReadAsStringAsync();

            if(APIValidation.IsJsonAPIResponseInitModel(responseString))
            {
                responseInitModel = JsonSerializer.Deserialize<APIResponseInitModel>(responseString);
                return true;
            }
            else if(APIValidation.IsJsonAPIErrorModel(responseString))
            {
                errorModel = JsonSerializer.Deserialize<APIErrorModel>(responseString);
                return false;
            }

            throw new InvalidAPIDataException();

        }
    }
}
