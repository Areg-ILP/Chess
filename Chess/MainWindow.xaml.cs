﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        #region Board


        private Button[,] _viewBoard;

        private void InitilizeBoard()
        {
            _viewBoard = new Button[8,8];

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
                    _viewBoard[i, j].Margin = new Thickness(left,top,0,0);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void SetProfileFrontEndSettings()
        {

        }

        private void SetPersonalAreaElements(UserViewModel user)
        {
            
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion


    }
}
