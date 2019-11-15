
using SuiviCompresseur.GestionResponsable.Application.Models;
using SuiviCompresseur.GestionResponsable.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuiviCompresseur.GestionResponsable.Application.Interfaces
{
    public interface IFilialeService
    {
        IEnumerable<Filiale> GetFiliales();
        void Transfer(FilialeEnv filialeEnv);
    }
}