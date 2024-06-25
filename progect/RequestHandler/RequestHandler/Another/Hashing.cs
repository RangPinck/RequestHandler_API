using System.Security.Cryptography;
using System.Text;

namespace RequestHandler.Another
{
    public static class Hashing
    {
        public static string ToSHA256(string s)
        {
            //using var sha256 = SHA256.Create();
            //byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
            //var sb = new StringBuilder();
            //for (int i = 0; i < bytes.Length; i++)
            //{
            //    sb.Append(bytes[i]);
            //}
            //return sb.ToString();

            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = sha256.ComputeHash(bytes);
            var hex = BitConverter.ToString(hash).Replace("-", "").ToLower();
            return hex;
        }
    }
}
