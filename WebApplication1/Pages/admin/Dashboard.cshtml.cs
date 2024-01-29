using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MotelManagement.Data.Models;
using Newtonsoft.Json;

namespace MotelManagement.Pages.admin
{
    public class DashboardModel : PageModel
    {
        private readonly MotelManagementContext _context;

        public DashboardModel(MotelManagementContext context)
        {
            _context = context;
        }
        public class BarChartData
        {
            public string RoomTypeName { get; set; }
            public decimal TotalRevenue { get; set; }
        }

        public class ChartData
        {
            public string Label { get; set; } // Nhãn của dữ liệu
            public double Percentage { get; set; } // Phần trăm
            public int Count { get; set; } // Số lượng
        }

        public List<BarChartData> BarChartDatas { get; set; }
        public List<ChartData> PieChartDatas { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            BarChartDatas = await _context.Bills
                .Include(b => b.Room) // Kết hợp thông tin từ bảng Room
                 .ThenInclude(r => r.RoomType) // Kết hợp thông tin từ bảng RoomType
                .GroupBy(b => b.Room.RoomType.Name) // Nhóm theo tên của loại phòng
                .Select(group => new BarChartData
                {
                    RoomTypeName = group.Key, // Tên của loại phòng
                    TotalRevenue = group.Sum(b => b.RoomBill + b.ElectricBill + b.WaterBill + b.ServiceBill) // Tổng doanh số của loại phòng
                })
                .ToListAsync();

            var roomStatusCounts = await _context.Rooms
                .GroupBy(r => r.StatusId)
                .Select(group => new
                {
                    StatusId = group.Key,
                    Count = group.Count()
                })
                .ToListAsync();

            int totalRooms = roomStatusCounts.Sum(x => x.Count);

            var pieChartData = new List<ChartData>();
            foreach (var statusCount in roomStatusCounts)
            {
                string statusName = "";
                switch (statusCount.StatusId)
                {
                    case 1:
                        statusName = "Đã thuê";
                        break;
                    case 2:
                        statusName = "Còn trống";
                        break;
                    case 3:
                        statusName = "Cần Pass";
                        break;
                        // Thêm các trạng thái khác nếu cần
                }

                // Tính phần trăm và số lượng
                double percentage = (double)statusCount.Count / totalRooms * 100;

                pieChartData.Add(new ChartData
                {
                    Label = statusName,
                    Percentage = Math.Round(percentage, 2), // Làm tròn phần trăm đến 2 chữ số thập phân
                    Count = statusCount.Count
                });

                PieChartDatas = pieChartData;
            }

            return Page();
        }
    }
}
