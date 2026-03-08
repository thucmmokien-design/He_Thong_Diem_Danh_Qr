--Tao database
CREATE DATABASE DiemDanhQR
GO

USE DiemDanhQR
GO

--Tao bang giang_vien
CREATE TABLE giang_vien (
    gv_id INT IDENTITY(1,1) PRIMARY KEY,
    hoten NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    password NVARCHAR(255) NOT NULL
);
GO

--Tao bang sinh_vien
CREATE TABLE sinh_vien (
    msv NVARCHAR(20) PRIMARY KEY,
    hoten NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    password NVARCHAR(255) NOT NULL
);
GO

--Tao bang lop_hoc_phan
CREATE TABLE lop_hoc_phan (
    class_id NVARCHAR(20) PRIMARY KEY,
    ten_mon NVARCHAR(100) NOT NULL,
    hoc_ky NVARCHAR(20),
    gv_id INT,

    CONSTRAINT FK_lop_giangvien 
    FOREIGN KEY (gv_id) REFERENCES giang_vien(gv_id)
        ON DELETE SET NULL
        ON UPDATE CASCADE
);
GO

--Tao bang buoi_hoc
CREATE TABLE buoi_hoc (
    session_id INT IDENTITY(1,1) PRIMARY KEY,
    class_id NVARCHAR(20),
    ngay_hoc DATE NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    qr_secret NVARCHAR(255),
    is_active BIT DEFAULT 1,

    CONSTRAINT FK_buoi_lop
    FOREIGN KEY (class_id) REFERENCES lop_hoc_phan(class_id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

--Tao bang diem_danh
CREATE TABLE diem_danh (
    id INT IDENTITY(1,1) PRIMARY KEY,
    session_id INT NOT NULL,
    msv NVARCHAR(20) NOT NULL,
    checkin_time DATETIME2 DEFAULT SYSDATETIME(),
    status NVARCHAR(20) DEFAULT 'PRESENT',

    CONSTRAINT FK_diemdanh_buoi
    FOREIGN KEY (session_id) REFERENCES buoi_hoc(session_id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT FK_diemdanh_sinhvien
    FOREIGN KEY (msv) REFERENCES sinh_vien(msv)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

--Insert du lieu bang giang vien
INSERT INTO giang_vien (hoten, email, password)
VALUES 
(N'Nguyễn Văn An', 'an.nguyen@university.edu', '123456'),
(N'Trần Thị Bình', 'binh.tran@university.edu', '123456'),
(N'Lê Văn Cường', 'cuong.le@university.edu', '123456'),
(N'Phạm Thị Dung', 'dung.pham@university.edu', '123456'),
(N'Hoàng Văn Em', 'em.hoang@university.edu', '123456'),
(N'Vũ Thị Hạnh', 'hanh.vu@university.edu', '123456'),
(N'Đặng Văn Khoa', 'khoa.dang@university.edu', '123456'),
(N'Bùi Thị Lan', 'lan.bui@university.edu', '123456'),
(N'Đỗ Văn Minh', 'minh.do@university.edu', '123456'),
(N'Ngô Thị Phương', 'phuong.ngo@university.edu', '123456');

--Insert du lieu bang sinh vien
INSERT INTO sinh_vien (msv, hoten, email, password)
VALUES
('SV001', N'Nguyễn Văn Nam', 'sv001@university.edu', '123456'),
('SV002', N'Trần Văn Huy', 'sv002@university.edu', '123456'),
('SV003', N'Lê Minh Hoàng', 'sv003@university.edu', '123456'),
('SV004', N'Phạm Quốc Bảo', 'sv004@university.edu', '123456'),
('SV005', N'Hoàng Minh Tâm', 'sv005@university.edu', '123456'),
('SV006', N'Vũ Quang Huy', 'sv006@university.edu', '123456'),
('SV007', N'Đặng Tuấn Anh', 'sv007@university.edu', '123456'),
('SV008', N'Bùi Đức Long', 'sv008@university.edu', '123456'),
('SV009', N'Đỗ Thành Đạt', 'sv009@university.edu', '123456'),
('SV010', N'Ngô Hải Đăng', 'sv010@university.edu', '123456'),

('SV011', N'Nguyễn Thị Mai', 'sv011@university.edu', '123456'),
('SV012', N'Trần Thị Hồng', 'sv012@university.edu', '123456'),
('SV013', N'Lê Thị Ngọc', 'sv013@university.edu', '123456'),
('SV014', N'Phạm Thị Thu', 'sv014@university.edu', '123456'),
('SV015', N'Hoàng Thị Ánh', 'sv015@university.edu', '123456'),
('SV016', N'Vũ Thị Trang', 'sv016@university.edu', '123456'),
('SV017', N'Đặng Thị Hương', 'sv017@university.edu', '123456'),
('SV018', N'Bùi Thị Tuyết', 'sv018@university.edu', '123456'),
('SV019', N'Đỗ Thị Lan Anh', 'sv019@university.edu', '123456'),
('SV020', N'Ngô Thị Thanh', 'sv020@university.edu', '123456'),

('SV021', N'Nguyễn Văn Phúc', 'sv021@university.edu', '123456'),
('SV022', N'Trần Văn Dũng', 'sv022@university.edu', '123456'),
('SV023', N'Lê Văn Khánh', 'sv023@university.edu', '123456'),
('SV024', N'Phạm Văn Lộc', 'sv024@university.edu', '123456'),
('SV025', N'Hoàng Văn Tài', 'sv025@university.edu', '123456'),
('SV026', N'Vũ Văn Trung', 'sv026@university.edu', '123456'),
('SV027', N'Đặng Văn Hòa', 'sv027@university.edu', '123456'),
('SV028', N'Bùi Văn Kiên', 'sv028@university.edu', '123456'),
('SV029', N'Đỗ Văn Phong', 'sv029@university.edu', '123456'),
('SV030', N'Ngô Văn Sơn', 'sv030@university.edu', '123456');

--Insert du lieu bang lop hoc phan
INSERT INTO lop_hoc_phan (class_id, ten_mon, hoc_ky, gv_id)
VALUES
('LHP001', N'Lập trình C#', N'Học kỳ 1', 1),
('LHP002', N'Cơ sở dữ liệu', N'Học kỳ 1', 2),
('LHP003', N'Lập trình Web', N'Học kỳ 1', 3),
('LHP004', N'Cấu trúc dữ liệu và giải thuật', N'Học kỳ 1', 4),
('LHP005', N'Hệ điều hành', N'Học kỳ 1', 5),

('LHP006', N'Mạng máy tính', N'Học kỳ 2', 6),
('LHP007', N'Lập trình Java', N'Học kỳ 2', 7),
('LHP008', N'Phát triển ứng dụng di động', N'Học kỳ 2', 8),
('LHP009', N'Trí tuệ nhân tạo', N'Học kỳ 2', 9),
('LHP010', N'An toàn thông tin', N'Học kỳ 2', 10);