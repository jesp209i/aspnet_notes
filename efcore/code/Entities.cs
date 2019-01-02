namespace Entities
{

    public class City
    {
        [Key] // ikke nødvendigt med denne attribut, da EFC bruger Id som nøgle per konvention.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // DatabaseGeneratedOption.Identity = db genererer ved tilføjelse 
        // DatabaseGeneratedOption.Computed = db genererer ved tilføjelse eller opdatering
        // DatabaseGeneratedOption.None = bliver ikke genereret af db
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        public ICollection<PointsOfInterest> PointsOfInterest { get; set; } = new List<PointsOfInterest>();
    }

    public class PointsOfInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("CityId")] // eksplicit 
        public City City { get; set; } // navigation property - pejer automatisk på cityklassens id, 
        public int CityId { get; set; } // per konvention tilføjer man et felt til at holde på fremmednøglen
    }
}
