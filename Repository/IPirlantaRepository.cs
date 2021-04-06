using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using PirlantaApi.Entities;

namespace PirlantaApi.Repository
{
    public interface IPirlantaRepository
    {
        Task<Pirlanta> GetPirlanta(string id);
        Task<List<Pirlanta>> GetPirlantaFromQuery(string id, string cut, string color, string clarity, string cert, string magazaId, double? caratMin, double? caratMax, string type, int pageNumber);
        Task PutPirlanta(Pirlanta pirlanta);
        Task PostPirlanta(Pirlanta pirlanta);
        Task DeletePirlanta(Pirlanta pirlanta);
        Task<List<Pirlanta>> GetPirlantaUnderMagaza(string magazaId, int pageNumber);
    }
}
