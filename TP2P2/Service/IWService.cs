using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2P1.Models.EntityFramework;

namespace TP2P2.Service
{
    public interface IWSService
    {
        Task<List<Serie>> GetSeriesAsync(string nomControleur);
        Task<Serie> PostSeriesAsync(Serie serie);
        Task<bool> PutSeriesAsync(Serie serie);
        Task<bool> DeleteSeriesAsync(int id);
    }
}
