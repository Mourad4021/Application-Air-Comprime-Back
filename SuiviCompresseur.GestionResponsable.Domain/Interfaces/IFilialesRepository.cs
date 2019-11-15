using SuiviCompresseur.GestionResponsable.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SuiviCompresseur.GestionResponsable.Domain.Interfaces
{
    public interface IFilialesRepository
    {
        IEnumerable<Filiale> GetFiliales();

        Filiale GetFiliale(int id);

        int PostFiliale(Filiale filiale);

        bool DeleteFiliale(int id);

        int PutFiliale(int id, Filiale filiale);
    }
}
