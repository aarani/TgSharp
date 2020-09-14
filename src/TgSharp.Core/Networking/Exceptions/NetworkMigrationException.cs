namespace TgSharp.Core.Networking.Exceptions
{
    internal class NetworkMigrationException : DataCenterMigrationException
    {
        internal NetworkMigrationException(int dc)
            : base($"Network located on a different DC: {dc}.", dc)
        {
        }
    }
}