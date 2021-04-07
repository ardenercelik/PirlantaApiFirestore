using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PirlantaApi.Entities;

namespace PirlantaApi.Repository
{
    public interface IMagazaRepository
    {
        //Task<Magaza> GetPagedMagazalar(int PageNumber);
        Task<Magaza> GetMagaza(string id);
        Task<Magaza> GetMagazaByUid(string uid);
        Task PutMagaza(Magaza magaza);
        Task PostMagaza(Magaza magaza);
        Task DeleteMagaza(Magaza magaza);
        Task<Magaza> FindMagaza(string id);
        Task<bool> MagazaExists(string id);
    }
}
