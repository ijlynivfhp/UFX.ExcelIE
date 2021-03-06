USE [UFO_SCM]
GO
/****** Object:  Table [dbo].[CO_ExcelExportSQL]    Script Date: 2021/5/13 14:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CO_ExcelExportSQL](
	[Id] [uniqueidentifier] NOT NULL,
	[TemplateType] [tinyint] NOT NULL,
	[TemplateCode] [nvarchar](50) NULL,
	[TemplateName] [nvarchar](200) NULL,
	[TemplateUrl] [nvarchar](500) NOT NULL,
	[SourceType] [int] NOT NULL,
	[SourceUrl] [nvarchar](500) NULL,
	[ExportHead] [text] NOT NULL,
	[ExecSQL] [text] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[ExecMaxCountPer] [int] NOT NULL,
	[MainTableSign] [nvarchar](50) NOT NULL,
	[OrderField] [nvarchar](20) NULL,
	[Sort] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateUser] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyUser] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_CO_EXCELEXPORTSQL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CO_ExcelExportSQLLog]    Script Date: 2021/5/13 14:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CO_ExcelExportSQLLog](
	[Id] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[TemplateSQL] [text] NULL,
	[ExportSQL] [text] NULL,
	[ExportParameters] [text] NULL,
	[ExportDurationWrite] [decimal](18, 0) NOT NULL,
	[ExportDurationQuery] [decimal](18, 0) NOT NULL,
	[ExportDurationTask] [decimal](18, 0) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[ExportMsg] [nvarchar](200) NULL,
	[DownLoadUrl] [nvarchar](500) NULL,
	[DownLoadCount] [tinyint] NULL,
	[FileName] [nvarchar](200) NULL,
	[FileSize] [nvarchar](50) NULL,
	[ExportCount] [int] NULL,
	[ExecCount] [int] NOT NULL,
	[TenantId] [uniqueidentifier] NULL,
	[CreateUserId] [uniqueidentifier] NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ModifyUser] [nvarchar](50) NULL,
	[ModifyTime] [datetime] NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_CO_EXCELEXPORTSQLLOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a11', 0, N'IE20210408004', N'客户额度管理流水', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":1},{"FieldDbName":"A.PayNo","FieldEnName":"PayNo","FieldChName":"支付编号","IsHide":1},{"FieldDbName":"A.PayAccount","FieldEnName":"PayAccount","FieldChName":"支付帐号","IsHide":0},{"FieldDbName":"A.PayTime","FieldEnName":"PayTime","FieldChName":"支付时间","IsHide":0}]', N'SELECT #SelectSql#
FROM dbo.DB_CustomerQuotaManageFlow AS A WITH (NOLOCK)
    INNER JOIN dbo.DB_Customer AS B WITH (NOLOCK)
        ON A.CustomerId = B.Id
    INNER JOIN dbo.FX_BasicData AS C WITH (NOLOCK)
        ON A.BelongMainId = C.Id
    LEFT OUTER JOIN dbo.FX_BasicData AS D WITH (NOLOCK)
        ON A.PayType = D.Id
    LEFT OUTER JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON A.RevenueAccount = E.Id', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a12', 0, N'IE20210408005', N'样衣出入库', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"1"},{"FieldDbName":"B.BillCode","FieldEnName":"BillCode","FieldChName":"单据编号","IsHide":"0"},{"FieldDbName":"G.CfgValue","FieldEnName":"BillType","FieldChName":"单据类型","IsHide":"0"},{"FieldDbName":"CASE B.Statuz            WHEN 0 THEN                ''未审核''            ELSE                ''已审核''        END","FieldEnName":"Statuz","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"StoreName","FieldChName":"原仓库","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"ToStoreName","FieldChName":"调入仓库","IsHide":"0"},{"FieldDbName":"B.TotalQty","FieldEnName":"TotalQty","FieldChName":"总数量","IsHide":"0"},{"FieldDbName":"B.TotalPrice","FieldEnName":"TotalPrice","FieldChName":"总金额","IsHide":"0"},{"FieldDbName":"B.BillDate","FieldEnName":"BillDate","FieldChName":"制单日期","IsHide":"0"},{"FieldDbName":"B.CheckDate","FieldEnName":"CheckDate","FieldChName":"审核日期","IsHide":"0"},{"FieldDbName":"B.CheckMan","FieldEnName":"CheckMan","FieldChName":"审核人","IsHide":"0"},{"FieldDbName":"B.CreateUser","FieldEnName":"CreateUser","FieldChName":"制单人","IsHide":"0"},{"FieldDbName":"B.RelateMan","FieldEnName":"RelateMan","FieldChName":"干系人","IsHide":"0"},{"FieldDbName":"D.SmCode","FieldEnName":"SmCode","FieldChName":"样衣号","IsHide":"0"},{"FieldDbName":"C.SmSkuCode","FieldEnName":"SmSkuCode","FieldChName":"样衣代码","IsHide":"0"},{"FieldDbName":"C.ColorName","FieldEnName":"ColorName","FieldChName":"颜色","IsHide":"0"},{"FieldDbName":"C.SizeName","FieldEnName":"SizeName","FieldChName":"尺码","IsHide":"0"},{"FieldDbName":"A.Price","FieldEnName":"Price","FieldChName":"价格","IsHide":"0"},{"FieldDbName":"A.Qty","FieldEnName":"Qty","FieldChName":"数量","IsHide":"0"},{"FieldDbName":"SUBSTRING(ISNULL(A.SkuCode, ''''), CHARINDEX(''.'', A.SkuCode) + 1, LEN(A.SkuCode) - CHARINDEX(''.'', A.SkuCode))","FieldEnName":"SampleVersion","FieldChName":"版次","IsHide":"0"},{"FieldDbName":"A.DifferQty","FieldEnName":"DifferQty","FieldChName":"损益","IsHide":"0"},{"FieldDbName":"A.StockQty","FieldEnName":"StockQty","FieldChName":"库存数量","IsHide":"0"},{"FieldDbName":"B.Remark","FieldEnName":"AllRemark","FieldChName":"总备注","IsHide":"0"},{"FieldDbName":"A.Remark","FieldEnName":"Remark","FieldChName":"明细备注","IsHide":"0"}]', N'SELECT #SelectSql#
FROM SM_SampleStoreSub AS A WITH (NOLOCK)
    INNER JOIN SM_SampleStore AS B WITH (NOLOCK)
        ON A.BillId = B.Id
    LEFT JOIN SM_SampleClothingSku AS C WITH (NOLOCK)
        ON A.SkuId = C.Id
    LEFT JOIN SM_SampleClothing AS D WITH (NOLOCK)
        ON C.SmId = D.Id
    LEFT JOIN FX_BasicData AS E WITH (NOLOCK)
        ON B.StoreId = E.Id
    LEFT JOIN FX_BasicData AS F WITH (NOLOCK)
        ON B.ToStoreId = F.Id
    LEFT JOIN FX_BasicData AS G WITH (NOLOCK)
        ON B.BillType = G.Id
    LEFT JOIN FX_BasicData AS H WITH (NOLOCK)
        ON H.CfgKey = A.SkuCode', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a13', 0, N'IE20210408006', N'样衣借还单据', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"1"},{"FieldDbName":"ImgUrl","FieldEnName":"ImgUrl","FieldChName":"样衣图片","IsHide":"0"},{"FieldDbName":"CASE A.Statuz     WHEN 9 THEN     ''已作废''     ELSE     (CASE     WHEN     (     A.UseStatuz = 1     AND A.BackQty = 0     AND A.BackStatuz = 0     ) THEN     ''待归还''     ELSE     (CASE     WHEN     (     A.BackQty > 0     AND A.BackQty < A.UseQty     AND A.UseStatuz = 1     ) THEN     ''部分归还''     ELSE     (CASE     WHEN     (     A.BackQty = A.UseQty     AND A.BackStatuz = 1     ) THEN     ''已归还''     ELSE     ''未生效''     END     )     END     )     END     )     END","FieldEnName":"Statuz","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"FxStore","FieldChName":"仓库","IsHide":"0"},{"FieldDbName":"B.SmName","FieldEnName":"SmName","FieldChName":"样衣名称","IsHide":"0"},{"FieldDbName":"B.SmCode","FieldEnName":"SmCode","FieldChName":"样衣编码","IsHide":"0"},{"FieldDbName":"B.GoodCode","FieldEnName":"GoodCode","FieldChName":"货号","IsHide":"0"},{"FieldDbName":"A.SkuCode","FieldEnName":"SkuCode","FieldChName":"Sku编码","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"BrandName","FieldChName":"品牌","IsHide":"0"},{"FieldDbName":"D.ColorName","FieldEnName":"ColorName","FieldChName":"颜色","IsHide":"0"},{"FieldDbName":"D.SizeName","FieldEnName":"SizeName","FieldChName":"尺码","IsHide":"0"},{"FieldDbName":"A.FirstUser","FieldEnName":"FirstUser","FieldChName":"借出人","IsHide":"0"},{"FieldDbName":"A.FirstUseTime","FieldEnName":"FirstUseTime","FieldChName":"借出时间","IsHide":"0"},{"FieldDbName":"ISNULL(G.SpShortName, H.ShortName)","FieldEnName":"TargetName","FieldChName":"借出目标","IsHide":"0"},{"FieldDbName":"B.SmListDate","FieldEnName":"SmListDate","FieldChName":"上市日期","IsHide":"0"},{"FieldDbName":"A.LastUser","FieldEnName":"LastUser","FieldChName":"最后经手人","IsHide":"0"},{"FieldDbName":"A.UseQty","FieldEnName":"UseQty","FieldChName":"借出数量","IsHide":"0"},{"FieldDbName":"CASE A.UseStatuz            WHEN 0 THEN                ''未生效''            ELSE                ''已生效''        END","FieldEnName":"UseStatuz","FieldChName":"库存状态","IsHide":"0"},{"FieldDbName":"A.BackQty","FieldEnName":"BackQty","FieldChName":"归还数量","IsHide":"0"},{"FieldDbName":"A.BackUser","FieldEnName":"BackUser","FieldChName":"归还人","IsHide":"0"},{"FieldDbName":"CASE A.BackStatuz            WHEN 0 THEN                ''未生效''            ELSE                ''已生效''        END","FieldEnName":"BackStatuz","FieldChName":"归还状态","IsHide":"0"},{"FieldDbName":"A.CreateUser","FieldEnName":"CreateUser","FieldChName":"创建人","IsHide":"0"},{"FieldDbName":"A.CreateTime","FieldEnName":"CreateTime","FieldChName":"创建时间","IsHide":"0"},{"FieldDbName":"A.ModifyUser","FieldEnName":"ModifyUser","FieldChName":"最后修改人","IsHide":"0"},{"FieldDbName":"A.ModifyTime","FieldEnName":"ModifyTime","FieldChName":"修改时间","IsHide":"0"},{"FieldDbName":"A.Remark","FieldEnName":"Remark","FieldChName":"备注","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.SM_SampleStoreUse AS A WITH (NOLOCK)
    INNER JOIN dbo.SM_SampleClothingSku AS D WITH (NOLOCK)
        ON A.SkuId = D.Id
    INNER JOIN dbo.SM_SampleClothing AS B WITH (NOLOCK)
        ON D.SmId = B.Id
    LEFT OUTER JOIN dbo.SM_SampleClothingSkc AS C WITH (NOLOCK)
        ON D.SmId = C.SmId
           AND C.ColorId = D.ColorId
    INNER JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON A.StoreId = E.Id
    LEFT OUTER JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON B.BrandId = F.Id
    LEFT OUTER JOIN dbo.DB_Supplier AS G WITH (NOLOCK)
        ON A.SupplierId = G.Id
    LEFT OUTER JOIN dbo.DB_Customer AS H WITH (NOLOCK)
        ON A.CustomerId = H.Id', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a14', 0, N'IE20210408007', N'样衣库存', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"A.SmCode","FieldEnName":"SmCode","FieldChName":"样衣编号","IsHide":"0"},{"FieldDbName":"A.SmName","FieldEnName":"SmName","FieldChName":"样衣名称","IsHide":"0"},{"FieldDbName":"A.SmSkuCode","FieldEnName":"SmSkuCode","FieldChName":"样衣SKU","IsHide":"0"},{"FieldDbName":"B.CfgValue","FieldEnName":"SampleVersion","FieldChName":"版次","IsHide":"0"},{"FieldDbName":"A.ColorName","FieldEnName":"ColorName","FieldChName":"颜色","IsHide":"0"},{"FieldDbName":"A.SizeName","FieldEnName":"SizeName","FieldChName":"尺码","IsHide":"0"},{"FieldDbName":"A.StoreCode","FieldEnName":"StoreCode","FieldChName":"仓库编号","IsHide":"0"},{"FieldDbName":"A.StoreName","FieldEnName":"StoreName","FieldChName":"仓库名称","IsHide":"0"},{"FieldDbName":"A.SumQty","FieldEnName":"SumQty","FieldChName":"库存数量","IsHide":"0"},{"FieldDbName":"A.OriginBrand","FieldEnName":"OriginBrand","FieldChName":"原品牌","IsHide":"0"},{"FieldDbName":"A.UserName","FieldEnName":"UserName","FieldChName":"设计师","IsHide":"0"},{"FieldDbName":"A.OriginCountry","FieldEnName":"OriginCountry","FieldChName":"产地","IsHide":"0"},{"FieldDbName":"A.CreateTime","FieldEnName":"CreateTime","FieldChName":"创建时间","IsHide":"1"}]', N'SELECT #SelectSql#
FROM dbo.view_SampleStoreLog A WITH (NOLOCK)
    LEFT JOIN dbo.FX_BasicData B WITH (NOLOCK)
        ON B.CfgGroup = ''样板类型''
           AND B.CfgKey = SUBSTRING(
                                       ISNULL(A.SmSkuCode, ''''),
                                       CHARINDEX(''.'', A.SmSkuCode) + 1,
                                       LEN(A.SmSkuCode) - CHARINDEX(''.'', A.SmSkuCode)
                                   )', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a15', 0, N'IE20210408008', N'样衣库存日志', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"A.BillCode","FieldEnName":"BillCode","FieldChName":"单号","IsHide":"0"},{"FieldDbName":"C.SmName","FieldEnName":"SmName","FieldChName":"样衣名称","IsHide":"0"},{"FieldDbName":"A.SkuCode","FieldEnName":"SkuCode","FieldChName":"样衣代码","IsHide":"0"},{"FieldDbName":"C.CostPrice","FieldEnName":"CostPrice","FieldChName":"成本价","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"FxBillType","FieldChName":"单据类型","IsHide":"0"},{"FieldDbName":"A.RelateMan","FieldEnName":"RelateMan","FieldChName":"干系人","IsHide":"0"},{"FieldDbName":"D.BillDate","FieldEnName":"BillDate","FieldChName":"制单时间","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"StoreName","FieldChName":"仓库名称","IsHide":"0"},{"FieldDbName":"A.Qty","FieldEnName":"Qty","FieldChName":"变动数量","IsHide":"0"},{"FieldDbName":"CASE WHEN A.Statuz=9 THEN ''作废'' ELSE ''生效'' END","FieldEnName":"Statuz","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"A.ModifyTime","FieldEnName":"ModifyTime","FieldChName":"最后操作时间","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.SM_SampleStoreLog AS A WITH (NOLOCK)
    LEFT JOIN dbo.SM_SampleClothingSku AS B WITH (NOLOCK)
        ON A.SkuId = B.Id
    LEFT JOIN dbo.SM_SampleClothing AS C WITH (NOLOCK)
        ON B.SmId = C.Id
    LEFT JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON A.BillType = E.Id
    LEFT JOIN dbo.SM_SampleStore AS D WITH (NOLOCK)
        ON A.BillId = D.Id
    LEFT JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON A.StoreId = F.Id', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a16', 0, N'IE20210408009', N'样衣核销', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"G.ImgUrl","FieldEnName":"ImgUrl","FieldChName":"图片","IsHide":"0"},{"FieldDbName":"CASE B.Statuz            WHEN 0 THEN                ''未审核''            WHEN 1 THEN                ''已审核''            WHEN 2 THEN                ''已核销''            WHEN 3 THEN                ''已确认''            ELSE                ''作废''        END","FieldEnName":"Statuz","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"B.Code","FieldEnName":"Code","FieldChName":"单据编码","IsHide":"0"},{"FieldDbName":"B.SourceCode","FieldEnName":"SourceCode","FieldChName":"来源单号","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"BillType","FieldChName":"入库类型","IsHide":"0"},{"FieldDbName":"B.RelateMan","FieldEnName":"RelateMan","FieldChName":"报销人","IsHide":"0"},{"FieldDbName":"B.FollowMan","FieldEnName":"FollowMan","FieldChName":"跟单员","IsHide":"0"},{"FieldDbName":"B.Remark","FieldEnName":"Remark","FieldChName":"备注","IsHide":"0"},{"FieldDbName":"A.SkuCode","FieldEnName":"SkuCode","FieldChName":"样衣编码","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"SmSrc","FieldChName":"样衣来源","IsHide":"0"},{"FieldDbName":"C.ColorName","FieldEnName":"ColorName","FieldChName":"颜色","IsHide":"0"},{"FieldDbName":"C.SizeName","FieldEnName":"SizeName","FieldChName":"尺码","IsHide":"0"},{"FieldDbName":"A.OtherFee","FieldEnName":"OtherFee","FieldChName":"其他费用","IsHide":"0"},{"FieldDbName":"A.Price","FieldEnName":"Price","FieldChName":"价格","IsHide":"0"},{"FieldDbName":"A.Qty","FieldEnName":"Qty","FieldChName":"数量","IsHide":"0"},{"FieldDbName":"A.Remark","FieldEnName":"DetailRemark","FieldChName":"明细备注","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.SM_SampleFeeSub AS A WITH (NOLOCK)
    INNER JOIN dbo.SM_SampleFee AS B WITH (NOLOCK)
        ON A.FeeId = B.Id
    LEFT OUTER JOIN dbo.SM_SampleClothingSku AS C WITH (NOLOCK)
        ON A.SkuId = C.Id
    LEFT OUTER JOIN dbo.SM_SampleClothing AS D WITH (NOLOCK)
        ON C.SmId = D.Id
    LEFT OUTER JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON B.SourceId = E.Id
    LEFT OUTER JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON D.SmSrc = F.Id
    LEFT OUTER JOIN dbo.SM_SampleClothingSkc G WITH (NOLOCK)
        ON G.SmId = C.SmId
           AND G.ColorId = C.ColorId', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a17', 0, N'IE20210408010', N'吊牌信息', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"CASE A.Statuz WHEN -1 THEN ''未填写'' WHEN 0 THEN ''未审核'' WHEN 1 THEN ''已审核'' WHEN 7 THEN ''已结案'' WHEN 9 THEN ''已作废'' ELSE CAST(A.Statuz AS VARCHAR(200)) END","FieldEnName":"Statuz","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"CASE A.IsPdtSuit WHEN 0 THEN ''否'' WHEN 1 THEN ''是'' ELSE '''' END","FieldEnName":"IsPdtSuit","FieldChName":"是否有套装数据","IsHide":"0"},{"FieldDbName":"A.Code","FieldEnName":"Code","FieldChName":"款式编号","IsHide":"0"},{"FieldDbName":"ProductSkuColor.ColorNames","FieldEnName":"ColorNames","FieldChName":"颜色","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"SpecGroup","FieldChName":"尺码类型","IsHide":"0"},{"FieldDbName":"ProductExtItems.ItemDescs","FieldEnName":"ItemDescs","FieldChName":"尺码明细","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"WashMark","FieldChName":"洗涤模板","IsHide":"0"},{"FieldDbName":"A.BrandName","FieldEnName":"BrandName","FieldChName":"品牌","IsHide":"0"},{"FieldDbName":"A.YearName","FieldEnName":"YearName","FieldChName":"年份","IsHide":"0"},{"FieldDbName":"A.SeasonName","FieldEnName":"SeasonName","FieldChName":"季节","IsHide":"0"},{"FieldDbName":"A.BandName","FieldEnName":"BandName","FieldChName":"波段","IsHide":"0"},{"FieldDbName":"A.Name","FieldEnName":"Name","FieldChName":"名称","IsHide":"0"},{"FieldDbName":"A.CtgName","FieldEnName":"CtgName","FieldChName":"类目","IsHide":"0"},{"FieldDbName":"A.PdtSrcName","FieldEnName":"PdtSrcName","FieldChName":"来源","IsHide":"0"},{"FieldDbName":"A.ListDate","FieldEnName":"ListDate","FieldChName":"上市日期","IsHide":"0"},{"FieldDbName":"A.Price","FieldEnName":"Price","FieldChName":"吊牌价","IsHide":"0"},{"FieldDbName":"ProductSkuGb.GbCodes","FieldEnName":"GbCodes","FieldChName":"国际码","IsHide":"0"},{"FieldDbName":"A.StandardRule","FieldEnName":"StandardRule","FieldChName":"执行标准","IsHide":"0"},{"FieldDbName":"A.SafeLevel","FieldEnName":"SafeLevel","FieldChName":"安全等级","IsHide":"0"},{"FieldDbName":"A.LevelInfo","FieldEnName":"LevelInfo","FieldChName":"等级","IsHide":"0"},{"FieldDbName":"A.Composition","FieldEnName":"Composition","FieldChName":"成份","IsHide":"0"},{"FieldDbName":"A.DesignUserName","FieldEnName":"DesignUserName","FieldChName":"设计师","IsHide":"0"},{"FieldDbName":"B.CreateTime","FieldEnName":"CreateTime","FieldChName":"转大货时间","IsHide":"0"},{"FieldDbName":"A.PrintCount","FieldEnName":"PrintCount","FieldChName":"打印次数","IsHide":"0"},{"FieldDbName":"A.ModifyCount","FieldEnName":"ModifyCount","FieldChName":"填写次数","IsHide":"0"},{"FieldDbName":"A.ModifyUser","FieldEnName":"ModifyUser","FieldChName":"更新人","IsHide":"0"},{"FieldDbName":"A.ModifyTime","FieldEnName":"ModifyTime","FieldChName":"更新时间","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.view_PdtExtInfo AS A WITH (NOLOCK)
    LEFT JOIN dbo.DB_Product AS B WITH (NOLOCK)
        ON A.PdtId = B.Id
    LEFT JOIN dbo.FX_BasicData AS C WITH (NOLOCK)
        ON B.SizeGroupId = C.Id
    LEFT JOIN dbo.FX_BasicData AS D WITH (NOLOCK)
        ON B.PdtSrc = D.Id
    LEFT JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON A.SpecGroupId = E.Id
    LEFT JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON A.WashMarkId = F.Id
    LEFT JOIN
    (
        SELECT ProductSku.PdtId,
               LEFT(STUFF(
                    (
                        SELECT DISTINCT
                               ColorName + '',''
                        FROM DB_ProductSku WITH (NOLOCK)
                        WHERE ProductSku.PdtId = PdtId
                        FOR XML PATH('''')
                    ),
                    1,
                    0,
                    ''''
                         ), LEN(STUFF(
                                (
                                    SELECT DISTINCT
                                           ColorName + '',''
                                    FROM DB_ProductSku WITH (NOLOCK)
                                    WHERE ProductSku.PdtId = PdtId
                                    FOR XML PATH('''')
                                ),
                                1,
                                0,
                                ''''
                                     )
                               ) - 1) AS ColorNames
        FROM DB_ProductSku AS ProductSku WITH (NOLOCK)
        GROUP BY PdtId
    ) ProductSkuColor
        ON ProductSkuColor.PdtId = A.PdtId
    LEFT JOIN
    (
        SELECT ProductSku.PdtId,
               LEFT(STUFF(
                    (
                        SELECT DISTINCT
                               GbCode + '',''
                        FROM DB_ProductSku WITH (NOLOCK)
                        WHERE ProductSku.PdtId = PdtId
                        FOR XML PATH('''')
                    ),
                    1,
                    0,
                    ''''
                         ), LEN(STUFF(
                                (
                                    SELECT DISTINCT
                                           GbCode + '',''
                                    FROM DB_ProductSku WITH (NOLOCK)
                                    WHERE ProductSku.PdtId = PdtId
                                    FOR XML PATH('''')
                                ),
                                1,
                                0,
                                ''''
                                     )
                               ) - 1) AS GbCodes
        FROM DB_ProductSku AS ProductSku WITH (NOLOCK)
        GROUP BY PdtId
    ) ProductSkuGb
        ON ProductSkuGb.PdtId = A.PdtId
    LEFT JOIN
    (
        SELECT ProductExtItem.PdtId,
               LEFT(STUFF(
                    (
                        SELECT DISTINCT
                               ItemDesc + '',''
                        FROM DB_ProductExtItem WITH (NOLOCK)
                        WHERE ProductExtItem.PdtId = PdtId
                              AND ItemType = ''尺码规格''
                        FOR XML PATH('''')
                    ),
                    1,
                    0,
                    ''''
                         ), LEN(STUFF(
                                (
                                    SELECT DISTINCT
                                           ItemDesc + '',''
                                    FROM DB_ProductExtItem WITH (NOLOCK)
                                    WHERE ProductExtItem.PdtId = PdtId
                                          AND ItemType = ''尺码规格''
                                    FOR XML PATH('''')
                                ),
                                1,
                                0,
                                ''''
                                     )
                               ) - 1) AS ItemDescs
        FROM DB_ProductExtItem AS ProductExtItem WITH (NOLOCK)
        WHERE ItemType = ''尺码规格''
        GROUP BY PdtId
    ) ProductExtItems
        ON ProductExtItems.PdtId = A.PdtId', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a18', 0, N'IE20210408011', N'款式询报价', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"A.Code","FieldEnName":"Code","FieldChName":"询价单号","IsHide":"0"},{"FieldDbName":"B.Code","FieldEnName":"PdtCode","FieldChName":"款式编号","IsHide":"0"},{"FieldDbName":"B.Name","FieldEnName":"Name","FieldChName":"款式名称","IsHide":"0"},{"FieldDbName":"A.ColorName","FieldEnName":"ColorName","FieldChName":"颜色","IsHide":"0"},{"FieldDbName":"C.CfgValue","FieldEnName":"PdtSrcName","FieldChName":"款式来源","IsHide":"0"},{"FieldDbName":"D.CfgValue","FieldEnName":"CtgName","FieldChName":"款式分类","IsHide":"0"},{"FieldDbName":"B.ListDate","FieldEnName":"ListDate","FieldChName":"上市日期","IsHide":"0"},{"FieldDbName":"B.Designer","FieldEnName":"Designer","FieldChName":"设计师","IsHide":"0"},{"FieldDbName":"E.SpShortName","FieldEnName":"SpShortName","FieldChName":"供应商","IsHide":"0"},{"FieldDbName":"CASE A.Statuz WHEN 0 THEN ''待审核'' WHEN 1 THEN ''待报价'' WHEN 2 THEN ''已报价'' WHEN 3 THEN ''已确认'' WHEN 80 THEN ''审批中'' WHEN 81 THEN ''已拒绝'' ELSE ''作废'' END","FieldEnName":"Statuz","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"A.FobDate","FieldEnName":"FobDate","FieldChName":"制单日期","IsHide":"0"},{"FieldDbName":"A.AuditDate","FieldEnName":"AuditDate","FieldChName":"审核日期","IsHide":"0"},{"FieldDbName":"A.OfferDate","FieldEnName":"OfferDate","FieldChName":"报价日期","IsHide":"0"},{"FieldDbName":"A.ConfirOfferDate","FieldEnName":"ConfirOfferDate","FieldChName":"确认报价日期","IsHide":"0"},{"FieldDbName":"A.TotalPrice","FieldEnName":"TotalPrice","FieldChName":"总报价","IsHide":"0"},{"FieldDbName":"A.TaxRate","FieldEnName":"TaxRate","FieldChName":"税率","IsHide":"0"},{"FieldDbName":"A.DealCostPrice","FieldEnName":"DealCostPrice","FieldChName":"确认价","IsHide":"0"},{"FieldDbName":"A.NegoType","FieldEnName":"NegoType","FieldChName":"议价类型","IsHide":"0"},{"FieldDbName":"A.Coopere","FieldEnName":"Coopere","FieldChName":"是否采纳","IsHide":"0"},{"FieldDbName":"A.SalePrice","FieldEnName":"SalePrice","FieldChName":"销售单价","IsHide":"0"},{"FieldDbName":"A.ProCost","FieldEnName":"ProCost","FieldChName":"加工费","IsHide":"0"},{"FieldDbName":"A.SupplierProCost","FieldEnName":"SupplierProCost","FieldChName":"供应商加工费","IsHide":"0"},{"FieldDbName":"A.Remark","FieldEnName":"Remark","FieldChName":"备注","IsHide":"0"},{"FieldDbName":"A.ModifyUser","FieldEnName":"ModifyUser","FieldChName":"操作人","IsHide":"0"},{"FieldDbName":"A.ModifyTime","FieldEnName":"ModifyTime","FieldChName":"操作时间","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.DB_ProductPkgFob AS A WITH (NOLOCK)
    INNER JOIN dbo.DB_Product AS B WITH (NOLOCK)
        ON A.PdtId = B.Id
    INNER JOIN dbo.FX_BasicData AS C WITH (NOLOCK)
        ON B.PdtSrc = C.Id
    INNER JOIN dbo.FX_BasicData AS D WITH (NOLOCK)
        ON B.CtgId = D.Id
    INNER JOIN dbo.DB_Supplier AS E WITH (NOLOCK)
        ON A.SupplierId = E.Id
    LEFT JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON A.ColorId = F.Id', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a19', 0, N'IE20210408012', N'套装组合', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"CASE A.Statuz WHEN 0 THEN ''未审核'' WHEN 1 THEN ''已审核'' ELSE ''作废'' END","FieldEnName":"Statuz","FieldChName":"审核状态","IsHide":"0"},{"FieldDbName":"A.cpCode","FieldEnName":"cpCode","FieldChName":"流水号","IsHide":"0"},{"FieldDbName":"A.PdtCodes","FieldEnName":"PdtCodes","FieldChName":"款式编号","IsHide":"0"},{"FieldDbName":"B.Name","FieldEnName":"Name","FieldChName":"款式名称","IsHide":"0"},{"FieldDbName":"CASE ISNULL(A.FinancePrice,0) WHEN 0 THEN A.TagPrice ELSE A.FinancePrice END","FieldEnName":"FinanceTagPrice","FieldChName":"吊牌价","IsHide":"0"},{"FieldDbName":"A.CostPrice","FieldEnName":"CostPrice","FieldChName":"成本价","IsHide":"0"},{"FieldDbName":"A.RetailPrice","FieldEnName":"RetailPrice","FieldChName":"建议零售价","IsHide":"0"},{"FieldDbName":"A.FinancePrice","FieldEnName":"FinancePrice","FieldChName":"财务成本价","IsHide":"0"},{"FieldDbName":"C.CfgValue","FieldEnName":"CtgName","FieldChName":"产品分类","IsHide":"0"},{"FieldDbName":"G.CfgValue","FieldEnName":"BrandName","FieldChName":"品牌","IsHide":"0"},{"FieldDbName":"D.CfgValue","FieldEnName":"YearName","FieldChName":"年份","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"SeasonName","FieldChName":"季节","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"BandName","FieldChName":"波段","IsHide":"0"},{"FieldDbName":"A.CheckDate","FieldEnName":"CheckDate","FieldChName":"审核人","IsHide":"0"},{"FieldDbName":"A.CheckMan","FieldEnName":"CheckMan","FieldChName":"审核日期","IsHide":"0"},{"FieldDbName":"A.Remark","FieldEnName":"Remark","FieldChName":"备注","IsHide":"0"},{"FieldDbName":"A.CreateTime","FieldEnName":"CreateTime","FieldChName":"创建时间","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.DB_PdtCostPriceAcnt AS A WITH (NOLOCK)
    INNER JOIN dbo.DB_Product AS B WITH (NOLOCK)
        ON A.PdtId = B.Id
    INNER JOIN dbo.FX_BasicData AS C WITH (NOLOCK)
        ON B.CtgId = C.Id
    INNER JOIN dbo.FX_BasicData AS D WITH (NOLOCK)
        ON B.YearId = D.Id
    INNER JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON B.SeasonId = E.Id
    INNER JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON B.BandId = F.Id
    INNER JOIN dbo.FX_BasicData AS G WITH (NOLOCK)
        ON B.BrandId = G.Id', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a20', 0, N'IE20210408013', N'款式信息SKU', N'', 0, N'', N'[{"FieldDbName":"A.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"A.SkuCode","FieldEnName":"SkuCode","FieldChName":"SKU","IsHide":"0"},{"FieldDbName":"C.Name","FieldEnName":"Name","FieldChName":"款式名称","IsHide":"0"},{"FieldDbName":"A.ColorCode","FieldEnName":"ColorCode","FieldChName":"颜色编码","IsHide":"0"},{"FieldDbName":"A.ColorName","FieldEnName":"ColorName","FieldChName":"颜色名称","IsHide":"0"},{"FieldDbName":"CASE CHARINDEX(''#'',ISNULL(A.CreateUser,'''')) WHEN 1 THEN ''是'' ELSE ''否'' END","FieldEnName":"ColorFrozen","FieldChName":"颜色冻结","IsHide":"0"},{"FieldDbName":"A.SizeCode","FieldEnName":"SizeCode","FieldChName":"尺码编码","IsHide":"0"},{"FieldDbName":"A.SizeName","FieldEnName":"SizeName","FieldChName":"尺码名称","IsHide":"0"},{"FieldDbName":"A.GbCode","FieldEnName":"GbCode","FieldChName":"国标码","IsHide":"0"},{"FieldDbName":"C.Code","FieldEnName":"Code","FieldChName":"款式编号","IsHide":"0"},{"FieldDbName":"C.Sex","FieldEnName":"Sex","FieldChName":"性别","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"BrandName","FieldChName":"品牌","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"PdtSrcName","FieldChName":"来源","IsHide":"0"},{"FieldDbName":"D.SpShortName","FieldEnName":"SpShortName","FieldChName":"供应商","IsHide":"0"},{"FieldDbName":"C.GoodCode","FieldEnName":"GoodCode","FieldChName":"供应商货号","IsHide":"0"},{"FieldDbName":"C.Price","FieldEnName":"Price","FieldChName":"吊牌价","IsHide":"0"},{"FieldDbName":"C.CostPrice","FieldEnName":"CostPrice","FieldChName":"成本价","IsHide":"0"},{"FieldDbName":"C.RetailPrice","FieldEnName":"RetailPrice","FieldChName":"建议零售价","IsHide":"0"},{"FieldDbName":"C.FinancePrice","FieldEnName":"FinancePrice","FieldChName":"财务成本价","IsHide":"0"},{"FieldDbName":"B.CostPrice","FieldEnName":"SkcCostPrice","FieldChName":"颜色成本价","IsHide":"0"},{"FieldDbName":"C.Tags","FieldEnName":"Tags","FieldChName":"标签","IsHide":"0"},{"FieldDbName":"CASE WHEN C.CtgId = C.BigCtgId THEN G.CfgValue ELSE H.CfgValue + ''>'' + G.CfgValue END","FieldEnName":"CtgName","FieldChName":"产品分类","IsHide":"0"},{"FieldDbName":"A.CreateTime","FieldEnName":"CreateTime","FieldChName":"大货时间","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.DB_ProductSku AS A WITH (NOLOCK)
    LEFT JOIN dbo.DB_ProductSkc B WITH (NOLOCK)
        ON A.PdtId = B.PdtId
           AND A.ColorId = B.ColorId
    INNER JOIN dbo.DB_Product AS C WITH (NOLOCK)
        ON A.PdtId = C.Id
    LEFT OUTER JOIN dbo.DB_Supplier AS D WITH (NOLOCK)
        ON C.SupplierId = D.Id
    INNER JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON C.PdtSrc = E.Id
    INNER JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON C.BrandId = F.Id
    INNER JOIN dbo.FX_BasicData AS G WITH (NOLOCK)
        ON C.CtgId = G.Id
    INNER JOIN dbo.FX_BasicData AS H WITH (NOLOCK)
        ON C.BigCtgId = H.Id', 0, 20000, N'A', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
INSERT [dbo].[CO_ExcelExportSQL] ([Id], [TemplateType], [TemplateCode], [TemplateName], [TemplateUrl], [SourceType], [SourceUrl], [ExportHead], [ExecSQL], [Status], [ExecMaxCountPer], [MainTableSign], [OrderField], [Sort], [IsDelete], [CreateUser], [CreateTime], [ModifyUser], [ModifyTime]) VALUES (N'38f24c5a-c1c2-115a-0a14-39fbc2806a21', 0, N'IE20210408014', N'款式信息SPU', N'', 0, N'', N'[{"FieldDbName":"C.Id","FieldEnName":"Id","FieldChName":"主键","IsHide":"0"},{"FieldDbName":"CASE             WHEN CHARINDEX(''HTTP'', ISNULL(C.ImgUrl, '''')) + CHARINDEX(''ftp'', ISNULL(C.ImgUrl, '''')) > 0 THEN                 C.ImgUrl             ELSE                 ''''         END","FieldEnName":"ImgUrl","FieldChName":"图片","IsHide":"0"},{"FieldDbName":" CASE C.BomStatuz            WHEN 2 THEN                ''已审核''            WHEN 1 THEN                ''已填写''            ELSE                ''未填写''        END","FieldEnName":"BomStatuz","FieldChName":"BOM状态","IsHide":"0"},{"FieldDbName":"CASE C.IsDisable            WHEN 0 THEN                ''正常''            ELSE                ''冻结''        END","FieldEnName":"IsDisable","FieldChName":"状态","IsHide":"0"},{"FieldDbName":"L.CfgValue","FieldEnName":"Degree","FieldChName":"等级","IsHide":"0"},{"FieldDbName":"E.CfgValue","FieldEnName":"PdtSrcName","FieldChName":"来源","IsHide":"0"},{"FieldDbName":"C.Code","FieldEnName":"Code","FieldChName":"款式编号","IsHide":"0"},{"FieldDbName":"C.SampleCode","FieldEnName":"SampleCode","FieldChName":"样衣编号","IsHide":"0"},{"FieldDbName":"C.Name","FieldEnName":"Name","FieldChName":"款式名称","IsHide":"0"},{"FieldDbName":"D.SpName","FieldEnName":"SpName","FieldChName":"供应商全称","IsHide":"0"},{"FieldDbName":"D.SpShortName","FieldEnName":"SpShortName","FieldChName":"供应商","IsHide":"0"},{"FieldDbName":"D.SpCode","FieldEnName":"SpCode","FieldChName":"供应商编号","IsHide":"0"},{"FieldDbName":"C.Tags","FieldEnName":"Tags","FieldChName":"标签","IsHide":"0"},{"FieldDbName":"C.ListDate","FieldEnName":"ListDate","FieldChName":"上市日期","IsHide":"0"},{"FieldDbName":"C.Price","FieldEnName":"Price","FieldChName":"吊牌价","IsHide":"0"},{"FieldDbName":"C.CostPrice","FieldEnName":"CostPrice","FieldChName":"成本价","IsHide":"0"},{"FieldDbName":"C.RetailPrice","FieldEnName":"RetailPrice","FieldChName":"建议零售价","IsHide":"0"},{"FieldDbName":"C.FinancePrice","FieldEnName":"FinancePrice","FieldChName":"财务成本价","IsHide":"0"},{"FieldDbName":"CASE            WHEN C.CtgId = C.BigCtgId THEN                G.CfgValue            ELSE                H.CfgValue + ''>'' + G.CfgValue        END","FieldEnName":"CtgName","FieldChName":"产品分类","IsHide":"0"},{"FieldDbName":"C.Sex","FieldEnName":"Sex","FieldChName":"性别","IsHide":"0"},{"FieldDbName":"F.CfgValue","FieldEnName":"BrandName","FieldChName":"品牌","IsHide":"0"},{"FieldDbName":"I.CfgValue","FieldEnName":"YearName","FieldChName":"年份","IsHide":"0"},{"FieldDbName":"J.CfgValue","FieldEnName":"SeasonName","FieldChName":"季节","IsHide":"0"},{"FieldDbName":"K.CfgValue","FieldEnName":"BandName","FieldChName":"波段","IsHide":"0"},{"FieldDbName":"C.SeriesInfo","FieldEnName":"SeriesInfo","FieldChName":"系列","IsHide":"0"},{"FieldDbName":"C.PatternInfo","FieldEnName":"PatternInfo","FieldChName":"花版","IsHide":"0"},{"FieldDbName":"C.StyleInfo","FieldEnName":"StyleInfo","FieldChName":"风格","IsHide":"0"},{"FieldDbName":"C.ProfileInfo","FieldEnName":"ProfileInfo","FieldChName":"廓形","IsHide":"0"},{"FieldDbName":"M.CfgValue","FieldEnName":"DesignGroupName","FieldChName":"设计组","IsHide":"0"},{"FieldDbName":"C.Designer","FieldEnName":"Designer","FieldChName":"设计师","IsHide":"0"},{"FieldDbName":"C.GoodCode","FieldEnName":"GoodCode","FieldChName":"货号","IsHide":"0"},{"FieldDbName":"C.Composition","FieldEnName":"Composition","FieldChName":"成份","IsHide":"0"},{"FieldDbName":"C.SafeLevel","FieldEnName":"SafeLevel","FieldChName":"安全类别","IsHide":"0"},{"FieldDbName":"C.StandardRule","FieldEnName":"StandardRule","FieldChName":"执行标准","IsHide":"0"},{"FieldDbName":"C.SaleInfo","FieldEnName":"SaleInfo","FieldChName":"设计卖点","IsHide":"0"},{"FieldDbName":"C.OriBrand","FieldEnName":"OriBrand","FieldChName":"原品牌","IsHide":"0"},{"FieldDbName":"C.Remark","FieldEnName":"Remark","FieldChName":"备注","IsHide":"0"},{"FieldDbName":"C.CreateUser","FieldEnName":"CreateUser","FieldChName":"维护人","IsHide":"0"},{"FieldDbName":"C.CreateTime","FieldEnName":"CreateTime","FieldChName":"大货时间","IsHide":"0"},{"FieldDbName":"C.Att01","FieldEnName":"Att01","FieldChName":"Att01","IsHide":"0"},{"FieldDbName":"C.Att02","FieldEnName":"Att02","FieldChName":"Att02","IsHide":"0"},{"FieldDbName":"C.Att03","FieldEnName":"Att03","FieldChName":"Att03","IsHide":"0"},{"FieldDbName":"C.Att04","FieldEnName":"Att04","FieldChName":"Att04","IsHide":"0"},{"FieldDbName":"C.Att05","FieldEnName":"Att05","FieldChName":"Att05","IsHide":"0"},{"FieldDbName":"C.Att06","FieldEnName":"Att06","FieldChName":"Att06","IsHide":"0"},{"FieldDbName":"C.Att07","FieldEnName":"Att07","FieldChName":"Att07","IsHide":"0"},{"FieldDbName":"C.Att08","FieldEnName":"Att08","FieldChName":"Att08","IsHide":"0"},{"FieldDbName":"C.Att09","FieldEnName":"Att09","FieldChName":"Att09","IsHide":"0"},{"FieldDbName":"C.Att10","FieldEnName":"Att10","FieldChName":"Att10","IsHide":"0"},{"FieldDbName":"C.Att11","FieldEnName":"Att11","FieldChName":"Att11","IsHide":"0"},{"FieldDbName":"C.Att12","FieldEnName":"Att12","FieldChName":"Att12","IsHide":"0"}]', N'SELECT #SelectSql#
FROM dbo.DB_Product AS C WITH (NOLOCK)
    INNER JOIN dbo.FX_BasicData AS E WITH (NOLOCK)
        ON C.PdtSrc = E.Id
    INNER JOIN dbo.FX_BasicData AS F WITH (NOLOCK)
        ON C.BrandId = F.Id
    INNER JOIN dbo.FX_BasicData AS I WITH (NOLOCK)
        ON C.YearId = I.Id
    INNER JOIN dbo.FX_BasicData AS J WITH (NOLOCK)
        ON C.SeasonId = J.Id
    INNER JOIN dbo.FX_BasicData AS K WITH (NOLOCK)
        ON C.BandId = K.Id
    INNER JOIN dbo.FX_BasicData AS G WITH (NOLOCK)
        ON C.CtgId = G.Id
    INNER JOIN dbo.FX_BasicData AS H WITH (NOLOCK)
        ON C.BigCtgId = H.Id
    LEFT OUTER JOIN dbo.DB_Supplier AS D WITH (NOLOCK)
        ON C.SupplierId = D.Id
    LEFT OUTER JOIN dbo.FX_BasicData AS L WITH (NOLOCK)
        ON C.DegreeId = L.Id
    INNER JOIN dbo.FX_BasicData AS M WITH (NOLOCK)
        ON C.DesignGroupId = M.Id', 0, 20000, N'C', NULL, 0, 0, N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime), N'', CAST(N'2021-04-15T21:25:32.550' AS DateTime))
GO
ALTER TABLE [dbo].[CO_ExcelExportSQL] ADD  DEFAULT ((0)) FOR [TemplateType]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQL] ADD  DEFAULT ((0)) FOR [SourceType]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQL] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQL] ADD  DEFAULT ((0)) FOR [ExecMaxCountPer]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQL] ADD  DEFAULT ((0)) FOR [Sort]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQL] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((0)) FOR [ExportDurationWrite]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((0)) FOR [ExportDurationQuery]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((0)) FOR [ExportDurationTask]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((0)) FOR [DownLoadCount]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((0)) FOR [ExportCount]
GO
ALTER TABLE [dbo].[CO_ExcelExportSQLLog] ADD  DEFAULT ((1)) FOR [ExecCount]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'TemplateType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'TemplateCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'TemplateName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板附件Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'TemplateUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'SourceType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'SourceUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行SQL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'ExecSQL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'ModifyUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL', @level2type=N'COLUMN',@level2name=N'RowVersion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Excel导出SQL模板' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出模板SQL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'TemplateSQL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出实际SQL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportSQL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportParameters'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出时间（秒）写入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportDurationWrite'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出时间（秒）查询时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportDurationQuery'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出时间（秒）任务耗时' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportDurationTask'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出结果' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportMsg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'DownLoadUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下载次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'DownLoadCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出文件名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'FileSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'导出数据数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExportCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'执行次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ExecCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'租户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'TenantId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'CreateUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ModifyUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'行版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog', @level2type=N'COLUMN',@level2name=N'RowVersion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Excel导出日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CO_ExcelExportSQLLog'
GO
