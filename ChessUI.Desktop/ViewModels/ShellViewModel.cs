using APILibrary.Endpoints;
using Autofac;
using Caliburn.Micro;
using ChessUI.Desktop.ButtonHelper;
using ChessUI.Library.AutoFac;
using ChessUI.Library.Models;
using APILibrary.Models;
using System.Threading.Tasks;
using System.Windows.Controls;
using APILibrary.Exceptions;
using System.Windows;
using System;

namespace ChessUI.Desktop.ViewModels
{
    public class ShellViewModel : Screen
    {
        IGameEndpoint _gameEndpoint;
        SquareUIModel _oldSquare;
        SquareUIModel _newSquare;
        bool _firstSelection = true;
        bool initSuccessful = false;    
        Button _selectedPiece { get; set; }


        public ShellViewModel()
        {
            using (ILifetimeScope scope = ContainerConfig.Configure().BeginLifetimeScope())
            {
                _gameEndpoint = scope.Resolve<IGameEndpoint>();
            }   
        }

        private async Task Init()
        {
            try
            {
                //Initialize the board
                bool result = await _gameEndpoint.InitializeBoard(new APIInitModel() { AgainstComputer = false, Player1Color = APIColor.White, Player1GoFirst = true });
                if(result)
                {
                    string initMessage = "Initialization was successful";
                    MessageBox.Show(initMessage);
                    initSuccessful = true;
                }
                else
                {
                    string errorMessage = "Initialization was unsuccessful";
                    MessageBox.Show(errorMessage);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async Task SquareSelected(Button button)
        {
            //If the initialization was unsuccessful we need to return
            if (initSuccessful == false)
            {
                MessageBox.Show("Initilization has been unsuccessful, please try again");
                return;
            }

            if (_firstSelection)
            {
                _oldSquare = SetSquare(_oldSquare, button.Name);
                _selectedPiece = button;
            }
            else
            {
                _newSquare = SetSquare(_newSquare, button.Name);
                try
                {
                    bool result = await _gameEndpoint.SendMove(new APIMoveModel() { newXCoord = _newSquare.XCoord, newYCoord = _newSquare.YCoord, oldXCoord = _oldSquare.XCoord, oldYCoord = _oldSquare.YCoord });
                    if(result)
                    {
                        if(_gameEndpoint.ResponseMoveModel.IsAllowed == false)
                        {
                            MessageBox.Show("Given move is not allowed");
                            return;
                        }
                        if(_gameEndpoint.ResponseMoveModel.HasWon == true)
                        {
                            MessageBox.Show("Game has been won");
                        }
                        if(_gameEndpoint.ResponseMoveModel.HasDrawn == true)
                        {
                            MessageBox.Show("Game has been drawn");
                        }
                        button.Content = _selectedPiece.Content.ToString();
                        _selectedPiece.Content = "";
                    }
                    else
                    { 
                        MessageBox.Show($"{ _gameEndpoint.ErrorModel.ErrorName} : { _gameEndpoint.ErrorModel.ErrorMessage}");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public async Task InitializeGame()
        {
            MessageBox.Show("Game has now started");
            await Init();
        }

        private SquareUIModel SetSquare(SquareUIModel square, string buttonName)
        {
            _firstSelection ^= true;
            return Conversion.ButtonNameToSquareModel(buttonName);
        }


    }
}
