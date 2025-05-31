using System.ComponentModel.DataAnnotations.Schema;

namespace NexepiApi.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Column("sn")]
        public string? Sn { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        [Column("manufacturer")]
        public string? Manufacturer { get; set; }

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        [Column("acquisition_date")]
        public DateTime? AcquisitionDate { get; set; }

        [Column("first_use_date")]
        public DateTime? FirstUseDate { get; set; }

        [Column("max_discard_date")]
        public DateTime? MaxDiscardDate { get; set; }

        [Column("discard_date")]
        public DateTime? DiscardDate { get; set; }

        [Column("record_date")]
        public DateTime? RecordDate { get; set; }

        [Column("record_time")]
        public TimeSpan? RecordTime { get; set; }

        [Column("avc_ref")]
        public string? AvcRef { get; set; }

        [Column("company_id")]
        public int? CompanyId { get; set; }

        [Column("comments")]
        public string? Comments { get; set; }

        [Column("manual_url")]
        public string? ManualUrl { get; set; }

        [Column("certification_url")]
        public string? CertificationUrl { get; set; }

        [Column("photo_url")]
        public string? PhotoUrl { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("special_conditions")]
        public string? SpecialConditions { get; set; }

        [Column("price_purchase")]
        public decimal? PricePurchase { get; set; }

        [Column("price_current")]
        public decimal? PriceCurrent { get; set; }

        [Column("materials")]
        public string? Materials { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("location")]
        public string? Location { get; set; }
    }
}
