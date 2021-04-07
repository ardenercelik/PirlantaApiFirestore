using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirlantaApi.Helpers
{
    public static class DbHelper<T>
    {
        public static List<T> SnapshotToList(QuerySnapshot documents)
        {
            List<T> result = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in documents.Documents)
            {
                T city = documentSnapshot.ConvertTo<T>();
                result.Add(city);
            }
            return result;
        }

        public async static void DeleteSnapshot(QuerySnapshot documents)
        {
            foreach (DocumentSnapshot documentSnapshot in documents.Documents)
            {
                await documentSnapshot.Reference.DeleteAsync();
            }
        }



    }
    
}
