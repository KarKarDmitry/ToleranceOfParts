using System.Windows;
using OPP;
using OPP.Autentification;

namespace ToleranceOfParts.Views.Windows
{
    public partial class AutentificationWindow : Window
    {
        public AutentificationWindow()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем пароль из PasswordBox
                string password = PasswordBox.Password;

                // Пытаемся пройти аутентификацию
                bool isAuthenticated = AutentificationProperties.Autentificate(password);

                if (isAuthenticated)
                {
                    // Если аутентификация успешна, закрываем окно и возвращаем true
                    this.DialogResult = true; // Устанавливаем DialogResult в true
                    this.Close(); // Закрываем окно
                }
                else
                {
                    // Если аутентификация не удалась, выбрасываем исключение
                    throw new Exception("Неверный пароль. Попробуйте снова.");
                }
            }
            catch (Exception ex)
            {
                // Отображаем ошибку в ToolTip
                PasswordBox.ToolTip = ex.Message;
                PasswordBox.BorderBrush = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);

                // Отображаем сообщение об ошибке под PasswordBox
                ErrorTextBlock.Text = ex.Message;
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
