-- READ ONLY: run in SSMS as your existing inspection/deployment login. Creates/grants nothing.
USE [JamunaCustomer360];
SET NOCOUNT ON;
IF @@SERVERNAME<>N'DESKTOP-BUDV4V7' OR DB_NAME()<>N'JamunaCustomer360'
 THROW 53430,'Unexpected server/database.',1;
SELECT @@SERVERNAME AS ServerName,DB_NAME() AS DatabaseName,ORIGINAL_LOGIN() AS InspectorLogin;
SELECT u.name AS DatabaseUser,u.type_desc,u.authentication_type_desc,l.name AS MappedServerLogin,
 l.is_disabled AS LoginDisabled,
 IS_SRVROLEMEMBER(N'sysadmin',l.name) AS DirectSysadminMembership,
 r.name AS DirectDatabaseRole
FROM sys.database_principals u
LEFT JOIN sys.server_principals l ON l.sid=u.sid
LEFT JOIN sys.database_role_members rm ON rm.member_principal_id=u.principal_id
LEFT JOIN sys.database_principals r ON r.principal_id=rm.role_principal_id
WHERE u.type IN(N'S',N'U',N'G') AND u.name NOT IN(N'guest',N'INFORMATION_SCHEMA',N'sys')
ORDER BY u.name,r.name;
SELECT r.name AS ServerRole,m.name AS MemberName
FROM sys.server_role_members rm
JOIN sys.server_principals r ON r.principal_id=rm.role_principal_id
JOIN sys.server_principals m ON m.principal_id=rm.member_principal_id
ORDER BY m.name,r.name;
SELECT p.name AS PrincipalName,x.state_desc,x.permission_name
FROM sys.server_permissions x JOIN sys.server_principals p ON p.principal_id=x.grantee_principal_id
WHERE x.permission_name IN(N'CONTROL SERVER',N'ALTER ANY LOGIN',N'CREATE ANY DATABASE',N'ALTER ANY SERVER ROLE');
SELECT N'Inventory only. Windows group inheritance/effective privileges require verification under the proposed login. WITHOUT LOGIN users cannot authenticate API connections. No identity is approved by these rows alone.' AS Limitation;
