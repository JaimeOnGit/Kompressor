USE [PraxmaMalhe]
GO
/****** Object:  StoredProcedure [dbo].[usp_DailyMachiningProductionReport]    Script Date: 8/26/2017 10:33:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[usp_DailyMachiningProductionReport] 
	-- Add the parameters for the stored procedure here
	@FromDate DATETIME
	,@ToDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	MC.CaptureDate [Date]
	,M.Description Module
	,P.Description Part
	, M.Cell Celda
	, C.Description Component
	, T.Description Type
	, M.TotalPRocess [PCsxHr]
	, SUM (MC.Total) TotalProduced
	--, MC.IdModuleCapture
	--, MC.ModuleId
	--, MC.ShiftHourId, MC.Total 
	--,MC.ShiftHourId
FROM [dbo].[ModuleCapture] MC
INNER JOIN Module M ON M.IdModule = MC.ModuleId
INNER JOIN Part P ON P.IdPart = M.PartId
INNER JOIN Component C ON C.IdComponent = M.ComponentId
INNER JOIN Type T ON T.IdType = M.TypeId
WHERE --MC.ModuleId IN 	(SELECT IdModule FROM Module WHERE Alias = 'M1') AND 
ProcessTypeId = 1
AND CaptureDate BETWEEN @FromDate AND @ToDate 
GROUP BY 
	MC.CaptureDate
	,M.Description
	,P.Description
	, M.Cell
	,C.Description
	 ,T.Description
	 ,M.TotalPRocess
	-- ,MC.ShiftHourId
END




SELECT 
	
	MC.CaptureDate
	,M.Description Module
	,P.Description Part
	, M.Cell Celda
	, C.Description Component
	, T.Description Type
	, M.TotalPRocess [PCsxHr]
	,MC.Total
	,MC.Shift
--	, iif(SH.ShiftGroup = 'First', SUM (MC.Total),0) TotalFirstShift
--	, iif(SH.ShiftGroup = 'Second', SUM (MC.Total),0) TotalSecondShift
	--,SH.ShiftGroup
	--, MC.IdModuleCapture
	--, MC.ModuleId
	--, MC.ShiftHourId, MC.Total 
--	,IIF(MC.ShiftHourId<13,SUM (MC.Total),0) AS TotalFristShift
--	,IIF(MC.ShiftHourId>12,SUM (MC.Total),0) AS TotalSecondShift
FROM [dbo].[ModuleCapture] MC
INNER JOIN Module M ON M.IdModule = MC.ModuleId
INNER JOIN Part P ON P.IdPart = M.PartId
INNER JOIN Component C ON C.IdComponent = M.ComponentId
INNER JOIN Type T ON T.IdType = M.TypeId
--INNER JOIN ShiftHour SH ON MC.ShiftHourId = SH.IdShiftHour
--WHERE 
--MC.ModuleId IN (SELECT IdModule FROM Module WHERE Alias = 'M1') 
AND  ProcessTypeId = 1 
--AND MC.ShiftHourId < 13
AND CaptureDate BETWEEN '08/01/2017' AND '08/31/2017' 
--GROUP BY MC.CaptureDate,M.Description,P.Description, M.Cell,  C.Description ,T.Description,M.TotalPRocess
--,SH.ShiftGroup


--, Mc.ShiftHourId

select * from ModuleCapture

select * from UserShift

select * from ShiftHour

select * from Users



















SELECT * FROM 
(

/******************************* FIRST SHIFT ******************************/

  -- Insert statements for procedure here
	SELECT 
	(CAST(MC.CaptureDate AS VARCHAR)
	+CAST(M.Description AS VARCHAR)
	+CAST(P.Description AS VARCHAR)
	+CAST(M.Cell AS VARCHAR)
	+CAST(C.Description AS VARCHAR)
	+CAST(T.Description AS VARCHAR)
	+CAST(M.TotalPRocess AS VARCHAR)) AS Keys

	,MC.CaptureDate
	,M.Description Module
	,P.Description Part
	, M.Cell Celda
	, C.Description Component
	, T.Description Type
	, M.TotalPRocess [PCsxHr]
	, SUM (MC.Total) TotalFirstShift
	--, MC.IdModuleCapture
	--, MC.ModuleId
	--, MC.ShiftHourId, MC.Total 
--	,IIF(MC.ShiftHourId<13,SUM (MC.Total),0) AS TotalFristShift
--	,IIF(MC.ShiftHourId>12,SUM (MC.Total),0) AS TotalSecondShift
FROM [dbo].[ModuleCapture] MC
INNER JOIN Module M ON M.IdModule = MC.ModuleId
INNER JOIN Part P ON P.IdPart = M.PartId
INNER JOIN Component C ON C.IdComponent = M.ComponentId
INNER JOIN Type T ON T.IdType = M.TypeId
--WHERE 
--MC.ModuleId IN (SELECT IdModule FROM Module WHERE Alias = 'M1') 
AND  ProcessTypeId = 1 
AND MC.ShiftHourId < 13
AND CaptureDate BETWEEN '08/01/2017' AND '08/31/2017' 
GROUP BY MC.CaptureDate,M.Description,P.Description, M.Cell,  C.Description ,T.Description,M.TotalPRocess
--, Mc.ShiftHourId
)
AS FirstShift

 RIGHT OUTER JOIN 
(

/********************************** SECOND SHIFT ******************************************/


  -- Insert statements for procedure here
	SELECT 

	 
	(CAST(MC.CaptureDate AS VARCHAR)
	+CAST(M.Description AS VARCHAR)
	+CAST(P.Description AS VARCHAR)
	+CAST(M.Cell AS VARCHAR)
	+CAST(C.Description AS VARCHAR)
	+CAST(T.Description AS VARCHAR)
	+CAST(M.TotalPRocess AS VARCHAR)) AS Keys

	,MC.CaptureDate
	,M.Description Module
	,P.Description Part
	, M.Cell Celda
	, C.Description Component
	, T.Description Type
	, M.TotalPRocess [PCsxHr]
	, SUM (MC.Total) TotalSecondShift
	--, MC.IdModuleCapture
	--, MC.ModuleId
	--, MC.ShiftHourId, MC.Total 
--	,IIF(MC.ShiftHourId<13,SUM (MC.Total),0) AS TotalFristShift
--	,IIF(MC.ShiftHourId>12,SUM (MC.Total),0) AS TotalSecondShift
FROM [dbo].[ModuleCapture] MC
INNER JOIN Module M ON M.IdModule = MC.ModuleId
INNER JOIN Part P ON P.IdPart = M.PartId
INNER JOIN Component C ON C.IdComponent = M.ComponentId
INNER JOIN Type T ON T.IdType = M.TypeId
--WHERE 
--MC.ModuleId IN (SELECT IdModule FROM Module WHERE Alias = 'M1') 
AND  ProcessTypeId = 1 
AND CaptureDate BETWEEN '08/01/2017' AND '08/31/2017' 
AND MC.ShiftHourId > 12
GROUP BY MC.CaptureDate,M.Description,P.Description, M.Cell,  C.Description ,T.Description,M.TotalPRocess
--, Mc.ShiftHourId

)  AS SecondShift On FirstShift.Keys=SecondShift.Keys











sp_help modulecapture


alter table ModuleCapture alter column CaptureDate datetime


exec [usp_DailyMachiningProductionReport_bk082617] '08/19/2017','08/31/2017'


SELECT * FROM ModuleCapture


SELECT * FROM ShiftHour
SELECT * FROM UserShift



alter table ShiftHour add ShiftGroup varchar(20) 