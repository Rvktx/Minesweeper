using Minesweeper.Properties;
using System;
using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private int fieldWidth;
        private int fieldHeight;
        private int fieldArea;
        private int mines;
        private int minesMax;

        public SettingsWindow()
        {
            this.fieldWidth = Settings.Default.fieldWidth;
            this.fieldHeight = Settings.Default.fieldHeight;
            this.mines = Settings.Default.mines;
            this.UpdateArea();
            InitializeComponent();
        }

        private void UpdateArea()
        {
            this.fieldArea = this.fieldWidth * this.fieldHeight;
            this.minesMax = (int)Math.Floor(0.4 * this.fieldArea);
            if (this.mines > this.minesMax)
                this.mines = this.minesMax;
        }

        private void UpdateChance()
        {
            String textPart1 = "There will be ";
            String textPart2 = "% chance of hitting a mine.";
            float chance = 100 * this.mines / this.fieldArea;

            chanceLabel.Content = textPart1 + chance + textPart2;
        }

        private void UpdateMinesSlider()
        {
            minesSlider.Maximum = this.minesMax;
            minesSlider.Value = this.mines;
            minesLabel.Content = this.mines;
            this.UpdateChance();
        }

        private void Canvas_Initialized(object sender, EventArgs e)
        {
            widthSlider.Value = this.fieldWidth;
            widthLabel.Content = this.fieldWidth;
            heightSlider.Value = this.fieldHeight;
            heightLabel.Content = this.fieldHeight;
            this.UpdateMinesSlider();
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.fieldWidth = (int)widthSlider.Value;
            widthLabel.Content = this.fieldWidth;
            this.UpdateArea();
            this.UpdateMinesSlider();
        }

        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.fieldHeight = (int)heightSlider.Value;
            heightLabel.Content = this.fieldHeight;
            this.UpdateArea();
            this.UpdateMinesSlider();
        }

        private void MinesSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.mines = (int)minesSlider.Value;
            minesLabel.Content = this.mines;

            this.UpdateChance();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.fieldWidth = this.fieldWidth;
            Settings.Default.fieldHeight = this.fieldHeight;
            Settings.Default.mines = this.mines;
            this.Close();
            MainWindow.Restart();
        }
    }
}
