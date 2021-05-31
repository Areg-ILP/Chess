using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess.Logic;
using Chess.Logic.Data_Managment;
using Chess.Logic.Game_Mangement;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitilizeBoard();
            InitilizeMaster();
            SetEventsBeforeLogin();
            SetChoosebuttonParametrs();
        }

        #region Fundamental

        /// <summary>
        /// Chess master
        /// </summary>
        private ChessMaster ChessMaster;

        /// <summary>
        /// UI board
        /// </summary>
        private Button[,] _viewBoard;

        /// <summary>
        /// Initilize UI board
        /// </summary>
        private void InitilizeBoard()
        {
            _viewBoard = new Button[8, 8];

            int left, top = 0;
            var colors = new SolidColorBrush[] { Brushes.SaddleBrown, Brushes.LemonChiffon };

            for (int i = 0; i < 8; i++)
            {
                left = 0;
                if (i % 2 == 0)
                {
                    colors[0] = Brushes.SaddleBrown;
                    colors[1] = Brushes.LemonChiffon;
                }
                else
                {
                    colors[0] = Brushes.LemonChiffon;
                    colors[1] = Brushes.SaddleBrown;
                }
                for (int j = 0; j < 8; j++)
                {
                    _viewBoard[i, j] = new Button();
                    _viewBoard[i, j].Background = colors[(j % 2 == 0) ? 1 : 0];
                    _viewBoard[i, j].Width = 66;
                    _viewBoard[i, j].Height = 66;
                    _viewBoard[i, j].Margin = new Thickness(left, top, 0, 0);
                    _viewBoard[i, j].IsEnabled = true;
                    _viewBoard[i, j].HorizontalAlignment = HorizontalAlignment.Left;
                    _viewBoard[i, j].VerticalAlignment = VerticalAlignment.Top;
                    _viewBoard[i, j].Opacity = 0.85;

                    left += 66;
                    Board.Children.Add(_viewBoard[i, j]);
                }
                top += 66;
            }
        }

        /// <summary>
        /// Initilize master and set default board
        /// </summary>
        private void InitilizeMaster()
        {
            ChessMaster = new ChessMaster();
            SetAllFiguresImages();
        }

        /// <summary>
        /// Set all figures in view board (images)
        /// </summary>
        private void SetAllFiguresImages()
        {
            foreach (var figure in ChessMaster.GetAllFiguresViewModel())
                SetFigureImage(figure);
        }

        /// <summary>
        /// Clear all figures in board
        /// </summary>
        private void ImageVipe()
        {
            foreach (var vb in _viewBoard)
                vb.Content = null;
        }

        /// <summary>
        /// Set figure in board (image)
        /// </summary>
        /// <param name="figure">figure post model</param>
        private void SetFigureImage(FigureViewModel figure)
        {
            try
            {
                _viewBoard[figure.Position.X, figure.Position.Y].Content = ViewManager.GetImage(figure);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Events Settings

        private void SetEventsBeforeLogin()
        {
            //LinkedInButton.Click += OpenLinkedInPage;
            //GitHubButton.Click += OpenGitHubPage;
        }

        private void SetEventsAfterLogin()
        {
            PlayBtn.Click += PlayClick;
            RestartBtn.Click += RestartClick;
            HelpBtn.Click += HelpClick;
            SaveBtn.Click += SaveClick;
            EndGameModeBtn.Click += EndGameModeClick;
            HorsepathModeBtn.Click += HorsepathModeClick;
            PvpModeBtn.Click += PVPModeClick;
            CreateBtn.Click += CreateBtnClick;
        }

        private void PlayClick(object sender, EventArgs e)
        {
            if (ChessMaster.GameType == GameType.Endgame)
            {
                if (!ChessMaster.IsStartableGame()) return;
                else CreationBorder.IsEnabled = false;
            }

            foreach (var vb in _viewBoard)
            {
                vb.Click += ImageClick;
                vb.Click += ClickToChange;
            }
            PlayBtn.IsEnabled = false;

            MessageBox.Show("Good luck , have fun !", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RestartClick(object sender, EventArgs e)
        {
            ImageVipe();
            switch (ChessMaster.GameType)
            {
                case GameType.PVP:
                    ChessMaster.SetChessboardDefaultParametrs();
                    break;
                case GameType.Endgame:
                case GameType.Horsepath:
                    ChessMaster.SetEmptyBoard();
                    CreationBorder.IsEnabled = true;
                    break;
            }
            _logsCounter = 0;
            Logs.Document.Blocks.Clear();
            SetAllFiguresImages();
        }

        private void HelpClick(object sender, EventArgs e)
        {
            switch (ChessMaster.GameType)
            {
                case GameType.PVP:
                    MessageBox.Show("Press play button", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case GameType.Endgame:
                    MessageBox.Show("Places figures and play", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case GameType.Horsepath:
                    MessageBox.Show("Pick horse , and click any cell", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        private void SaveClick(object sender, EventArgs e)
        {
            //mb
        }

        private void EndGameModeClick(object sender, EventArgs e)
        {
            ChessMaster.SetModeEndgame();
            OnChangeMode();

            ChooseFigureBox.Items.Clear();
            ChooseFigureBox.Items.Add("King");
            ChooseFigureBox.Items.Add("Queen");
            ChooseFigureBox.Items.Add("Rook");
            ChooseFigureBox.Items.Add("Horse");
            ChooseFigureBox.Items.Add("Bishop");
            ChooseFigureBox.Items.Add("Pawn");
        }


        private void HorsepathModeClick(object sender, EventArgs e)
        {
            ChessMaster.SetModeHorsePath();
            OnChangeMode();

            SetMoveEventsFlag(false);
            foreach (var vb in _viewBoard)
            {
                vb.Click += ClickForHorseTask;
            }

            ChooseFigureBox.Items.Clear();
            ChooseFigureBox.Items.Add("Horse");
        }

        private void PVPModeClick(object sender, EventArgs e)
        {
            ChessMaster.SetModePVP();
            OnChangeMode();

            //mb
            SetMoveEventsFlag(true);
            foreach (var vb in _viewBoard)
            {
                vb.Click -= ClickForHorseTask;
            }
        }

        private void OnChangeMode()
        {
            GameDebuff();
            ImageVipe();
            SetAllFiguresImages();
        }

        #endregion

        #region Login and Registration

        private static UserViewModel CurrentUser;

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == string.Empty ||
                PassBox.Password == string.Empty)
            {
                MessageBox.Show("Input login and password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                CurrentUser = UserManager.SignIn(LoginBox.Text, PassBox.Password);
                SetProfileFrontEndSettings();
                SetPersonalAreaElements(CurrentUser);
                SetEventsAfterLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegisterLoginBox.Text == string.Empty ||
                RegisterFirstPassBox.Password == string.Empty ||
                RegisterSecondPassBox.Password == string.Empty)
            {
                MessageBox.Show("Input login and password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RegisterFirstPassBox.Password != RegisterSecondPassBox.Password)
            {
                MessageBox.Show("Password mismatch", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                CurrentUser = UserManager.Registration(RegisterLoginBox.Text, RegisterFirstPassBox.Password);
                SetProfileFrontEndSettings();
                SetPersonalAreaElements(CurrentUser);
                SetEventsAfterLogin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ChangeViewLogin_Click(object sender, RoutedEventArgs e)
        {
            RegistrationBox.Visibility = Visibility.Hidden;
            SignInBox.Visibility = Visibility.Visible;
        }

        private void ChangeViewRegistration_Click(object sender, RoutedEventArgs e)
        {
            SignInBox.Visibility = Visibility.Hidden;
            RegistrationBox.Visibility = Visibility.Visible;
            RegistrationBox.Margin = new Thickness(9, 175, 9, 160);
        }

        private void SetProfileFrontEndSettings()
        {
            SignInBox.Visibility = Visibility.Hidden;
            RegistrationBox.Visibility = Visibility.Hidden;
            PersonalAreaBox.Visibility = Visibility.Visible;
            GameSettingsTabItem.IsEnabled = true;
        }

        private void SetPersonalAreaElements(UserViewModel user)
        {
            UserNameLabel.Content = user.Name;
            UserIdLabel.Content = user.Id;
            UserPartyCountLabel.Content = user.PartyCount;
            UserWinCountLabel.Content = user.WinCount;
            UserLoseCountLabel.Content = user.LoseCount;
            UserDrawCountLabel.Content = user.DrawCount;
            UserCreationDateLabel.Content = user.CreationDate;
            if (user.PartyCount != 0)
            {
                WinRateBar.Value = user.WinCount * 100 / user.PartyCount;
                DrawRateBar.Value = user.DrawCount * 100 / user.PartyCount;
            }
        }

        #endregion

        #region Create and Color Pick

        /// <summary>
        /// Create button click event for computer mode
        /// </summary>
        /// <param name="sender">Create button</param>
        /// <param name="e"></param>
        private void CreateBtnClick(object sender, EventArgs e)
        {
            if (!CheckCreationArguments())
                return;

            int x = AlhpabetHelper.GetNumber(ChooseLetterBox.Text[0]);
            int.TryParse(ChooseNumberBox.Text, out int y);

            var figureViewModel = new FigureViewModel()
            {
                FigureName = ChooseFigureBox.Text,
                ColorFlag = WhiteFigureCheckBox.IsChecked == true,
                Position = (--y, --x)
            };

            if (ChessMaster.AppendFigure(figureViewModel))
            {
                SetFigureImage(figureViewModel);
                ClearCreateBoxComponents();
            }
            else
            {
                MessageBox.Show("\tOOPS!\nSomething went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Check if creation arguments is valid
        /// </summary>
        /// <returns>true if arguments valid else false</returns>
        private bool CheckCreationArguments()
        {
            bool flag = true;
            if (ChooseFigureBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose Figure", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                flag = false;
            }

            if (ChooseLetterBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose Letter", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                flag = false;
            }

            if (ChooseNumberBox.SelectedIndex == -1)
            {
                MessageBox.Show("Choose Number", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                flag = false;
            }

            if (!WhiteFigureCheckBox.IsChecked == false && !BlackFigureCheckBox.IsChecked == false)
            {
                MessageBox.Show("Choose color of figure", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                flag = false;
            }

            if (WhiteFigureCheckBox.IsChecked == true && BlackFigureCheckBox.IsChecked == true)
            {
                MessageBox.Show("Choose color of figure", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Create box component cleaning method
        /// </summary>
        private void ClearCreateBoxComponents()
        {
            ChooseFigureBox.SelectedIndex = -1;
            ChooseNumberBox.SelectedIndex = -1;
            ChooseLetterBox.SelectedIndex = -1;
            WhiteFigureCheckBox.IsChecked = false;
            BlackFigureCheckBox.IsChecked = false;
        }

        /// <summary>
        /// White color pick check box checked event
        /// </summary>
        /// <param name="sender">white color pick check box</param>
        /// <param name="e"></param>
        private void WhiteColorPickBox_Checked(object sender, RoutedEventArgs e)
        {
            WhiteColorPickBox.Click += WhiteColorPickBox_Checked;
            if (WhiteColorPickBox.IsChecked == true)
            {
                WhiteColorPickBox.IsChecked = true;
                BlackColorPickBox.IsEnabled = false;
                ChessMaster.ChangePlayerColor(true);
                ImageVipe();
                Logs.Document.Blocks.Clear();
                if (ChessMaster.GameType != GameType.PVP)
                {
                    ChessMaster.SetEmptyBoard();
                    CreationBorder.IsEnabled = true;
                }
                else
                {
                    ChessMaster.SetChessboardDefaultParametrs();
                }
                SetAllFiguresImages();
            }
            else if (WhiteColorPickBox.IsChecked == false)
            {
                WhiteColorPickBox.IsChecked = false;
                BlackColorPickBox.IsEnabled = true;
                CreationBorder.IsEnabled = false;
            }
        }

        /// <summary>
        /// Black color pick check box checked event
        /// </summary>
        /// <param name="sender">black color pick check box</param>
        /// <param name="e"></param>
        private void BlackColorPickBox_Checked(object sender, RoutedEventArgs e)
        {
            BlackColorPickBox.Click += BlackColorPickBox_Checked;
            if (BlackColorPickBox.IsChecked == true)
            {
                BlackColorPickBox.IsChecked = true;
                WhiteColorPickBox.IsEnabled = false;
                ChessMaster.ChangePlayerColor(false);
                ImageVipe();
                Logs.Document.Blocks.Clear();
                if (ChessMaster.GameType != GameType.PVP)
                {
                    ChessMaster.SetEmptyBoard();
                    CreationBorder.IsEnabled = true;
                }
                else
                {
                    ChessMaster.SetChessboardDefaultParametrs();
                }
                SetAllFiguresImages();
            }
            else if (BlackColorPickBox.IsChecked == false)
            {
                BlackColorPickBox.IsChecked = false;
                WhiteColorPickBox.IsEnabled = true;
                CreationBorder.IsEnabled = false;
            }
        }

        #endregion

        #region Standart Move Settings

        /// <summary>
        /// Check is clicked
        /// </summary>
        private bool _isClicked;
        /// <summary>
        /// Clicked figure
        /// </summary>
        private FigureViewModel _currentFigureOnClick;
        /// <summary>
        /// Clicked figure recomendation list
        /// </summary>
        private List<(int, int)> _currentFigureRecomendations;

        /// <summary>
        /// Event: Picturebox click (s,s)
        /// </summary>
        /// <param name="sender">all cells in board</param>
        /// <param name="e">event args</param>
        private void ImageClick(object sender, EventArgs e)
        {
            var b = sender as Button;
            var position = GetPointbyImage(b);
            if (!_isClicked)
            {
                if (b != null && b.Content != null)
                {
                    var figureViewModel = ChessMaster.GetFigureByPosition(position);
                    if (figureViewModel.ColorFlag == ChessMaster.Turn)
                    {
                        _currentFigureOnClick = figureViewModel;
                        _currentFigureRecomendations = ChessMaster.GetRecomendationsByFigure(figureViewModel);
                        _isClicked = true;
                        SetRecomendations();
                    }
                }
            }
            else
            {
                foreach (var recomendedPoint in _currentFigureRecomendations)
                {
                    if (recomendedPoint == position)
                    {
                        ImageVipe();
                        if (ChessMaster.GameType == GameType.PVP)
                        {
                            if (ChessMaster.Play(_currentFigureOnClick, recomendedPoint))
                                SetLogs(_currentFigureOnClick, recomendedPoint);
                        }
                        else if (ChessMaster.GameType == GameType.Endgame)
                        {
                            if (ChessMaster.PlayWithBrain(_currentFigureOnClick, recomendedPoint,
                                       out FigureViewModel brainFigure, out (int, int) brainPoint))
                            {
                                SetLogs(_currentFigureOnClick, recomendedPoint);
                                SetLogs(brainFigure, brainPoint);
                            }
                        }
                        _isClicked = false;
                        SetAllFiguresImages();
                        if (CheckGameEventsInBoard(_currentFigureOnClick.ColorFlag))
                            GameDebuff();
                        break;
                    }
                }
                if (_isClicked)
                {
                    ImageVipe();
                    SetAllFiguresImages();
                    _isClicked = false;
                }
                SetDefaultColors();
            }
        }

        /// <summary>
        /// Debuff game
        /// </summary>
        private void GameDebuff()
        {
            SetMoveEventsFlag(false);
            _logsCounter = 0;
            Logs.Document.Blocks.Clear();
            WhiteColorPickBox.IsChecked = false;
            BlackColorPickBox.IsChecked = false;
            WhiteColorPickBox.IsEnabled = true;
            BlackColorPickBox.IsEnabled = true;
            CreationBorder.IsEnabled = false;
            PlayBtn.IsEnabled = true;
        }

        /// <summary>
        /// By flag set events or remove
        /// </summary>
        /// <param name="add">add flag</param>
        private void SetMoveEventsFlag(bool add)
        {
            if (add)
                foreach (var vb in _viewBoard)
                {
                    vb.Click += ImageClick;
                    vb.Click += ClickToChange;
                }
            else
                foreach (var vb in _viewBoard)
                {
                    vb.Click -= ImageClick;
                    vb.Click -= ClickToChange;
                }
        }

        /// <summary>
        /// Check events in board
        /// </summary>
        /// <param name="colorFlag">color to check</param>
        /// <returns>true if mate or stale mate else false</returns>
        private bool CheckGameEventsInBoard(bool colorFlag)
        {
            bool endFlag = false;
            switch (ChessMaster.GetEventsInBoard(colorFlag))
            {
                case "Check":
                    MessageBox.Show("    Check!", "Event", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case "Mate":
                    MessageBox.Show("   Mate!", "Event", MessageBoxButton.OK, MessageBoxImage.Information);
                    endFlag = true;
                    break;
                case "StaleMate":
                    endFlag = true;
                    break;
            }
            return endFlag;
        }

        /// <summary>
        /// temp colors for eat points
        /// </summary>
        private List<Brush> _tempColors;

        /// <summary>
        /// Set recomendation images in board
        /// </summary>
        private void SetRecomendations()
        {
            _tempColors = new List<Brush>();
            foreach (var recomendedPoint in _currentFigureRecomendations)
            {
                if (_viewBoard[recomendedPoint.Item1, recomendedPoint.Item2].Content == null)
                {
                    _viewBoard[recomendedPoint.Item1, recomendedPoint.Item2].Content = ViewManager.GetRecomendedPoint();
                }
                else
                {
                    _tempColors.Add(_viewBoard[recomendedPoint.Item1, recomendedPoint.Item2].Background);
                    _viewBoard[recomendedPoint.Item1, recomendedPoint.Item2].Background = Brushes.OrangeRed;
                }
            }
        }

        /// <summary>
        /// Set default colors of points
        /// </summary>
        private void SetDefaultColors()
        {
            int i = 0;
            foreach (var recomendedPoint in _currentFigureRecomendations)
            {
                if (_viewBoard[recomendedPoint.Item1, recomendedPoint.Item2].Background == Brushes.OrangeRed)
                {
                    _viewBoard[recomendedPoint.Item1, recomendedPoint.Item2].Background = _tempColors[i++];
                }
            }
            _tempColors = default;
        }

        /// <summary>
        /// Get position by pichture box
        /// </summary>
        /// <param name="p">picture box</param>
        /// <returns>tuple of integers</returns>
        private (int, int) GetPointbyImage(Button p)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (p == _viewBoard[i, j])
                        return (i, j);
            throw new Exception("Figure image not found!");
        }

        /// <summary>
        /// counter of logs
        /// </summary>
        private int _logsCounter;

        /// <summary>
        /// Set logs for current event
        /// </summary>
        /// <param name="figure">figure to move</param>
        /// <param name="finalPosition">final position</param>
        private void SetLogs(FigureViewModel figure, (int X, int Y) finalPosition)
        {
            var start = AlhpabetHelper.GetCoordinates(Math.Abs(figure.Position.X - 7), Math.Abs(figure.Position.Y));
            var end = AlhpabetHelper.GetCoordinates(Math.Abs(finalPosition.X - 7), Math.Abs(finalPosition.Y));
            Logs.AppendText(figure.ColorFlag ? "Whites" : "Blacks");
            Logs.AppendText($"\n {++_logsCounter} : {figure.FigureName} : {start} to {end}\n");
        }

        #endregion

        #region Horsepath Move Settings

        /// <summary>
        /// Event: Click for horse task
        /// </summary>
        /// <param name="sender">current point</param>
        /// <param name="e">event args</param>
        private async void ClickForHorseTask(object sender, EventArgs e)
        {
            var p = sender as Button;
            if (p != null)
            {
                (int, int) start = default;
                foreach (var pb in _viewBoard)
                {
                    if (pb.Content != null)
                    {
                        start = GetPointbyImage(pb);
                    }
                }
                var end = GetPointbyImage(p);
                var horsePath = ChessMaster.GetPathForHorseTask(start, end);
                //front
                foreach (var vb in _viewBoard)
                    vb.Click -= ClickForHorseTask;
                await ShowAsync(horsePath);
                SetLogsForHorsePath(horsePath);
                foreach (var vb in _viewBoard)
                    vb.Click += ClickForHorseTask;
            }
        }

        /// <summary>
        /// Run async Show Path
        /// </summary>
        /// <param name="path">path</param>
        private async Task ShowAsync(List<(int x, int y)> path)
        {
            await Task.Run(() =>
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _viewBoard[path[i].x, path[i].y].Content = ViewManager.GetChooseHorse(ChessMaster.Turn);
                    });
                    Thread.Sleep(800);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ImageVipe();
                    });
                }
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SetAllFiguresImages();
                    MessageBox.Show($"Move count : {path.Count}", "Info");
                }
                );
            }
            );
        }

        /// <summary>
        /// Method for set logs for horse path
        /// </summary>
        /// <param name="path">list of points (path)</param>
        private void SetLogsForHorsePath(List<(int x, int y)> path)
        {
            _logsCounter = 0;
            Logs.Document.Blocks.Clear();
            for (int i = path.Count - 2; i >= 0; i--)
            {
                Logs.AppendText(AlhpabetHelper.GetCoordinates(path[i].x, path[i].y) + "->");
            }
            var lastPoint = ChessMaster.GetAllFiguresViewModel()[0].Position;
            Logs.AppendText(AlhpabetHelper.GetCoordinates(lastPoint.X, lastPoint.Y));
        }

        #endregion

        #region Choose Figure

        /// <summary>
        /// choose array of button
        /// </summary>
        private Button[] _chooseButtons;

        /// <summary>
        /// Set choosing parametrs
        /// </summary>
        private void SetChoosebuttonParametrs()
        {
            _chooseButtons = new Button[4]
            {
                QueenChooseButton,
                RookChooseButton,
                HorseChooseButton,
                BishopChooseButton
            };

            foreach (var b in _chooseButtons)
            {
                b.Click += ChooseButtonsClick;
                b.IsEnabled = true;
            }
        }

        /// <summary>
        /// Choose button click
        /// </summary>
        /// <param name="sender">any choose button</param>
        /// <param name="e">event args</param>
        private void ChooseButtonsClick(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                switch (b.Name)
                {
                    case "QueenChooseButton":
                        ChessMaster.AddChoosedFigure("Queen");
                        break;
                    case "RookChooseButton":
                        ChessMaster.AddChoosedFigure("Rook");
                        break;
                    case "HorseChooseButton":
                        ChessMaster.AddChoosedFigure("Horse");
                        break;
                    case "BishopChooseButton":
                        ChessMaster.AddChoosedFigure("Bishop");
                        break;
                }
            }
            ImageVipe();
            SetAllFiguresImages();
            FigureChooseBorder.Visibility = Visibility.Hidden;
            SetMoveEventsFlag(true);
            if (CheckGameEventsInBoard(_currentFigureOnClick.ColorFlag))
                GameDebuff();
        }

        /// <summary>
        /// Event: To check is need change figure
        /// </summary>
        /// <param name="sender">cell in viewboard</param>
        /// <param name="e">event args</param>
        private void ClickToChange(object sender, EventArgs e)
        {
            if (!_isClicked)
                IsNeedChangeFigure();
        }

        /// <summary>
        /// Check is need open choose form
        /// </summary>
        private void IsNeedChangeFigure()
        {
            //for top
            IsNeedChangeFigure(0);
            //for bot
            IsNeedChangeFigure(7);
        }

        /// <summary>
        /// Check is culumn flexible to changes
        /// </summary>
        /// <param name="column">given column to check</param>
        private void IsNeedChangeFigure(int column)
        {
            for (int i = 0, j = column; i < 8; i++)
            {
                if (_viewBoard[j, i].Content != null)
                {
                    var position = GetPointbyImage(_viewBoard[j, i]);
                    var figure = ChessMaster.GetFigureByPosition(position);
                    if (figure.FigureName == "Pawn")
                    {
                        FigureChooseBorder.Visibility = Visibility.Visible;
                        SetMoveEventsFlag(false);
                        SetChooseImages(figure.ColorFlag);
                        ChessMaster.ChangePawn((j, i));
                    }
                }
            }
        }

        /// <summary>
        /// Set Choose images
        /// </summary>
        /// <param name="colorFlag">color flag</param>
        private void SetChooseImages(bool colorFlag)
        {
            _chooseButtons[0].Content = ViewManager.GetChooseQueen(colorFlag);
            _chooseButtons[1].Content = ViewManager.GetChooseRook(colorFlag);
            _chooseButtons[2].Content = ViewManager.GetChooseHorse(colorFlag);
            _chooseButtons[3].Content = ViewManager.GetChooseBishop(colorFlag);
        }

        #endregion

    }
}
