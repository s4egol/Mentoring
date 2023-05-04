using LiteDB;
using System.IO;

namespace NoSql.Context.Abstract
{
    public abstract class LiteDbContext : IDisposable
    {
        protected LiteDatabase InternalDatabase;

        protected LiteDbContext(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                File.Create(databasePath);
            }

            InternalDatabase = new LiteDatabase(databasePath);
        }

        public void Dispose()
        {
            InternalDatabase.Dispose();
        }
    }
}
