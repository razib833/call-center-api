-- READ ONLY. Set the ID returned by the real-SQL Postman request. No cleanup or fixture changes.
USE [JamunaCustomer360];
SET NOCOUNT ON;
IF @@SERVERNAME<>N'DESKTOP-BUDV4V7' OR DB_NAME()<>N'JamunaCustomer360' THROW 53420,'Unexpected target; stop.',1;
DECLARE @RequestID bigint=NULL; -- replace NULL with returned numeric requestID
IF @RequestID IS NULL OR @RequestID<=0 THROW 53420,'Supply the actual API requestID.',1;
SELECT DB_NAME() AS ActualDatabase,@@SERVERNAME AS ActualServer,RequestID,TicketNumber,ServiceID,VersionID,ApprovalStatus,CreatedAt
FROM dbo.ServiceRequest WHERE RequestID=@RequestID;
