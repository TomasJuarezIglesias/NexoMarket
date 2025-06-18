using NexoMarket.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NexoMarket.NexoMarket
{
    public partial class BackupRestore : System.Web.UI.Page
    {
        private readonly BackupRestoreBussiness _backupRestorebusiness;
        public BackupRestore()
        {
            _backupRestorebusiness = new BackupRestoreBussiness();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            string fullPath = _backupRestorebusiness.Backup();

            if (File.Exists(fullPath))
            {
                string fileName = Path.GetFileName(fullPath);

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.TransmitFile(fullPath);
                Response.Flush();

                File.Delete(fullPath);

                Response.End();
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            // Validación inicial
            if (!fileUploadRestore.HasFile)
            {

                return;
            }

            // Validar extensión .bak
            string extension = Path.GetExtension(fileUploadRestore.FileName).ToLower();
            if (extension != ".bak")
            {
                return;
            }

            try
            {
                // Ruta donde se guarda temporalmente
                string backupFolder = @"C:\SQLBackups\";

                // Crear la carpeta si no existe
                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);

                // Guardar archivo en esa ruta
                string fileName = Path.GetFileName(fileUploadRestore.FileName);
                string fullPath = Path.Combine(backupFolder, fileName);
                fileUploadRestore.SaveAs(fullPath);

                // Ejecutar el restore
                _backupRestorebusiness.Restore(fullPath);

                // Eliminar el archivo temporal
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                // Error
            }
        }

    }
}