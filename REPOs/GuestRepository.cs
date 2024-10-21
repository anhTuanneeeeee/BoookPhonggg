using BOs.Entity;
using DAOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOs
{
    public class GuestRepository : IGuestRepository
    {
        private readonly IGuestDAO _guestDAO;
        private readonly BookroomSwdContext _context;

        public GuestRepository(IGuestDAO guestDAO, BookroomSwdContext context)
        {
            _guestDAO = guestDAO;
            _context = context;
        }

        public async Task<Guest?> CreateCustomer(string userName, string email, string password, string phoneNumber)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingGuest = await _guestDAO.GetGuestByEmail(email);
            if (existingGuest != null)
            {
                return null; // Email đã tồn tại
            }

            // Tạo Guest mới với role Customer (RoleId = 3)
            var guest = new Guest
            {
                UserName = userName,
                Email = email,
                Password = password, // Nên hash mật khẩu ở đây
                PhoneNumber = phoneNumber,
                RoleId = 3, // Role Customer
                CreateUser = DateTime.Now,
                Status = true // Kích hoạt tài khoản ngay
            };

            return await _guestDAO.CreateGuest(guest);
        }

        public async Task<Guest> Authenticate(string username, string password)
        {
            var guest = await _context.Guests.FirstOrDefaultAsync(g => g.UserName == username);
            if (guest == null || guest.Password != password) // Kiểm tra mật khẩu (nên hash mật khẩu trước khi lưu)
            {
                return null; // Trả về null nếu không tìm thấy người dùng
            }
            return guest; // Trả về người dùng nếu xác thực thành công
        }

        public async Task<IEnumerable<Guest>> GetAllCustomersAsync()
        {
            return await _context.Guests.ToListAsync();
        }

        public async Task<Guest> GetCustomerByIdAsync(int id)
        {
            return await _context.Guests.FindAsync(id);
        }
        public async Task<Guest> CreateStaff(string userName, string email, string password, string phoneNumber)
        {
            var staff = new Guest
            {
                UserName = userName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password), // Hash password here
                PhoneNumber = phoneNumber,
                RoleId = 2 // Giả sử ID của Staff là 2, cập nhật nếu cần
            };
            return await _guestDAO.CreateGuestAsync(staff);
        }
        public async Task<Guest?> GetGuestById(int id)
        {
            return await _guestDAO.GetGuestById(id);
        }
        public async Task<Guest> UpdateGuest(Guest guest)
        {
            return await _guestDAO.UpdateGuest(guest);
        }
        public async Task<bool> DeleteGuest(int id)
        {
            return await _guestDAO.DeleteGuest(id);
        }
    }
}
