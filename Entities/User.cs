using Google.Cloud.Firestore;
using System;
using System.ComponentModel.DataAnnotations;

namespace PirlantaApi.Entities
{


    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string Uid { get; set; }
        [FirestoreProperty]
        public string Name { get; set; }
        [FirestoreProperty]
        public string Number { get; set; }
        [EmailAddress]
        [FirestoreProperty]
        public string Mail { get; set; }
        [FirestoreProperty]
        public Magaza Magaza { get; set; }

        [FirestoreDocumentCreateTimestamp]
        public Timestamp CreateTime { get; set; }
    }

}
