using NexoMarket.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexoMarket.Business
{
    public class BackupRestoreBussiness
    {
        private readonly RepositoryBackupRestore _repositoryBackupRestore;
        public BackupRestoreBussiness()
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
