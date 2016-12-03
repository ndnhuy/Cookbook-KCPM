create proc usp_TimKiemMonAn @TT nvarchar(50),@row int,@count int
as
begin

	SET FMTONLY OFF

	if(@count is null and @row is null )
	begin
		set @row=0
		select @count = count(*)
		from(select distinct MaMonAn from MonAn where TenMon like '%'+@TT+'%') a
	end

	CREATE TABLE #TMP (MAMON INT, SOLUONGTHICH INT)
	INSERT INTO #TMP
	SELECT MA.MaMonAn AS MAMON, "SOLUONGTHICH" = 
				CASE
					WHEN SOLUONGTHICH IS NULL THEN 0
					ELSE SOLUONGTHICH
				END
	FROM MONAN MA LEFT JOIN (SELECT MaMonAn AS MAMON, COUNT(*) AS SOLUONGTHICH
								FROM THICH
								GROUP BY MaMonAn) TH ON MA.MaMonAn = TH.MAMON
	ORDER BY SOLUONGTHICH DESC
	
	select m.MaMonAn AS MaMonAn,
		m.MaNguoiDung AS MaNguoiDung,
		m.TenMon AS TenMon,
		m.GioiThieu AS GioiThieu,
		m.Hinh AS Hinh,
		m.ThoiGianChuanBi AS ThoiGianChuanBi,
		m.ThoiGianNau AS ThoiGianNau,
		m.NgayDang AS NgayDang,
		m.MaLoaiMon AS MaLoaiMon,
		m.MaMucDo AS MaMucDo,
		m.NguyenLieu AS NguyenLieu,
		m.CachLam AS CachLam,
		#TMP.SOLUONGTHICH AS SoLuongThich
	from MonAn m, #TMP,
				(select MaMonAn,row_number() over(order by count(*) desc) as row
				from MonAn where TenMon like '%'+@TT+'%'
				group by MaMonAn) m1
	where m1.row>@row and m1.row <=@count+@row and m.MaMonAn = m1.MaMonAn
	AND #TMP.MAMON = m.MaMonAn

	--DROP TABLE #TMP
end
go 

create proc usp_TimKiemNguoiDung @TT nvarchar(50),@row int,@count int
as
begin
	if(@count is null and @row is null )
	begin
		set @row=0
		select @count = count(*)
		from(select distinct MaNguoiDung from NguoiDung where Ten like '%'+@TT+'%' OR Ho like '%'+@TT+'%') a
	end
	
	select n.MaNguoiDung AS MaNguoiDung,
		n.Ho + ' ' + n.Ten AS HoTen,
		n.Hinh AS Hinh,
		n.DiaChi AS DiaChi
	
	 from NguoiDung n,
				(select MaNguoiDung,row_number() over(order by count(*) desc) as row
				from NguoiDung where Ten like '%'+@TT+'%' OR Ho like '%'+@TT+'%'
				group by MaNguoiDung) n1
	where n1.row>@row and n1.row <=@count+@row and n.MaNguoiDung = n1.MaNguoiDung
end
go 

CREATE PROCEDURE usp_TopMonAnThich @count int
AS
BEGIN
	SELECT M.MaMonAn AS MaMonAn,
	M.MaNguoiDung AS MaNguoiDung,
	M.TenMon AS TenMon,
	M.GioiThieu AS GioiThieu,
	M.Hinh AS Hinh,
	M.ThoiGianChuanBi AS ThoiGianChuanBi,
	M.ThoiGianNau AS ThoiGianNau,
	M.NgayDang AS NgayDang,
	M.MaLoaiMon AS MaLoaiMon,
	M.MaMucDo AS MaMucDo,
	M.NguyenLieu AS NguyenLieu,
	M.CachLam AS CachLam,
	ND.Ho + ' ' + ND.Ten AS HoTen,
	ND.Hinh AS HinhNguoiDung,
	ND.DiaChi AS DiaChi,
	SORT.SoLuongThich AS SoLuongThich
	FROM MONAN M, NGUOIDUNG ND, (SELECT TOP(@count) TH.MaMonAn, COUNT(*) AS SoLuongThich FROM THICH TH GROUP BY TH.MaMonAn ORDER BY COUNT(*) DESC ) SORT
	WHERE M.MaNguoiDung = ND.MaNguoiDung 
	AND SORT.MaMonAn = M.MaMonAn
END
GO

CREATE PROCEDURE usp_TopDauBep
AS
BEGIN

	DECLARE @daubep NVARCHAR(255)
	DECLARE @soluongthich INT
	DECLARE @soluongmon INT

	SELECT TOP(1) @daubep = NG.MaNguoiDung, @soluongthich = COUNT(*)
	FROM MONAN M, NGUOIDUNG NG, THICH TH
	WHERE M.MaNguoiDung = NG.MaNguoiDung
	AND M.MaMonAn = TH.MaMonAn
	GROUP BY NG.MaNguoiDung
	ORDER BY COUNT(*) DESC

	SELECT @soluongmon = COUNT(*)
	FROM MONAN M
	WHERE M.MaNguoiDung = @daubep

	SELECT ND.MaNguoiDung AS MaNguoiDung,
		ND.Ho + ' ' + ND.Ten AS HoTen,
		ND.Hinh AS Hinh,
		ND.DiaChi AS DiaChi,
		@soluongthich AS SoLuongThich,
		@soluongmon AS SoLuongMon
	FROM NguoiDung ND
	WHERE ND.MaNguoiDung = @daubep
END
GO

CREATE PROCEDURE usp_TopMonAnCuaDauBep @madaubep NVARCHAR(255), @count int
AS
BEGIN
SELECT M.MaMonAn AS MaMonAn,
	M.MaNguoiDung AS MaNguoiDung,
	M.TenMon AS TenMon,
	M.GioiThieu AS GioiThieu,
	M.Hinh AS Hinh,
	M.ThoiGianChuanBi AS ThoiGianChuanBi,
	M.ThoiGianNau AS ThoiGianNau,
	M.NgayDang AS NgayDang,
	M.MaLoaiMon AS MaLoaiMon,
	M.MaMucDo AS MaMucDo,
	M.NguyenLieu AS NguyenLieu,
	M.CachLam AS CachLam
FROM MONAN M,  (SELECT TOP(@count) TH.MaMonAn AS MaMonAn
	FROM THICH TH, MONAN M
	WHERE TH.MaMonAn = M.MaMonAn
	AND M.MaNguoiDung = @madaubep
	GROUP BY TH.MaMonAn 
	ORDER BY COUNT(*) DESC) AS SORT
WHERE M.MaMonAn = SORT.MaMonAn
END
GO

CREATE PROCEDURE usp_LietKeBinhLuanMonAn @monan INT
AS
BEGIN
	SELECT ND.MaNguoiDung AS MaNguoiDung,
		MaMonAn AS MaMonAn,
		NgayDang AS ThoiGian,
		NoiDung AS NoiDung,
		ND.Ho + ' ' + ND.Ten AS HoTen,
		Hinh AS Hinh
	FROM BINHLUAN BL JOIN NGUOIDUNG ND ON BL.MaNguoiDung = ND.MaNguoiDung 
	WHERE BL.MaMonAn = @monan
	ORDER BY BL.NgayDang ASC
END
GO

CREATE PROCEDURE usp_BinhLuanMonAn @monan INT, @nguoidung NVARCHAR(255), @noidung NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO BINHLUAN(MaNguoiDung, MaMonAn, NgayDang, NoiDung)
	VALUES(@nguoidung, @monan, GETDATE(), @noidung)
END
GO

CREATE PROCEDURE usp_ThichMonAn @manguoidung NVARCHAR(255), @mamon INT
AS
BEGIN
	SET FMTONLY OFF

	IF NOT EXISTS ( SELECT * FROM Thich WHERE MaNguoiDung = @manguoidung AND @mamon = MaMonAn )
	BEGIN
		INSERT INTO THICH VALUES (@mamon, @manguoidung)
	END

	CREATE TABLE #TMP (MAMON INT, SOLUONGTHICH INT)
	INSERT INTO #TMP
	SELECT MA.MaMonAn AS MAMON, "SOLUONGTHICH" = 
				CASE
					WHEN SOLUONGTHICH IS NULL THEN 0
					ELSE SOLUONGTHICH
				END
	FROM MONAN MA LEFT JOIN (SELECT MaMonAn AS MAMON, COUNT(*) AS SOLUONGTHICH
								FROM THICH
								GROUP BY MaMonAn) TH ON MA.MaMonAn = TH.MAMON
	ORDER BY SOLUONGTHICH DESC

	SELECT "SOLUONGTHICH" FROM #TMP WHERE MAMON = @mamon
END
GO