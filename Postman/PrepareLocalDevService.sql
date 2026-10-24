-- Local development configuration only. Run the ENTIRE file in a NEW SSMS query
-- as the existing deployment/inspection login, NOT Customer360ApiAgent.
-- This is not an installation, migration, test-suite fixture or ticket insert.
-- Default preview executes procedure calls then ROLLS BACK all rows and audit rows.
-- SQL identity/rowversion counters can advance even when rolled back.
-- After reviewing preview, change only @CommitChanges to 1 to persist configuration.
USE [JamunaCustomer360];
SET NOCOUNT ON;
SET XACT_ABORT ON;
DECLARE @CommitChanges bit=0;
IF CONVERT(nvarchar(128),SERVERPROPERTY('MachineName'))<>N'DESKTOP-BUDV4V7'
 OR COALESCE(CONVERT(nvarchar(128),SERVERPROPERTY('InstanceName')),N'MSSQLSERVER')<>N'MSSQLSERVER'
 OR DB_NAME()<>N'JamunaCustomer360'
 THROW 53440,'Unexpected target. No configuration created.',1;
IF @@TRANCOUNT<>0 THROW 53440,'Use a new query without an open transaction.',1;
IF COALESCE(IS_ROLEMEMBER('db_owner'),0)<>1
 THROW 53440,'Run as the approved existing deployment login, never the API Agent.',1;
DECLARE @Keys TABLE(Name sysname);
INSERT @Keys VALUES(N'Customer360.ActorID'),(N'Customer360.ActorName'),
 (N'Customer360.ActorRole'),(N'Customer360.AuthenticationSource'),
 (N'Customer360.VerifiedUntil'),(N'Customer360.CorrelationID');
IF EXISTS(SELECT 1 FROM @Keys WHERE SESSION_CONTEXT(Name) IS NOT NULL)
 THROW 53440,'Existing identity context found. Open a NEW SSMS query connection.',1;
DECLARE @CategoryID int,@ServiceID bigint,@VersionID bigint,@FieldID bigint,
 @TemplateID bigint,@PriorityID int,@RV binary(8),@Until datetime2(3)=DATEADD(minute,15,SYSUTCDATETIME()),
 @Correlation uniqueidentifier=NEWID();
-- This is an explicit DBA maintenance identity for synthetic configuration;
-- no external Manager authentication or restricted CONFIG-role test is claimed.
BEGIN TRY
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorID',@value=N'LOCAL_DEV_CONFIG';
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorName',@value=N'Local development configuration maintenance';
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorRole',@value=N'MANAGER';
 EXEC sys.sp_set_session_context @key=N'Customer360.AuthenticationSource',@value=N'EXTERNAL_MANAGER';
 EXEC sys.sp_set_session_context @key=N'Customer360.VerifiedUntil',@value=@Until;
 EXEC sys.sp_set_session_context @key=N'Customer360.CorrelationID',@value=@Correlation;
 BEGIN TRANSACTION;
 DECLARE @LockResult int;
 EXEC @LockResult=sys.sp_getapplock @Resource=N'Customer360.LocalDevService.Setup',
  @LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=10000;
 IF @LockResult<0 THROW 53440,'Configuration setup lock unavailable.',1;
 IF EXISTS(SELECT 1 FROM dbo.ServiceMaster WHERE ServiceCode='LOCAL_DEV_TEST')
  OR EXISTS(SELECT 1 FROM dbo.ServiceCategory WHERE CategoryCode='LOCAL_DEV_TEST')
  OR EXISTS(SELECT 1 FROM dbo.NotificationTemplate WHERE TemplateCode='LOCAL_DEV_TEST_SMS')
  THROW 53440,'LOCAL_DEV_TEST objects already exist. Stop for review; nothing overwritten.',1;
 IF EXISTS(SELECT 1 FROM dbo.CommunicationOutbox)
  THROW 53440,'Outbox is not empty. Stop for review; no records deleted.',1;
 SELECT @PriorityID=PriorityID FROM dbo.PriorityMaster WHERE PriorityCode='NORMAL' AND IsActive=1;
 IF @PriorityID IS NULL THROW 53440,'Active NORMAL priority seed missing. No seed auto-repair.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.TicketStatusMaster WHERE StatusCode='PENDING_APPROVAL' AND IsActive=1)
  THROW 53440,'Active PENDING_APPROVAL seed missing.',1;
 IF NOT EXISTS(SELECT 1 FROM dbo.RequestSourceMaster WHERE RequestSourceCode='AVAYA_AGENT' AND IsActive=1)
  THROW 53440,'Active AVAYA_AGENT seed missing.',1;
 EXEC dbo.usp_ServiceCategory_Create @CategoryCode='LOCAL_DEV_TEST',@CategoryName=N'LOCAL DEV TEST',
  @CategoryDescription=N'Synthetic local development only',@DisplayOrder=999,@IsActive=1,@NewID=@CategoryID OUTPUT;
 EXEC dbo.usp_Service_Create @CategoryID=@CategoryID,@ServiceCode='LOCAL_DEV_TEST',
  @ServiceName=N'LOCAL DEV TEST',@ShortName=N'LOCAL DEV TEST',@VeryShortName=N'LOCAL DEV TEST',
  @ServiceTitle=N'LOCAL DEV TEST - Synthetic ticket',@Description=N'Synthetic local development only; no real customer data',
  @IsActive=1,@NewID=@ServiceID OUTPUT;
 EXEC dbo.usp_ServiceVersion_CreateDraft @ServiceID=@ServiceID,@NewVersionID=@VersionID OUTPUT;
 SELECT @RV=RowVersion FROM dbo.ServiceVersion WHERE VersionID=@VersionID;
 EXEC dbo.usp_ServiceWorkflow_Save @VersionID=@VersionID,@DefaultPriorityID=@PriorityID,
  @ApprovalTargetMinutes=60,@ResolutionTargetMinutes=1440,@AllowForwardToBranch=0,
  @AllowForwardToDivision=0,@FollowUpRequired=0,@DefaultFollowUpMinutes=NULL,@ExpectedVersionRowVersion=@RV;
 SELECT @RV=RowVersion FROM dbo.ServiceVersion WHERE VersionID=@VersionID;
 EXEC dbo.usp_ServiceField_Add @VersionID=@VersionID,@FieldKey='LOCAL_DEV_NOTE',
  @FieldLabel=N'Local development note',@FieldType='TEXT',@IsRequired=0,@IsReadOnly=0,
  @IsVisible=1,@MaxLength=200,@DisplayOrder=1,@IsActive=1,@NewID=@FieldID OUTPUT,
  @ExpectedVersionRowVersion=@RV;
 EXEC dbo.usp_NotificationTemplate_CreateDraft @TemplateCode='LOCAL_DEV_TEST_SMS',@TemplateVersion=1,
  @TemplateName=N'LOCAL DEV TEST - SMS',@ChannelCode='SMS',
  @TemplateBody=N'LOCAL DEV TEST ONLY: ticket {TicketNumber} status updated.',
  @Description=N'Synthetic configuration; providers and consumers must remain disconnected.',@NewID=@TemplateID OUTPUT;
 SELECT @RV=RowVersion FROM dbo.NotificationTemplate WHERE TemplateID=@TemplateID;
 EXEC dbo.usp_NotificationTemplate_Publish @TemplateID=@TemplateID,@ExpectedRowVersion=@RV;
 DECLARE @Events TABLE(EventCode varchar(50) PRIMARY KEY);
 INSERT @Events VALUES('TICKET_CREATED'),('TICKET_REJECTED'),('TICKET_COMPLETED'),('TICKET_FAILED');
 DECLARE @Event varchar(50);
 WHILE EXISTS(SELECT 1 FROM @Events)
 BEGIN
  SELECT TOP(1) @Event=EventCode FROM @Events ORDER BY EventCode;
  SELECT @RV=RowVersion FROM dbo.ServiceVersion WHERE VersionID=@VersionID;
  EXEC dbo.usp_ServiceNotificationConfig_Save @VersionID=@VersionID,@EventCode=@Event,
   @ChannelCode='SMS',@TemplateID=@TemplateID,@IsEnabled=1,@ExpectedVersionRowVersion=@RV;
  DELETE FROM @Events WHERE EventCode=@Event;
 END;
 SELECT @RV=RowVersion FROM dbo.ServiceVersion WHERE VersionID=@VersionID;
 EXEC dbo.usp_ServiceVersion_PublishDraft @VersionID=@VersionID,@ExpectedRowVersion=@RV;
 IF NOT EXISTS(SELECT 1 FROM dbo.ServiceVersion WHERE VersionID=@VersionID AND VersionStatus='PUBLISHED')
  THROW 53440,'Published version verification failed.',1;
 IF (SELECT COUNT(*) FROM dbo.ServiceNotificationConfig WHERE VersionID=@VersionID AND IsEnabled=1 AND ChannelCode='SMS')<>4
  THROW 53440,'Mandatory notification configuration verification failed.',1;
 IF EXISTS(SELECT 1 FROM dbo.CommunicationOutbox)
  THROW 53440,'Unexpected outbox activity. Configuration rolled back; investigate external consumers/producers.',1;
 IF @CommitChanges=1 COMMIT TRANSACTION; ELSE ROLLBACK TRANSACTION;
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorID',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorName',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorRole',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.AuthenticationSource',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.VerifiedUntil',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.CorrelationID',@value=NULL;
 SELECT CASE WHEN @CommitChanges=1 THEN 'COMMITTED CONFIGURATION - NO TICKET CREATED'
  ELSE 'PREVIEW ROLLED BACK - DO NOT USE THESE IDS' END AS Result,
  @CategoryID AS CategoryID,@ServiceID AS ServiceID,@VersionID AS VersionID,
  @FieldID AS FieldID,@TemplateID AS TemplateID,@Correlation AS ConfigurationCorrelation;
 IF @CommitChanges=1
  SELECT ServiceID,VersionID,VersionStatus FROM dbo.ServiceVersion WHERE VersionID=@VersionID;
END TRY
BEGIN CATCH
 IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorID',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorName',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.ActorRole',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.AuthenticationSource',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.VerifiedUntil',@value=NULL;
 EXEC sys.sp_set_session_context @key=N'Customer360.CorrelationID',@value=NULL;
 THROW;
END CATCH;
