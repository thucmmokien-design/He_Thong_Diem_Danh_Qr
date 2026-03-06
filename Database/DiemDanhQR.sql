CREATE DATABASE DiemDanhQR
GO

USE DiemDanhQR
GO

CREATE TABLE giang_vien (
    gv_id INT IDENTITY(1,1) PRIMARY KEY,
    hoten NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    password NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE sinh_vien (
    msv NVARCHAR(20) PRIMARY KEY,
    hoten NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) UNIQUE,
    password NVARCHAR(255) NOT NULL
);
GO

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