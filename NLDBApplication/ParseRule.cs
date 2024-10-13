using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class ParseRule
{
	// 句子规则
	private static Dictionary<string, int> rules = new Dictionary<string, int>();

	[Microsoft.SqlServer.Server.SqlProcedure]
	public static void CreateParseRule(SqlBoolean sqlInitialize)
	{
		// 记录日志
		Log.LogMessage("ParseRule", "CreateParseRule", "创建数据表！");

		// 指令字符串
		string cmdString =
			// 删除之前的索引
			"IF OBJECT_ID('ParseRulePIDIndex') IS NOT NULL " +
			"DROP INDEX dbo.ParseRulePIDIndex; " +
			// 删除之前的索引
			"IF OBJECT_ID('ParseRuleClassificationIndex') IS NOT NULL " +
			"DROP INDEX dbo.ParseRuleClassificationIndex; " +
			// 删除之前的表
			"IF OBJECT_ID('ParseRule') IS NOT NULL " +
			"DROP TABLE dbo.ParseRule; " +
			// 创建数据表
			"CREATE TABLE dbo.ParseRule " +
			"( " +
			// 编号
			"[pid]                  INT                     IDENTITY(1, 1)              NOT NULL, " +
			// 分类描述
			"[classification]       NVARCHAR(64)            NULL, " +
			// 规则
			"[rule]                 NVARCHAR(450)           PRIMARY KEY                 NOT NULL, " +
			// 是否为正则式
			"normalized             BIT                     NOT NULL                    DEFAULT 0, " +
			// 是否有固定结尾标志
			"static_suffix          BIT                     NOT NULL                    DEFAULT 0, " +
			// 是否有固定开头标志
			"static_prefix          BIT                     NOT NULL                    DEFAULT 0, " +
			// 满足规则的最短长度
			"minimum_length         INT                     NOT NULL                    DEFAULT 0, " +
			// 已使用的参数个数
			"parameter_count        INT                     NOT NULL                    DEFAULT 0, " +
			// 人工可控优先级
			"controllable_priority  INT                     NOT NULL                    DEFAULT 0 " +
			"); " +
			// 创建简单索引
			"CREATE INDEX ParseRulePIDIndex ON dbo.ParseRule(pid); " +
			"CREATE INDEX ParseRuleClassificationIndex ON dbo.ParseRule(classification); ";

		// 执行指令
		NLDB.ExecuteNonQuery(cmdString);

		// 记录日志
		Log.LogMessage("ParseRule", "CreateParseRule", "数据表已创建！");

		// 初始化数据表
		if (sqlInitialize) InitializeParseRule();
	}

	public static void InitializeParseRule()
	{
		// 临时变量
		string strRule;

		// 记录日志
		Log.LogMessage("ParseRule", "InitializeParseRule", "初始化数据表！");

		AddParseRule("配对", "〈$a〉");
		AddParseRule("配对", "《$a》");
		AddParseRule("配对", "“$a”");
		AddParseRule("配对", "【$a】");
		AddParseRule("配对", "（$a）");
		AddParseRule("配对", "‘$a’");
		// 禁止加入
		// AddParseRule("拼接","$a");
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				'$' + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("拼接", strRule);
		}
		/*
		AddParseRule("拼接","$a$b");
		AddParseRule("拼接","$a$b$c");
		AddParseRule("拼接","$a$b$c$d");
		AddParseRule("拼接","$a$b$c$d$e");
		AddParseRule("拼接","$a$b$c$d$e$f");
		AddParseRule("拼接","$a$b$c$d$e$f$g");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t$u");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t$u$v");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t$u$v$w");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t$u$v$w$x");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t$u$v$w$x$y");
		AddParseRule("拼接","$a$b$c$d$e$f$g$h$i$j$k$l$m$n$o$p$q$r$s$t$u$v$w$x$y$z");
		*/
		// 禁止加入
		// AddParseRule("通用","$a，");
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"，$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("通用", strRule);
		}
		/*
		AddParseRule("通用","$a，$b");
		AddParseRule("通用","$a，$b，$c");
		AddParseRule("通用","$a，$b，$c，$d");
		AddParseRule("通用","$a，$b，$c，$d，$e");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w，$x");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w，$x，$y");
		AddParseRule("通用","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w，$x，$y，$z");
		*/
		// 禁止加入
		// AddParseRule("通用","$a：");
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"：$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("通用", strRule);
		}
		/*
		AddParseRule("通用","$a：$b");
		AddParseRule("通用","$a：$b：$c");
		AddParseRule("通用","$a：$b：$c：$d");
		AddParseRule("通用","$a：$b：$c：$d：$e");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t：$u");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t：$u：$v");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t：$u：$v：$w");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t：$u：$v：$w：$x");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t：$u：$v：$w：$x：$y");
		AddParseRule("通用","$a：$b：$c：$d：$e：$f：$g：$h：$i：$j：$k：$l：$m：$n：$o：$p：$q：$r：$s：$t：$u：$v：$w：$x：$y：$z");
		*/
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"；$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("复句", strRule);
		}
		/*
		AddParseRule("复句","$a；$b");
		AddParseRule("复句","$a；$b；$c");
		AddParseRule("复句","$a；$b；$c；$d");
		AddParseRule("复句","$a；$b；$c；$d；$e");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l；$m");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l；$m；$n");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l；$m；$n；$o");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l；$m；$n；$o；$p");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l；$m；$n；$o；$p；$q");
		AddParseRule("复句","$a；$b；$c；$d；$e；$f；$g；$h；$i；$j；$k；$l；$m；$n；$o；$p；$q；$r");
		*/
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"！$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("复句", strRule);
		}
		/*
		AddParseRule("复句","$a！$b");
		AddParseRule("复句","$a！$b！$c");
		AddParseRule("复句","$a！$b！$c！$d");
		AddParseRule("复句","$a！$b！$c！$d！$e");
		AddParseRule("复句","$a！$b！$c！$d！$e！$f");
		AddParseRule("复句","$a！$b！$c！$d！$e！$f！$g");
		AddParseRule("复句","$a！$b！$c！$d！$e！$f！$g！$h");
		AddParseRule("复句","$a！$b！$c！$d！$e！$f！$g！$h！$i");
		AddParseRule("复句","$a！$b！$c！$d！$e！$f！$g！$h！$i！$j");
		*/
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"？$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("复句", strRule);
		}
		/*
		AddParseRule("复句","$a？$b");
		AddParseRule("复句","$a？$b？$c");
		AddParseRule("复句","$a？$b？$c？$d");
		AddParseRule("复句","$a？$b？$c？$d？$e");
		AddParseRule("复句","$a？$b？$c？$d？$e？$f");
		AddParseRule("复句","$a？$b？$c？$d？$e？$f？$g");
		AddParseRule("复句","$a？$b？$c？$d？$e？$f？$g？$h");
		AddParseRule("复句","$a？$b？$c？$d？$e？$f？$g？$h？$i");
		AddParseRule("复句","$a？$b？$c？$d？$e？$f？$g？$h？$i？$j");
		*/
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"。$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("复句", strRule);
		}
		/*
		AddParseRule("复句","$a。$b");
		AddParseRule("复句","$a。$b。$c");
		AddParseRule("复句","$a。$b。$c。$d");
		AddParseRule("复句","$a。$b。$c。$d。$e");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t。$u");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t。$u。$v");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t。$u。$v。$w");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t。$u。$v。$w。$x");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t。$u。$v。$w。$x。$y");
		AddParseRule("复句","$a。$b。$c。$d。$e。$f。$g。$h。$i。$j。$k。$l。$m。$n。$o。$p。$q。$r。$s。$t。$u。$v。$w。$x。$y。$z");
		*/
		// 设置初始值
		strRule = "$a";
		// 循环处理
		for (int i = 2; i <= 26; i++)
		{
			// 增加参数
			strRule = strRule +
				"，$" + Lowercase.GetLowercase(i);
			// 加入规则
			AddParseRule("单句", strRule + "。");
		}
		/*
		AddParseRule("单句","$a，$b。");
		AddParseRule("单句","$a，$b，$c。");
		AddParseRule("单句","$a，$b，$c，$d。");
		AddParseRule("单句","$a，$b，$c，$d，$e。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w，$x。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w，$x，$y。");
		AddParseRule("单句","$a，$b，$c，$d，$e，$f，$g，$h，$i，$j，$k，$l，$m，$n，$o，$p，$q，$r，$t，$u，$v，$w，$x，$y，$z。");
		*/

		AddParseRule("单句", "$a；");
		AddParseRule("单句", "$a。");
		AddParseRule("单句", "$a？");
		AddParseRule("单句", "$a！");
		AddParseRule("单句", "$a？！");

		AddParseRule("通用", "“$a，”");
		AddParseRule("通用", "“$a：”");

		AddParseRule("单句", "“$a；”");
		AddParseRule("单句", "“$a。”");
		AddParseRule("单句", "“$a！”");
		AddParseRule("单句", "“$a？”");

		AddParseRule("单句", "（$a。）");
		AddParseRule("单句", "（$a！）");
		AddParseRule("单句", "（$a？）");

		AddParseRule("单句", "‘$a。’");
		AddParseRule("单句", "‘$a！’");
		AddParseRule("单句", "‘$a？’");

		AddParseRule("单句", "“‘$a。’”");
		AddParseRule("单句", "“‘$a！’”");
		AddParseRule("单句", "“‘$a？’”");

		AddParseRule("单句", "《$a。》");
		AddParseRule("单句", "《$a！》");
		AddParseRule("单句", "《$a？》");

		AddParseRule("通用", "【$a：】");
		AddParseRule("单句", "【$a。】");
		AddParseRule("单句", "【$a！】");
		AddParseRule("单句", "【$a？】");

		AddParseRule("单句", "$a：$b。");
		AddParseRule("单句", "$a：$b！");
		AddParseRule("单句", "$a：$b？");

		AddParseRule("单句", "$a：“$b”");
		AddParseRule("单句", "“$a，”$b。");
		AddParseRule("单句", "“$a：”$b。");
		AddParseRule("单句", "“$a；”$b。");
		AddParseRule("单句", "“$a。”$b。");
		AddParseRule("单句", "“$a？”$b。");
		AddParseRule("单句", "“$a！”$b。");

		AddParseRule("单句", "$a“$b。”");
		AddParseRule("单句", "$a“$b？”");
		AddParseRule("单句", "$a“$b！”");

		AddParseRule("单句", "$a，“$b；”");
		AddParseRule("单句", "$a，“$b。”");
		AddParseRule("单句", "$a，“$b？”");
		AddParseRule("单句", "$a，“$b！”");

		AddParseRule("单句", "$a：“$b。”");
		AddParseRule("单句", "$a：“$b？”");
		AddParseRule("单句", "$a：“$b！”");

		AddParseRule("单句", "$a（$b。）");
		AddParseRule("单句", "$a（$b？）");
		AddParseRule("单句", "$a（$b！）");

		AddParseRule("单句", "$a：（$b。）");
		AddParseRule("单句", "$a：（$b？）");
		AddParseRule("单句", "$a：（$b！）");

		AddParseRule("单句", "$a‘$b。’");
		AddParseRule("单句", "$a‘$b？’");
		AddParseRule("单句", "$a‘$b！’");

		AddParseRule("单句", "‘$a，’$b。");
		AddParseRule("单句", "‘$a。’$b。");
		AddParseRule("单句", "‘$a？’$b。");
		AddParseRule("单句", "‘$a！’$b。");

		AddParseRule("单句", "$a：‘$b。’");
		AddParseRule("单句", "$a：‘$b？’");
		AddParseRule("单句", "$a：‘$b！’");

		AddParseRule("单句", "“$a‘$b。’”");
		AddParseRule("单句", "“$a‘$b？’”");
		AddParseRule("单句", "“$a‘$b！’”");

		AddParseRule("单句", "“$a！‘$b！’”");
		AddParseRule("单句", "“$a‘$b？’。”");

		AddParseRule("单句", "“$a，‘$b。’”");
		AddParseRule("单句", "“$a，‘$b？’”");
		AddParseRule("单句", "“$a，‘$b！’”");

		AddParseRule("单句", "“$a：‘$b。’”");
		AddParseRule("单句", "“$a：‘$b？’”");
		AddParseRule("单句", "“$a：‘$b！’”");

		AddParseRule("单句", "“$a。‘$b。’”");

		AddParseRule("单句", "“$a。‘$b？’”");

		AddParseRule("单句", "“$a。‘$b！’”");

		AddParseRule("单句", "“‘$a，’”$b。");
		AddParseRule("单句", "“‘$a，’”$b？");
		AddParseRule("单句", "“‘$a，’”$b！");

		AddParseRule("复句", "$a《$b。》$c");
		AddParseRule("复句", "$a《$b！》$c");
		AddParseRule("复句", "$a《$b？》$c");

		AddParseRule("复句", "$a（$b。）$c");
		AddParseRule("复句", "$a（$b！），$c");
		AddParseRule("复句", "$a（$b？），$c");

		AddParseRule("复句", "$a！（$b。）$c");
		AddParseRule("复句", "$a！（$b！）$c");
		AddParseRule("复句", "$a！（$b？）$c");

		AddParseRule("单句", "‘$a，’$b。‘$c。’");

		AddParseRule("单句", "‘$a，’$b，‘$c。’");
		AddParseRule("单句", "‘$a，’$b，‘$c？’");
		AddParseRule("单句", "‘$a，’$b，‘$c！’");
		AddParseRule("单句", "‘$a！’$b，‘$c。’");
		AddParseRule("单句", "‘$a！’$b。‘$c！’");

		AddParseRule("单句", "“$a：‘$b？’$c”");
		AddParseRule("单句", "“$a：‘$b！’$c”");
		AddParseRule("单句", "“$a：‘$b。’$c”");

		AddParseRule("单句", "“$a：‘$b？’$c。”");
		AddParseRule("单句", "“$a：‘$b！’$c。”");
		AddParseRule("单句", "“$a：‘$b。’$c。”");

		AddParseRule("单句", "“$a‘$b！’，$c。”");
		AddParseRule("单句", "“$a。（$b。）$c！”");
		AddParseRule("单句", "“$a：”$b。“$c？”");

		AddParseRule("单句", "$a，“$b，”$c。");

		AddParseRule("单句", "$a：“$b；$c。”");
		AddParseRule("单句", "$a：“$b。$c。”");
		AddParseRule("单句", "$a：“$b？$c。”");
		AddParseRule("单句", "$a：“$b！$c。”");
		AddParseRule("单句", "$a：“$b。$c？”");
		AddParseRule("单句", "$a：“$b。$c！”");

		AddParseRule("单句", "“$a？！$b！”$c。");
		AddParseRule("单句", "“$a。”$b，“$c。”");
		AddParseRule("单句", "“$a，”$b，“$c。”");
		AddParseRule("单句", "“$a？”$b，“$c。”");
		AddParseRule("单句", "“$a！”$b，“$c。”");
		AddParseRule("单句", "“$a，”$b；“$c。”");
		AddParseRule("单句", "“$a，”$b：“$c。”");
		AddParseRule("单句", "“$a，”$b，“$c？”");
		AddParseRule("单句", "“$a！$b，”“$c！”");

		AddParseRule("单句", "$a，“$b；$c，”$d。");
		AddParseRule("单句", "$a，“$b，”$c，“$d。”");
		AddParseRule("单句", "$a，“$b‘$c，’$d。”");
		AddParseRule("单句", "$a：“$b：‘$c!’$d。”");
		AddParseRule("单句", "$a：“$b：‘$c。’$d！”");

		AddParseRule("单句", "“$a：‘$b。’$c：‘$d？’$e：‘$f。’”");

		// 记录日志
		Log.LogMessage("ParseRule", "InitializeParseRule", "数据表已初始化！");
	}

	public static void AddParseRule(string strClassification, string strRule)
	{
		// 删除标记
		string strValue = Regex.Replace(strRule, "[a-zA-Z]", "");
		// 检查规则
		int nMinimumLength = strValue.Length;
		int nParameterCount = Common.GetCharCount(strValue, '$');
		// 检查规则
		bool bNormalized = strValue.IndexOf("$$") < 0;
		bool bStaticPrefix = strValue.IndexOf("$") < 0;
		bool bStaticSuffix = strValue.LastIndexOf("$") < strValue.Length - 1;

		// 设置优先级
		int nControllablePriority = 0;
		// 以普通分隔符结尾
		if (strValue.EndsWith("…") || strValue.EndsWith("—")
			|| strValue.EndsWith("：") || strValue.EndsWith(" "))
		{
			// 设置优先级
			nControllablePriority = 1;
		}
		// 以句子标点结尾
		else if (strValue.EndsWith("。") || strValue.EndsWith("！")
			|| strValue.EndsWith("？") || strValue.EndsWith("；"))
		{
			// 设置优先级
			nControllablePriority = 3;
		}
		// 以成对分隔符标点结尾
		else if (strValue.EndsWith("’")
			|| strValue.EndsWith("）") || strValue.EndsWith("》")
			|| strValue.EndsWith("】") || strValue.EndsWith("〉"))
		{
			// 设置优先级
			nControllablePriority = 2;
		}
		// 以语句开头为结尾
		else if (strValue.EndsWith("：“") || strValue.EndsWith("，“"))
		{
			// 设置优先级
			nControllablePriority = 1;
		}
		// 以成对分隔符标点结尾
		else if (strValue.EndsWith("”"))
        {
			// 设置缺省值
			nControllablePriority = 2;
			// 以普通分隔符结尾
			if (strValue.EndsWith("，”") || strValue.EndsWith("；”"))
			{
				// 设置优先级
				//nControllablePriority = 2;
			}
			// 以句子标点结尾
			else if (strValue.EndsWith("。”") || strValue.EndsWith("？”")
				|| strValue.EndsWith("！”") || strValue.EndsWith("…”") || strValue.EndsWith("—”"))
			{
				nControllablePriority = 3;
			}
		}

		// 在数据库中也增加一条规则
		string cmdString = string.Format("INSERT INTO [dbo].[ParseRule] " +
			"([classification], [rule], [normalized], [static_suffix], [static_prefix], [minimum_length], [parameter_count], [controllable_priority]) " +
			"VALUES (@SqlClassification, @SqlRule, {0}, {1}, {2}, {3}, {4}, {5}); ",
			bNormalized ? 1 : 0, bStaticSuffix ? 1 : 0, bStaticPrefix ? 1 : 0, nMinimumLength, nParameterCount, nControllablePriority);
		// 设置参数
		Dictionary<string, string> parameters = new Dictionary<string, string>();
		// 加入参数
		parameters.Add("SqlClassification", strClassification);
		parameters.Add("SqlRule", strRule);
		// 执行指令
		NLDB.ExecuteNonQuery(cmdString, parameters);
	}
}
