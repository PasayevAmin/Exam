using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Indigo_Exam.Utilities.Extentions
{
    public static class FileValidator
    {
        public static bool CheckType(this IFormFile file, string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }
        public static bool CheckSize(this IFormFile file, int mb)
        {
            if (file.Length <= 1024 * 1024 * mb)
            {
                return true;
            }
            return false;
        }
        public static async Task<string> CreateFileAsync(this IFormFile file, string root, params string[] folders)
        {
            string Filename = Guid.NewGuid().ToString() + file.FileName;
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, Filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            return Filename;
        }
        public static void DeleteFile(this string filename, string root, params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
