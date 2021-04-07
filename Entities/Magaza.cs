using System;

using System.ComponentModel.DataAnnotations;
using Google.Cloud.Firestore;

namespace PirlantaApi.Entities
{
    [FirestoreData]
    public class Magaza
    {
        [FirestoreDocumentId]
        public string MagazaId { get; set; }
        [FirestoreProperty]
        public string MagazaPhoneNumber { get; set; }
        [FirestoreProperty]
        [StringLength(160)]
        public string Adres { get; set; }
        [FirestoreProperty]
        public string Uid { get; set; }
        [FirestoreProperty]
        public int PirlantaCount { get; set; }
        [FirestoreProperty]
        public string MagazaName { get; set; }
        [FirestoreDocumentCreateTimestamp]
        public DateTime DateCreated { get; set; }
        [FirestoreProperty, ServerTimestamp]
        public DateTime DateUpdated { get; set; }
    }

}
