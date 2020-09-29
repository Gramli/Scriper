namespace ScriperLib.Extensions
{
    public static class ErrorFormating
    {
        public static string FormatError(this string error)
        {
            return $"Error:: {error}";
        }
    }
}
