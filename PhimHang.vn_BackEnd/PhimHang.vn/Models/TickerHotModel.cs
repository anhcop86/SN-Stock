using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhimHang.Models
{
    public class TickerHotModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Mã cổ phiếu")]
        [StringLength(16, ErrorMessage = "Tên đăng nhập từ 3 đến 16 ký tự", MinimumLength = 3)]
        public string THName { get; set; }
    }
}