using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using PirlantaApi.Entities;
using PirlantaApi.Helpers;
using Newtonsoft.Json;


namespace PirlantaApi.Repository
{

    
   
    public class MagazaRepository : IMagazaRepository
    {
        FirestoreDb db = new FirestoreDbBuilder
        {
            ProjectId = AppConstants.ProjectId,
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,

        }.Build();
        public async Task DeleteMagaza(Magaza magaza)
        {
            if (magaza != null)
            {

            Query pirlantaQuery = db.Collection("pirlantalar").WhereEqualTo("MagazaId", magaza.MagazaId);
            QuerySnapshot pirlantaSnapshot = await pirlantaQuery.GetSnapshotAsync();
            var stats = await StatsCollection(magaza.Uid).GetSnapshotAsync();
            DbHelper<Pirlanta>.DeleteSnapshot(stats);
            DbHelper<Pirlanta>.DeleteSnapshot(pirlantaSnapshot);
            DocumentReference docRef = db.Collection("magazalar").Document(magaza.MagazaId);
            await docRef.DeleteAsync();
            }
        }

        public async Task<Magaza> GetMagaza(string id)
        {
            DocumentReference docRef = db.Collection("magazalar").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            var magaza = snapshot.ConvertTo<Magaza>();
            //magaza.Pirlantalar = await GetPirlantaUnderMagaza(id);
            return magaza;
        }

        public async Task<Magaza> GetMagazaByUid(string uid)
        {
            DocumentReference docRef = db.Collection("magazalar").Document(uid);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            var magaza = snapshot.ConvertTo<Magaza>();
            //magaza.Pirlantalar = await GetPirlantaUnderMagaza(magaza.MagazaId);
            return magaza;
        }

        public async Task PostMagaza(Magaza magaza)
        {
            magaza.PirlantaCount = 0;
            await db.Collection("magazalar").Document(magaza.Uid).SetAsync(magaza);
        }

        public async Task PutMagaza(Magaza magaza)
        {
            DocumentReference docRef = db.Collection("magazalar").Document(magaza.Uid);
            await docRef.SetAsync(magaza);
        }

        public async Task<Magaza> FindMagaza(string id)
        {
            DocumentReference docRef = db.Collection("magazalar").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            var magaza = snapshot.ConvertTo<Magaza>();
            return magaza;
        }
        private CollectionReference StatsCollection(string id)
        {
            return db
                .Collection("magazalar")
                .Document(id)
                .Collection("stats");
        }
        public async Task<bool> MagazaExists(string uid)
        {
            var magaza = await GetMagazaByUid(uid);
            if (magaza != null) return true;
            return false;
        }
        //private async Task<List<Pirlanta>> GetPirlantaUnderMagaza(string magazaId)
        //{
        //    QuerySnapshot pirlantaQuery = await db.Collection("pirlantalar").WhereEqualTo("MagazaId", magazaId).GetSnapshotAsync();
        //    return DbHelper<Pirlanta>.SnapshotToList(pirlantaQuery);
        //}
    }
}
