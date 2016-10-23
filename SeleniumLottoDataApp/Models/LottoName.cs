namespace SeleniumLottoDataApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LottoName")]
    public partial class LottoName
    {
        public int id { get; set; }

        [StringLength(25)]
        public string name { get; set; }
    }
}
