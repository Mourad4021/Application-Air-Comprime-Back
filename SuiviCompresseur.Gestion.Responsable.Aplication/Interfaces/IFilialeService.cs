using SuiviCompresseur.Gestion.Responsable.Aplication.Models;
using SuiviCompresseur.Gestion.Responsable.Domain.Models;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuiviCompresseur.Gestion.Responsable.Aplication.Interfaces
{
    public interface IFilialeService
    {
        Task<IEnumerable<Filiale>> GetFiliales();
        //rabbit
        Task<string> Transfer(Filiale filiale);
        Task<Filiale> GetFiliale(Guid id);

        Task<string> PutFiliale(Guid id, Filiale Filiale);

        Task<string> DeleteFiliale(Guid id);


    }
}
   