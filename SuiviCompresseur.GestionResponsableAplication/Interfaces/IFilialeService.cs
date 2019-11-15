using SuiviCompresseur.GestionResponsable.Domain.Models;
using SuiviCompresseur.GestionResponsableAplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionResponsableAplication.Interfaces
{
    public interface IFilialeService
    {
        IEnumerable<Filiale> GetFiliales();
        void Transfer(FilialeEnv filialeEnv);
    }
}