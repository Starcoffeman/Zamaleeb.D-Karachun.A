using System;
using System.Windows;

namespace Zamaleeb.D_Karachun {

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e) {
            try {

                if (string.IsNullOrWhiteSpace(XTextBox.Text)) {
                    MessageBox.Show("Поле X не должно быть пустым!", "Ошибка");
                    return;
                }
                if (string.IsNullOrWhiteSpace(YTextBox.Text)) {
                    MessageBox.Show("Поле Y не должно быть пустым!", "Ошибка");
                    return;
                }

                if (!double.TryParse(XTextBox.Text, out double x)) {
                    MessageBox.Show("Введено некорректное значение X!", "Ошибка");
                    return;
                }
                if (!double.TryParse(YTextBox.Text, out double y)) {
                    MessageBox.Show("Введено некорректное значение Y!", "Ошибка");
                    return;
                }

                Func<double, double> selectedFunction = SelectFunction();
                if (selectedFunction == null) {
                    MessageBox.Show("Пожалуйста, выберите функцию", "Ошибка");
                    return;
                }

                double result = CalculateB(x, y, selectedFunction);
                ResultTextBox.Text = result.ToString("F4");
            }
            catch (Exception ex) {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e) {
            XTextBox.Clear();
            YTextBox.Clear();
            ResultTextBox.Text = "0";
        }

        private Func<double, double> SelectFunction() {
            if (ShRadioButton.IsChecked == true) return Math.Sinh;
            if (SquareRadioButton.IsChecked == true) return x => Math.Pow(x, 2);
            if (ExpRadioButton.IsChecked == true) return Math.Exp;
            return null;
        }
        private double CalculateB(double x, double y, Func<double, double> f) {
            if (y == 0) {
                return 0; 
            }

            if (x / y > 0) {
                return Math.Log(f(x)) + Math.Pow(Math.Pow(f(x), 2) + y, 3);
            }
            else if (x / y < 0) {
                return Math.Log(Math.Abs(f(x) / y)) + Math.Pow(f(x) + y, 3);
            }
            else { 
                return Math.Pow(Math.Pow(f(x), 2) + y, 3);
            }
        }



        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите выйти?", "Осторожно", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) {
                e.Cancel = true;
            }
        }
    }
}
