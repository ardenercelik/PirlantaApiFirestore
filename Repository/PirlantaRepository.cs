using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Api.Gax;
using Google.Cloud.Firestore;
using PirlantaApi.Entities;
using Newtonsoft.Json;
using PirlantaApi.Helpers;

namespace PirlantaApi.Repository
{
    public class PirlantaRepository : IPirlantaRepository
    {
        FirestoreDb db = new FirestoreDbBuilder
        {
            ProjectId = "pirlantam-3adc8",
            EmulatorDetection = EmulatorDetection.EmulatorOrProduction,

        }.Build();
        public async Task DeletePirlanta(Pirlanta pirlanta)
        {
            DocumentReference docRef = PirlantaReference(pirlanta.PirlantaId);
            await docRef.DeleteAsync();
        }

        public async Task<Pirlanta> GetPirlanta(string id)
        {
            Pirlanta pirlanta = new();
            DocumentSnapshot docRef = await db.Collection("pirlantalar").Document(id).GetSnapshotAsync();
            pirlanta = docRef.ConvertTo<Pirlanta>();
            return pirlanta;
        }
        public async Task<List<Pirlanta>> GetPirlantaUnderMagaza(string magazaId, int pageNumber)
        {
            var pirlantaQuery = await db.Collection("pirlantalar").WhereEqualTo("MagazaId", magazaId).GetSnapshotAsync();
            return DbHelper<Pirlanta>.SnapshotToList(pirlantaQuery);
        }
        public async Task<List<Pirlanta>> GetPirlantaFromQuery(string id, string cut, string color, string clarity, string cert, string magazaId, double? caratMin, double? caratMax, string type, int pageNumber)
        {
            int pageSize = 10;
            Query query = db.CollectionGroup("pirlantalar");
            if (color is not null)
            {
                query = query.WhereEqualTo("Color", color);
            };
            if (cut is not null ){
                query = query.WhereEqualTo("Cut", cut);
            };
            if (type is not null)
            {
                query = query.WhereEqualTo("Type", type);
            };
            if (clarity is not null)
            {
                query = query.WhereEqualTo("Clarity", clarity);
            };
            if (cert is not null)
            {
                query = query.WhereEqualTo("Cert", cert);
            };
            if (caratMax != (double?)0)
            {
                Console.WriteLine("cm: " +caratMax.ToString());
                query = query.WhereLessThanOrEqualTo("Carat", caratMax);
            };
            if (caratMin != (double?)0)
            {
                Console.WriteLine("c: " + caratMin.ToString());
                query = query.WhereGreaterThanOrEqualTo("Carat", caratMin);
            };

            query = query.OrderByDescending("Carat").Limit(pageSize).Offset((pageNumber-1)*pageSize);

            QuerySnapshot snapshot = await query.GetSnapshotAsync();
       
            var result = DbHelper<Pirlanta>.SnapshotToList(snapshot);
            return result;
        }

        public async Task PostPirlanta(Pirlanta pirlanta)
        {

            CollectionReference collection = PirlantaCollection();
            await collection.AddAsync(pirlanta);
        }

        public async Task PutPirlanta(Pirlanta pirlanta)
        {
            DocumentReference docRef = PirlantaReference(pirlanta.PirlantaId);
            await docRef.SetAsync(pirlanta);
        }

        private DocumentReference PirlantaReference(string id)
        {
            return db.Collection("pirlantalar").Document(id);
        }

        private CollectionReference PirlantaCollection()
        {
            return db.Collection("pirlantalar");
        }
    }
}
