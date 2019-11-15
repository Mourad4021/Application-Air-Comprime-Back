using Microsoft.AspNetCore.Mvc;
using SuiviCompresseur.GestionCompresseur.Data.Context;
using SuiviCompresseur.GestionCompresseur.Domain.Interfaces;
using SuiviCompresseur.GestionCompresseur.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace SuiviCompresseur.GestionCompresseur.Data.Repository
{
    public class AttachementRepository : IAttachementRepository
    {
       private CompresseurDbContext _db;

        public AttachementRepository(CompresseurDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Attachment> getAttachmentsByFicheSuiviId(Guid ficheSuiviId)
        {
            return _db.Attachments.Where(a => a.FicheSuiviID == ficheSuiviId).ToList();
        }


        public byte[] getAttachmentFileById(Guid attachementId)
        {

          Guid? v=  _db.Attachments.Where(x => x.AttachmentId == attachementId).Select(x => x.EntretienReservoirID).FirstOrDefault(); 
 byte[] buff = null;
            if (v!=null)
            {
                var folderName = Path.Combine("Resources", "EntretienReservoirFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = "pieceJointeER" + attachementId;
                var fullPath = Path.Combine(pathToSave, fileName);
                var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);


               
                FileStream fs = new FileStream(fullPath,
                                               FileMode.Open,
                                               FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(fullPath).Length;
                buff = br.ReadBytes((int)numBytes);

                var memoryStream = new MemoryStream(buff);

            }
            else 
            {
                var folderName = Path.Combine("Resources", "FicheSuiviFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = "pieceJointeFS" + attachementId;
                var fullPath = Path.Combine(pathToSave, fileName);
                var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                stream.Close();

              
                FileStream fs = new FileStream(fullPath,
                                               FileMode.Open,
                                               FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(fullPath).Length;
                buff = br.ReadBytes((int)numBytes);

                var memoryStream = new MemoryStream(buff);
              
            }


            return buff;

        }

        public IEnumerable<Attachment> getAttachmentsByEntretienReservoirId(Guid entretienReservoirID)
        {
            return _db.Attachments.Where(a => a.EntretienReservoirID == entretienReservoirID).ToList();
        }
    }
}

