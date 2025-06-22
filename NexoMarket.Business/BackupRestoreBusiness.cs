using NexoMarket.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BackupRestoreBusiness
    {
        private readonly RepositoryBackupRestore _repositoryBackupRestore;
        public BackupRestoreBusiness()
        {
            _repositoryBackupRestore = new RepositoryBackupRestore();
        }

        public string Backup()
        {
            return _repositoryBackupRestore.Backup();
        }

        public void Restore(string path)
        {
            _repositoryBackupRestore.Restore(path);
        }
    }
}
