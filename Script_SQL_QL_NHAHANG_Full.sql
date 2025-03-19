------------------------------------------- Tạo Database -------------------------------------------
-- Sử dụng database mặc định của server
USE MASTER
GO

-- Kiểm tra tồn tại database QL_NHAHANG chưa
IF EXISTS(SELECT name FROM sysdatabases WHERE name = 'QL_NHAHANG')
DROP Database QL_NHAHANG
GO

-- Tạo database QL_NHAHANG
CREATE DATABASE QL_NHAHANG
ON PRIMARY 
(
    NAME = 'QL_NHAHANG_PRIMARY',
    FILENAME = 'QL_NHAHANG_PRIMARY.mdf', -- Cung cấp đường dẫn
    SIZE = 10MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10%
)
LOG ON
(
    NAME = 'QL_NHAHANG_LOG',
    FILENAME = 'QL_NHAHANG_LOG.ldf', -- Cung cấp đường dẫn
    SIZE = 10MB,
    MAXSIZE = 2GB,
    FILEGROWTH = 10MB
);
GO

-- Sử dụng database QL_NHAHANG
USE QL_NHAHANG
GO

-------------------------------------------------------------------------------------------------

------------------------------------------- Tạo Table -------------------------------------------
-- ON DELETE CASCADE : khi khai báo khóa ngoại, giúp tự động xóa các bản ghi liên quan nếu khi xóa khóa chính ở table

-- Tạo bảng khách hàng
CREATE TABLE KhachHang
(
    ------------ Thuộc tính bảng ------------
    MaKhachHang VARCHAR(10) NOT NULL, -- Mã khách hàng
    TenKhachHang NVARCHAR(MAX),		  -- Tên đầy đủ của khách hàng
    GioiTinh NVARCHAR(MAX),			  -- Giới tính của khách hàng (Nam/Nữ)
    SoDienThoai VARCHAR(15),		  -- Số điện thoại liên lạc
    Email VARCHAR(30),				  -- Địa chỉ email liên lạc

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_KhachHang PRIMARY KEY(MaKhachHang),
    -- Check
    CONSTRAINT CK_KhachHang_SoDienThoai CHECK (SoDienThoai LIKE '[0-9]%'),
    CONSTRAINT CK_KhachHang_Email CHECK (Email LIKE '%@%.%')
)
GO

SELECT TOP 0 * INTO Temp_KhachHang FROM KhachHang;

-- Tạo bảng lấy tên tài khoản
CREATE TABLE TaiKhoan
(
	------------ Thuộc tính bảng ------------
	TenTaiKhoan varchar(50) not null,
	MaKhachHang  VARCHAR(10) UNIQUE,
	CONSTRAINT PK_TaiKhoan PRIMARY KEY(TenTaiKhoan),
	CONSTRAINT FK_TaiKhoan_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang)
)
GO

-- Tạo bảng nhân viên
CREATE TABLE NhanVien
(
    ------------ Thuộc tính bảng ------------
    MaNhanVien VARCHAR(10) NOT NULL, -- Mã nhân viên
    TenNhanVien NVARCHAR(MAX),		 -- Tên đầy đủ của nhân viên
    GioiTinh NVARCHAR(MAX),			 -- Giới tính của nhân viên (Nam/Nữ)
    SoDienThoai VARCHAR(15),		 -- Số điện thoại liên lạc
    Email VARCHAR(30),				 -- Địa chỉ email liên lạc
    MaQuanLy VARCHAR(10) NULL,       -- Mã nhân viên quản lý, có thể null

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_NhanVien PRIMARY KEY(MaNhanVien),
    -- Khóa ngoại
    CONSTRAINT FK_NhanVien_MaQuanLy FOREIGN KEY (MaQuanLy) REFERENCES NhanVien(MaNhanVien),
    -- Check
    CONSTRAINT CK_NhanVien_SoDienThoai CHECK (SoDienThoai LIKE '[0-9]%'),
    CONSTRAINT CK_NhanVien_Email CHECK (Email LIKE '%@%.%')
)
GO

SELECT TOP 0 * INTO Temp_NhanVien FROM NhanVien;

-- Tạo bảng bàn ăn
CREATE TABLE Ban
(
    ------------ Thuộc tính bảng ------------
    MaBan VARCHAR(10) NOT NULL,					-- Mã bàn
    SoGhe INT,									-- Số lượng ghế ngồi của bàn
    TrangThai NVARCHAR(MAX) DEFAULT (N'Trống'), -- Trạng thái hiện tại của bàn (trống hoặc đã đặt)

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_Ban PRIMARY KEY(MaBan),  
    -- Check
    CONSTRAINT CK_Ban_SoGhe CHECK(SOGHE > 1)
)
GO

SELECT TOP 0 * INTO Temp_Ban FROM Ban;

-- Tạo bảng đặt bàn ăn
CREATE TABLE DatBan
(
    ------------ Thuộc tính bảng ------------
	MaDatBan VARCHAR(10) NOT NULL,	  -- Mã đặt bàn
    MaBan VARCHAR(10) NOT NULL,		  -- Mã bàn
    MaKhachHang VARCHAR(10) NOT NULL, -- Mã khách hàng
    ThoiGian DATETIME NOT NULL,		  -- Thời gian đặt
    SoKhach INT NOT NULL,			  -- Số lượng khách

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_DatBan PRIMARY KEY(MaDatBan),
    -- Khóa ngoại
    CONSTRAINT FK_DatBan_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    CONSTRAINT FK_DatBan_Ban FOREIGN KEY (MaBan) REFERENCES Ban(MaBan),
    -- Check
    CONSTRAINT CK_DatBan_ThoiGian CHECK(ThoiGian >= GETDATE()),
    CONSTRAINT CK_DatBan_SoKhach CHECK(SoKhach > 0)
)
GO

SELECT TOP 0 * INTO Temp_DatBan FROM DatBan;

-- Tạo phục vụ bàn
CREATE TABLE PhucVuBan
(
    ------------ Thuộc tính bảng ------------
    MaNhanVien VARCHAR(10) NOT NULL,  -- Mã nhân viên phục vụ
    MaDatBan VARCHAR(10) NOT NULL,		  -- Mã bàn
	ThoiGian DATETIME,

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_PhucVuBan PRIMARY KEY (MaNhanVien, MaDatBan, ThoiGian),
    -- Khóa ngoại
    CONSTRAINT FK_PhucVuBan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien),
    CONSTRAINT FK_PhucVuBan_DatBan FOREIGN KEY (MaDatBan) REFERENCES DatBan(MaDatBan),
)
GO

-- Tạo bảng món ăn
CREATE TABLE MonAn
(
    ------------ Thuộc tính bảng ------------
    MaMonAn VARCHAR(10) NOT NULL,     -- Mã món ăn
    TenMonAn NVARCHAR(255) UNIQUE,	  -- Tên món ăn
    LoaiMon NVARCHAR(MAX),			  -- Loại món ăn
    DonGia DECIMAL(18,2),			  -- Giá tiền của món ăn
    TrangThai NVARCHAR(MAX) DEFAULT(N'Có sẵn'),  -- Trạng thái món ăn

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_MonAn PRIMARY KEY(MaMonAn),
    -- Check
    CONSTRAINT CK_MonAn_DonGia CHECK(DonGia >= 0)
)
GO

SELECT TOP 0 * INTO Temp_MonAn FROM MonAn;

-- Tạo bảng đặt món ăn
CREATE TABLE DatMon
(
    ------------ Thuộc tính bảng ------------
	MaDatMon VARCHAR(10) NOT NULL, -- Mã đặt món
    MaKhachHang VARCHAR(10) NOT NULL,   -- Mã khách
    NgayLap DATETIME,        -- Ngày lập đơn đặt món

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_DatMon PRIMARY KEY(MaDatMon),
    -- Khóa ngoại
    CONSTRAINT FK_DatMon_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang) ON DELETE CASCADE,
    -- Check
    CONSTRAINT CK_DatMon_NgayLap CHECK(NGAYLAP >= GETDATE())
)
GO

SELECT TOP 0 * INTO Temp_DatMon FROM DatMon;

-- Tạo bảng chi tiết đặt món
CREATE TABLE ChiTietDatMon
(
    ------------ Thuộc tính bảng ------------
    MaDatMon VARCHAR(10) NOT NULL,    -- Mã đặt món
    MaMonAn VARCHAR(10) NOT NULL,   -- Mã món ăn
    TenMonAn NVARCHAR(255),      -- Tên món ăn
    SoLuong INT,               -- Số lượng món ăn
    ThanhTien DECIMAL(18,2),   -- Thành tiền
    GhiChu NVARCHAR(MAX),      -- Ghi chú là yêu cầu với thức ăn

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_ChiTietDatMon PRIMARY KEY(MaDatMon, MaMonAn),
    -- Khóa ngoại
    CONSTRAINT FK_ChiTietDatMon_DatMon FOREIGN KEY (MaDatMon) REFERENCES DatMon(MaDatMon) ON DELETE CASCADE,
    CONSTRAINT FK_ChiTietDatMon_MonAn FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn) ON DELETE CASCADE,
    -- Check
    CONSTRAINT CK_ChiTietDatMon_SoLuong CHECK(SoLuong > 0)
)
GO

-- Tạo bảng hóa đơn
CREATE TABLE HoaDon
(
    ------------ Thuộc tính bảng ------------
    MaHoaDon VARCHAR(10) NOT NULL,      -- Mã hóa đơn
    MaDatMon VARCHAR(10) NOT NULL UNIQUE,
	NgayLap DATETIME,					-- Ngày hóa đơn được tạo
    TongTien DECIMAL(18,2),				-- Tổng số tiền phải thanh toán
    PhuongThucThanhToan NVARCHAR(MAX) DEFAULT (N'Tiền mặt'),  -- Phương thức thanh toán (Tiền mặt, thẻ, chuyển khoản, v.v.)

    ------------ Ràng buộc bảng ------------
    -- Khóa chính
    CONSTRAINT PK_HoaDon PRIMARY KEY(MaHoaDon),
	-- Khóa ngoại
	CONSTRAINT FK_HoaDon_DatMon FOREIGN KEY (MaDatMon) REFERENCES DatMon(MaDatMon),
    -- Check
    CONSTRAINT CK_HoaDon_NgayLap CHECK(NgayLap >= GETDATE()),
    CONSTRAINT CK_HoaDon_TongTien CHECK(TongTien >= 0)
)
GO

SELECT TOP 0 * INTO Temp_HoaDon FROM HoaDon;

-- Tạo bảng duyệt thực đơn:
CREATE TABLE DuyetThucDon
(
	------------ Thuộc tính bảng ------------
	MaKhachHang VARCHAR(10) NOT NULL,
	MaMonAn VARCHAR(10) NOT NULL,
	Soluong INT,
	TrangThai NVARCHAR(50),

	------------ Ràng buộc bảng ------------
	CONSTRAINT PK_DuyetThucDon PRIMARY KEY (MaKhachHang,MaMonAn),
	CONSTRAINT FK_DuyetThucDon_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
	CONSTRAINT FK_DuyetThucDon_MonAn FOREIGN KEY (MaMonAn) REFERENCES MonAn(MaMonAn)
)
GO

-- Tạo bảng lưu lại phiên đăng nhập của tài khoản
CREATE TABLE UserSession 
(
	------------ Thuộc tính bảng ------------
    SessionID UNIQUEIDENTIFIER, -- Mã phiên duy nhất
    UserID NVARCHAR(50),		-- Mã người dùng liên kết với phiên đăng nhập

	------------ Ràng buộc bảng ------------
	-- Khóa chính
	CONSTRAINT PK_UserSessions PRIMARY KEY (SessionID)
)
GO
-------------------------------------------------------------------------------------------------------
------------------------------------------- Tạo Trigger -------------------------------------------
-- TRG - TRIGGER

-- Tự đọng tạo mã cho table KhachHang
CREATE TRIGGER TRG_Insert_MaKhachHang_KhachHang
ON KhachHang
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaKhachHang VARCHAR(10);
    DECLARE @NewMaKhachHang VARCHAR(10);

    -- Lấy mã khách hàng lớn nhất
    SELECT @MaxMaKhachHang = MAX(MaKhachHang) FROM KhachHang;

    -- Nếu mã khách hàng lớn nhất là NULL, bắt đầu từ KH0001
    IF @MaxMaKhachHang IS NULL
        SET @NewMaKhachHang = 'KH001';
    ELSE
        -- Tăng mã khách hàng lên 1
        SET @NewMaKhachHang = 'KH' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaKhachHang, 3, LEN(@MaxMaKhachHang) - 2) AS INT) + 1 AS VARCHAR), LEN(@MaxMaKhachHang) - 2);

    -- Thực hiện insert với mã khách hàng mới
    INSERT INTO KhachHang (MaKhachHang, TenKhachHang, GioiTinh, SoDienThoai, Email)
    SELECT @NewMaKhachHang, TenKhachHang, GioiTinh, SoDienThoai, Email 
	FROM inserted;
END
GO

-- Tự đọng tạo mã cho table NhanVien
CREATE TRIGGER TRG_Insert_MaNhanVien_NhanVien
ON NhanVien
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaNhanVien VARCHAR(10);
    DECLARE @NewMaNhanVien VARCHAR(10);

    -- Lấy mã nhân viên lớn nhất
    SELECT @MaxMaNhanVien = MAX(MaNhanVien) FROM NhanVien;

    -- Nếu mã nhân viên lớn nhất là NULL, bắt đầu từ NV0001
    IF @MaxMaNhanVien IS NULL
        SET @NewMaNhanVien = 'NV001';
    ELSE
        -- Tăng mã nhân viên lên 1
        SET @NewMaNhanVien = 'NV' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaNhanVien, 3, LEN(@MaxMaNhanVien) - 2) AS INT) + 1 AS VARCHAR), LEN(@MaxMaNhanVien) - 2);

    -- Thực hiện insert với mã nhân viên mới
    INSERT INTO NhanVien (MaNhanVien, TenNhanVien, GioiTinh, SoDienThoai, Email, MaQuanLy)
    SELECT @NewMaNhanVien, TenNhanVien, GioiTinh, SoDienThoai, Email, MaQuanLy 
	FROM inserted;
END
GO

-- Tự đọng tạo mã cho table Ban
CREATE TRIGGER TRG_Insert_MaBan_Ban
ON Ban
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaBan VARCHAR(10);
    DECLARE @NewMaBan VARCHAR(10);

    -- Lấy mã bàn lớn nhất
    SELECT @MaxMaBan = MAX(MaBan) FROM Ban;

    -- Nếu mã bàn lớn nhất là NULL, bắt đầu từ BAN0001
    IF @MaxMaBan IS NULL
        SET @NewMaBan = 'BAN001';
    ELSE
        -- Tăng mã bàn lên 1
        SET @NewMaBan = 'BAN' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaBan, 4, LEN(@MaxMaBan) - 3) AS INT) + 1 AS VARCHAR), LEN(@MaxMaBan) - 3);

    -- Thực hiện insert với mã bàn mới
    INSERT INTO Ban (MaBan, SoGhe, TrangThai)
    SELECT @NewMaBan, SoGhe, TrangThai 
	FROM inserted;
END
GO

-- Tự động tạo mã cho bảng DatBan
CREATE TRIGGER TRG_Insert_MaDatBan_DatBan
ON DatBan
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaDatBan VARCHAR(10);  -- Lưu trữ mã đặt bàn lớn nhất hiện tại
    DECLARE @NewMaDatBan VARCHAR(10);  -- Mã đặt bàn mới

    -- Lấy mã đặt bàn lớn nhất
    SELECT @MaxMaDatBan = MAX(MaDatBan) FROM DatBan;

    -- Nếu mã đặt bàn lớn nhất là NULL, bắt đầu từ DB0001
    IF @MaxMaDatBan IS NULL
        SET @NewMaDatBan = 'DB001';
    ELSE
        -- Tăng mã đặt bàn lên 1
        SET @NewMaDatBan = 'DB' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaDatBan, 3, LEN(@MaxMaDatBan) - 2) AS INT) + 1 AS VARCHAR), LEN(@MaxMaDatBan) - 2);

    -- Thực hiện insert với mã đặt bàn mới
    INSERT INTO DatBan (MaDatBan, MaBan, MaKhachHang, ThoiGian, SoKhach)
    SELECT @NewMaDatBan, MaBan, MaKhachHang, ThoiGian, SoKhach 
	FROM inserted;
END
GO

-- Tự đọng tạo mã cho table MonAn
CREATE TRIGGER TRG_Insert_MaMonAn_MonAn
ON MonAn
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaMonAn VARCHAR(10);
    DECLARE @NewMaMonAn VARCHAR(10);

    -- Lấy mã món ăn lớn nhất
    SELECT @MaxMaMonAn = MAX(MaMonAn) FROM MonAn;

    -- Nếu mã món ăn lớn nhất là NULL, bắt đầu từ MA0001
    IF @MaxMaMonAn IS NULL
        SET @NewMaMonAn = 'MA001';
    ELSE
        -- Tăng mã món ăn lên 1
        SET @NewMaMonAn = 'MA' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaMonAn, 3, LEN(@MaxMaMonAn) - 2) AS INT) + 1 AS VARCHAR), LEN(@MaxMaMonAn) - 2);

    -- Thực hiện insert với mã món ăn mới
    INSERT INTO MonAn (MaMonAn, TenMonAn, LoaiMon, DonGia, TrangThai)
    SELECT @NewMaMonAn, TenMonAn, LoaiMon, DonGia, TrangThai
	FROM inserted;
END
GO

-- Tự đọng tạo mã cho table DatMon
CREATE TRIGGER TRG_Insert_MaDatMon_DatMon
ON DatMon
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaDatMon VARCHAR(10);
    DECLARE @NewMaDatMon VARCHAR(10);

    -- Lấy mã đặt món lớn nhất
    SELECT @MaxMaDatMon = MAX(MaDatMon) FROM DatMon;

    -- Nếu mã đặt món lớn nhất là NULL, bắt đầu từ DM0001
    IF @MaxMaDatMon IS NULL
        SET @NewMaDatMon = 'DM001';
    ELSE
        -- Tăng mã đặt món lên 1
        SET @NewMaDatMon = 'DM' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaDatMon, 3, LEN(@MaxMaDatMon) - 2) AS INT) + 1 AS VARCHAR), LEN(@MaxMaDatMon) - 2);

    -- Thực hiện insert với mã đặt món mới
    INSERT INTO DatMon (MaDatMon, MaKhachHang, NgayLap)
    SELECT @NewMaDatMon, MaKhachHang, NgayLap 
	FROM inserted;
END
GO

-- Tự đọng tạo mã cho table HoaDon
CREATE TRIGGER TRG_Insert_MaHoaDon_HoaDon
ON HoaDon
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaxMaHoaDon VARCHAR(10);
    DECLARE @NewMaHoaDon VARCHAR(10);

    -- Lấy mã hóa đơn lớn nhất
    SELECT @MaxMaHoaDon = MAX(MaHoaDon) FROM HoaDon;

    -- Nếu mã hóa đơn lớn nhất là NULL, bắt đầu từ HD0001
    IF @MaxMaHoaDon IS NULL
        SET @NewMaHoaDon = 'HD001';
    ELSE
        -- Tăng mã hóa đơn lên 1
        SET @NewMaHoaDon = 'HD' + RIGHT('00' + CAST(CAST(SUBSTRING(@MaxMaHoaDon, 3, LEN(@MaxMaHoaDon) - 2) AS INT) + 1 AS VARCHAR), LEN(@MaxMaHoaDon) - 2);

    -- Thực hiện insert với mã hóa đơn mới
    INSERT INTO HoaDon (MaHoaDon, MaDatMon, NgayLap, TongTien, PhuongThucThanhToan)
    SELECT @NewMaHoaDon, MaDatMon, NgayLap, TongTien, PhuongThucThanhToan 
	FROM inserted;
END
GO

-- Tự động tính thành tiền cho chi tiết đặt móSP_
CREATE TRIGGER TRG_TinhThanhTien_ChiTietDatMon
ON ChiTietDatMon
AFTER INSERT, UPDATE
AS
BEGIN
    -- Khai báo biến lưu 
    DECLARE @MaDatMon VARCHAR(10);
	DECLARE @MaMonAn VARCHAR(10);
    
    -- Lấy mã đặt món từ bản ghi mới được chèn hoặc cập nhật
    SELECT @MaDatMon = i.MaDatMon FROM inserted i;
	SELECT @MaMonAn = i.MaMonAn FROM inserted i;
    
    -- Tính thành tiền của bảng ChiTietDatMon dựa trên số lượng và đơn giá từ bảng Món Ăn
    DECLARE @ThanhTien DECIMAL(18, 2);
    SET @ThanhTien = (SELECT i.SoLuong FROM inserted i) * 
					 (SELECT DonGia FROM MonAn WHERE MaMonAN = @MaMonAN);
    
    -- Cập nhật tổng tiền vào bảng HoaDon
    UPDATE ChiTietDatMon
    SET ThanhTien = @ThanhTien
    WHERE MaDatMon = @MaDatMon AND MaMonAn = @MaMonAn;

	IF EXISTS(SELECT * FROM inserted) 
	BEGIN
		-- Tính tổng tiền từ bảng ChiTietDatMon dựa trên MaDatMon
		DECLARE @TongTien DECIMAL(18, 2);
		SELECT @TongTien = SUM(ThanhTien) 
		FROM ChiTietDatMon 
		WHERE MaDatMon = @MaDatMon;
    
		-- Cập nhật tổng tiền vào bảng HoaDon
		UPDATE HoaDon 
		SET TongTien = @TongTien
		WHERE MaDatMon = @MaDatMon;
	END
END;
GO

-- Tự động tính tổng tiền cho hóa đơn
CREATE TRIGGER TRG_TinhTongTien_HoaDon
ON HoaDon
AFTER INSERT, UPDATE
AS
BEGIN
    -- Khai báo biến lưu mã đặt món
    DECLARE @MaDatMon VARCHAR(10);
    
    -- Lấy mã đặt món từ bản ghi mới được chèn hoặc cập nhật
    SELECT @MaDatMon = i.MaDatMon FROM inserted i;
    
    -- Tính tổng tiền từ bảng ChiTietDatMon dựa trên MaDatMon
    DECLARE @TongTien DECIMAL(18, 2);
    SELECT @TongTien = SUM(ThanhTien) 
    FROM ChiTietDatMon 
    WHERE MaDatMon = @MaDatMon;
    
    -- Cập nhật tổng tiền vào bảng HoaDon
    UPDATE HoaDon 
    SET TongTien = @TongTien
    WHERE MaDatMon = @MaDatMon;
END;
GO

---- Tự động xóa đặt món quá hạn (TRG_DeleteExpired_DatMon)
--CREATE TRIGGER TRG_XoaQuaHan_DatMon
--ON DatMon
--AFTER INSERT, UPDATE
--AS
--BEGIN
--    -- Xóa các đơn đặt món có NgayLap trước ngày hiện tại
--    DELETE FROM DatMon
--    WHERE NgayLap < GETDATE();
--END;
--GO

-- Tự động tạo bản ghi trong bảng PhucVuBan khi bảng DatBan insert
CREATE TRIGGER TRG_AutoInsertPhucVuBan
ON DatBan
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

	DECLARE @MaNhanVien VARCHAR(10);
    DECLARE @MaDatBan VARCHAR(10);
	DECLARE @ThoiGian DATETIME;

    -- Lấy giá trị từ bảng DatBan vừa được chèn
    SELECT @MaDatBan = MaDatBan, @ThoiGian = ThoiGian
    FROM inserted;

    -- Chọn ngẫu nhiên một nhân viên chưa phục vụ bàn nào tại thời điểm đó
    SELECT TOP 1 @MaNhanVien = nv.MaNhanVien
    FROM NhanVien nv
    LEFT JOIN PhucVuBan pv ON nv.MaNhanVien = pv.MaNhanVien AND pv.ThoiGian = @ThoiGian
    WHERE pv.MaNhanVien IS NULL AND MaQuanLy IS NULL -- Chỉ chọn nhân viên không phục vụ tại thời điểm đó
    ORDER BY NEWID() -- Chọn ngẫu nhiên

    -- Nếu tìm thấy nhân viên, thêm bản ghi vào bảng PhucVuBan
    IF @MaNhanVien IS NOT NULL
    BEGIN
        INSERT INTO PhucVuBan (MaNhanVien, MaDatBan, ThoiGian)
        VALUES (@MaNhanVien, @MaDatBan, @ThoiGian);
    END
    ELSE
    BEGIN
        -- Xử lý trường hợp không tìm thấy nhân viên trống (nếu cần)
        PRINT 'Không có nhân viên nào trống tại thời gian này.';
    END
END;
GO

-- Tự động cập nhật bản ghi trong bảng PhucVuBan khi bảng DatBan update
CREATE TRIGGER TRG_UpdatePhucVuBan
ON DatBan
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật thời gian phục vụ trong PhucVuBan khi thời gian trong DatBan thay đổi
    UPDATE pv
    SET pv.ThoiGian = i.ThoiGian
    FROM PhucVuBan pv
    INNER JOIN inserted i ON pv.MaDatBan = i.MaDatBan
    WHERE pv.MaDatBan = i.MaDatBan;
END;
GO

-- Tự động xóa bản ghi trong bảng PhucVuBan khi bảng DatBan delete
CREATE TRIGGER TRG_DeletePhucVuBan
ON DatBan
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Xóa bản ghi trong PhucVuBan khi bản ghi tương ứng trong DatBan bị xóa
    DELETE FROM PhucVuBan
    WHERE MaDatBan IN (SELECT MaDatBan FROM deleted);
END;
GO

-- Tự động xóa phục vụ bàn trước khi xóa đặt bàn
CREATE TRIGGER TRG_Delete_PhucVuBan_Before_DatBan
ON DatBan
INSTEAD OF DELETE
AS
BEGIN
	DECLARE @MaDatBan VARCHAR(10);

	SET @MaDatBan = (Select MaDatBan FROM deleted);

	DELETE FROM PhucVuBan WHERE MaDatBan = @MaDatBan
	DELETE FROM DatBan WHERE MaDatBan = @MaDatBan
END;
GO
---------------------------------------------------------------------------------------------------

------------------------------------------- Tạo View Table ----------------------------------------
-- V - view

-- View cho bảng KhachHang
CREATE VIEW V_KhachHang AS
	SELECT MaKhachHang, TenKhachHang, GioiTinh, SoDienThoai, Email
	FROM KhachHang;
GO

-- View cho bảng NhanVien
CREATE VIEW V_NhanVien AS
	SELECT MaNhanVien, TenNhanVien, GioiTinh, SoDienThoai, Email, MaQuanLy
	FROM NhanVien;
GO

-- View cho bảng Ban
CREATE VIEW V_Ban AS
	SELECT MaBan, SoGhe, TrangThai
	FROM Ban;
GO

-- View cho bảng DatBan
CREATE VIEW V_DatBan AS
	SELECT MaDatBan, MaBan, MaKhachHang, ThoiGian, SoKhach
	FROM DatBan;
GO

-- Tạo View cho bảng PhucVuBan
CREATE VIEW V_PhucVuBan
AS
	SELECT MaNhanVien, MaDatBan, ThoiGian
	FROM PhucVuBan;
GO

-- View cho bảng MonAn
CREATE VIEW V_MonAn AS
	SELECT MaMonAn, TenMonAn, LoaiMon, DonGia, TrangThai
	FROM MonAn;
GO

-- View cho bảng DatMon
CREATE VIEW V_DatMon AS
	SELECT MaDatMon, MaKhachHang, NgayLap
	FROM DatMon;
GO

-- View cho bảng ChiTietDatMon
CREATE VIEW V_ChiTietDatMon AS
	SELECT MaDatMon, MaMonAn, TenMonAn, SoLuong, ThanhTien, GhiChu
	FROM ChiTietDatMon;
GO

-- View cho bảng HoaDon
CREATE VIEW V_HoaDon AS
	SELECT MaHoaDon, MaDatMon, NgayLap, TongTien, PhuongThucThanhToan
	FROM HoaDon;
GO

--View cho bảng DuyetThucDon
CREATE VIEW V_DuyetThucDon AS
	SELECT * FROM DuyetThucDon
GO

-- View cho bảng UserSessions
CREATE VIEW V_UserSession AS
	SELECT SessionID, UserID
	FROM UserSession;
GO

-- View cho tên cột của các bảng
CREATE VIEW V_COLUMN_NAME_TABLE AS
	SELECT COLUMN_NAME, TABLE_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE SUBSTRING(TABLE_NAME,1,2) <> 'V_' AND SUBSTRING(TABLE_NAME,1,5) <> 'Temp_'
GO
---------------------------------------------------------------------------------------------------

------------------------------------------- Tạo Stored Procedure -----------------------------------
-- SP - STORED PROCEDURE
-- WITH ENCRYPTION - mã hóa các sp và các câu lệnh bên trong
-- SET NOCOUNT ON - Ngăn thông báo số lượng hàng bị ảnh hưởng, giúp giảm thiểu thông tin không cần thiết. (ví dụ: "1 row(s) affected").
-- SP_TênChứcNăng_Table

---------------- CREATE (INSERT) ---------------------
-- Thêm Khách Hàng 
CREATE PROCEDURE SP_Insert_KhachHang
    @TenKhachHang NVARCHAR(50),
    @GioiTinh NVARCHAR(10),
    @SoDienThoai VARCHAR(15),
    @Email VARCHAR(30)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra số điện thoại hợp lệ
    IF LEN(@SoDienThoai) < 10 OR @SoDienThoai NOT LIKE '[0-9]%'
    BEGIN
        RAISERROR(N'Số điện thoại không hợp lệ!', 16, 1);
        RETURN;
    END

    -- Thêm khách hàng
    INSERT INTO KhachHang (TenKhachHang, GioiTinh, SoDienThoai, Email)
    VALUES (@TenKhachHang, @GioiTinh, @SoDienThoai, @Email);

    PRINT N'Thêm khách hàng thành công';
END;
GO

-- Thủ tục thêm thông tin toàn khoản
CREATE PROCEDURE SP_Insert_TaiKhoan
	@TenTaiKhoan varchar(50)
AS
BEGIN
	insert into TaiKhoan (TenTaiKhoan)
	values (@TenTaiKhoan)
	PRINT 'Đã tạo tài khoản thành công'
END
GO

-- Thêm Nhân Viên
CREATE PROCEDURE SP_Insert_NhanVien
    @TenNhanVien NVARCHAR(50),
    @GioiTinh NVARCHAR(10),
    @SoDienThoai VARCHAR(15),
    @Email VARCHAR(30),
    @MaQuanLy VARCHAR(10) = NULL
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra số điện thoại hợp lệ
    IF LEN(@SoDienThoai) < 10 OR @SoDienThoai NOT LIKE '[0-9]%'
    BEGIN
        RAISERROR(N'Số điện thoại không hợp lệ!', 16, 1);
        RETURN;
    END

	-- Kiểm tra mã nhân viên quản lý nếu mã quản lí không null
	IF @MaQuanLy IS NOT NULL
	BEGIN
		IF NOT EXISTS (SELECT * FROM NhanVien WHERE MaNhanVien = @MaQuanLy)
		BEGIN
			RAISERROR(N'Mã nhân viên quản lý không tồn tại!', 16, 1);
			RETURN;
		END
	END

    -- Thêm nhân viên
    INSERT INTO NhanVien (TenNhanVien, GioiTinh, SoDienThoai, Email, MaQuanLy)
    VALUES (@TenNhanVien, @GioiTinh, @SoDienThoai, @Email, @MaQuanLy);

    PRINT N'Thêm nhân viên thành công';
END;
GO

-- Thêm Bàn 
CREATE PROCEDURE SP_Insert_Ban
    @SoGhe INT,
    @TrangThai NVARCHAR(MAX) = N'Trống'
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
	
	-- Kiểm tra số lượng ghế
	IF (@SoGhe <= 1)
	BEGIN
		RAISERROR(N'Số lượng ghế không hợp lệ!', 16, 1);
        RETURN;
	END

    -- Thêm bàn
    INSERT INTO Ban (SoGhe, TrangThai)
    VALUES (@SoGhe, @TrangThai);

    PRINT N'Thêm bàn thành công';
END;
GO

-- Thêm Đặt Bàn
CREATE PROCEDURE SP_Insert_DatBan
    @MaBan VARCHAR(10),
    @MaKhachHang VARCHAR(10),
    @ThoiGian DATETIME,
    @SoKhach INT
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem mã bàn có tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM Ban WHERE MaBan = @MaBan)
    BEGIN
        RAISERROR(N'Mã Bàn không tồn tại!', 16, 1);
        RETURN;
    END

	-- Kiểm tra xem mã khách có tồn tại hay không
    IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại!', 16, 1);
        RETURN;
    END

	-- Kiểm tra số khách
	IF (@SoKhach <= 0)
	BEGIN
		RAISERROR(N'Số lượng khách không hợp lệ!', 16, 1);
        RETURN;
	END

    -- Thêm đặt bàn
    INSERT INTO DatBan (MaBan, MaKhachHang, ThoiGian, SoKhach)
    VALUES (@MaBan, @MaKhachHang, @ThoiGian, @SoKhach);

    PRINT N'Thêm đặt bàn thành công';
END;
GO

-- Thêm Món Ăn
CREATE PROCEDURE SP_Insert_MonAn
    @TenMonAn NVARCHAR(255),
    @LoaiMon NVARCHAR(MAX) = N'Món chính',
    @DonGia DECIMAL(18, 2),
    @TrangThai NVARCHAR(MAX) = N'Có sẵn'
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	-- Kiểm tra đơn giá
	IF (@DonGia < 0)
	BEGIN
		RAISERROR(N'Đơn giá không hợp lệ!', 16, 1);
        RETURN;
	END

    -- Thêm món ăn
    INSERT INTO MonAn (TenMonAn, LoaiMon, DonGia, TrangThai)
    VALUES (@TenMonAn, @LoaiMon, @DonGia, @TrangThai);

    PRINT N'Thêm món ăn thành công';
END;
GO

-- Thêm Đặt Món
CREATE PROCEDURE SP_Insert_DatMon
    @MaKhachHang VARCHAR(10),
    @NgayLap DATETIME
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	-- Kiểm tra xem món ăn có tồn tại trong bảng MonAn
    IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        RAISERROR(N'Mã khách hàng không tồn tại!', 16, 1);
        RETURN;
    END

    -- Thêm đặt món
    INSERT INTO DatMon (MaKhachHang, NgayLap)
    VALUES (@MaKhachHang, @NgayLap);

    PRINT N'Thêm đặt món thành công';
END;
GO

--Thêm mã khách hàng vào DatMon với thời gian hiện tại
CREATE PROC SP_Insert_DatMon_TheoThoiGianHienTai 
	@MaKhachHang varchar(10)
WITH ENCRYPTION
AS
BEGIN
	Declare @dateTime datetime = DATEADD(Minute, 1, getdate())  -- cộng thêm 1p để cho thời gian chạy kịp mà ko bị lỗi về CK_DatMon_NgayLap
	exec SP_Insert_DatMon @MaKhachHang, @dateTime
END
GO

-- Thêm Chi Tiết Đặt Món
CREATE PROCEDURE SP_Insert_ChiTietDatMon
	@MaDatMon VARCHAR(10),
    @MaMonAn VARCHAR(10),
    @TenMonAn NVARCHAR(255),
    @SoLuong INT,
    @GhiChu NVARCHAR(MAX)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

	-- Kiểm tra xem mã đặt món có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DatMon WHERE MaDatMon = @MaDatMon)
    BEGIN
        RAISERROR(N'Mã đặt món không tồn tại!', 16, 1);
        RETURN;
    END

    -- Kiểm tra xem mã món ăn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM MonAn WHERE MaMonAn = @MaMonAn)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END

	-- Kiểm tra số lượng
	IF (@SoLuong <= 0)
	BEGIN
		RAISERROR(N'Số lượng không hợp lệ!', 16, 1);
        RETURN;
	END

    -- Thêm chi tiết đặt món
    INSERT INTO ChiTietDatMon (MaDatMon, MaMonAn, TenMonAn, SoLuong, GhiChu)
    VALUES (@MaDatMon, @MaMonAn, @TenMonAn, @SoLuong, @GhiChu);

    PRINT N'Thêm chi tiết đặt món thành công';
END;
GO

--Thêm món ăn vào ChiTietDatMon với mã đặt món mới nhất
CREATE PROC SP_Insert_ChiTietDatMon_ThemMonAnVoiMaDatMonMoiNhat
	@MaMonAn varchar(10),
	@TenMonAn NVARCHAR(max),
	@Soluong INT
AS
BEGIN
	DECLARE @MaxMaDatMon VARCHAR(10);
	SELECT @MaxMaDatMon = MAX(MaDatMon) FROM DatMon; -- Lấy mã đặt món lớn nhất (mới nhất)

	exec SP_Insert_ChiTietDatMon @MaxMaDatMon, @MaMonAn, @TenMonAn, @Soluong, null 
END
GO

-- Thêm Hóa Đơn 
CREATE PROCEDURE SP_Insert_HoaDon
    @MaDatMon VARCHAR(10),
    @NgayLap DATETIME,
    @PhuongThucThanhToan NVARCHAR(MAX) = N'Tiền mặt'
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Thêm hóa đơn
    INSERT INTO HoaDon (MaDatMon, NgayLap, PhuongThucThanhToan)
    VALUES (@MaDatMon, @NgayLap, @PhuongThucThanhToan);

    PRINT N'Thêm hóa đơn thành công';
END;
GO

--Thêm vào hoá đơn với mã đặt món mới nhất và thời gian hiện tại
CREATE PROC SP_Insert_HoaDon_ThemMaDatMonMoiNhat
	@PhuongThucThanhToan NVARCHAR(20)
AS
BEGIN
	DECLARE @MaxMaDatMon VARCHAR(10),
			@thoiGian datetime = DATEADD(Minute, 1, GETDATE()) -- cộng thêm 1s để cho thời gian chạy kịp mà ko bị lỗi về CK_HoaDon_NgayLap
	SELECT @MaxMaDatMon= MAX(MaDatMon) FROM DatMon; -- Lấy mã đặt món lớn nhất (mới nhất)

	exec SP_Insert_HoaDon @MaxMaDatMon, @thoiGian, @PhuongThucThanhToan
END
GO

--Thêm món ăn khách hàng muốn đặt ( Chua )
CREATE PROCEDURE SP_Insert_DuyetThucDon
	@MaKhachHang varchar(10),
	@MaMonAn varchar(10),
	@SoLuong int = 1
AS
BEGIN
	declare @MaMon varchar(10), -- Lấy mã món
			@TenMon nvarchar(max) -- Lấy tên món
	set @MaMon = (select MaMonAn from DuyetThucDon where MaKhachHang = @MaKhachHang and MaMonAn = @MaMonAn)
	set @TenMon = (select TenMonAn from MonAn where MaMonAn = @MaMonAn)

	IF(@MaMon = @MaMonAn) --Trường hợp cùng một món ăn thì tăng số lượng lên
		BEGIN
			declare @Trangthai Nvarchar(max) = null
			UPDATE DuyetThucDon set Soluong = Soluong + @Soluong, @Trangthai = Trangthai 
			where TrangThai = N'Chưa đặt'
					and MaKhachHang = @MaKhachHang
					and MaMonAn = @MaMonAn

			if(@Trangthai is null) -- Trường hợp lấy dữ liệu @Trangthai từ dữ liệu Update ko có thì xuất thông báo
				Print(N'Không thể chọn món ' + '"'+ @TenMon + '"' + N' vì món đó đã duyệt. Để thực hiện được thì cần xoá món ' + '"'+ @TenMon + '"' + N' đã được duyệt')
			else
				PRINT(N'Đã cập nhật số lượng món ' + '"'+@TenMon+'"' + N' thành công')
		END
	ELSE --Không có thì thêm mới vào DuyetThucDon
		BEGIN
			insert into DuyetThucDon (MaKhachHang, MaMonAn, TrangThai, Soluong) values(@MaKhachHang, @MaMonAn, N'Chưa đặt', @SoLuong)
			PRINT N'Đã thêm món ' + '"'+@TenMon+'"' + N' thành công'
		END
END
GO

-- Thêm User Session 
CREATE PROCEDURE SP_Insert_UserSession
    @UserID NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    -- Thêm user ID
    INSERT INTO UserSession (SessionID, UserID)
    VALUES (NEWID(), @UserID);
END;
GO
----------------------------------------------------

---------------- Read (SELECT) ---------------------
-- Xem dữ liệu từ view KhachHang
CREATE PROCEDURE SP_Select_KhachHang
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_KhachHang;
END;
GO

-- Xem từ view NhanVien
CREATE PROCEDURE SP_Select_NhanVien
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_NhanVien;
END;
GO

-- Xem từ view Ban
CREATE PROCEDURE SP_Select_Ban
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_Ban;
END;
GO

-- Xem từ view DatBan
CREATE PROCEDURE SP_Select_DatBan
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_DatBan;
END;
GO

-- Xem từ view PhucVuBan
CREATE PROCEDURE SP_Select_PhucVuBan
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_PhucVuBan;
END;
GO

-- Xem từ view MonAn
CREATE PROCEDURE SP_Select_MonAn
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_MonAn;
END;
GO

-- Xem từ view DatMon
CREATE PROCEDURE SP_Select_DatMon
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_DatMon;
END;
GO

-- Xem từ view ChiTietDatMon
CREATE PROCEDURE SP_Select_ChiTietDatMon
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_ChiTietDatMon;
END;
GO

-- Xem từ view HoaDon
CREATE PROCEDURE SP_Select_HoaDon
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_HoaDon;
END;
GO

--Xem danh sách DuyetThucDon
CREATE PROCEDURE SP_Select_DuyetThucDon
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM V_DuyetThucDon
END
GO

-- Xem từ view UserSessions
CREATE PROCEDURE SP_Select_UserSession
	@UserID NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM V_UserSession WHERE UserID = @UserID;
END;
GO

-- Lấy tên cột của table
CREATE PROCEDURE SP_Select_CotTable
    @TenBang NVARCHAR(128)
AS
BEGIN
    -- Lấy thông tin tên cột từ bảng
    SELECT COLUMN_NAME
    FROM V_COLUMN_NAME_TABLE
    WHERE TABLE_NAME = @TenBang;
END
GO

-- Lấy trạng thái bàn
CREATE PROCEDURE SP_Select_Ban_TrangThai
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT TrangThai from Ban
END
GO

-- Lọc lấy trạng thái món ăn
CREATE PROCEDURE SP_Select_MonAn_TrangThai
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT TrangThai FROM MonAn
END
GO

-- Lọc lấy loại món ăn
CREATE PROCEDURE SP_Select_MonAn_LoaiMon
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT LoaiMon FROM MonAn
END
GO

-- Xem danh sách DuyetThucDon mà khách hàng đã chọn món ăn với thông tin cần thiết
CREATE PROCEDURE SP_Select_DuyetThucDon_XuatThongTinCanThiet 
	@maKhachHang VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT MonAn.MaMonAn,TenMonAn,LoaiMon,DonGia, DuyetThucDon.TrangThai, Soluong 
	FROM MonAn, DuyetThucDon
	WHERE MonAn.MaMonAn = DuyetThucDon.MaMonAn and MaKhachHang = @maKhachHang
END
GO

--Xuất ra các thông tin cần thiết của DuyetThucDon đang trong trạng thái 'Đang đợi duyệt'
CREATE PROCEDURE SP_Select_DuyetThucDon_XuatThongTinCanThiet_DangDoiDuyet @makh varchar(10)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT MonAn.MaMonAn,TenMonAn,LoaiMon,DonGia, DuyetThucDon.TrangThai, Soluong FROM MonAn, DuyetThucDon
	WHERE MonAn.MaMonAn = DuyetThucDon.MaMonAn and MaKhachHang = @makh and DuyetThucDon.TrangThai = N'Đang đợi duyệt'
END
GO

--Xuất các thông tin mã khách hàng đang đợi duyệt trong DuyetThucDon
CREATE PROCEDURE SP_Select_DuyetThucDon_TimKHDangDoiDatMon
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	select distinct MaKhachHang from DuyetThucDon where TrangThai = N'Đang đợi duyệt'
END
GO

--Xuất ra các thông tin theo mã khách hàng
CREATE PROCEDURE SP_Select_KhachHang_MaKH 
	@MaKhachHang VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM KhachHang WHERE MaKhachHang = @MaKhachHang
END
GO

--  Lấy danh sách các hóa đơn theo tháng
CREATE PROCEDURE SP_Select_HoaDon_TheoThang
	@Month INT,
	@Year INT
WITH ENCRYPTION
AS
	SET NOCOUNT ON;

	SELECT * FROM HoaDon
	WHERE MONTH(NgayLap) = @Month AND YEAR(NgayLap) = @Year
GO

-- Thủ tục xem thông tin tài khoản
CREATE PROCEDURE SP_Select_TaiKhoan
AS
BEGIN
	select * from TaiKhoan
END
GO

-- Thủ tục lấy mã khách hàng thông qua tên tài khoản
CREATE PROCEDURE SP_Select_TaiKhoan_LayMaKhachHang
	@TenTaiKhoan varchar(50)
AS
BEGIN
	select MaKhachHang from TaiKhoan where TenTaiKhoan = @TenTaiKhoan
END
GO

EXEC SP_Select_TaiKhoan_LayMaKhachHang 'tk77';

--  Lấy danh sách tất cả các bảng trong cơ sở dữ liệu
CREATE PROCEDURE SP_Select_TenTatCaBang
WITH ENCRYPTION
AS
	SET NOCOUNT ON;

	SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'
	AND TABLE_NAME NOT LIKE 'Temp%' AND TABLE_NAME NOT LIKE 'UserSession' AND TABLE_NAME NOT LIKE 'sysdiagrams'
GO

--  Lấy danh sách tất cả các view bảng trong cơ sở dữ liệu
CREATE PROCEDURE SP_Select_Views
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT name AS ViewName
    FROM sys.views
    ORDER BY name;
END;
GO

--  Lấy danh sách tất cả các stored procedure trong cơ sở dữ liệu
CREATE PROCEDURE SP_Select_Procedures
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT name AS ProcedureName
    FROM sys.procedures
    ORDER BY name;
END;
GO

--  Lấy danh sách tất cả các functions trong cơ sở dữ liệu
CREATE PROCEDURE SP_Select_Functions
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT name AS FunctionName
    FROM sys.objects
    WHERE type IN ('FN', 'IF', 'TF')  -- FN: Scalar function, IF: Inline table-valued function, TF: Table-valued function
    ORDER BY name;
END;
GO

--  Lấy danh sách tất cả các đối tượng trong cơ sở dữ liệu
CREATE PROCEDURE SP_Select_AllObjects
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT name AS ObjectName, type_desc AS ObjectType
    FROM sys.objects
    WHERE type IN ('U', 'V', 'P', 'FN', 'IF', 'TF')  -- U: Table, V: View, P: Stored Procedure, FN: Scalar function, IF: Inline table-valued function, TF: Table-valued function
    ORDER BY ObjectType, name;
END;
GO

--  Lấy danh sách các user trong cơ sở dữ liệu
CREATE PROCEDURE SP_Select_UserInDatabase
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    SELECT name 
    FROM sys.database_principals
    WHERE type_desc = 'SQL_USER' 
      AND name NOT IN ('sys', 'guest') -- Loại trừ user 'sys' và 'guest'
      AND principal_id > 4; -- Loại bỏ các user hệ thống

    PRINT N'Đã lấy danh sách các user có trong database thành công!';
END;
GO

----------------------------------------------------

----------------- Update ---------------------------
-- Cập nhật Dữ liệu Khách Hàng 
CREATE PROCEDURE SP_Update_KhachHang
    @MaKhachHang VARCHAR(10),
    @TenKhachHang NVARCHAR(50),
    @GioiTinh NVARCHAR(10),
    @SoDienThoai VARCHAR(15),
    @Email VARCHAR(30)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại khách hàng
    IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        RAISERROR(N'Khách hàng không tồn tại!', 16, 1);
        RETURN;
    END

    -- Kiểm tra số điện thoại hợp lệ
    IF LEN(@SoDienThoai) < 10 OR @SoDienThoai NOT LIKE '[0-9]%'
    BEGIN
        RAISERROR(N'Số điện thoại không hợp lệ!', 16, 1);
        RETURN;
    END

    -- Cập nhật khách hàng
    UPDATE KhachHang
    SET TenKhachHang = @TenKhachHang,
        GioiTinh = @GioiTinh,
        SoDienThoai = @SoDienThoai,
        Email = @Email
    WHERE MaKhachHang = @MaKhachHang;

    PRINT N'Cập nhật khách hàng thành công';
END;
GO

-- Thủ tục cập nhật mã khách hàng thông qua tên tài khoản
CREATE PROCEDURE SP_Update_TaiKhoan
	@TenTaiKhoan varchar(50),
	@MaKhachHang varchar(10)
AS
BEGIN
	update TaiKhoan
	set MaKhachHang = @MaKhachHang
	where TenTaiKhoan = @TenTaiKhoan
	PRINT N'Đã sửa thành công'
END
GO

-- Cập nhật Dữ liệu Nhân Viên 
CREATE PROCEDURE SP_Update_NhanVien
    @MaNhanVien VARCHAR(10),
    @TenNhanVien NVARCHAR(50),
    @GioiTinh NVARCHAR(10),
    @SoDienThoai VARCHAR(15),
    @Email NVARCHAR(30),
    @MaQuanLy VARCHAR(10) = NULL
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại nhân viên
    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaNhanVien = @MaNhanVien)
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại!', 16, 1);
        RETURN;
    END

    -- Kiểm tra số điện thoại hợp lệ
    IF LEN(@SoDienThoai) < 10 OR @SoDienThoai NOT LIKE '[0-9]%'
    BEGIN
        RAISERROR(N'Số điện thoại không hợp lệ!', 16, 1);
        RETURN;
    END

    -- Cập nhật nhân viên
    UPDATE NhanVien
    SET TenNhanVien = @TenNhanVien,
        GioiTinh = @GioiTinh,
        SoDienThoai = @SoDienThoai,
        Email = @Email,
        MaQuanLy = @MaQuanLy
    WHERE MaNhanVien = @MaNhanVien;

    PRINT N'Cập nhật nhân viên thành công';
END;
GO

-- Cập nhật Dữ liệu Bàn 
CREATE PROCEDURE SP_Update_Ban
    @MaBan VARCHAR(10),
    @SoGhe INT
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại bàn
    IF NOT EXISTS (SELECT 1 FROM Ban WHERE MaBan = @MaBan)
    BEGIN
        RAISERROR(N'Bàn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật bàn
    UPDATE Ban
    SET SoGhe = @SoGhe
    WHERE MaBan = @MaBan;

    PRINT N'Cập nhật bàn thành công';
END;
GO

-- Cập nhật Dữ liệu Đặt Bàn
CREATE PROCEDURE SP_Update_DatBan
    @MaDatBan VARCHAR(10),
    @MaBan VARCHAR(10),
    @MaKhachHang VARCHAR(10),
    @ThoiGian DATETIME,
    @SoKhach INT
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra xem mã đặt bàn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DatBan WHERE MaDatBan = @MaDatBan)
    BEGIN
        RAISERROR(N'Mã đặt bàn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật thông tin đặt bàn
    UPDATE DatBan
    SET MaBan = @MaBan,
        MaKhachHang = @MaKhachHang,
        ThoiGian = @ThoiGian,
        SoKhach = @SoKhach
    WHERE MaDatBan = @MaDatBan;

    PRINT N'Cập nhật đặt bàn thành công';
END;
GO

-- Cập nhật Dữ liệu Món Ăn
CREATE PROCEDURE SP_Update_MonAn
    @MaMonAn VARCHAR(10),
    @TenMonAn NVARCHAR(255),
    @LoaiMon NVARCHAR(MAX),
    @TrangThai NVARCHAR(MAX)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại món ăn
    IF NOT EXISTS (SELECT 1 FROM MonAn WHERE MaMonAn = @MaMonAn)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật món ăn
    UPDATE MonAn
    SET TenMonAn = @TenMonAn,
        LoaiMon = @LoaiMon,
        TrangThai = @TrangThai
    WHERE MaMonAn = @MaMonAn;

    PRINT N'Cập nhật món ăn thành công';
END;
GO

-- Cập nhật Dữ liệu Đặt Món
CREATE PROCEDURE SP_Update_DatMon
    @MaDatMon VARCHAR(10),
    @MaKhachHang VARCHAR(10),
    @NgayLap DATETIME
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại đặt món
    IF NOT EXISTS (SELECT 1 FROM DatMon WHERE MaDatMon = @MaDatMon)
    BEGIN
        RAISERROR(N'Đặt món không tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật đặt món
    UPDATE DatMon
    SET MaKhachHang = @MaKhachHang,
        NgayLap = @NgayLap
    WHERE MaDatMon = @MaDatMon;

    PRINT N'Cập nhật đặt món thành công';
END;
GO

-- Cập nhật Dữ liệu Chi Tiết Đặt Món
CREATE PROCEDURE SP_Update_ChiTietDatMon
    @MaDatMon VARCHAR(10),
    @MaMonAn VARCHAR(10),
    @TenMonAn NVARCHAR(255),
    @SoLuong INT,
    @GhiChu NVARCHAR(MAX)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại chi tiết đặt món
    IF NOT EXISTS (SELECT 1 FROM ChiTietDatMon WHERE MaDatMon = @MaDatMon AND MaMonAn = @MaMonAn)
    BEGIN
        RAISERROR(N'Chi tiết đặt món không tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật chi tiết đặt món
    UPDATE ChiTietDatMon
    SET MaMonAn = @MaMonAn,
		TenMonAn = @TenMonAn,
        SoLuong = @SoLuong,
        GhiChu = @GhiChu
    WHERE MaDatMon = @MaDatMon AND MaMonAn = @MaMonAn;

    PRINT N'Cập nhật chi tiết đặt món thành công';
END;
GO

-- Cập nhật Dữ liệu Hóa Đơn (Sửa lại)
CREATE PROCEDURE SP_Update_HoaDon
    @MaHoaDon VARCHAR(10),
    @MaDatMon VARCHAR(10),
    @NgayLap DATETIME,
    @PhuongThucThanhToan NVARCHAR(MAX)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại hóa đơn
    IF NOT EXISTS (SELECT 1 FROM HoaDon WHERE MaHoaDon = @MaHoaDon)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Cập nhật hóa đơn
    UPDATE HoaDon
    SET	MaDatMon = @MaDatMon,
		NgayLap = @NgayLap,
		PhuongThucThanhToan = @PhuongThucThanhToan
    WHERE MaHoaDon = @MaHoaDon;

    PRINT N'Cập nhật hóa đơn thành công';
END;
GO

--Cập nhật trạng thái duyệt thực đơn khi khách hàng đồng ý đặt 'Đang đợi duyệt' (Dùng khi khách hàng ấn nút 'Đặt món')
CREATE PROCEDURE SP_Update_DuyetThucDon_DangDoiDuyet 
	@MaKhachHang varchar(10)
AS
BEGIN
	UPDATE DuyetThucDon
	set TrangThai = N'Đang đợi duyệt'
	where MaKhachHang = @MaKhachHang and TrangThai = N'Chưa đặt' 
	PRINT(N'Đã đặt món thành công, xin vui lòng chờ admin hoặc nhân viên duyệt')
END
GO

--Cập nhật trạng thái duyệt thực đơn khi nhân viên duyệt 'Đã duyệt' (Dùng khi nhân viên ấn nút 'Duyệt thông tin')
CREATE PROC SP_Update_DuyetThucDon_DaDuyet
	@MaKhachHang varchar(10)
AS
BEGIN
	UPDATE DuyetThucDon
	set TrangThai = N'Đã duyệt'
	where MaKhachHang = @MaKhachHang and TrangThai = N'Đang đợi duyệt'
	PRINT(N'Đã duyệt vào hoá đơn thành công!')
END
GO

--Cập nhật số lượng món theo ý muốn khách hàng
CREATE PROCEDURE SP_Update_DuyetThucDon_SoLuong 
	@MaKhachHang varchar(10),
	@MaMonAn varchar(10),
	@Soluong int
AS
BEGIN
	SET NOCOUNT ON

	UPDATE DuyetThucDon
	SET Soluong = @Soluong
	WHERE MaKhachHang = @MaKhachHang AND MaMonAn = @MaMonAn 
	
	-- Sửa số lượng duyệt thực đơn
	PRINT(N'Đã sửa số lượng món ăn cần đặt thành công')
END
GO
----------------------------------------------------

----------------- Delete ---------------------------
-- Xóa Khách Hàng 
CREATE PROCEDURE SP_Delete_KhachHang
    @MaKhachHang VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại khách hàng
    IF NOT EXISTS (SELECT 1 FROM KhachHang WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        RAISERROR(N'Khách hàng không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa khách hàng
    DELETE FROM KhachHang
    WHERE MaKhachHang = @MaKhachHang;

    PRINT N'Xóa khách hàng thành công';
END;
GO

-- Thủ tục xoá tài khoản
CREATE PROCEDURE SP_Delete_TaiKhoan
	@TenTaiKhoan varchar(50)
AS
BEGIN
	-- Lấy sessionID của tài khoản người dùng
    DECLARE @SessionID INT;
    SELECT @SessionID = session_id
    FROM sys.dm_exec_sessions
    WHERE login_name = @TenTaiKhoan;

	-- Thực hiện ngắt kết nối người dùng
    IF @SessionID IS NOT NULL
    BEGIN
        -- Ngắt kết nối của người dùng
        EXEC('KILL ' + @SessionID);
    END

	-- Xoá User session còn tồn động
	EXEC SP_Delete_UserSession @TenTaiKhoan

	-- Xoá tài khoản login và user
    EXEC sp_dropuser @TenTaiKhoan;
    EXEC sp_droplogin @TenTaiKhoan;
    DELETE FROM TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan;

	PRINT N'Xoá tài khoản người dùng thành công'
END
GO

-- Xóa Nhân Viên 
CREATE PROCEDURE SP_Delete_NhanVien
    @MaNhanVien VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại nhân viên
    IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaNhanVien = @MaNhanVien)
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa nhân viên
    DELETE FROM NhanVien
    WHERE MaNhanVien = @MaNhanVien;

    PRINT N'Xóa nhân viên thành công';
END;
GO

-- Xóa Bàn 
CREATE PROCEDURE SP_Delete_Ban
    @MaBan VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại bàn
    IF NOT EXISTS (SELECT 1 FROM Ban WHERE MaBan = @MaBan)
    BEGIN
        RAISERROR(N'Bàn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa bàn
    DELETE FROM Ban
    WHERE MaBan = @MaBan;

    PRINT N'Xóa bàn thành công';
END;
GO

-- Xóa Đặt Bàn
CREATE PROCEDURE SP_Delete_DatBan
    @MaDatBan VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra mã đặt bàn có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM DatBan WHERE MaDatBan = @MaDatBan)
    BEGIN
        RAISERROR(N'Mã đặt bàn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa đặt bàn
    DELETE FROM DatBan
    WHERE MaDatBan = @MaDatBan;

    PRINT N'Xóa đặt bàn thành công';
END;
GO

-- Xóa Món Ăn 
CREATE PROCEDURE SP_Delete_MonAn
    @MaMonAn VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại món ăn
    IF NOT EXISTS (SELECT 1 FROM MonAn WHERE MaMonAn = @MaMonAn)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa món ăn
    DELETE FROM MonAn
    WHERE MaMonAn = @MaMonAn;

    PRINT N'Xóa món ăn thành công';
END;
GO

-- Xóa Đặt Món 
CREATE PROCEDURE SP_Delete_DatMon
    @MaDatMon VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại đặt món
    IF NOT EXISTS (SELECT 1 FROM DatMon WHERE MaDatMon = @MaDatMon)
    BEGIN
        RAISERROR(N'Đặt món không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa đặt món
    DELETE FROM DatMon
    WHERE MaDatMon = @MaDatMon;

    PRINT N'Xóa đặt món thành công';
END;
GO

-- Xóa Chi Tiết Đặt Món 
CREATE PROCEDURE SP_Delete_ChiTietDatMon
    @MaDatMon VARCHAR(10),
    @MaMonAn VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại chi tiết đặt món
    IF NOT EXISTS (SELECT 1 FROM ChiTietDatMon WHERE MaDatMon = @MaDatMon AND MaMonAn = @MaMonAn)
    BEGIN
        RAISERROR(N'Chi tiết đặt món không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa chi tiết đặt món
    DELETE FROM ChiTietDatMon
    WHERE MaDatMon = @MaDatMon AND MaMonAn = @MaMonAn;

    PRINT N'Xóa chi tiết đặt món thành công';
END;
GO

-- Xóa Hóa Đơn 
CREATE PROCEDURE SP_Delete_HoaDon
    @MaHoaDon VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra tồn tại hóa đơn
    IF NOT EXISTS (SELECT 1 FROM HoaDon WHERE MaHoaDon = @MaHoaDon)
    BEGIN
        RAISERROR(N'Hóa đơn không tồn tại!', 16, 1);
        RETURN;
    END

    -- Xóa hóa đơn
    DELETE FROM HoaDon
    WHERE MaHoaDon = @MaHoaDon;

    PRINT N'Xóa hóa đơn thành công';
END;
GO

--Xoá món ăn mà khách hàng không muốn
CREATE PROC SP_Delete_DuyetThucDon
	@MaKhachHang varchar(10),
	@MaMonAn varchar(10)
AS
BEGIN
	SET NOCOUNT ON;

    -- Kiểm tra tồn tại khách hàng
    IF NOT EXISTS (SELECT 1 FROM DuyetThucDon WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        RAISERROR(N'Khách hàng không tồn tại!', 16, 1);
        RETURN;
    END

    -- Kiểm tra tồn tại hóa đơn
    IF NOT EXISTS (SELECT 1 FROM DuyetThucDon WHERE MaMonAn = @MaMonAn)
    BEGIN
        RAISERROR(N'Món ăn không tồn tại!', 16, 1);
        RETURN;
    END

	-- Xóa duyệt đơn
	DELETE FROM DuyetThucDon 
	WHERE MaMonAn = @MaMonAn AND MaKhachHang = @MaKhachHang
	
	PRINT(N'Đã xoá duyệt thực đơn thành công')
END
GO

-- Xóa UserID
CREATE PROCEDURE SP_Delete_UserSession
    @UserID NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    -- Xóa user id
    DELETE FROM UserSession
    WHERE UserID = @UserID;
END;
GO
----------------------------------------------------

--------------- Thủ tục chức năng ------------------
CREATE PROCEDURE SP_LietKeHoaDon_TheoThang
WITH ENCRYPTION
AS 
BEGIN
	SELECT * FROM HoaDon
END
GO

-- Thủ tục tạo báo cáo hàng tháng
CREATE PROCEDURE SP_MonthlyReport
    @Month INT,
    @Year INT
WITH ENCRYPTION
AS
BEGIN
	BEGIN TRY
		DECLARE @TongTienThang DECIMAL(18, 2) = 0;
		DECLARE @TongTien DECIMAL(18, 2);

		DECLARE invoice_cursor CURSOR LOCAL FOR
		SELECT TongTien FROM HoaDon WHERE MONTH(NgayLap) = @Month AND YEAR(NgayLap) = @Year;

		OPEN invoice_cursor;
		FETCH NEXT FROM invoice_cursor INTO @TongTien;

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @TongTienThang = @TongTienThang + ISNULL(@TongTien, 0);
			FETCH NEXT FROM invoice_cursor INTO @TongTien;
		END;

		PRINT N'Tổng tiền hóa đơn trong tháng là: ' + CAST(@TongTienThang AS NVARCHAR(50)) + ' VND';

		CLOSE invoice_cursor;
		DEALLOCATE invoice_cursor;
	END TRY
	BEGIN CATCH          
			DECLARE @ErrorMessage NVARCHAR(MAX);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(N'Lỗi trong quá trình tính doanh thu: %s', 16, 1, @ErrorMessage);
	END CATCH
END
GO

-- Thủ tục kiểm tra và cập nhật trạng thái bàn
CREATE PROCEDURE SP_UpdateTableStatus
    @MaBan VARCHAR(10)
WITH ENCRYPTION
AS
BEGIN
    DECLARE @TrangThai NVARCHAR(MAX)
    SELECT @TrangThai = TrangThai FROM Ban WHERE MaBan = @MaBan;

    IF (@TrangThai = N'Trống')
    BEGIN
        UPDATE Ban SET TrangThai = N'Đã đặt' WHERE MaBan = @MaBan;
    END
    ELSE
    BEGIN
        PRINT 'Bàn đã được đặt trước đó.'
    END
END
GO

-- Kiểm tra bảng đã tồn tại
CREATE PROC SP_KiemTra_BangTonTai @tableName NVARCHAR(MAX)
WITH ENCRYPTION
AS
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @tableName) BEGIN SELECT 0 END ELSE BEGIN SELECT 1 END
GO
----------------------------------------------------

------------------ Permissions ---------------------
-- Quyền đối tượng (Object Permissions): Là quyền được gán cho các đối tượng trong cơ sở dữ liệu 
-- như bảng, view, stored procedure, và các đối tượng khác. Một số quyền phổ biến bao gồm:
-- SELECT: Quyền truy vấn dữ liệu từ bảng hoặc view.
-- INSERT: Quyền thêm dữ liệu vào bảng.
-- UPDATE: Quyền cập nhật dữ liệu trong bảng.
-- DELETE: Quyền xóa dữ liệu khỏi bảng.
-- EXECUTE: Quyền thực thi stored procedure hoặc function.

-- Quyền cấp độ cơ sở dữ liệu (Database-Level Permissions): Là quyền được gán cho cơ sở dữ liệu như CREATE TABLE, ALTER, DROP, và VIEW DATABASE.

-- Quyền đối tượng:
-- SELECT: Truy vấn dữ liệu.
-- INSERT: Thêm dữ liệu.
-- UPDATE: Cập nhật dữ liệu.
-- DELETE: Xóa dữ liệu.
-- EXECUTE: Thực thi procedure hoặc function.

-- Quyền cấp độ cơ sở dữ liệu:
-- CREATE DATABASE: Tạo cơ sở dữ liệu mới.
-- ALTER DATABASE: Thay đổi cấu hình của cơ sở dữ liệu.
-- DROP DATABASE: Xóa cơ sở dữ liệu.
-- CREATE TABLE: Tạo bảng mới trong cơ sở dữ liệu.
-- ALTER: Thay đổi cấu trúc của bảng.
-- DROP: Xóa bảng.

-- Thủ tục để tạo User
ALTER PROCEDURE SP_Create_User
    @LoginName NVARCHAR(50),
    @Password NVARCHAR(50),
    @Database NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem login đã tồn tại chưa
        IF EXISTS (SELECT * FROM sys.server_principals WHERE name = @LoginName)
        BEGIN
            RAISERROR(N'User đã tồn tại!', 16, 1);
            RETURN;
        END

        -- Tạo login mới
        SET @SQL = 'CREATE LOGIN [' + @LoginName + '] WITH PASSWORD = ''' + @Password + ''';';
        EXEC sp_executesql @SQL;

		WAITFOR DELAY '00:00:7';
		
        -- Tạo user trong cơ sở dữ liệu cụ thể
        SET @SQL = 'USE [' + @Database + ']; CREATE USER [' + @LoginName + '] FOR LOGIN [' + @LoginName + '];';
        EXEC sp_executesql @SQL;

		WAITFOR DELAY '00:00:7';
		
        -- Gán quyền cho user
        SET @SQL = 'GRANT EXECUTE ON SP_Insert_UserSession TO [' + @LoginName + '];';
        EXEC sp_executesql @SQL;

        SET @SQL = 'GRANT EXECUTE ON SP_Select_UserSession TO [' + @LoginName + '];';
        EXEC sp_executesql @SQL;

        SET @SQL = 'GRANT EXECUTE ON SP_Delete_UserSession TO [' + @LoginName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction nếu tất cả các bước đều thành công
        COMMIT TRANSACTION;
        PRINT N'Tạo user thành công trong cơ sở dữ liệu ' + @Database + '!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để xóa user
CREATE PROCEDURE SP_Delete_User
    @LoginName NVARCHAR(50),
    @Database NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem login có tồn tại hay không
        IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = @LoginName)
        BEGIN
            RAISERROR(N'Login không tồn tại!', 16, 1);
            RETURN;
        END

        -- Xóa user khỏi cơ sở dữ liệu cụ thể
        SET @SQL = 'USE [' + @Database + ']; DROP USER [' + @LoginName + '];';
        EXEC sp_executesql @SQL;

        -- Xóa login khỏi SQL Server
        SET @SQL = 'DROP LOGIN [' + @LoginName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Xóa user và login thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để gán quyền cho user
CREATE PROCEDURE SP_Grant_BatKyChoUser
    @UserName NVARCHAR(50),
    @Quyen NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem user có tồn tại trong cơ sở dữ liệu không
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
        BEGIN
            RAISERROR('User không tồn tại trong cơ sở dữ liệu!', 16, 1);
            RETURN;
        END

        -- Gán quyền cho user
        SET @SQL = 'GRANT ' + @Quyen + ' TO [' + @UserName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Đã gán quyền ' + @Quyen + ' cho user ' + @UserName + N' thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để thu hồi quyền của user
CREATE PROCEDURE SP_Revoke_BatKyChoUser
    @UserName NVARCHAR(50),
    @Quyen NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem user có tồn tại hay không
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
        BEGIN
            RAISERROR(N'User không tồn tại trong cơ sở dữ liệu!', 16, 1);
            RETURN;
        END

        -- Thu hồi quyền của user
        SET @SQL = 'REVOKE ' + @Quyen + ' FROM [' + @UserName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Đã thu hồi quyền ' + @Quyen + N' từ user ' + @UserName + N' thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để gán quyền cho user trên đối tượng cụ thể
CREATE PROCEDURE SP_Grant_BatKyChoUser_TrenDoiTuong_CuThe
    @UserName NVARCHAR(50),
    @Permission NVARCHAR(50),
    @ObjectName NVARCHAR(50),
    @ObjectType NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Sql NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem User có tồn tại không
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
        BEGIN
            RAISERROR('User không tồn tại!', 16, 1);
            RETURN;
        END

        -- Kiểm tra xem đối tượng có tồn tại không
        IF @ObjectType = 'TABLE' OR @ObjectType = 'VIEW'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = @ObjectName AND type IN ('U', 'V'))
            BEGIN
                RAISERROR(N'Đối tượng không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'PROCEDURE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = @ObjectName AND type = 'P')
            BEGIN
                RAISERROR(N'Stored Procedure không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE
        BEGIN
            RAISERROR(N'Loại đối tượng không hợp lệ!', 16, 1);
            RETURN;
        END

        -- Tạo câu lệnh SQL để cấp quyền
        SET @Sql = 'GRANT ' + @Permission + ' ON ' + @ObjectType + '::' + @ObjectName + ' TO ' + @UserName;

        -- Thực thi câu lệnh SQL
        EXEC sp_executesql @Sql;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Quyền ' + @Permission + N' đã được cấp cho user ' + @UserName + N' trên ' + @ObjectName;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để thu hồi quyền của user trên đối tượng cụ thể
CREATE PROCEDURE SP_Revoke_BatKyChoUser_TrenDoiTuong_CuThe
    @UserName NVARCHAR(50),
    @Permission NVARCHAR(50),
    @ObjectName NVARCHAR(255),
    @ObjectType NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem user có tồn tại trong cơ sở dữ liệu không
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
        BEGIN
            RAISERROR(N'User không tồn tại trong cơ sở dữ liệu!', 16, 1);
            RETURN;
        END

        -- Kiểm tra xem đối tượng có tồn tại không
        IF @ObjectType = 'TABLE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = @ObjectName)
            BEGIN
                RAISERROR(N'Bảng không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'PROCEDURE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = @ObjectName)
            BEGIN
                RAISERROR(N'Stored procedure không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'FUNCTION'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = @ObjectName AND type IN ('FN', 'IF', 'TF'))
            BEGIN
                RAISERROR(N'Hàm không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE
        BEGIN
            RAISERROR(N'Loại đối tượng không hợp lệ!', 16, 1);
            RETURN;
        END

        -- Thu hồi quyền từ user
        SET @SQL = 'REVOKE ' + @Permission + ' ON [' + @ObjectName + '] FROM [' + @UserName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Quyền ' + @Permission + N' đã được thu hồi từ user ' + @UserName + N' trên đối tượng ' + @ObjectName;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để gán quyền cụ thể cho user trên đối tượng cụ thể
CREATE PROCEDURE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe
    @UserName NVARCHAR(50),
    @Quyen NVARCHAR(50),
    @ObjectName NVARCHAR(255),
    @ObjectType NVARCHAR(50)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem user có tồn tại trong cơ sở dữ liệu không
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
        BEGIN
            RAISERROR(N'User không tồn tại trong cơ sở dữ liệu!', 16, 1);
            RETURN;
        END

        -- Kiểm tra xem đối tượng có tồn tại không
        IF @ObjectType = 'TABLE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = @ObjectName)
            BEGIN
                RAISERROR(N'Bảng không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'PROCEDURE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = @ObjectName)
            BEGIN
                RAISERROR(N'Stored procedure không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'FUNCTION'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = @ObjectName AND type IN ('FN', 'IF', 'TF'))
            BEGIN
                RAISERROR(N'Hàm không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE
        BEGIN
            RAISERROR(N'Loại đối tượng không hợp lệ!', 16, 1);
            RETURN;
        END

        -- Gán quyền cho user
        SET @SQL = 'GRANT ' + @Quyen + ' ON [' + @ObjectName + '] TO [' + @UserName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Đã gán quyền ' + @Quyen + N' cho user ' + @UserName + N' trên đối tượng ' + @ObjectName + N' thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

-- Thủ tục để thu hồi quyền cụ thể từ user trên đối tượng cụ thể
CREATE PROCEDURE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe
    @UserName NVARCHAR(50),
    @Quyen NVARCHAR(50),
    @ObjectName NVARCHAR(255),
    @ObjectType NVARCHAR(50) -- VD: 'TABLE', 'PROCEDURE', 'FUNCTION', ...
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Kiểm tra xem user có tồn tại trong cơ sở dữ liệu không
        IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = @UserName)
        BEGIN
            RAISERROR(N'User không tồn tại trong cơ sở dữ liệu!', 16, 1);
            RETURN;
        END

        -- Kiểm tra xem đối tượng có tồn tại không
        IF @ObjectType = 'TABLE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = @ObjectName)
            BEGIN
                RAISERROR(N'Bảng không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'PROCEDURE'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = @ObjectName)
            BEGIN
                RAISERROR(N'Stored procedure không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE IF @ObjectType = 'FUNCTION'
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name = @ObjectName AND type IN ('FN', 'IF', 'TF'))
            BEGIN
                RAISERROR(N'Hàm không tồn tại!', 16, 1);
                RETURN;
            END
        END
        ELSE
        BEGIN
            RAISERROR(N'Loại đối tượng không hợp lệ!', 16, 1);
            RETURN;
        END

        -- Thu hồi quyền từ user
        SET @SQL = 'REVOKE ' + @Quyen + ' ON [' + @ObjectName + '] FROM [' + @UserName + '];';
        EXEC sp_executesql @SQL;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Đã thu hồi quyền ' + @Quyen + N' từ user ' + @UserName + N' trên đối tượng ' + @ObjectName + N' thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO

CREATE PROCEDURE SP_XemQuyenUser
	@UserName varchar(50)
WITH ENCRYPTION
AS
BEGIN
SELECT 
    princ.name AS UserName,
    perm.permission_name AS Permission,
    perm.state_desc AS State,
    obj.name AS ObjectName
FROM 
    sys.database_principals princ
LEFT JOIN 
    sys.database_permissions perm ON perm.grantee_principal_id = princ.principal_id
LEFT JOIN 
    sys.objects obj ON perm.major_id = obj.object_id
WHERE 
    princ.name = @UserName;
END​
----------------------------------------------------

---------------------------------------------------------------------------------------------------

------------------------- Tạo User để quản lý user trong database ---------------------------------
-- Tạo login cho ql_account
CREATE LOGIN ql_account
WITH PASSWORD = '123';
GO

-- Chọn database tương ứng
USE QL_NHAHANG; -- Thay thế lại cái tên db của mình cho đúng
GO

-- Tạo user cho ql_account
CREATE USER ql_account FOR LOGIN ql_account;
GO

-- Chọn database master
USE MASTER
GO

-- Gán quyền để tạo và xóa login cho ql_account
GRANT ALTER ANY LOGIN TO ql_account;
GO

-- Chọn database tương ứng
USE QL_NHAHANG
GO

-- Gán quyền để tạo và xóa user trong db cho ql_account
GRANT ALTER ANY USER TO ql_account;
GO

GRANT CONNECT TO ql_account
GO
GRANT EXECUTE ON SP_Create_User TO ql_account;
GO
GRANT EXECUTE ON SP_Grant_BatKyChoUser TO ql_account;
GO
GRANT EXECUTE ON SP_Grant_BatKyChoUser_TrenDoiTuong_CuThe TO ql_account;
GO
GRANT EXECUTE ON SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe TO ql_account;
GO
GRANT CONTROL ON SCHEMA::dbo TO ql_account;
GO

---- Kiểm tra quyền của user
--SELECT 
--    prin.name AS UserName,
--    perm.permission_name AS Permission,
--    perm.state_desc AS State,
--    obj.name AS ObjectName
--FROM 
--    sys.database_principals AS prin
--JOIN  
--    sys.database_permissions AS perm ON prin.principal_id = perm.grantee_principal_id
--LEFT JOIN 
--    sys.objects AS obj ON perm.major_id = obj.object_id
--WHERE 
--    prin.name = 'ql_account'; -- tên user muốn xem
--GO
---------------------------------------------------------------------------------------------------

--------------------------------------------- Insert Data Mẫu -------------------------------------------
-- Set kiểu thời gian theo dmy
SET DATEFORMAT DMY;
GO

-- Thêm khách hàng
EXEC SP_Insert_KhachHang N'Nguyễn Văn A', N'Nam', '0912345678', 'nguyenvana@example.com';
EXEC SP_Insert_KhachHang N'Phan Thị B', N'Nữ', '0912345679', 'phanthib@example.com';
EXEC SP_Insert_KhachHang N'Trần Minh C', N'Nam', '0912345680', 'tranminhc@example.com';
EXEC SP_Insert_KhachHang N'Lê Thị D', N'Nữ', '0912345681', 'lethid@example.com';
EXEC SP_Insert_KhachHang N'Hoàng Văn E', N'Nam', '0912345682', 'hoangvane@example.com';
GO

-- Thêm nhân viên
EXEC SP_Insert_NhanVien N'Trần Quang H', N'Nam', '0901234567', 'tranquangh@example.com', NULL;
EXEC SP_Insert_NhanVien N'Nguyễn Thị I', N'Nữ', '0901234568', 'nguyenhi@example.com', NULL;
EXEC SP_Insert_NhanVien N'Phan Hải K', N'Nam', '0901234569', 'phanhaik@example.com', NULL;
EXEC SP_Insert_NhanVien N'Lê Tân L', N'Nữ', '0901234570', 'letanl@example.com', NULL;
EXEC SP_Insert_NhanVien N'Hoàng Minh M', N'Nam', '0901234571', 'hoangminhm@example.com', NULL;
GO

-- Thêm bàn ăn
EXEC SP_Insert_Ban 4, N'Trống';
EXEC SP_Insert_Ban 6, N'Trống';
EXEC SP_Insert_Ban 2, N'Trống';
EXEC SP_Insert_Ban 8, N'Trống';
EXEC SP_Insert_Ban 6, N'Trống';
GO

-- Thêm đặt bàn
EXEC SP_Insert_DatBan 'BAN002', 'KH001', '10-12-2024 18:00', 4;
EXEC SP_Insert_DatBan 'BAN002', 'KH002', '10-12-2024 19:00', 6;
EXEC SP_Insert_DatBan 'BAN003', 'KH003', '10-12-2024 20:00', 2;
EXEC SP_Insert_DatBan 'BAN004', 'KH004', '10-12-2024 21:00', 4;
EXEC SP_Insert_DatBan 'BAN005', 'KH005', '10-12-2024 22:00', 6;
GO

-- Thêm món ăn
EXEC SP_Insert_MonAn N'Phở Bò', N'Món chính', 50000, N'Có sẵn';
EXEC SP_Insert_MonAn N'Gà Rán', N'Món chính', 70000, N'Có sẵn';
EXEC SP_Insert_MonAn N'Chè Đậu Đỏ', N'Dessert', 20000, N'Có sẵn';
EXEC SP_Insert_MonAn N'Nem Chua', N'Món nhậu', 30000, N'Có sẵn';
EXEC SP_Insert_MonAn N'Sushi', N'Món Nhật', 150000, N'Có sẵn';
GO

-- Thêm đặt món
EXEC SP_Insert_DatMon 'KH001', '10-12-2024 18:30';
EXEC SP_Insert_DatMon 'KH002', '10-12-2024 19:30';
EXEC SP_Insert_DatMon 'KH003', '10-12-2024 20:30';
EXEC SP_Insert_DatMon 'KH004', '10-12-2024 21:30';
EXEC SP_Insert_DatMon 'KH005', '10-12-2024 22:30';
GO

-- Thêm chi tiết đặt món
EXEC SP_Insert_ChiTietDatMon 'DM001', 'MA001', N'Phở Bò', 2, N'';
EXEC SP_Insert_ChiTietDatMon 'DM001', 'MA003', N'Chè Đậu Đỏ', 1, N'';
EXEC SP_Insert_ChiTietDatMon 'DM002', 'MA002', N'Gà Rán', 3, N'';
EXEC SP_Insert_ChiTietDatMon 'DM002', 'MA004', N'Nem Chua', 2, N'';
EXEC SP_Insert_ChiTietDatMon 'DM003', 'MA005', N'Sushi', 4, N'Món đặc biệt';
GO

-- Thêm hóa đơn
EXEC SP_Insert_HoaDon 'DM001', '10-12-2024 18:45', N'Tiền mặt';
EXEC SP_Insert_HoaDon 'DM002', '10-12-2024 19:45', N'Thẻ';
EXEC SP_Insert_HoaDon 'DM003', '10-12-2024 20:45', N'Chuyển khoản';
EXEC SP_Insert_HoaDon 'DM004', '10-12-2024 21:45', N'Tiền mặt';
EXEC SP_Insert_HoaDon 'DM005', '10-12-2024 22:45', N'Thẻ';
GO

---- Kiểm tra dữ liệu bảng KhachHang
--SELECT * FROM KhachHang
--GO

---- Kiểm tra dữ liệu bảng NhanVien
--SELECT * FROM NhanVien
--GO

---- Kiểm tra dữ liệu bảng Ban
--SELECT * FROM Ban
--GO

---- Kiểm tra dữ liệu bảng DatBan
--SELECT * FROM DatBan
--GO

---- Kiểm tra dữ liệu bảng PhucVuBan
--SELECT * FROM PhucVuBan
--GO

---- Kiểm tra dữ liệu bảng MonAn
--SELECT * FROM MonAn
--GO

---- Kiểm tra dữ liệu bảng DatMon
--SELECT * FROM DatMon
--GO

---- Kiểm tra dữ liệu bảng ChiTietDatMon
--SELECT * FROM ChiTietDatMon
--GO

---- Kiểm tra dữ liệu bảng HoaDon
--SELECT * FROM HoaDon
--GO
---------------------------------------------------------------------------------------------------

------------------------------------------- Tạo Function ------------------------------------------
-- FUNC - FUNCTION

----------------- Truy vấn dữ liệu -----------------
--------------------- Cơ bản -----------------------
-- Liệt kê đặt bàn theo ngày trong tương lai
CREATE FUNCTION FUNC_LietKe_DatBan_TheoNgay(@Ngay DATETIME)
RETURNS TABLE
AS
RETURN (
    SELECT * 
    FROM V_DatBan
    WHERE CAST(ThoiGian AS DATE) = @Ngay
    AND ThoiGian >= GETDATE()
)
GO

-- Liệt kê đặt bàn theo tháng trong tương lai
CREATE FUNCTION FUNC_LietKe_DatBan_TheoThang(@Thang INT, @Nam INT)
RETURNS TABLE
AS
RETURN (
    SELECT * 
    FROM V_DatBan
    WHERE MONTH(ThoiGian) = @Thang 
    AND YEAR(ThoiGian) = @Nam
    AND ThoiGian >= GETDATE()
)
GO

-- Lấy ra những bàn nào còn trống trong ngày hôm nay
CREATE FUNCTION FUNC_LietKe_BanTrong_HomNay()
RETURNS TABLE
AS
RETURN (
    SELECT Ban.*
    FROM Ban
    WHERE Ban.MaBan NOT IN (
        SELECT DatBan.MaBan
        FROM DatBan
        WHERE CAST(DatBan.ThoiGian AS DATE) = CAST(GETDATE() AS DATE)
    )
)
GO

-- Lấy ra những bàn nào được đặt trong ngày hôm nay
CREATE FUNCTION FUNC_LietKe_BanDat_HomNay()
RETURNS TABLE
AS
RETURN (
    SELECT Ban.*, DatBan.ThoiGian, DatBan.SoKhach, KhachHang.TenKhachHang
    FROM Ban
    INNER JOIN DatBan ON Ban.MaBan = DatBan.MaBan
    INNER JOIN KhachHang ON DatBan.MaKhachHang = KhachHang.MaKhachHang
    WHERE CAST(DatBan.ThoiGian AS DATE) = CAST(GETDATE() AS DATE)
)
GO

-- Hóa đơn theo ngày
CREATE FUNCTION FUNC_LietKeHoaDon_TheoNgay(@Ngay DATETIME)
RETURNS TABLE
AS
RETURN 
(
    SELECT * 
    FROM HoaDon
    WHERE CAST(NgayLap AS DATE) = @Ngay
)
GO

-- Hóa đơn theo tháng
CREATE FUNCTION FUNC_LietKeHoaDon_TheoThang(@Thang INT, @Nam INT)
RETURNS TABLE
AS
RETURN 
(
    SELECT * 
    FROM HoaDon
    WHERE MONTH(NgayLap) = @Thang AND YEAR(NgayLap) = @Nam
)
GO

-- Hóa đơn theo năm
CREATE FUNCTION FUNC_LietKeHoaDon_TheoNam(@Nam INT)
RETURNS TABLE
AS
RETURN 
(
    SELECT * 
    FROM HoaDon
    WHERE YEAR(NgayLap) = @Nam
)
GO

-- Liệt kê các món ăn có sẵn trong ngày
CREATE FUNCTION FUNC_LietKeMonAn_CoSan()
RETURNS TABLE
AS
RETURN 
(
    SELECT * 
    FROM MonAn
    WHERE TrangThai = N'Có sẵn'
)
GO

-- Liệt kê các món ăn chưa có sẵn trong ngày
CREATE FUNCTION FUNC_LietKeMonAn_ChuaCoSan()
RETURNS TABLE
AS
RETURN (
    SELECT * 
    FROM MonAn
    WHERE TrangThai != N'Có sẵn'
)
GO

-- Liệt kê các nhân viên có quản lý
CREATE FUNCTION FUNC_LietKeNhanVien_CoQuanLy()
RETURNS TABLE
AS
RETURN (
    SELECT NV1.*, NV2.TenNhanVien AS TenQuanLy
    FROM NhanVien NV1
    INNER JOIN NhanVien NV2 ON NV1.MaQuanLy = NV2.MaNhanVien
)
GO

-- Liệt kê các món ăn và tổng số lượng sử dụng theo ngày
CREATE FUNCTION FUNC_LietKeMonAn_TongSoLuong_TheoNgay(@Ngay DATETIME)
RETURNS TABLE
AS
RETURN (
    SELECT 
        CTDM.MaMonAn,
        CTDM.TenMonAn,
        SUM(CTDM.SoLuong) AS TongSoLuong,
        SUM(CTDM.ThanhTien) AS TongThanhTien,
        CAST(DM.NgayLap AS DATE) AS NgayLap
    FROM ChiTietDatMon CTDM
    INNER JOIN DatMon DM ON CTDM.MaDatMon = DM.MaDatMon
    WHERE CAST(DM.NgayLap AS DATE) = @Ngay
    GROUP BY 
        CTDM.MaMonAn, 
        CTDM.TenMonAn, 
        CAST(DM.NgayLap AS DATE)
		)
GO

-- Liệt kê các món ăn và tổng số lượng sử dụng theo tháng
CREATE FUNCTION FUNC_LietKeMonAn_TongSoLuong_TheoThang(@Thang INT, @Nam INT)
RETURNS TABLE
AS
RETURN (
    SELECT 
        CTDM.MaMonAn,
        CTDM.TenMonAn,
        SUM(CTDM.SoLuong) AS TongSoLuong,
        SUM(CTDM.ThanhTien) AS TongThanhTien,
        YEAR(DM.NgayLap) AS Nam,
        MONTH(DM.NgayLap) AS Thang
    FROM ChiTietDatMon CTDM
    INNER JOIN DatMon DM ON CTDM.MaDatMon = DM.MaDatMon
    WHERE MONTH(DM.NgayLap) = @Thang AND YEAR(DM.NgayLap) = @Nam
    GROUP BY 
        CTDM.MaMonAn, 
        CTDM.TenMonAn, 
        YEAR(DM.NgayLap), 
        MONTH(DM.NgayLap)
)
GO

-- Liệt kê các món ăn và tổng số lượng sử dụng theo năm
CREATE FUNCTION FUNC_LietKeMonAn_TongSoLuong_TheoNam(@Nam INT)
RETURNS TABLE
AS
RETURN (
    SELECT 
        CTDM.MaMonAn,
        CTDM.TenMonAn,
        SUM(CTDM.SoLuong) AS TongSoLuong,
        SUM(CTDM.ThanhTien) AS TongThanhTien,
        YEAR(DM.NgayLap) AS Nam
    FROM ChiTietDatMon CTDM
    INNER JOIN DatMon DM ON CTDM.MaDatMon = DM.MaDatMon
    WHERE YEAR(DM.NgayLap) = @Nam
    GROUP BY 
        CTDM.MaMonAn, 
        CTDM.TenMonAn, 
        YEAR(DM.NgayLap)
)
GO

----------------------------------------------------

-------------------- Nâng cao ----------------------
---- Truy vấn lồng tương quan (TSQL Tương quan) ----
-- Liệt kê các nhân viên có quản lý trực tiếp
CREATE FUNCTION FUNC_LietKeNhanVienCoQuanLyTrucTiep()
RETURNS TABLE
AS
RETURN (
    SELECT 
        NV1.MaNhanVien, 
        NV1.TenNhanVien, 
        NV1.GioiTinh, 
        NV1.SoDienThoai, 
        NV1.Email,
        NV2.TenNhanVien AS TenQuanLy
    FROM NhanVien NV1
    LEFT JOIN NhanVien NV2 ON NV1.MaQuanLy = NV2.MaNhanVien
    WHERE NV1.MaQuanLy IS NOT NULL
)
GO

-- Liệt kê các món ăn đã được đặt cùng số lượng theo từng khách hàng
CREATE FUNCTION FUNC_LietKe_MonAn_Theo_KhachHang (@MaKhachHang VARCHAR(10))
RETURNS TABLE
AS
RETURN (
    SELECT 
        MA.TenMonAn, 
        SUM(CTDM.SoLuong) AS TongSoLuong,
        DM.MaKhachHang
    FROM ChiTietDatMon CTDM
    INNER JOIN DatMon DM ON CTDM.MaDatMon = DM.MaDatMon
    INNER JOIN MonAn MA ON CTDM.MaMonAn = MA.MaMonAn
    WHERE DM.MaKhachHang = @MaKhachHang
    GROUP BY MA.TenMonAn, DM.MaKhachHang
)
GO

-- Liệt kê các khách hàng và tổng số tiền họ đã chi
CREATE FUNCTION FUNC_LietKe_KhachHang_Va_TongTienDaChi()
RETURNS TABLE
AS
RETURN (
    SELECT 
        KH.MaKhachHang, 
        KH.TenKhachHang,
        SUM(HD.TongTien) AS TongTienDaChi
    FROM KhachHang KH
    LEFT JOIN DatMon DM ON DM.MaKhachHang = KH.MaKhachHang
	LEFT JOIN HoaDon HD ON DM.MaDatMon = HD.MaDatMon
    GROUP BY KH.MaKhachHang, KH.TenKhachHang
)
GO

-- Liệt kê các món ăn đã hết (không có sẵn)
CREATE FUNCTION FUNC_LietKeMonAnHetHang()
RETURNS TABLE
AS
RETURN (
    SELECT 
        MA.MaMonAn, 
        MA.TenMonAn, 
        MA.LoaiMon, 
        MA.DonGia 
    FROM MonAn MA
    WHERE MA.TrangThai = N'Không có sẵn'
)
GO

-- Liệt kê các khách hàng đã đặt bàn nhiều nhất
CREATE FUNCTION FUNC_KhachHang_DatBan_NhieuNhat()
RETURNS TABLE
AS
RETURN (
    SELECT 
        KH.MaKhachHang, 
        KH.TenKhachHang, 
        COUNT(DB.MaBan) AS SoLanDatBan
    FROM KhachHang KH
    INNER JOIN DatBan DB ON KH.MaKhachHang = DB.MaKhachHang
    GROUP BY KH.MaKhachHang, KH.TenKhachHang
    HAVING COUNT(DB.MaBan) = (SELECT MAX(SoLanDatBan) 
                              FROM (SELECT COUNT(DB2.MaBan) AS SoLanDatBan
                                    FROM DatBan DB2
                                    GROUP BY DB2.MaKhachHang) AS Temp)
)
GO

-- Liệt kê các món ăn được đặt nhiều nhất trong tháng
CREATE FUNCTION FUNC_MonAnDatNhieuNhatTrongThang(@Thang INT, @Nam INT)
RETURNS TABLE
AS
RETURN (
    SELECT 
        MA.TenMonAn, 
        SUM(CTDM.SoLuong) AS TongSoLuong
    FROM MonAn MA
    INNER JOIN ChiTietDatMon CTDM ON MA.MaMonAn = CTDM.MaMonAn
    INNER JOIN DatMon DM ON CTDM.MaDatMon = DM.MaDatMon
    WHERE MONTH(DM.NgayLap) = @Thang AND YEAR(DM.NgayLap) = @Nam
    GROUP BY MA.TenMonAn
    HAVING SUM(CTDM.SoLuong) = (SELECT MAX(TongSoLuong) 
                                FROM (SELECT SUM(CTDM2.SoLuong) AS TongSoLuong
                                      FROM ChiTietDatMon CTDM2
                                      INNER JOIN DatMon DM2 ON CTDM2.MaDatMon = DM2.MaDatMon
                                      WHERE MONTH(DM2.NgayLap) = @Thang AND YEAR(DM2.NgayLap) = @Nam
                                      GROUP BY CTDM2.MaMonAn) AS Temp)
)
GO

-- Liệt kê các khách hàng chưa bao giờ đặt bàn
CREATE FUNCTION FUNC_KhachHangChuaDatBan()
RETURNS TABLE
AS
RETURN (
    SELECT 
        KH.MaKhachHang, 
        KH.TenKhachHang, 
        KH.SoDienThoai, 
        KH.Email
    FROM KhachHang KH
    WHERE NOT EXISTS (
        SELECT 1
        FROM DatBan DB
        WHERE KH.MaKhachHang = DB.MaKhachHang
    )
)
GO

-- Liệt kê các món ăn chưa bao giờ được đặt
CREATE FUNCTION FUNC_MonAnChuaDuocDat()
RETURNS TABLE
AS
RETURN (
    SELECT 
        MA.MaMonAn, 
        MA.TenMonAn, 
        MA.LoaiMon, 
        MA.DonGia
    FROM MonAn MA
    WHERE NOT EXISTS (
        SELECT 1
        FROM ChiTietDatMon CTDM
        WHERE MA.MaMonAn = CTDM.MaMonAn
    )
)
GO

-- Liệt kê các khách hàng có tổng số tiền đã chi lớn hơn mức trung bình
CREATE FUNCTION FUNC_KhachHang_ChiNhieuHonTrungBinh()
RETURNS TABLE
AS
RETURN (
    SELECT 
        KH.MaKhachHang, 
        KH.TenKhachHang, 
        SUM(HD.TongTien) AS TongTienDaChi
    FROM KhachHang KH
    INNER JOIN DatMon DM ON KH.MaKhachHang = DM.MaKhachHang
	INNER JOIN HoaDon HD ON HD.MaDatMon = DM.MaDatMon
    GROUP BY KH.MaKhachHang, KH.TenKhachHang
    HAVING SUM(HD.TongTien) > (SELECT AVG(TongTien) FROM HoaDon)
)
GO

-- Liệt kê các món ăn có giá cao hơn mức trung bình
CREATE FUNCTION FUNC_MonAnGiaCaoHonTrungBinh()
RETURNS TABLE
AS
RETURN (
    SELECT 
        MA.MaMonAn, 
        MA.TenMonAn, 
        MA.DonGia
    FROM MonAn MA
    WHERE MA.DonGia > (SELECT AVG(DonGia) FROM MonAn)
)
GO

------------------------------------------------
---- Truy vấn lồng phân cấP (TSQL phân cấp) ----
-- Lấy ra danh sách tất cả các nhân viên và các nhân viên quản lý họ
CREATE FUNCTION FUNC_LietKeNhanVienQuanLy()
RETURNS TABLE
AS
RETURN (
    WITH NhanVienQuanLyCTE (MaNhanVien, TenNhanVien, MaQuanLy, CapDo) AS
    (
        SELECT 
            NV.MaNhanVien, 
            NV.TenNhanVien, 
            NV.MaQuanLy,
            1 AS CapDo
        FROM NhanVien NV
        WHERE NV.MaQuanLy IS NULL

        UNION ALL

        SELECT 
            NV.MaNhanVien, 
            NV.TenNhanVien, 
            NV.MaQuanLy,
            CapDo + 1
        FROM NhanVien NV
        INNER JOIN NhanVienQuanLyCTE QL ON NV.MaQuanLy = QL.MaNhanVien
    )
    SELECT * FROM NhanVienQuanLyCTE
)
GO

-- Liệt kê các bàn được đặt trong ngày hôm nay cùng với khách hàng đặt
CREATE FUNCTION FUNC_LietKeBanDatHomNay()
RETURNS TABLE
AS
RETURN (
    SELECT 
        B.MaBan, 
        B.SoGhe, 
        DB.MaDatBan, 
        DB.MaKhachHang, 
        KH.TenKhachHang,
        DB.ThoiGian
    FROM Ban B
    INNER JOIN DatBan DB ON B.MaBan = DB.MaBan
    INNER JOIN KhachHang KH ON DB.MaKhachHang = KH.MaKhachHang
    WHERE CAST(DB.ThoiGian AS DATE) = CAST(GETDATE() AS DATE)
)
GO

-- Liệt kê các hóa đơn và món ăn được đặt cho từng khách hàng
CREATE FUNCTION FUNC_LietKe_HoaDon_Va_MonAn()
RETURNS TABLE
AS
RETURN (
    SELECT 
        HD.MaHoaDon,
		DM.MaKhachHang,
		KH.TenKhachHang,
        HD.NgayLap, 
        MA.TenMonAn,
        CTDM.SoLuong,
        CTDM.ThanhTien
    FROM HoaDon HD
    INNER JOIN DatMon DM ON HD.MaDatMon = DM.MaDatMon
	INNER JOIN KhachHang KH ON KH.MaKhachHang = DM.MaKhachHang
    INNER JOIN ChiTietDatMon CTDM ON DM.MaDatMon = CTDM.MaDatMon
    INNER JOIN MonAn MA ON CTDM.MaMonAn = MA.MaMonAn
)
GO

------------------------------------------------

----------------------------------------------------

---------------------------------------------------------------------------------------------------

------------------------------ Hàm, thủ tục cấu trúc rẽ nhánh, lặp, đệ quy -----------------------------------
-- Thủ tục xử lý thanh toán
CREATE PROCEDURE SP_XuLyThanhToan
    @MaHoaDon VARCHAR(10),
    @PhuongThucThanhToan NVARCHAR(MAX)
AS
BEGIN
    DECLARE @TongTien DECIMAL(18,2);

    -- Lấy tổng tiền từ hóa đơn
    SELECT @TongTien = TongTien FROM HoaDon WHERE MaHoaDon = @MaHoaDon;

    -- Kiểm tra tổng tiền có tồn tại hay không
    IF @TongTien IS NULL
    BEGIN
        PRINT 'Hóa đơn không tồn tại';
        RETURN;
    END

    -- Xử lý theo phương thức thanh toán
    IF @PhuongThucThanhToan = N'Tiền mặt'
    BEGIN
        PRINT 'Khách thanh toán bằng tiền mặt. Tổng tiền: ' + CAST(@TongTien AS NVARCHAR(MAX));
    END
    ELSE IF @PhuongThucThanhToan = N'Thẻ'
    BEGIN
        PRINT 'Khách thanh toán bằng thẻ. Tổng tiền: ' + CAST(@TongTien AS NVARCHAR(MAX));
    END
    ELSE
    BEGIN
        PRINT 'Phương thức thanh toán không hợp lệ';
    END
END
GO

-- Thủ tục cập nhật giá món ăn theo phần trăm
CREATE PROCEDURE SP_LietKeMonAnTheoLoai
    @LoaiMon NVARCHAR(MAX)
AS
BEGIN
    SELECT MaMonAn, TenMonAn, LoaiMon, DonGia,
        CASE
            WHEN TrangThai = N'Có sẵn' THEN 'Đang bán'
            ELSE 'Hết hàng'
        END AS TinhTrang
    FROM MonAn
    WHERE LoaiMon = @LoaiMon;
END
GO

-- Thủ tục cập nhật giá món ăn theo phần trăm
CREATE PROCEDURE SP_CapNhatGiaMonAn
    @PhanTramTang DECIMAL(5,2)
AS
BEGIN
    DECLARE @i INT = 1;
    DECLARE @SoLuongMon INT = (SELECT COUNT(*) FROM MonAn);
    DECLARE @MaMonAn VARCHAR(10);

    -- Duyệt qua tất cả các món ăn theo vòng lặp WHILE
    WHILE @i <= @SoLuongMon
    BEGIN
        -- Lấy mã món ăn hiện tại theo chỉ số @i
        SET @MaMonAn = (SELECT MaMonAn 
                        FROM (SELECT ROW_NUMBER() OVER (ORDER BY MaMonAn) AS RowNum, MaMonAn 
                              FROM MonAn) AS Temp
                        WHERE RowNum = @i);

        -- Cập nhật giá món ăn dựa trên mã món ăn
        UPDATE MonAn
        SET DonGia = DonGia + (DonGia * @PhanTramTang / 100)
        WHERE MaMonAn = @MaMonAn;

        -- Tăng chỉ số vòng lặp
        SET @i = @i + 1;
    END

    PRINT 'Đã cập nhật giá của tất cả các món ăn theo phần trăm tăng: ' + CAST(@PhanTramTang AS NVARCHAR(10)) + '%';
END
GO

-- Hàm kiểm tra trạng thái món ăn
CREATE FUNCTION FUNC_KiemTraTrangThaiMonAn(@MaMonAn VARCHAR(10))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @TrangThai NVARCHAR(MAX);

    SELECT @TrangThai = CASE
        WHEN TrangThai = N'Có sẵn' THEN N'Món ăn đang có sẵn'
        ELSE N'Món ăn đã hết'
    END
    FROM MonAn
    WHERE MaMonAn = @MaMonAn;

    RETURN @TrangThai;
END
GO
------------------------------------------- Xử lý bcp ---------------------------------------------
---- Cú pháp
---- Export dữ liệu từ SQL Server vào tệp:
---- bcp <Database>.<Schema>.<Table> out <FilePath> -S <ServerName> -U <UserName> -P <Password> -c

---- Import dữ liệu từ tệp vào SQL Server:
---- bcp <Database>.<Schema>.<Table> in <FilePath> -S <ServerName> -U <UserName> -P <Password> -c

---- Ví dụ

---- Export Dữ Liệu Từ SQL Server Vào Tệp
---- bcp QL_NHAHANG.dbo.KhachHang out "C:\Backup\khachhang_data.txt" -S <ServerName> -U <UserName> -P <Password> -c
---- Giải thích
---- QL_NHAHANG.dbo.KhachHang: Tên bảng bạn muốn xuất dữ liệu.
---- out: Chỉ thị xuất dữ liệu ra tệp.
---- "C:\Backup\khachhang_data.txt": Đường dẫn tới tệp xuất.
---- -S <ServerName>: Tên máy chủ SQL Server.
---- -U <UserName> và -P <Password>: Tên người dùng và mật khẩu kết nối SQL Server.
---- -c: Sử dụng kiểu dữ liệu character (dữ liệu dưới dạng văn bản, mỗi cột tách biệt bằng dấu tab).

---- Import Dữ Liệu Từ Tệp Vào SQL Server
---- bcp QL_NHAHANG.dbo.KhachHang in "C:\Backup\khachhang_data.txt" -S <ServerName> -U <UserName> -P <Password> -c
---- QL_NHAHANG.dbo.KhachHang: Tên bảng bạn muốn nhập dữ liệu vào.
---- in: Chỉ thị nhập dữ liệu từ tệp.
---- "C:\Backup\khachhang_data.txt": Đường dẫn tới tệp nhập.
---- -S <ServerName>, -U <UserName>, -P <Password>: Thông tin kết nối SQL Server.
---- -c: Dữ liệu nhập dưới dạng ký tự (characters).

---- Sử Dụng BCP Trong T-SQL với xp_cmdshell
---- Bật xp_cmdshell (Cẩn thận khi bật tính năng này vì có thể gây rủi ro bảo mật)
---- EXEC sp_configure 'show advanced options', 1;
---- RECONFIGURE;
---- EXEC sp_configure 'xp_cmdshell', 1;
---- RECONFIGURE;

---- Export Dữ Liệu (Gọi BCP Từ T-SQL):
--EXEC xp_cmdshell 'bcp QL_NHAHANG.dbo.KhachHang out "C:\Backup\khachhang_data.txt" -S <ServerName> -U <UserName> -P <Password> -c';

---- Import Dữ Liệu (Gọi BCP Từ T-SQL):
--EXEC xp_cmdshell 'bcp QL_NHAHANG.dbo.KhachHang in "C:\Backup\khachhang_data.txt" -S <ServerName> -U <UserName> -P <Password> -c';

---- Tùy chọn khác
---- -t: Chỉ định ký tự phân tách cột. Ví dụ: -t, để sử dụng dấu phẩy như phân tách.
---- -w: Chỉ định mã hóa Unicode (UTF-16LE)
---- -r: Chỉ định ký tự phân tách dòng (ví dụ: -r\n cho phân tách theo dòng mới).
---- -F: Chỉ định dòng bắt đầu để xuất nhập (dùng cho việc xuất nhập từ một dòng cụ thể).
---- -L: Chỉ định dòng kết thúc để xuất nhập (dùng cho việc xuất nhập đến một dòng cụ thể).

---- Export
---- bcp QL_NHAHANG.dbo.KhachHang out "C:\Backup\khachhang_data.csv" -S localhost -U sa -P mypassword -c -t, -r\n
---- Import
---- bcp QL_NHAHANG.dbo.KhachHang in "C:\Backup\khachhang_data.csv" -S localhost -U sa -P mypassword -c -t, -r\n

-- Bật các tùy chọn cấu hình nâng cao
EXEC sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO

-- Kích hoạt xp_cmdshell
EXEC sp_configure 'xp_cmdshell', 1;
GO
RECONFIGURE;
GO

/*
-- BCP Export dữ liệu
CREATE PROCEDURE SP_BCP_Export
    @TableName VARCHAR(128),         -- Tên bảng cần xuất
    @FilePath VARCHAR(255),          -- Đường dẫn và tên file .txt (hoặc .csv)
    @ServerName VARCHAR(128) = @@SERVERNAME, -- Tên server mặc định
    @Username VARCHAR(128),   -- Tên đăng
    @Password VARCHAR(128)    -- Mật khẩu
WITH ENCRYPTION
AS
BEGIN
    DECLARE @BCPCommand VARCHAR(255);

    -- Tạo lệnh BCP để xuất dữ liệu từ bảng ra file .txt
    SET @BCPCommand = 'bcp QL_NHAHANG.dbo.' + @TableName + 
					  ' out "' + @FilePath + 
                      '" -S ' + @ServerName + 
                      ' -U ' + @Username + 
					  ' -P ' + @Password + 
                      ' -w -t, -r\n';

    -- Thực thi lệnh BCP qua xp_cmdshell
    EXEC xp_cmdshell @BCPCommand;

	PRINT N'Dữ liệu đã được xuất thành công từ bảng ' + @TableName + ' vào file ' + @FilePath;
END
GO

--EXEC SP_BCP_Export
--    @TableName = 'KhachHang', 
--    @FilePath = 'D:\Code Visual\.NET C# FORM\FILE CSV\khachang_data.csv', 
--    @ServerName = 'DESKTOP-VPHJT4U\SQLEXPRESS', 
--    @Username = 'sa',
--    @Password = '123456789';
--GO
*/

/*
-- Thực hiện import dữ liệu
CREATE PROCEDURE SP_Import
	@TableName VARCHAR(255),         -- Tên bảng cần nhập
    @FilePath VARCHAR(255),          -- Đường dẫn và tên file .csv
    @ServerName VARCHAR(255) = @@SERVERNAME, -- Tên server mặc định
    @Username VARCHAR(128),          -- Tên đăng nhập
    @Password VARCHAR(128),           -- Mật khẩu
	@PrimaryKeyColumn NVARCHAR(128) -- Cột khóa chính dùng để kiểm tra trùng lặp (ghi đè)
WITH ENCRYPTION
AS
BEGIN
    BEGIN TRY
		-- Kiểm tra bảng tạm nếu chưa tồn tại, thì tạo bảng tạm với cùng cấu trúc như bảng chính
		IF OBJECT_ID('Temp_' + @TableName) IS NULL
		BEGIN
			DECLARE @CreateTempTableCommand NVARCHAR(MAX);
			SET @CreateTempTableCommand = 'SELECT TOP 0 * INTO Temp_' + @TableName + ' FROM ' + @TableName;
			EXEC sp_executesql @CreateTempTableCommand;
		END

		-- Vô hiệu hóa trigger
        DECLARE @DisableTrigger NVARCHAR(MAX);
        SET @DisableTrigger = 'DISABLE TRIGGER ALL ON ' + @TableName;
        EXEC sp_executesql @DisableTrigger;

        -- Import dữ liệu vào bảng tạm
		DECLARE @BCPCommand VARCHAR(1000);
		SET @BCPCommand = 'bcp QL_NHAHANG.dbo.Temp_' + @TableName + 
							' in "' + @FilePath + 
							'" -S ' + @ServerName + 
							' -U ' + @Username + 
							' -P ' + @Password + 
							' -w -t, -r\n';

		-- Thực thi lệnh BCP để đưa dữ liệu vào bảng tạm
		EXEC xp_cmdshell @BCPCommand;

		-- Tạo câu lệnh cập nhật động để loại bỏ dấu ngoặc kép
		DECLARE @UpdateCommand NVARCHAR(MAX);
		SET @UpdateCommand = 'UPDATE Temp_' + @TableName + ' SET ' +
			(SELECT STRING_AGG('[' + COLUMN_NAME + ']= REPLACE([' + COLUMN_NAME + '], ''"'', '''')', ', ') 
			 FROM INFORMATION_SCHEMA.COLUMNS 
			 WHERE TABLE_NAME = 'Temp_' + @TableName 
			 AND DATA_TYPE IN ('CHAR', 'VARCHAR', 'NVARCHAR'));

		-- Thực hiện lệnh cập nhật động
		EXEC sp_executesql @UpdateCommand;

        -- Merge dữ liệu từ bảng tạm vào bảng chính
        DECLARE @MergeCommand NVARCHAR(MAX);
        SET @MergeCommand = '
            MERGE ' + @TableName + ' AS target
            USING Temp_' + @TableName + ' AS source
            ON target.' + @PrimaryKeyColumn + ' = source.' + @PrimaryKeyColumn + '
            WHEN MATCHED THEN 
                UPDATE SET ' + -- Cập nhật các cột không phải khóa chính
                STUFF(
                    (SELECT ', target.' + name + ' = source.' + name 
                     FROM sys.columns 
                     WHERE object_id = OBJECT_ID(@TableName) 
                     AND name <> @PrimaryKeyColumn 
                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 
                     1, 2, '') + '
            WHEN NOT MATCHED BY TARGET THEN 
                INSERT (' + 
                (SELECT STRING_AGG(name, ', ') 
                 FROM sys.columns 
                 WHERE object_id = OBJECT_ID(@TableName)) + ')
                VALUES (' + 
                (SELECT STRING_AGG('source.' + name, ', ') 
                 FROM sys.columns 
                 WHERE object_id = OBJECT_ID(@TableName)) + ');';

        -- Thực hiện lệnh MERGE
        EXEC sp_executesql @MergeCommand;

        -- Xóa dữ liệu trong bảng tạm để chuẩn bị cho lần nhập tiếp theo
		DECLARE @TruncateTempTable NVARCHAR(255);
		SET @TruncateTempTable = 'TRUNCATE TABLE Temp_' + @TableName;
		EXEC sp_executesql @TruncateTempTable;

		-- Kích hoạt lại trigger
        DECLARE @EnableTrigger NVARCHAR(MAX);
        SET @EnableTrigger = 'ENABLE TRIGGER ALL ON ' + @TableName;
        EXEC sp_executesql @EnableTrigger;

		-- Thông báo thành công
		PRINT N'Dữ liệu đã được nhập thành công vào bảng ' + @TableName;
    END TRY
    BEGIN CATCH
        -- Bắt lỗi và trả về thông báo
        DECLARE @ErrorMessage NVARCHAR(MAX);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR('Lỗi trong quá trình import: %s', 16, 1, @ErrorMessage);
    END CATCH
END
GO

-- Thực hiện bcp import
CREATE PROCEDURE SP_BCP_Import
	@TableName VARCHAR(255),         -- Tên bảng cần nhập
    @FilePath VARCHAR(255),          -- Đường dẫn và tên file .csv
    @ServerName VARCHAR(255) = @@SERVERNAME, -- Tên server mặc định
    @Username VARCHAR(128),          -- Tên đăng nhập
    @Password VARCHAR(128)           -- Mật khẩu
WITH ENCRYPTION
AS
BEGIN
	-- Khởi tạo giá trị khóa chính
	DECLARE @PrimaryKey NVARCHAR(128);

	-- Tìm khóa chính của bảng
	SELECT @PrimaryKey = Col.COLUMN_NAME
	FROM 
		INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab,
		INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col
	WHERE 
		Col.TABLE_NAME = @TableName
		AND Tab.TABLE_NAME = @TableName
		AND Tab.CONSTRAINT_TYPE = 'PRIMARY KEY'
		AND Tab.CONSTRAINT_NAME = Col.CONSTRAINT_NAME;

	-- Khởi tạo chạy thủ tục bcp import bằng cách gọi tới SP_Import
	DECLARE @sp_import NVARCHAR(1000);
	SET @sp_import = 'EXEC SP_Import ' + 
								'@TableName = N''' + @TableName + ''', ' + 
								'@FilePath = N''' + @FilePath + ''', ' + 
								'@ServerName = N''' + @ServerName + ''', ' +
								'@Username = N''' + @Username + ''', ' +
								'@Password = N''' + @Password + ''', ' +
								'@PrimaryKeyColumn = N''' + @PrimaryKey + '''';
								
    -- In ra lệnh SQL để kiểm tra
	--PRINT @sp_import;

	-- Thực thi thủ tục SP_Import
	EXEC sp_executesql @sp_import;
END
GO

--EXEC SP_BCP_Import
--    @TableName = 'KhachHang',
--    @FilePath = 'H:\HQTCSDL\DACK\CSV\khachhang_data.csv', 
--    @ServerName = 'AILATUI25\MSSQLSERVER01', 
--    @Username = 'sa',
--    @Password = 'sa'
--GO 
*/
GO

-- Thủ tục lọc dữ liệu từ bảng phụ đẩy vào bảng chính
CREATE PROCEDURE SP_Merge
	@TableName VARCHAR(255)         -- Tên bảng cần nhập
WITH ENCRYPTION
AS
BEGIN
    BEGIN TRY
		-- Khởi tạo giá trị khóa chính
		DECLARE @PrimaryKey NVARCHAR(128);

		-- Tìm khóa chính của bảng
		SELECT @PrimaryKey = Col.COLUMN_NAME
		FROM 
			INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab,
			INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col
		WHERE 
			Col.TABLE_NAME = @TableName
			AND Tab.TABLE_NAME = @TableName
			AND Tab.CONSTRAINT_TYPE = 'PRIMARY KEY'
			AND Tab.CONSTRAINT_NAME = Col.CONSTRAINT_NAME;

		-- Vô hiệu hóa trigger
        DECLARE @DisableTrigger NVARCHAR(MAX);
        SET @DisableTrigger = 'DISABLE TRIGGER ALL ON ' + @TableName;
        EXEC sp_executesql @DisableTrigger;

        -- Merge dữ liệu từ bảng tạm vào bảng chính
        DECLARE @MergeCommand NVARCHAR(MAX);
        SET @MergeCommand = '
            MERGE ' + @TableName + ' AS target
            USING Temp_' + @TableName + ' AS source
            ON target.' + @PrimaryKey + ' = source.' + @PrimaryKey + '
            WHEN MATCHED THEN 
                UPDATE SET ' + -- Cập nhật các cột không phải khóa chính
                STUFF(
                    (SELECT ', target.' + name + ' = source.' + name 
                     FROM sys.columns 
                     WHERE object_id = OBJECT_ID(@TableName) 
                     AND name <> @PrimaryKey 
                     FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 
                     1, 2, '') + '
            WHEN NOT MATCHED BY TARGET THEN 
                INSERT (' + 
                (SELECT STRING_AGG(name, ', ') 
                 FROM sys.columns 
                 WHERE object_id = OBJECT_ID(@TableName)) + ')
                VALUES (' + 
                (SELECT STRING_AGG('source.' + name, ', ') 
                 FROM sys.columns 
                 WHERE object_id = OBJECT_ID(@TableName)) + ');';

        -- Thực hiện lệnh MERGE
        EXEC sp_executesql @MergeCommand;

        -- Xóa dữ liệu trong bảng tạm để chuẩn bị cho lần nhập tiếp theo
		DECLARE @TruncateTempTable NVARCHAR(255);
		SET @TruncateTempTable = 'TRUNCATE TABLE Temp_' + @TableName;
		EXEC sp_executesql @TruncateTempTable;

		-- Kích hoạt lại trigger
        DECLARE @EnableTrigger NVARCHAR(MAX);
        SET @EnableTrigger = 'ENABLE TRIGGER ALL ON ' + @TableName;
        EXEC sp_executesql @EnableTrigger;
    END TRY
    BEGIN CATCH
        -- Bắt lỗi và trả về thông báo
        DECLARE @ErrorMessage NVARCHAR(MAX);
        SET @ErrorMessage = ERROR_MESSAGE();
        RAISERROR(N'Lỗi trong quá trình import: %s', 16, 1, @ErrorMessage);
    END CATCH
END
GO

-------------------------------------------------------------------------------
USE QL_NHAHANG
GO

-- Thực hiện gán quyền cho user thực hiện chức năng Đặt món ăn
CREATE PROCEDURE SP_Grant_ChoUser
    @UserName NVARCHAR(20)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Gán quyền từng bước, sử dụng tham số hóa
        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_MonAn'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_DuyetThucDon_XuatThongTinCanThiet'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_MonAn_TrangThai'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_MonAn_LoaiMon'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Insert_DuyetThucDon'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Delete_DuyetThucDon'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Update_DuyetThucDon_SoLuong'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Update_DuyetThucDon_DangDoiDuyet'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_KhachHang_MaKH'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

		SET @SQL = N'EXECUTE SP_Grant_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_TaiKhoan_LayMaKhachHang'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Đã thực hiện chỉnh quyền cho user ' + @UserName + N' thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END
GO

-- Thực hiện thu hồi quyền cho user thực hiện chức năng Đặt món ăn
CREATE PROCEDURE SP_Revoke_ChoUser
    @UserName NVARCHAR(20)
WITH ENCRYPTION
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @SQL NVARCHAR(MAX);

    BEGIN TRY
        -- Bắt đầu transaction
        BEGIN TRANSACTION;

        -- Thu hồi quyền từng bước, sử dụng tham số hóa
        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_MonAn'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_DuyetThucDon_XuatThongTinCanThiet'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_MonAn_TrangThai'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_MonAn_LoaiMon'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Insert_DuyetThucDon'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Delete_DuyetThucDon'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Update_DuyetThucDon_SoLuong'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Update_DuyetThucDon_DangDoiDuyet'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_KhachHang_MaKH'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

		SET @SQL = N'EXECUTE SP_Revoke_CuTheChoUser_TrenDoiTuong_CuThe @UserName, N''EXECUTE'', N''SP_Select_TaiKhoan_LayMaKhachHang'', N''PROCEDURE''';
        EXEC sp_executesql @SQL, N'@UserName NVARCHAR(20)', @UserName;

        -- Commit transaction
        COMMIT TRANSACTION;

        PRINT N'Đã thu hồi quyền của user ' + @UserName + N' thành công!';
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Trả về thông tin lỗi
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END
GO
--------------------------------------------------------------------------------
-- Thủ tục lấy mã khách hàng thông qua tên tài khoản

