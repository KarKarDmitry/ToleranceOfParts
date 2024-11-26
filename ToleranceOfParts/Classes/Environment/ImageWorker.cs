using System.IO;

namespace ToleranceOfParts.Classes.Environment
{
    public static class ImageWorker
    {

        public static byte[] TakeImageOrCompasAsBytes()
        {
            string filePath = FileDialog.SelectFile("*.png | *.png");

            if (filePath == null) return null;

            try
            {
                // Читаем файл в массив байтов
                byte[] imageBytes = File.ReadAllBytes(filePath);
                return imageBytes;
            }
            catch (Exception ex)
            {
                // Обработка ошибок при чтении файла
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return null;
            }

        }

    }
}
