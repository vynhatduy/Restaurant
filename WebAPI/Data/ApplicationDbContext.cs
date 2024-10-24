using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<PhanQuyen> PhanQuyens { get; set; }
    public DbSet<DanhMuc> DanhMucs { get; set; }
    public DbSet<Loai> Loais { get; set; }
    public DbSet<NhanVien> NhanViens { get; set; }
    public DbSet<SanPham> SanPhams { get; set; }
    public DbSet<KhuVuc> KhuVucs { get; set; }
    public DbSet<KhachHang> KhachHangs { get; set; }
    public DbSet<Ban> Bans { get; set; }
    public DbSet<HoaDon> HoaDons { get; set; }
    public DbSet<DanhGia> DanhGias { get; set; }
    public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Combo> Combos { get; set; }
    public DbSet<ChiTietCombo> ChiTietCombos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Loai>()
            .HasOne(l => l.DanhMuc)
            .WithMany(dm => dm.Loais)
            .HasForeignKey(l => l.IdDanhMuc);

        modelBuilder.Entity<SanPham>()
            .HasOne(sp => sp.Loai)
            .WithMany(l => l.SanPhams)
            .HasForeignKey(sp => sp.IdLoai);

        modelBuilder.Entity<NhanVien>()
            .HasOne(nv => nv.PhanQuyen)
            .WithMany(pq => pq.NhanViens)
            .HasForeignKey(nv => nv.IdQuyen);

        modelBuilder.Entity<HoaDon>()
            .HasOne(hd => hd.KhachHang)
            .WithMany(kh => kh.HoaDons)
            .HasForeignKey(hd => hd.IdKhachHang);

        modelBuilder.Entity<HoaDon>()
            .HasOne(hd => hd.NhanVien)
            .WithMany(nv => nv.HoasDons)
            .HasForeignKey(hd => hd.IdNhanVien);

        modelBuilder.Entity<HoaDon>()
            .HasOne(hd => hd.Ban)
            .WithMany(b => b.HoaDons)
            .HasForeignKey(hd => hd.IdBan);

        modelBuilder.Entity<ChiTietHoaDon>()
            .HasOne(cthd => cthd.HoaDon)
            .WithMany(hd => hd.ChiTietHoaDons)
            .HasForeignKey(cthd => cthd.IdHoaDon);

        modelBuilder.Entity<ChiTietHoaDon>()
            .HasOne(cthd => cthd.SanPham)
            .WithMany(sp => sp.ChiTietHoaDons)
            .HasForeignKey(cthd => cthd.IdSanPham);

        modelBuilder.Entity<DanhGia>()
            .HasOne(dg => dg.KhachHang)
            .WithMany(kh => kh.DanhGias)
            .HasForeignKey(dg => dg.IdKhachHang);

        modelBuilder.Entity<DanhGia>()
            .HasOne(dg => dg.SanPham)
            .WithMany(sp => sp.DanhGias)
            .HasForeignKey(dg => dg.IdSanPham);

        modelBuilder.Entity<ChiTietCombo>()
            .HasOne(ctc => ctc.Combo)
            .WithMany(c => c.ChiTietCombos)
            .HasForeignKey(ctc => ctc.IdCombo);

        modelBuilder.Entity<ChiTietCombo>()
            .HasOne(ctc => ctc.SanPham)
            .WithMany(sp => sp.ChiTietCombos)
            .HasForeignKey(ctc => ctc.IdSanPham);
        base.OnModelCreating(modelBuilder);
    }
}
