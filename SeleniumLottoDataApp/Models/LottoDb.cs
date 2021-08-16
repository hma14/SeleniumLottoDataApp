namespace SeleniumLottoDataApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;

    public partial class LottoDb : DbContext
    {
        public LottoDb()
            : base("name=LottoDbContext")
        {
        }

        public virtual DbSet<BC49> BC49 { get; set; }
        public virtual DbSet<BritishLotto> BritishLottoes { get; set; }
        public virtual DbSet<CaSuperlottoPlu> CaSuperlottoPlus { get; set; }
        public virtual DbSet<CaSuperlottoPlus_Mega> CaSuperlottoPlus_Mega { get; set; }
        public virtual DbSet<ColoradoLotto> ColoradoLottoes { get; set; }
        public virtual DbSet<ConnecticutLotto> ConnecticutLottoes { get; set; }
        public virtual DbSet<EuroJackpot> EuroJackpots { get; set; }
        public virtual DbSet<EuroJackpot_Euros> EuroJackpot_Euros { get; set; }
        public virtual DbSet<EuroMillion> EuroMillions { get; set; }
        public virtual DbSet<EuroMillions_LuckyStars> EuroMillions_LuckyStars { get; set; }
        public virtual DbSet<FloridaFantasy5> FloridaFantasy5 { get; set; }
        public virtual DbSet<FloridaLotto> FloridaLottoes { get; set; }
        public virtual DbSet<FloridaLucky> FloridaLuckies { get; set; }
        public virtual DbSet<GermanLotto> GermanLottoes { get; set; }
        public virtual DbSet<Lottery> Lotteries { get; set; }
        public virtual DbSet<LottoMax> LottoMaxes { get; set; }
        public virtual DbSet<MegaMillion> MegaMillions { get; set; }
        public virtual DbSet<MegaMillions_MegaBall> MegaMillions_MegaBall { get; set; }
        public virtual DbSet<MyProduct> MyProducts { get; set; }
        public virtual DbSet<NewJerseyPick6Lotto> NewJerseyPick6Lotto { get; set; }
        public virtual DbSet<NYLotto> NYLottoes { get; set; }
        public virtual DbSet<NYSweetMillion> NYSweetMillions { get; set; }
        public virtual DbSet<OregonMegabuck> OregonMegabucks { get; set; }
        public virtual DbSet<OZLottoMon> OZLottoMons { get; set; }
        public virtual DbSet<OZLottoSat> OZLottoSats { get; set; }
        public virtual DbSet<OZLottoTue> OZLottoTues { get; set; }
        public virtual DbSet<OZLottoWed> OZLottoWeds { get; set; }
        public virtual DbSet<PowerBall> PowerBalls { get; set; }
        public virtual DbSet<PowerBall_PowerBall> PowerBall_PowerBall { get; set; }
        public virtual DbSet<SevenLotto> SevenLottoes { get; set; }
        public virtual DbSet<SSQ> SSQs { get; set; }
        public virtual DbSet<SSQ_Blue> SSQ_Blue { get; set; }
        public virtual DbSet<SuperLotto> SuperLottoes { get; set; }
        public virtual DbSet<SuperLotto_Rear> SuperLotto_Rear { get; set; }
        public virtual DbSet<LottoName> LottoNames { get; set; }
        public virtual DbSet<Lotto> Lottos { get; set; }
        public virtual DbSet<NewYorkTake5> NewYorkTake5 { get; set; }
        public virtual DbSet<TexasCashFive> TexasCashFive { get; set; }
        public virtual DbSet<DailyGrand> DailyGrand { get; set; }
        public virtual DbSet<DailyGrand_GrandNumber> DailyGrand_GrandNumber { get; set; }
        public virtual DbSet<Cash4Life> Cash4Life { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BC49>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<BritishLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<CaSuperlottoPlu>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<CaSuperlottoPlus_Mega>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<ColoradoLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<ConnecticutLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<EuroJackpot>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<EuroJackpot_Euros>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<EuroMillion>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<EuroMillions_LuckyStars>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<FloridaFantasy5>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<FloridaLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<FloridaLucky>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<GermanLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<Lottery>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<LottoMax>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<MegaMillion>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<MegaMillions_MegaBall>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<NewJerseyPick6Lotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<NYLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<NYSweetMillion>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<OregonMegabuck>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<OZLottoMon>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<OZLottoSat>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<OZLottoTue>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<OZLottoWed>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<PowerBall>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<PowerBall_PowerBall>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<SevenLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<SSQ>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<SSQ_Blue>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<SuperLotto>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<SuperLotto_Rear>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<NewYorkTake5>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            modelBuilder.Entity<TexasCashFive>()
                .Property(e => e.DrawDate)
                .IsUnicode(false);

            //modelBuilder.Entity<DailyGrand>()
            //    .Property(e => e.DrawDate)
            //    .IsUnicode(false);
        }
    }
}
