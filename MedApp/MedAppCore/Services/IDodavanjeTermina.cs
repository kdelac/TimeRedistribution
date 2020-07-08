using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    /// <summary>
    /// Pomocno, samo u svrhu pregleda podataka
    /// </summary>
    /// <returns></returns>
    public interface IDodavanjeTermina
    {
        Task Dodaj(int pocMin, int pocSat, int razmak);
    }
}
