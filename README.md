# Hệ Thống Điểm Danh Bằng Mã QR (QR Attendance System)

Đây là một ứng dụng Desktop được xây dựng bằng *C# WPF (Windows Presentation Foundation)* kết hợp với cơ sở dữ liệu *SQL Server*. Hệ thống giúp tự động hóa quá trình điểm danh sinh viên thông qua mã QR, giúp giảng viên tiết kiệm thời gian và quản lý lớp học hiệu quả hơn.

## 🚀 Tính năng chính

- *Đăng nhập hệ thống:* Dành cho giảng viên / quản trị viên.
- *Quản lý thông tin:* Quản lý sinh viên, giảng viên, lớp học phần và các buổi học.
- *Tạo và gửi mã QR:* Tính năng tạo mã QR cho từng buổi học để sinh viên quét (PageSendQr).
- *Điểm danh tự động:* Ghi nhận trực tiếp trạng thái điểm danh của sinh viên vào cơ sở dữ liệu thông qua mã QR.
- *Thống kê & Báo cáo:* Xem danh sách lớp và trạng thái điểm danh của từng sinh viên trong các buổi học.

## 🛠️ Công nghệ sử dụng

- *Ngôn ngữ lập trình:* C#
- *Giao diện người dùng (UI):* WPF (Windows Presentation Foundation)
- *Cơ sở dữ liệu:* Microsoft SQL Server
- *Mô hình kiến trúc:* 3-layer architecture (DAO - Service - UI)

## 📂 Cấu trúc dự án

- /BackEnd/Model/: Chứa các lớp thực thể (SinhVien, GiangVien, LopHocPhan, DiemDanh, BuoiHoc).
- /BackEnd/Dao/: Xử lý tương tác trực tiếp với cơ sở dữ liệu (Data Access Object).
- /BackEnd/Service/: Chứa các lớp nghiệp vụ (Business Logic) kết nối giữa UI và DAO.
- /UI/: Giao diện ứng dụng gồm các trang XAML (PageHome, PageLogin, PageSendQr,...).
- /Database/: Chứa file script .sql và file backup .bak để khởi tạo cơ sở dữ liệu.
- /Configsystem/: Chứa file cấu hình (như App.config).

## ⚙️ Hướng dẫn cài đặt và chạy dự án

### Yêu cầu hệ thống
- Visual Studio (Khuyến nghị bản 2019 hoặc 2022) có cài đặt workload *.NET Desktop Development*.
- Microsoft SQL Server & SQL Server Management Studio (SSMS).

### Các bước cài đặt

*1. Thiết lập Cơ sở dữ liệu (Database):*
- Mở SQL Server Management Studio (SSMS).
- Bạn có thể tạo Database bằng 1 trong 2 cách sau:
  - *Cách 1:* Mở file Database/DiemDanhQR.sql và chạy (Execute) toàn bộ script để tạo bảng và dữ liệu mẫu.
  - *Cách 2:* Khôi phục (Restore) từ file backup Configsystem/qr_attendance_system.bak.

*2. Cấu hình chuỗi kết nối (Connection String):*
- Mở dự án bằng Visual Studio (file He_Thong_Diem_Danh_Qr.sln).
- Tìm file App.config (hoặc mở file /BackEnd/Dao/ConnectDB.cs) để cập nhật chuỗi kết nối (ConnectionString) sao cho khớp với tên Server và thông tin đăng nhập SQL Server của máy bạn.

*3. Build và Chạy ứng dụng:*
- Nhấn F5 hoặc nút *Start* trong Visual Studio để biên dịch và chạy ứng dụng.
- Đăng nhập bằng tài khoản giảng viên/admin có sẵn trong database để bắt đầu sử dụng.

## 👥 Tác giả
- [Tên của bạn hoặc tên Nhóm]

## 📝 Giấy phép
- Dự án này phục vụ cho mục đích học tập và tham khảo.