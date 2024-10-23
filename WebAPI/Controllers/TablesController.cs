using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Request;
using WebAPI.Models.Response;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private ApplicationDbContext _context;
        public TablesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Area")]
        public IActionResult GetAllArea()
        {
            return Ok(_context.KhuVucs.Select(x => new AreaResponseModel
            {
                IdKhuVuc = x.IdKhuVuc,
                TenKhuVuc = x.TenKhuVuc,
                MoTa = x.MoTa,
                TrangThai = x.TrangThai,
                DsBan = _context.Bans.Where(b => b.IdKhuVuc == x.IdKhuVuc).Select(b => new TableResponseModel
                {
                    IdBan = b.IdBan,
                    SoBan = b.SoBan,
                    SoChoNgoi = b.SoChoNgoi,
                    TrangThai = b.TrangThai
                }).ToList()
            }).ToList());
        }
        [HttpGet("Area/{TenKhuVuc}")]
        public IActionResult GetAreaByName(string TenKhuVuc)
        {
            return Ok(_context.KhuVucs.Where(x=>x.TenKhuVuc.ToLower().Contains(TenKhuVuc.ToLower())).Select(x => new AreaResponseModel
            {
                IdKhuVuc = x.IdKhuVuc,
                TenKhuVuc = x.TenKhuVuc,
                MoTa = x.MoTa,
                TrangThai = x.TrangThai,
                DsBan = _context.Bans.Where(b => b.IdKhuVuc == x.IdKhuVuc).Select(b => new TableResponseModel
                {
                    IdBan = b.IdBan,
                    SoBan = b.SoBan,
                    SoChoNgoi = b.SoChoNgoi,
                    TrangThai = b.TrangThai
                }).ToList()
            }).ToList());
        }
        [HttpGet("Table")]
        public IActionResult GetAllTable()
        {
            return Ok(_context.Bans.Select(x=>new TableResponseModel
            {
                IdBan=x.IdBan,
                SoBan=x.SoBan,
                SoChoNgoi=x.SoChoNgoi,
                TrangThai=x.TrangThai
            }).ToList());
        }
        [HttpGet("Table/{TrangThai:bool}")]
        public IActionResult GetTableByStatus(bool TrangThai)
        {
            return Ok(_context.Bans.Where(x=>x.TrangThai==TrangThai).Select(x => new TableResponseModel
            {
                IdBan = x.IdBan,
                SoBan = x.SoBan,
                SoChoNgoi = x.SoChoNgoi,
                TrangThai = x.TrangThai
            }).ToList());
        }

        [HttpGet("Table/{SoChoNgoi:int}")]
        public IActionResult GetTableByNum(int SoChoNgoi)
        {
            return Ok(_context.Bans.Where(x => x.SoChoNgoi == SoChoNgoi).Select(x => new TableResponseModel
            {
                IdBan = x.IdBan,
                SoBan = x.SoBan,
                SoChoNgoi = x.SoChoNgoi,
                TrangThai = x.TrangThai
            }).ToList());
        }

        [HttpGet("Table/{SoBan}")]
        public IActionResult GetTableByTableNum(string SoBan)
        {
            return Ok(_context.Bans.Where(x => x.SoBan==SoBan).Select(x => new TableResponseModel
            {
                IdBan = x.IdBan,
                SoBan = x.SoBan,
                SoChoNgoi = x.SoChoNgoi,
                TrangThai = x.TrangThai
            }).ToList());
        }
        [HttpPost("CreateTable")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult CreateTable(TableRequestModel model)
        {
            try
            {
                var khuvuc = _context.KhuVucs.FirstOrDefault(b => b.IdKhuVuc == model.IdKhuVuc);
                string soBan = _context.Bans.Where(x => x.IdKhuVuc == model.IdKhuVuc).OrderByDescending(x => x.SoBan).FirstOrDefault().SoBan??"0000";
                if (khuvuc == null)
                {
                    return NotFound(new { Message = "Khu vực không tồn tại" });
                }

                var newItem = new Ban
                {
                    IdBan = Guid.NewGuid(),
                    IdKhuVuc = model.IdKhuVuc,
                    SoBan=(int.Parse(soBan)+1).ToString(),
                    NgayTao=DateTime.Now,
                    ThoiGianDatBan=DateTime.Now,
                    ThoiGianTraBan= DateTime.Now,
                    TrangThai=true,
                    SoChoNgoi=model.SoChoNgoi
                };
                _context.Bans.Add(newItem);
                _context.SaveChanges();
                return Ok(new { Message = "Tạo bàn thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpPost("CreateArea")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult CreateArea(AreaRequestModel model)
        {
            try
            {
                var khuvuc = _context.KhuVucs.FirstOrDefault(b => b.TenKhuVuc.ToLower().Contains(model.TenKhuVuc.ToLower()));
                
                if (khuvuc != null)
                {
                    return NotFound(new { Message = "Tên khu vực đã tồn tại" });
                }

                var newItem = new KhuVuc
                {
                    IdKhuVuc = Guid.NewGuid(),
                    TenKhuVuc = model.TenKhuVuc,
                    MoTa = model.MoTa,
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now,
                    TrangThai = true
                };
                _context.KhuVucs.Add(newItem);
                _context.SaveChanges();
                return Ok(new { Message = "Tạo  khu vực thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpPut("UpdateTable/{IdBan:guid}")]
        [Authorize(Roles = "Administrator,Management,User")]
        public IActionResult UpdateTableStatus(Guid IdBan, bool status)
        {
            try
            {
                var table = _context.Bans.FirstOrDefault(b => b.IdBan == IdBan);
                if(table == null)
                {
                    return NotFound(new { Message = "Bàn không tồn tại" });
                }
                table.TrangThai = status;
                _context.Bans.Update(table);
                _context.SaveChanges();
                return Ok(new { Message = "Cập nhật trạng thái bàn thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpPut("UpdateArea/{IdKhuVuc:guid}")]
        [Authorize(Roles = "Administrator,Management,User")]
        public IActionResult UpdateAreaStatus(Guid IdKhuVuc, bool status)
        {
            try
            {
                var khuvuc = _context.KhuVucs.FirstOrDefault(b => b.IdKhuVuc == IdKhuVuc);
                if (khuvuc == null)
                {
                    return NotFound(new { Message = "Khu vực không tồn tại" });
                }
                khuvuc.TrangThai = status;
                _context.KhuVucs.Update(khuvuc);
                var dsBan = _context.Bans.Where(x => x.IdKhuVuc == IdKhuVuc).ToList();
                if(dsBan.Count > 0)
                {
                    foreach(var item in dsBan)
                    {
                        item.TrangThai = status;
                        _context.Bans.Update(item);
                    }
                }
                _context.SaveChanges();
                return Ok(new { Message = "Cập nhật trạng thái khu vực và bàn thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        
        [HttpDelete("DeleteArea/{IdKhuVuc:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult  DeleteArea(Guid IdKhuVuc)
        {
            try
            {
                var khuvuc = _context.KhuVucs.FirstOrDefault(b => b.IdKhuVuc == IdKhuVuc);
                if (khuvuc == null)
                {
                    return NotFound(new { Message = "Khu vực không tồn tại" });
                }
                var dsBan = _context.Bans.Where(x => x.IdKhuVuc == IdKhuVuc).ToList();
                if(dsBan.Count > 0)
                {
                    _context.Bans.RemoveRange(dsBan);
                    _context.SaveChanges();
                }
                _context.KhuVucs.Remove(khuvuc);
                _context.SaveChanges();
                return Ok(new { Message = "Xóa khu vực và bàn thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }
        [HttpDelete("DeleteTable/{IdBan:guid}")]
        [Authorize(Roles = "Administrator,Management")]
        public IActionResult  DeleteTable(Guid IdBan)
        {
            try
            {
                var ban = _context.Bans.FirstOrDefault(b => b.IdBan == IdBan);
                if (ban == null)
                {
                    return NotFound(new { Message = "Bàn không tồn tại" });
                }
                
                _context.Bans.Remove(ban);
                _context.SaveChanges();
                return Ok(new { Message = "Xóa bàn thành công" });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = $"Lỗi: {e.Message}" });
            }
        }


    }
}
