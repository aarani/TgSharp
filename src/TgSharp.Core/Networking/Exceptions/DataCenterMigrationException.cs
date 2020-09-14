using System;

namespace TgSharp.Core.Networking.Exceptions
{
    internal abstract class DataCenterMigrationException : Exception
    {
        internal int DC { get; private set; }

        private const string REPORT_MESSAGE =
            " See: https://github.com/nblockchain/TgSharp#i-get-a-xxxmigrationexception-or-a-migrate_x-error";

        protected DataCenterMigrationException(string msg, int dc) : base (msg + REPORT_MESSAGE)
        {
            DC = dc;
        }
    }
}