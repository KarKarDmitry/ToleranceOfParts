using Microsoft.Win32;

namespace ToleranceOfParts.Classes.Environment
{
    public static class FileDialog
    {

        static OpenFileDialog fileDialog;

        public static string SelectFile(string filter = "*.txt | *.txt", bool multiselect = false)
        {
            fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = multiselect;
            fileDialog.Filter = filter;

            if (fileDialog.ShowDialog() == false) return null;

            return fileDialog.FileName;
        }

    }
}
