USE [nldb2]
GO 

SET NOCOUNT ON;

--------------------------------------------------------------------------------
--
-- ExceptionLog
--
--------------------------------------------------------------------------------

-- 创建Exception Log
EXEC dbo.SetLog 1
EXEC dbo.CreateExceptionLog;
EXEC dbo.GetLogs;

--------------------------------------------------------------------------------
--
-- FilterRule
--
--------------------------------------------------------------------------------

-- 创建FilterRule
EXEC dbo.SetLog 1
EXEC dbo.CreateFilterRule 0;/*不调用内置初始化*/
EXEC dbo.GetLogs;

-- 外部初始化FilterRule
INSERT INTO dbo.FilterRule
([rule], [classification], [replace])
SELECT [rule], [classification], [replace] FROM dbo.FilterRule;

-- 加载FilterRule
EXEC dbo.SetLog 1
EXEC dbo.ReloadFilterRule;
EXEC dbo.GetLogs;

--------------------------------------------------------------------------------
--
-- RegularRule
--
--------------------------------------------------------------------------------

-- 创建RegularRule
EXEC dbo.SetLog 1
EXEC dbo.CreateRegularRule 0;/*不调用内置初始化*/
EXEC dbo.GetLogs;

-- 外部初始化RegularRule
INSERT INTO dbo.RegularRule
([rule], [classification], [attribute])
SELECT [rule], [classification], [attribute] FROM dbo.RegularResource;

--------------------------------------------------------------------------------
--
-- CoreContent
--
--------------------------------------------------------------------------------

-- 创建CoreContent
EXEC dbo.SetLog 1
EXEC dbo.CreateCoreContent 0;/*不调用内置初始化*/
EXEC dbo.GetLogs;

-- 外部初始化CoreContent
INSERT INTO dbo.CoreContent
([content], [classification], [attribute])
SELECT [content], [classification], [attribute] FROM dbo.CoreResource;

-- 加载CoreContent
EXEC dbo.SetLog 1
EXEC dbo.ReloadCoreContent;
EXEC dbo.GetLogs;

--------------------------------------------------------------------------------
--
-- InnerContent
--
--------------------------------------------------------------------------------

-- 创建InnerContent
EXEC dbo.SetLog 1
EXEC dbo.CreateInnerContent;
EXEC dbo.GetLogs;

-- 外部初始化InnerContent
INSERT INTO dbo.InnerContent
([dictionary], [content], [classification], [attribute])
SELECT 1, [content], [classification], [attribute] FROM dbo.InnerResource;

--------------------------------------------------------------------------------
--
-- OuterContent
--
--------------------------------------------------------------------------------

-- 创建OuterContent
EXEC dbo.SetLog 1
EXEC dbo.CreateOuterContent;
EXEC dbo.GetLogs;

--------------------------------------------------------------------------------
--
-- 加载
--
--------------------------------------------------------------------------------

-- 加载计数器
EXEC dbo.SetLog 0
EXEC dbo.ReloadGrammarCount;/*全部加载才能依据内存计算*/
EXEC dbo.GetLogs;

-- 加载计数器
EXEC dbo.SetLog 0
EXEC dbo.ReloadDictionaryCount 0;/*全部加载才能依据内存计算*/
EXEC dbo.GetLogs;

--------------------------------------------------------------------------------
--
-- 计算
--
--------------------------------------------------------------------------------

-- 初始化Dictionary
UPDATE dbo.Dictionary SET count = 1;

-- 统计Dictionary
EXEC dbo.SetLog 0;
SELECT
dbo.AddUpGrammarCount(content, 1/*仅依据内存计算*/),
dbo.AddUpDictionaryCount(content, 1/*仅依据内存计算*/) FROM dbo.Dictionary;
EXEC dbo.GetLogs;

-- 统计TextContent
EXEC dbo.SetLog 0;
SELECT
dbo.AddUpGrammarCount(content, 1/*仅依据内存计算*/),
dbo.AddUpDictionaryCount(content, 1/*仅依据内存计算*/) FROM dbo.TextContent;
EXEC dbo.GetLogs;

-- 计算相关系数
EXEC dbo.SetLog 0;
EXEC dbo.RecalculateGamma 1;/*全部重新计算*/
EXEC dbo.GetLogs;

--------------------------------------------------------------------------------
--
-- 保存
--
--------------------------------------------------------------------------------

-- 更新计数器
EXEC dbo.SetLog 0;
EXEC dbo.UpdateGrammarCount;/*保存数据*/
EXEC dbo.GetLogs;

-- 更新计数器
EXEC dbo.SetLog 0;
EXEC dbo.UpdateDictionaryCount;/*保存内存数据*/
EXEC dbo.GetLogs;

SET NOCOUNT OFF;