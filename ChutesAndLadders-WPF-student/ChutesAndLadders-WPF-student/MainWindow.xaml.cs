using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChutesAndLadders_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int NrOfRows = 10;
        const int NrOfColumns = 10;
        const int CellWidth = 54;
        const int CellHeight = 55;

        private ChutesAndLadders cl;
        private List<Shape> shapes = new List<Shape>();
        private List<Label> labels = new List<Label>();

        public MainWindow()
        {
            InitializeComponent();

            cl = new ChutesAndLadders();

            CreateTiles();

            // update tiles according to values in Chutes&Ladders
            UpdateTiles();
        }

        private void CreateTiles()
        {
            for (int row = 0; row < NrOfRows; row++)
            {
                for (int col = 0; col < NrOfColumns; col++)
                {
                    int left = 10;
                    int top = 10;

                    if (row % 2 == 0)
                        // 1st, 3rd, 5th, ... row => from left to right
                        left += col * CellWidth;
                    else
                        // 2nd, 4th, 6th, ... row => from right to left
                        left += (NrOfColumns * CellWidth - (col + 1) * CellWidth);
                    top += (NrOfRows * CellHeight - CellHeight * (row + 1)); // start from bottom

                    Rectangle border = new Rectangle();
                    border.Stroke = new SolidColorBrush(Colors.Black);
                    border.StrokeThickness = 1;
                    border.Width = CellWidth + 1;
                    border.Height = CellHeight + 1;
                    border.Name = $"border_{row}_{col}";
                    Canvas.SetLeft(border, left);
                    Canvas.SetTop(border, top);
                    border.StrokeThickness = 1;
                    canvas.Children.Add(border);

                    Rectangle rect = new Rectangle();
                    rect.Fill = new SolidColorBrush(Colors.Red);
                    rect.Opacity = 0.0; // will be set later
                    rect.Width = CellWidth;
                    rect.Height = CellHeight;
                    rect.Name = $"tile_{row}_{col}";
                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    canvas.Children.Add(rect);

                    this.shapes.Add(rect);

                    Label lbl = new Label();
                    lbl.Content = "?";
                    lbl.Name = $"label_{row}_{col}";
                    lbl.FontSize = 8;
                    lbl.FontWeight = FontWeights.Bold;
                    Canvas.SetLeft(lbl, left + 5);
                    Canvas.SetTop(lbl, top + 5);
                    canvas.Children.Add(lbl);

                    this.labels.Add(lbl);
                }
            }
        }

        private void UpdateTiles()
        {
            for (int row = 0; row < NrOfRows; row++)
            {
                for (int col = 0; col < NrOfColumns; col++)
                {
                    int index = row * NrOfColumns + col;

                    // get rectancle for this position
                    Rectangle rect = (Rectangle)this.shapes[index];

                    // change opacity according to Position
                    double probability = cl.Position[index + 1];    // !!
                    rect.Opacity = probability; // 0.0 .. 1.0

                    // update label
                    Label lbl = this.labels[index];
                    double chance = probability * 100;
                    lbl.Content = $"{chance:0.0000} % ";
                }
            }

            lblNrOfSteps.Content = this.cl.NrOfMoves;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            this.cl.Reset();
            UpdateTiles();
        }

        private void btnNextStep_Click(object sender, RoutedEventArgs e)
        {
            this.cl.NextMove();
            UpdateTiles();
        }

        private void btnAnimate_Click(object sender, RoutedEventArgs e)
        {
            this.cl.AnimationChanged += Cl_AnimationChanged;
            this.cl.StartAnimation(100);
        }

        private void Cl_AnimationChanged(object sender, EventArgs e)
        {
            UpdateTiles();
        }

        private void sliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.cl != null)
            {
                this.cl.AnimationSpeed = (int)sliderSpeed.Value;
            }
        }
    }
}