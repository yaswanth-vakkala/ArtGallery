namespace ArtGalleryAPI.CustomExceptions
{
    public class InvalidDeletionException : Exception
    {
        private const string defaultMessage = "Resource not found!";
        public InvalidDeletionException() : base(defaultMessage) { }

        public InvalidDeletionException(string message) : base(message) { }
    }
}
