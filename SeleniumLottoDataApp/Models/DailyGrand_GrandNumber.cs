namespace SeleniumLottoDataApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DailyGrand_GrandNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DrawNumber { get; set; }

        [StringLength(25)]
        public string DrawDate { get; set; }

        public int? GrandNumber { get; set; }
    }
}
