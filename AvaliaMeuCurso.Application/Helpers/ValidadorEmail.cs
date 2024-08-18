using System.Text.RegularExpressions;

namespace AvaliaMeuCurso.Application.Helpers
{
    public static class ValidadorEmail
    {
        // Expressão regular para validar emails
        private static readonly Regex _emailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static bool EmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return _emailRegex.IsMatch(email);
        }
    }
}
