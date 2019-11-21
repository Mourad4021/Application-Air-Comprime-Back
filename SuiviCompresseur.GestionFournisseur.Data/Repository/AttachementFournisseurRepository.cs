using SuiviCompresseur.GestionFournisseur.Data.Context;
using SuiviCompresseur.GestionFournisseur.Domain.Interfaces;
using SuiviCompresseur.GestionFournisseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SuiviCompresseur.GestionFournisseur.Data.Repository
{
    public class AttachementFournisseurRepository : IAttachementFournisseurRepository
    {
        private readonly FournisseurDbContext _db;
        public AttachementFournisseurRepository(FournisseurDbContext db)
        {
            _db = db;
        }
        public byte[] getAttachmentFileById(Guid attachementId)
        {

                var folderName = Path.Combine("Ressources", "FournisseurFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = "pieceJointeFR" + attachementId;
                var fullPath = Path.Combine(pathToSave, fileName);
                var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

           byte[] buff = null;

                FileStream fs = new FileStream(fullPath,
                                               FileMode.Open,
                                               FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(fullPath).Length;
                buff = br.ReadBytes((int)numBytes);

            return buff;

        }

        public IEnumerable<AttachementFournisseur> getAttachmentsByFournisseurId(Guid fournisseurID)
        {
            return _db.AttachementFournisseurs.Where(a => a.FournisseurID == fournisseurID).ToList();
        }
    }
}
