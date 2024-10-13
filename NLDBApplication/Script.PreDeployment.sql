/*
 预先部署脚本模板							
--------------------------------------------------------------------------------------
 此文件包含将在生成脚本之前执行的 SQL 语句。	
 使用 SQLCMD 语法将文件包含在预先部署脚本中。			
 示例:      :r .\myfile.sql								
 使用 SQLCMD 语法引用预先部署脚本中的变量。		
 示例:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- 声明临时变量
DECLARE @SqlHash AS BINARY(64);

-- 生成Hash
SET @SqlHash =
(
	SELECT HASHBYTES('SHA2_512',
		(SELECT * FROM OPENROWSET (BULK 'E:\VisualStudioProjects\NLDBApplication\NLDBApplication\bin\Debug\NLDBApplication.dll', SINGLE_BLOB) AS [Data]))
);

-- 增加信任代码
EXEC sp_add_trusted_assembly @SqlHash