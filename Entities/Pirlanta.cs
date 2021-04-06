using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;

namespace PirlantaApi.Entities
{
    [FirestoreData]
    public class Pirlanta
    {
        [FirestoreDocumentId]
        public string PirlantaId { get; set; }
        [JsonRequired]
        [FirestoreProperty]
        public int Adet { get; set; }
        [JsonRequired]
        [FirestoreProperty]
        [RegularExpression("ROUND|PRINCESS|OVAL|MARKIZ|PEAR|CUSHION|EMERALD|ASSCHER|RADIANT|HEART|BAGET", ErrorMessage = "SADECE PIRLANTA TIPLERI")]
        public string Type { get; set; }
        [Range(0,4.00, ErrorMessage ="Karat 0 ve 4 arası olabilir.")]
        [FirestoreProperty]
        [JsonRequired]
        public double Carat { get; set; }
        [FirestoreProperty]
        public int Price { get; set; }
        [FirestoreProperty]
        public string MagazaId { get; set; }
        [FirestoreDocumentCreateTimestamp]
        public DateTime DateCreated { get; set; }
        [FirestoreDocumentUpdateTimestamp]
        public DateTime DateUpdated { get; set; }
        [RegularExpression("D|E|F|G|H|I|J|K|L|M|N|P|R|S|Z", ErrorMessage = "Renk bilgisi sadece D-Z arası harf olabilir.")]
        [FirestoreProperty]
#nullable enable
        public string? Color { get; set; }
        [RegularExpression("IF|VVS1|VVS2|VS1|VS2|SI1|SI2|SI3|I1|I2|I3", ErrorMessage = "sadece imperfection verisi olabilir")]
        [FirestoreProperty]
        public string? Clarity { get; set; }
        [RegularExpression("POOR|FAIR|GOOD|VERY GOOD|EXCELLENT", ErrorMessage ="Sadece kalite bilgileri olabilir")]
        [FirestoreProperty]
        public string? Cut { get; set; }
        [RegularExpression("GIA|HRD", ErrorMessage = "GIA ve HRD olabilir")]
        [FirestoreProperty]
        public string? Cert { get; set; }
#nullable disable
    }


}
