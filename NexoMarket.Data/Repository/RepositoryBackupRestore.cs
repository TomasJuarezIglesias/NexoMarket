using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Data.Repository
{
    public class RepositoryBackupRestore
    {
        public string Backup()
        {
            using (var context = new NexoMarketEntities())
            {
                string backupFolder = @"C:\SQLBackups\";

                // Crear la carpeta si no existe
                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);

                string fileName = $"NexoMarket_Backup_{DateTime.Now:yyyy_MM_dd_HH_mm}.bak";
                string fullPath = Path.Combine(backupFolder, fileName);

                // Escapar la ruta para SQL Server
                string sqlPath = fullPath.Replace("\\", "\\\\");

                string query = $"BACKUP DATABASE [NexoMarket] TO DISK = N'{sqlPath}'";

                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);

                return fullPath;
            }
        }


        public void Restore(string path)
        {
            using (var context = new NexoMarketEntities())
            {
                string escapedPath = path.Replace("\\", "\\\\");
                string query =
                    $"USE MASTER; " +
                    $"ALTER DATABASE [NexoMarket] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; " +
                    $"RESTORE DATABASE [NexoMarket] FROM DISK = N'{escapedPath}' WITH REPLACE; " +
                    $"ALTER DATABASE [NexoMarket] SET MULTI_USER;";

                context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);
            }
        }

    }
}
