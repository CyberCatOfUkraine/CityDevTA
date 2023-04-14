using System;
using System.Windows;
using System.Windows.Input;

namespace Project_Sentinel.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static Action MinimizeWindowCommand
        {
            get
            {
                return new(() => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
            }
            set { }
        }

        public static Action CloseWindowCommand
        {
            get
            {
                return new Action(Application.Current.MainWindow.Close);
            }
            set { }
        }

        #region Переміщення вікна за допомогою Drag

        private bool isDragging;
        private Point clickOffset;

        private void WindowControlPanelGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickOffset = e.GetPosition(this);
        }

        private void WindowControlPanelGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPos = e.GetPosition(this);
                double dx = currentPos.X - clickOffset.X;
                double dy = currentPos.Y - clickOffset.Y;

                Left += dx;
                Top += dy;
            }
        }

        private void WindowControlPanelGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void WindowControlPanelGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        #endregion Переміщення вікна за допомогою Drag
    }
}