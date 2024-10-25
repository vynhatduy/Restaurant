using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class DatabaseInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {


        if (!context.Banners.Any())
        {
            AddBanner(context);
        }
        if (!context.PhanQuyens.Any())
        {
            AddPhanQuyen(context);
        }
        if (!context.DanhMucs.Any())
        {
            AddDanhMuc(context);
        }
        if (!context.Loais.Any())
        {
            AddLoai(context);
        }
        if (!context.NhanViens.Any())
        {
            AddNhanVien(context);
        }
        if (!context.KhuVucs.Any())
        {
            AddKhuVuc(context);
        }
        if (!context.KhachHangs.Any())
        {
            AddKhachHang(context);
        }
        if (!context.SanPhams.Any())
        {
            AddSanPham(context);
        }
        if (!context.HoaDons.Any())
        {
            AddHoaDon(context);
        }
        if (!context.DanhGias.Any())
        {
            AddDanhGia(context);
        }
        if (!context.ChiTietHoaDons.Any())
        {
            AddChiTietHoaDon(context);
        }


    }
    public static void AddBanner(ApplicationDbContext context)
    {
        context.Banners.AddRange(
            new Banner
            {
                IdBanner = Guid.NewGuid(),
                HinhAnh = "Banner_1",
                Link = "#",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                TrangThai = true
            },
            new Banner
            {
                IdBanner = Guid.NewGuid(),
                HinhAnh = "Banner_2",
                Link = "#",
                NgayTao = DateTime.Now,
                NgayCapNhat = DateTime.Now,
                TrangThai = true
            }
            );
        context.SaveChanges();
    }
    public static void AddPhanQuyen(ApplicationDbContext context)
    {
        context.PhanQuyens.AddRange(
                new PhanQuyen { IdQuyen = Guid.NewGuid(), TenQuyen = "Administrator", MoTa = "Quyền quản trị viên" },
                new PhanQuyen { IdQuyen = Guid.NewGuid(), TenQuyen = "Management", MoTa = "Quyền quản lý" },
                new PhanQuyen { IdQuyen = Guid.NewGuid(), TenQuyen = "User", MoTa = "Quyền người dùng" }
            );
        context.SaveChanges();
    }
    public static void AddDanhMuc(ApplicationDbContext context)
    {
        context.DanhMucs.AddRange(
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Khai vị", MoTa = "Món khai vị", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Món chính", MoTa = "Món chính", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Cơm - Mì - Cháo", MoTa = "Các món bao gồm cơm, mì, cháo", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Đồ uống", MoTa = "Đồ uống đóng chai và dùng trong ngày", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Bánh và tráng miệng", MoTa = "Các loại bánh và tráng miệng", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        context.SaveChanges();
    }
    public static void AddLoai(ApplicationDbContext context)
    {
        var khaiVi = context.DanhMucs.FirstOrDefault(l => l.TenDanhMuc == "Khai vị") ?? new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Khai vị", MoTa = "Món khai vị", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now };
        var monChinh = context.DanhMucs.FirstOrDefault(l => l.TenDanhMuc == "Món chính") ?? new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Món chính", MoTa = "Món chính", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now };
        var comMiChao = context.DanhMucs.FirstOrDefault(l => l.TenDanhMuc == "Cơm - Mì - Cháo") ?? new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Cơm - Mì - Cháo", MoTa = "Các món bao gồm cơm, mì, cháo", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now };
        var doUong = context.DanhMucs.FirstOrDefault(l => l.TenDanhMuc == "Đồ uống") ?? new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Đồ uống", MoTa = "Đồ uống đóng chai và dùng trong ngày", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now };
        var banhVaTrangMieng = context.DanhMucs.FirstOrDefault(l => l.TenDanhMuc == "Bánh và tráng miệng") ?? new DanhMuc { IdDanhMuc = Guid.NewGuid(), TenDanhMuc = "Bánh và tráng miệng", MoTa = "Các loại bánh và tráng miệng", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now };

        // Kiểm tra nếu danh mục chưa có trong context, thì thêm mới
        if (context.DanhMucs.All(l => l.TenDanhMuc != "Khai vị")) context.DanhMucs.Add(khaiVi);
        if (context.DanhMucs.All(l => l.TenDanhMuc != "Món chính")) context.DanhMucs.Add(monChinh);
        if (context.DanhMucs.All(l => l.TenDanhMuc != "Cơm - Mì - Cháo")) context.DanhMucs.Add(comMiChao);
        if (context.DanhMucs.All(l => l.TenDanhMuc != "Đồ uống")) context.DanhMucs.Add(doUong);
        if (context.DanhMucs.All(l => l.TenDanhMuc != "Bánh và tráng miệng")) context.DanhMucs.Add(banhVaTrangMieng);

        context.Loais.AddRange(
                new Loai
                {
                    IdDanhMuc = khaiVi.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Salad",
                    MoTa = "Salad rau củ, bò",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = khaiVi.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Nem",
                    MoTa = "Nem nướng",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = khaiVi.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Khác",
                    MoTa = "Các món khai vị khác",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = monChinh.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Gà",
                    MoTa = "Các món gà",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },

                new Loai
                {
                    IdDanhMuc = monChinh.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Heo",
                    MoTa = "Các món heo",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = monChinh.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Mì",
                    MoTa = "Các món mì",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = monChinh.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Nem",
                    MoTa = "Các món nem",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },

                new Loai
                {
                    IdDanhMuc = comMiChao.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Cơm",
                    MoTa = "Các món cơm",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = comMiChao.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Mì",
                    MoTa = "Các món mì",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = comMiChao.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Cháo",
                    MoTa = "Các món cháo",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },

                new Loai
                {
                    IdDanhMuc = doUong.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Đóng chai",
                    MoTa = "Nước giải khát đóng chai",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },

                new Loai
                {
                    IdDanhMuc = doUong.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Dùng trong ngày",
                    MoTa = "Nước giải khát dùng trong ngày",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },

                new Loai
                {
                    IdDanhMuc = banhVaTrangMieng.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Bánh",
                    MoTa = "Các món bánh",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                },
                new Loai
                {
                    IdDanhMuc = banhVaTrangMieng.IdDanhMuc,
                    IdLoai = Guid.NewGuid(),
                    TenLoai = "Tráng miệng",
                    MoTa = "Các món tráng miệng",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                }

            );
        context.SaveChanges();
    }
    public static void AddNhanVien(ApplicationDbContext context)
    {
        var adminRole = context.PhanQuyens.FirstOrDefault(p => p.TenQuyen == "Administrator");
        var managerRole = context.PhanQuyens.FirstOrDefault(p => p.TenQuyen == "Management");
        var userRole = context.PhanQuyens.FirstOrDefault(p => p.TenQuyen == "User");

        var hasher1 = HasherPassword.HasherPass("admin");
        var hasher2 = HasherPassword.HasherPass("manager");
        var hasher3 = HasherPassword.HasherPass("user");

        var salt1 = hasher1.Salt;
        var pass1 = hasher1.Hash;
        var salt2 = hasher2.Salt;
        var pass2 = hasher2.Hash;
        var salt3 = hasher3.Salt;
        var pass3 = hasher3.Hash;

        if (adminRole != null && userRole != null)
        {
            context.NhanViens.AddRange(
                new NhanVien { IdNhanVien = Guid.NewGuid(), Username = "admin", PasswordHash = pass1, Salt = salt1, SDT = "0123456789", HoTen = "Nguyễn Văn A", NgayTao = DateTime.Now, IdQuyen = adminRole.IdQuyen },
                new NhanVien { IdNhanVien = Guid.NewGuid(), Username = "manager", PasswordHash = pass2, Salt = salt2, SDT = "0987654321", HoTen = "Trần Thị B", NgayTao = DateTime.Now, IdQuyen = managerRole.IdQuyen },
                new NhanVien { IdNhanVien = Guid.NewGuid(), Username = "user", PasswordHash = pass3, Salt = salt3, SDT = "0987654322", HoTen = "Trần Van C", NgayTao = DateTime.Now, IdQuyen = userRole.IdQuyen }
            );
        }
        context.SaveChanges();
    }
    public static void AddKhuVuc(ApplicationDbContext context)
    {
        context.KhuVucs.AddRange(
                new KhuVuc { IdKhuVuc = Guid.NewGuid(), TenKhuVuc = "Khu A", MoTa = "Khu vực chính", TrangThai = true, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now },
                new KhuVuc { IdKhuVuc = Guid.NewGuid(), TenKhuVuc = "Khu B", MoTa = "Khu vực phụ", TrangThai = true, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        context.SaveChanges();
    }
    public static void AddKhachHang(ApplicationDbContext context)
    {
        context.KhachHangs.AddRange(
                new KhachHang { IdKhachHang = Guid.NewGuid(), Username = "khach1", PasswordHash = "hashed_password_3", Salt = "salt_password_3", SDT = "0123456780", HoTen = "Lê Văn C", NgayTao = DateTime.Now, DiaChi = "Hà Nội", GioiTinh = true },
                new KhachHang { IdKhachHang = Guid.NewGuid(), Username = "khach2", PasswordHash = "hashed_password_4", Salt = "salt_password_4", SDT = "0987654310", HoTen = "Phạm Thị D", NgayTao = DateTime.Now, DiaChi = "Đà Nẵng", GioiTinh = false }
            );
        context.SaveChanges();
    }
    private static void AddSanPham(ApplicationDbContext context)
    {
        var loaiSalad = context.Loais.FirstOrDefault(l => l.TenLoai == "Salad");
        var loaiKhac = context.Loais.FirstOrDefault(l => l.TenLoai == "Khác");
        var loaiNem = context.Loais.FirstOrDefault(l => l.TenLoai == "Nem");
        var loaiGa = context.Loais.FirstOrDefault(l => l.TenLoai == "Gà");
        var loaiHeo = context.Loais.FirstOrDefault(l => l.TenLoai == "Heo");
        var loaiMi = context.Loais.FirstOrDefault(l => l.TenLoai == "Mì");

        var newGuid = Guid.NewGuid();

        var sanPhams = new List<SanPham>
        {
             new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Salad rau mùa sốt cam TASTY",
                MoTa = "Xà lách carol, xà lách frise, xà lách lô lô tím, xà lách mỡ, xà lách radicchio tím, táo đỏ, táo xanh, cà chua bi, củ cải đường, rau mầm, cà rốt baby, trái olive đen, trái olive xanh.",
                MoTaChiTiet = "Salad rau mùa sốt cam TASTY là sự lựa chọn tuyệt vời cho các tín đồ yêu eat clean. Món ăn có đến 5 loại xà lách (carol, frise, lô lô tím, xà lách mỡ và radicchio tím) kết hợp cùng các loại trái cây như táo, cà chua, ô liu... mang lại nguồn vitamin tổng hợp dồi dào, hỗ trợ tăng cường đề kháng cho cơ thể. Điểm nhấn tạo nên nét chấm phá cho món nằm ở nước sốt cam độc đáo với vị chua ngọt tự nhiên dịu dàng. Salad rau mùa sốt cam TASTY thực sự là một bữa tiệc về màu sắc, xua tan cơn nóng mùa hè, đánh thức tối đa vị giác.",
                HinhAnh = "https://img.tastykitchen.vn/2022/03/28/salad-sot-cam-6818.jpg",
                TrangThai = true,
                DonGia = 69000,
                SoSao = 5,
                IdLoai = loaiSalad?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Salad rau mùa sốt mác mác",
                MoTa = "Táo đỏ, táo xanh, củ dền, cà rốt, xà lách lolo, xà lách carron, chanh dây, dầu oliu, rau quế, mayonaise,...",
                MoTaChiTiet = "Salad rau mùa sốt mác mác được lựa chọn từ những loại rau củ ẩm thực phương Tây như xà lách lolo, xà lách carron, dầu oliu, kết hợp với hương đồng cỏ nội trong văn hoá ẩm thực Việt Nam là củ dền, táo đỏ, táo xanh, chanh dây và rau quế. Tất cả được hòa quyện dưới lớp sốt mác mác rau mùi được cấu thành bởi 3 thành phần chính là chanh dây, rau mùi và mayonaise, đem đến hương vị độc đáo, giàu vitamin C và chất xơ.",
                HinhAnh = "https://img.tastykitchen.vn/2021/06/03/18-salad-rau-mua-3-fea4.jpg",
                TrangThai = true,
                DonGia = 69000,
                SoSao = 5,
                IdLoai = loaiSalad?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Phở cuốn TASTY",
                MoTa = "Nạc vai bò Úc, bánh ướt, húng lủi, húng quế, ngò gai, giá sống, cà chua, hành phi, đậu phộng, nước mắm, đường cát Biên Hòa, giấm nuôi, tỏi lột, mè trắng, bột thịt gà, tiêu đen",
                MoTaChiTiet = "Phở cuốn TASTY là món ăn được các đầu bếp TASTY Kitchen dành nhiều thời gian dày công chế biến. Với bánh phở tạo ra từ hạt gạo ngâm suốt 12 tiếng liền, sau đó xay và tráng cách thủy mang đến miếng bánh ướt mỏng, dai dai hoàn toàn tự nhiên. Thêm vào đó là sự kết hợp hài hòa cùng nguyên liệu bò Úc thượng hạng tẩm ướp đậm vị và các loại rau thơm nhiệt đới. Khi thưởng thức kèm nước sốt chấm được pha chế đặc biệt mang đến trải nghiệm ẩm thực tuyệt hảo, đầy thú vị.",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2021/01/25/pho-cuon-hn-1280x1000-7020.jpg",
                TrangThai = true,
                DonGia = 83000,
                SoSao = 5,
                IdLoai = loaiKhac?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Salad bò Nam Bộ",
                MoTa = "Thăn bò, húng quế, ngò gai, rau càng cua, lá cóc, lá quế vị, xà lách lô lô xanh, tắc, khế, cà pháo, hành tím, sả, ớt sừng, mè trắng, lá chanh thái",
                MoTaChiTiet = "Salad bò Nam Bộ tại TASTY Kitchen là món ăn vô cùng đặc sắc với sự kết hợp tinh tế của nhiều nguyên liệu làm khơi dậy vị giác của thực khách. Những phần bò fillet cung cấp lượng protein cần thiết cho sức khỏe cùng những loại rau dân dã và mộc mạc như càng cua, lá quế tươi mát. Bên cạnh đó còn có vị chua của nhiều loại trái cây giàu vitamin C như khế chua, tắc được cân bằng bởi sự ngọt dịu của sốt salad đã tạo nên một món ăn vô cùng hấp dẫn.",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2020/11/04/salad-bo-1280x1000-4dbe.jpg",
                TrangThai = true,
                DonGia = 127000,
                SoSao = 5,
                IdLoai = loaiSalad?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Sụn gà xóc muối Tây Ninh",
                MoTa = "Sụn gà, muối Tây Ninh, trứng gà, sả, nghệ, lá chanh, ớt sừng, hành phi, tỏi phi, tôm khô, chà bông heo, bột chiên xù",
                MoTaChiTiet = "Món sụn gà xóc muối Tây Ninh là một món ăn vặt hoàn hảo với độ giòn từ lớp bột bên ngoài và độ dai dai từ sụn gà bên trong. Các đầu bếp TASTY Kitchen đã sáng tạo khéo léo khi kết hợp muối ớt Tây Ninh và các gia vị hấp dẫn giúp tạo nên một món ăn mới lạ với mùi thơm cùng hương vị đậm đà. Món ăn được gói gọn trong một chiếc tổ chim làm bằng sả chiên, không chỉ đẹp mắt mà thực khách có thể thưởng thức độ giòn thơm, vị vừa ăn.",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2020/11/17/2-d84e.jpg",
                TrangThai = true,
                DonGia = 137000,
                SoSao = 5,
                IdLoai = loaiKhac?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Nem lụi nướng mía",
                MoTa = "Mỡ gáy, thịt nạc mông, giò sống heo, mía cây, màu thực phẩm, chất tạo độ dai thực phẩm, bột nở, bột bắp, tiêu đen, tiêu sọ trắng, sả cây, hành tím, tỏi, mật ong, mắm khô, bột ngũ vị hương, bột ngọt, đường cát",
                MoTaChiTiet = "Nem lụi được biết đến là đặc sản của vùng đất kinh kỳ đồng thời là lựa chọn mà mọi tín đồ yêu thích ẩm thực không thể bỏ qua. Món ăn hấp dẫn ngay từ cái nhìn đầu tiên với màu sắc vàng ươm cùng mùi vị thơm lừng sau khi được nướng lên. Thực khách sẽ cảm nhận trọn vẹn vị đậm đà pha chút mềm dai của thịt heo, giò sống hài hòa với các gia vị đặc biệt. Thêm vào đó, Nem lụi TASTY còn ngon hơn khi dùng kèm bánh tráng, bún tươi, rau sống và nước chấm sền sệt, vị bùi ngậy do chính các đầu bếp TASTY sáng tạo.",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2021/11/02/nem-lui-434c.jpg",
                TrangThai = true,
                DonGia = 160000,
                SoSao = 5,
                IdLoai = loaiNem?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Gà cuốn lá dứa",
                MoTa = "Đùi gà, lá dứa, xà lách lô lô xanh, xà lách lô lô tím, cà chua bi, hành tây tím, đường cát, tiêu sọ trắng, bột bắp, bột chiên giòn, bột năng, dầu mè, tỏi xay, ngò rí, nước tương, dầu ăn",
                MoTaChiTiet = "Gà cuốn lá dứa là món ăn mang phong vị ẩm thực Thái Lan, đã được các đầu bếp TASTY Kitchen biến tấu mang đầy mới mẻ và phù hợp với khẩu vị người Việt. Thịt gà lóc xương, giữ nguyên da cắt miếng vừa ăn tẩm ướp suốt hơn 3 tiếng cùng các gia vị đặc trưng của Việt Nam như tỏi, dầu hào, điều,...cân chỉnh với tỷ lệ thích hợp. Thêm điểm nhấn khi kết hợp mùi thơm tự nhiên của lá dứa được trồng tại Đà Lạt cuốn cẩn thận với gà và chiên giòn hấp dẫn. Không chỉ dễ dàng chiêu đãi vị giác món ăn còn mang lại giá trị dinh dưỡng cao, rất tốt cho tim mạch.",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2021/03/04/ga-cuon-la-dua-1280x1000-628f.jpg",
                TrangThai = true,
                DonGia = 170000,
                SoSao = 5,
                IdLoai = loaiGa?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Ba rọi chiên mắm ngò",
                MoTa = "Thịt ba rọi rút sườn Ba Lan, bột chiên giòn, gạo thái, ngò rí, tỏi củ, sả cây, tắc, ớt sừng, dưa leo, ngò rí, ớt hiểm, thơm gọt, húng quế, húng lủi, lô lô xanh, lô lô tím, dầu ăn, đường cát, tương ớt, nước mắm, giấm gạo",
                MoTaChiTiet = "Ba rọi chiên mắm ngò là sự sáng tạo được khơi nguồn từ món ba rọi chiên mắm vốn đã quen thuộc trong bữa cơm người Việt, giúp nâng tầm tinh túy ẩm thực dân gian và mang lại trải nghiệm hoàn toàn mới cho thực khách. Bằng cách xử lý tinh tế, miếng thịt ba rọi chiên giòn kết hợp cùng sốt nước mắm thơm thêm chút vị chua thanh nhẹ của tắc tươi, phảng phất mùi ngò rí tạo nên sự cân bằng tuyệt hảo. Món ăn sẽ tuyệt vời hơn khi thưởng thức kèm rau thơm và kim chi hấp dẫn.",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2021/01/25/ba-roi-mam-ngo-1280x1000-9503.jpg",
                TrangThai = true,
                DonGia = 147000,
                SoSao = 5,
                IdLoai = loaiHeo?.IdLoai ?? newGuid
            },
            new SanPham
            {
                IdSanPham = Guid.NewGuid(),
                TenSanPham = "Mì spaghetti sốt kem nấm",
                MoTa = "Mì spaghetti, ba rọi xông khói, nấm hương tươi, nấm đùi gà, hành tây, tỏi, trứng gà, sữa tươi, dầu ô-liu, kem nấu, phô mai Parmesan, ngò tây, đường cát, muối Thái...",
                MoTaChiTiet = "",
                HinhAnh = "https://img.tastykitchen.vn/crop/820x642/2021/11/09/my-y-kem-nam-4bd1.jpg",
                TrangThai = true,
                DonGia = 92000,
                SoSao = 5,
                IdLoai = loaiMi?.IdLoai ?? newGuid
            }

        };

        context.SanPhams.AddRange(sanPhams);
        context.SaveChanges();
    }

    public static void AddHoaDon(ApplicationDbContext context)
    {
        var firstNhanVien = context.NhanViens.FirstOrDefault();
        var firstKhachHang = context.KhachHangs.FirstOrDefault();
        if (firstNhanVien != null && firstKhachHang != null)
        {
            context.HoaDons.AddRange(
                new HoaDon { IdHoaDon = Guid.NewGuid(), IdNhanVien = firstNhanVien.IdNhanVien, IdBan = null, IdKhachHang = firstKhachHang.IdKhachHang, NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now, ChietKhau = null, Coupon = null, TongHoaDon = 50000, ThanhTien = 50000, TrangThai = true, DaThanhToan = true, PhuongThucThanhToan = "Tiền mặt" }
            );
        }
        context.SaveChanges();
    }
    public static void AddDanhGia(ApplicationDbContext context)
    {
        var firstKhachHang = context.KhachHangs.FirstOrDefault();
        var firstSanPham = context.SanPhams.FirstOrDefault();
        if (firstKhachHang != null && firstSanPham != null)
        {
            context.DanhGias.AddRange(
                new DanhGia { IdDanhGia = Guid.NewGuid(), IdKhachHang = firstKhachHang.IdKhachHang, IdSanPham = firstSanPham.IdSanPham, MoTa = "Tuyệt vời, Sản phẩm rất tốt", SoSao = 5, HinhAnh = "image_url_review", NgayTao = DateTime.Now, NgayCapNhat = DateTime.Now }
            );
        }
        context.SaveChanges();
    }
    public static void AddChiTietHoaDon(ApplicationDbContext context)
    {
        var firstHoaDon = context.HoaDons.FirstOrDefault();
        var firstSanPham = context.SanPhams.FirstOrDefault();
        if (firstHoaDon != null && firstSanPham != null)
        {
            context.ChiTietHoaDons.AddRange(
                new ChiTietHoaDon { IdChiTietHoaDon = Guid.NewGuid(), IdHoaDon = firstHoaDon.IdHoaDon, IdSanPham = firstSanPham.IdSanPham, SoLuong = 2, GiaBan = firstSanPham.DonGia, ThanhTien = firstSanPham.DonGia * 2 }
            );
        }
        context.SaveChanges();
    }
}
