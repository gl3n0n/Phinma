USE [ebid_transasia]
GO
/****** Object:  Table [dbo].[tblBACSupplyPosition]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACSupplyPosition](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[SupplyPosition] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACSourcingStrategy]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACSourcingStrategy](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[SourcingStrategy] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACPaymentTerms]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACPaymentTerms](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[PaymentTerm] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACNatureOfSavings]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACNatureOfSavings](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[NatureOfSavings] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACExecutiveSummary]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBACExecutiveSummary](
	[ESCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [text] NOT NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [PK_tblBACExecutiveSummary] PRIMARY KEY CLUSTERED 
(
	[ESCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBACEvaluationSummary]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACEvaluationSummary](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NULL,
	[VendorID] [varchar](50) NULL,
	[VendorName] [varchar](1000) NULL,
	[TechCompliance] [varchar](30) NULL,
	[CommCompliance] [varchar](30) NULL,
	[ContCompliance] [varchar](30) NULL,
	[Criteria_0_name] [varchar](60) NULL,
	[Criteria_0_value] [varchar](30) NULL,
	[Criteria_1_name] [varchar](60) NULL,
	[Criteria_1_value] [varchar](30) NULL,
	[Criteria_2_name] [varchar](60) NULL,
	[Criteria_2_value] [varchar](30) NULL,
	[Criteria_3_name] [varchar](60) NULL,
	[Criteria_3_value] [varchar](30) NULL,
	[Criteria_4_name] [varchar](60) NULL,
	[Criteria_4_value] [varchar](30) NULL,
	[Criteria_5_name] [varchar](60) NULL,
	[Criteria_5_value] [varchar](30) NULL,
	[Criteria_6_name] [varchar](60) NULL,
	[Criteria_6_value] [varchar](30) NULL,
	[Criteria_7_name] [varchar](60) NULL,
	[Criteria_7_value] [varchar](30) NULL,
	[Criteria_8_name] [varchar](60) NULL,
	[Criteria_8_value] [varchar](30) NULL,
	[Criteria_9_name] [varchar](60) NULL,
	[Criteria_9_value] [varchar](30) NULL,
	[DateCreated] [datetime] NOT NULL,
	[BidTenderNo] [int] NULL,
	[fromDB] [int] NULL,
	[Accredited] [int] NULL,
	[PerformanceRating] [varchar](30) NULL,
 CONSTRAINT [PK_tblBACEvaluationSummary] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACEvaluationDetails]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACEvaluationDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BidDetailNo] [int] NULL,
	[BidRefNo] [int] NULL,
	[BidTenderNo] [varchar](25) NULL,
	[SKU] [nvarchar](50) NULL,
	[ItemName] [varchar](1000) NULL,
	[Chkd] [int] NULL,
	[Qty] [float] NULL,
	[UnitCost] [varchar](1000) NULL,
	[TotalCost] [float] NULL,
	[Ranking] [int] NULL,
	[fromDB] [int] NULL,
	[UnitMeasure] [varchar](10) NULL,
	[VendorName] [varchar](1000) NULL,
	[Currenzy] [varchar](7) NULL,
	[UserId] [int] NULL,
	[PR_No] [varchar](120) NULL,
 CONSTRAINT [PK_tblBACEvaluationDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACCriteria]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACCriteria](
	[BidRefNo] [int] NULL,
	[RowNum] [int] NULL,
	[VendorID] [varchar](50) NULL,
	[CriteriaText] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACCRCPO]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACCRCPO](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[CRCPO_Type] [varchar](3) NULL,
	[CRCPO] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBACComments](
	[ESCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [text] NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [PK_tblBACComments] PRIMARY KEY CLUSTERED 
(
	[ESCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBACClarifications]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBACClarifications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[FrUserId] [int] NOT NULL,
	[ToUserId] [int] NOT NULL,
	[Comment] [text] NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [PK_tblBACClarifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBacBidItems]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBacBidItems](
	[BacRefNo] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[ItemDesc] [varchar](1000) NULL,
	[BuyerId] [int] NULL,
	[SAPPRNo] [varchar](30) NULL,
	[SAPPRDate] [datetime] NULL,
	[RcvdDate] [datetime] NULL,
	[BidAmount] [float] NULL,
	[Status] [int] NULL,
	[Budgeted] [int] NULL,
	[CompanyId] [int] NULL,
	[Payment_Terms] [varchar](50) NULL,
	[descPaymentTerms] [text] NULL,
	[SS_NoBidders] [int] NULL,
	[SS_NoBidsRcvd] [int] NULL,
	[SS_TechCompliance] [int] NULL,
	[AccumulativeCost] [float] NULL,
	[SavingsGen_Nature] [float] NULL,
	[SavingsGen_Amount] [float] NULL,
	[SavingsGen_PctSpend] [float] NULL,
	[PreparedBy] [int] NULL,
	[PreparedDt] [datetime] NULL,
	[DateSavedAsDraft] [datetime] NULL,
	[DateSubmitted] [datetime] NULL,
	[Approver_0] [int] NULL,
	[ApprovedDt_0] [datetime] NULL,
	[ClarifyDt_0] [datetime] NULL,
	[Approver_1] [int] NULL,
	[ApprovedDt_1] [datetime] NULL,
	[ClarifyDt_1] [datetime] NULL,
	[Approver_2] [int] NULL,
	[ApprovedDt_2] [datetime] NULL,
	[ClarifyDt_2] [datetime] NULL,
	[Approver_3] [int] NULL,
	[ApprovedDt_3] [datetime] NULL,
	[ClarifyDt_3] [datetime] NULL,
	[Approver_4] [int] NULL,
	[ApprovedDt_4] [datetime] NULL,
	[ClarifyDt_4] [datetime] NULL,
	[Approver_5] [int] NULL,
	[ApprovedDt_5] [datetime] NULL,
	[ClarifyDt_5] [datetime] NULL,
	[Approver_6] [int] NULL,
	[ApprovedDt_6] [datetime] NULL,
	[ClarifyDt_6] [datetime] NULL,
	[Approver_7] [int] NULL,
	[ApprovedDt_7] [datetime] NULL,
	[ClarifyDt_7] [datetime] NULL,
	[Approver_8] [int] NULL,
	[ApprovedDt_8] [datetime] NULL,
	[ClarifyDt_8] [datetime] NULL,
	[Approver_9] [int] NULL,
	[ApprovedDt_9] [datetime] NULL,
	[ClarifyDt_9] [datetime] NULL,
	[Approver_10] [int] NULL,
	[ApprovedDt_10] [datetime] NULL,
	[ClarifyDt_10] [datetime] NULL,
	[AccumulativeCost1] [float] NULL,
	[ss_crc_no] [varchar](120) NULL,
 CONSTRAINT [PK_tblBacBidItems_1] PRIMARY KEY CLUSTERED 
(
	[BacRefNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACBasisForAwarding]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACBasisForAwarding](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[BasisForAwarding] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBidAwardingCommittee]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidAwardingCommittee](
	[BACId] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[EmailAdd] [varchar](150) NOT NULL,
	[ApprovingLimitOnLowestPrice] [int] NULL,
	[ApprovingLimitOnNonLowestPrice] [int] NULL,
	[OIC] [int] NULL,
	[ApprovingLimitOnLowestPriceTo] [float] NULL,
	[ApprovingLimitOnNonLowestPriceTo] [float] NULL,
	[Approver] [int] NULL,
	[Committee] [int] NULL,
	[emailadd_tmp] [varchar](120) NULL,
 CONSTRAINT [PK_tblBidAwardingCommittee] PRIMARY KEY CLUSTERED 
(
	[BACId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACTypeOfPurchase]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACTypeOfPurchase](
	[BidRefNo] [int] NULL,
	[UserId] [int] NULL,
	[TypeOfPurchase] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBidTendersForPOFileImport]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidTendersForPOFileImport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NULL,
	[0a] [varchar](150) NULL,
	[1a] [varchar](150) NULL,
	[2a] [varchar](150) NULL,
	[3a] [varchar](150) NULL,
	[4a] [varchar](150) NULL,
	[5a] [varchar](150) NULL,
	[6a] [varchar](150) NULL,
	[7a] [varchar](150) NULL,
	[8o] [varchar](150) NULL,
	[8a] [varchar](150) NULL,
	[9a] [varchar](150) NULL,
	[10a] [varchar](150) NULL,
	[8b] [varchar](150) NULL,
	[11a] [varchar](150) NULL,
	[12a] [varchar](150) NULL,
	[13a] [varchar](150) NULL,
	[14a] [varchar](150) NULL,
	[15a] [varchar](150) NULL,
	[16a] [varchar](150) NULL,
	[10b] [varchar](150) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBidTendersAddedCosts.bak]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBidTendersAddedCosts.bak](
	[AddedCostId] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[InLandFreight] [float] NULL,
	[SeaAirFreight] [float] NULL,
	[FowardingBrokerage] [float] NULL,
	[DutiesTaxes] [float] NULL,
	[Insurance] [float] NULL,
	[DeliveryCostToSite] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBidTendersAddedCosts]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBidTendersAddedCosts](
	[AddedCostId] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[InLandFreight] [float] NULL,
	[SeaAirFreight] [float] NULL,
	[FowardingBrokerage] [float] NULL,
	[DutiesTaxes] [float] NULL,
	[Insurance] [float] NULL,
	[DeliveryCostToSite] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProducts_bak]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblProducts_bak](
	[SKU] [nvarchar](10) NOT NULL,
	[ItemName] [varchar](200) NULL,
	[ProductDescription] [varchar](1000) NULL,
	[UnitOfMeasure] [nvarchar](7) NULL,
	[SubCategoryId] [int] NULL,
	[Service] [int] NULL,
	[Brand] [int] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[MainCategoryId] [nvarchar](7) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPR]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPR](
	[PrRefNo] [int] IDENTITY(1,1) NOT NULL,
	[PRNo] [int] NOT NULL,
	[PRLineNo] [int] NULL,
	[ItemCode] [varchar](18) NULL,
	[PRDescription] [varchar](400) NULL,
	[PRDate] [datetime] NULL,
	[Company] [varchar](50) NULL,
	[SubCategory] [varchar](9) NULL,
	[DeliveryDate] [varchar](50) NULL,
	[UOM] [varchar](9) NULL,
	[Qty] [float] NULL,
	[UnitPrice] [float] NULL,
	[Currency] [varchar](15) NULL,
	[GroupName] [varchar](200) NULL,
	[Commodity] [varchar](300) NULL,
	[BuyerCode] [varchar](50) NULL,
	[Remarks] [text] NULL,
	[CompanyCode] [varchar](20) NULL,
	[Budget] [float] NULL,
	[Workflow] [char](10) NULL,
	[Location] [char](10) NULL,
 CONSTRAINT [PK_tblPR] PRIMARY KEY CLUSTERED 
(
	[PrRefNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblContent]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblContent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NOT NULL,
	[Content] [varchar](8000) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[ModifiedBy] [int] NOT NULL,
	[ImageIcon] [nvarchar](150) NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_tblContent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Inactive; 1:Active;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblContent', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Image Filename' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblContent', @level2type=N'COLUMN',@level2name=N'ImageIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Welcome; 1: News; 2:Announcement;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblContent', @level2type=N'COLUMN',@level2name=N'Type'
GO
/****** Object:  Table [dbo].[tblvendors_bkp]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblvendors_bkp](
	[VendorId] [int] NOT NULL,
	[VendorName] [varchar](100) NOT NULL,
	[VendorCode] [nvarchar](10) NULL,
	[Accredited] [int] NULL,
	[VendorEmail] [varchar](100) NULL,
	[MobileNo] [nvarchar](50) NULL,
	[VendorAddress] [varchar](200) NULL,
	[VendorAddress1] [varchar](200) NULL,
	[VendorAddress2] [varchar](200) NULL,
	[VendorAddress3] [varchar](200) NULL,
	[Classification] [int] NULL,
	[AccredDate] [datetime] NULL,
	[ContactPerson] [varchar](50) NULL,
	[SalesPersonTelNo] [varchar](20) NULL,
	[VendorCategory] [nvarchar](7) NULL,
	[VendorSubCategory] [nvarchar](20) NULL,
	[Syskey] [varchar](5) NULL,
	[TelephoneNo] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Extension] [varchar](20) NULL,
	[BranchTelephoneNo] [varchar](20) NULL,
	[BranchFax] [varchar](20) NULL,
	[BranchExtension] [varchar](20) NULL,
	[VatRegNo] [varchar](50) NULL,
	[TIN] [varchar](50) NULL,
	[POBox] [varchar](50) NULL,
	[TermsofPayment] [varchar](100) NULL,
	[SpecialTerms] [varchar](100) NULL,
	[MinOrderValue] [float] NULL,
	[PostalCode] [varchar](20) NULL,
	[OwnershipFilipino] [int] NULL,
	[OwnershipOther] [int] NULL,
	[OrgTypeID] [int] NULL,
	[Specialization] [varchar](50) NULL,
	[SoleSupplier1] [varchar](200) NULL,
	[SoleSupplier2] [varchar](200) NULL,
	[KeyPersonnel] [varchar](100) NULL,
	[KpPosition] [varchar](100) NULL,
	[ISOStandard] [varchar](4) NULL,
	[PCABClass] [int] NULL,
	[IsBlackListed] [smallint] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcPCABClass]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcPCABClass](
	[PCAB Class Id] [int] NOT NULL,
	[PCAB Class Name] [varchar](50) NULL,
 CONSTRAINT [PK_rfcPCABClass] PRIMARY KEY CLUSTERED 
(
	[PCAB Class Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcPaymentTerms]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcPaymentTerms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTerm] [varchar](50) NULL,
	[TERMSCODE] [char](6) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcOrganizationType]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcOrganizationType](
	[OrgTypeID] [int] IDENTITY(1,1) NOT NULL,
	[OrgTypeName] [varchar](100) NULL,
 CONSTRAINT [PK_rfcOrganizationType] PRIMARY KEY CLUSTERED 
(
	[OrgTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcNatureOfSavings]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcNatureOfSavings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NatureOfSavings] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcLocations]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcLocations](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [varchar](50) NULL,
 CONSTRAINT [PK_rfcLocations] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcItemsCarried]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcItemsCarried](
	[ItemNo] [int] IDENTITY(1,1) NOT NULL,
	[ItemsCarried] [varchar](50) NULL,
 CONSTRAINT [PK_rfcItemsCarried] PRIMARY KEY CLUSTERED 
(
	[ItemNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcISOStandard]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcISOStandard](
	[ISOStandardId] [int] IDENTITY(1,1) NOT NULL,
	[ISOStandardName] [varchar](30) NULL,
 CONSTRAINT [PK_rfcISOStandard] PRIMARY KEY CLUSTERED 
(
	[ISOStandardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcIncoterm]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcIncoterm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Incoterm] [varchar](50) NULL,
	[IncotermCode] [varchar](50) NULL,
 CONSTRAINT [PK_rfcIncoterm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcGroupDeptSec]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rfcGroupDeptSec](
	[GroupDeptSecId] [int] IDENTITY(1,1) NOT NULL,
	[GroupDeptSecName] [nvarchar](50) NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_rfcGroupDept] PRIMARY KEY CLUSTERED 
(
	[GroupDeptSecId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rfcGlobePlans]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcGlobePlans](
	[PlanID] [int] IDENTITY(1,1) NOT NULL,
	[PlanName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_rfcGlobePlans] PRIMARY KEY CLUSTERED 
(
	[PlanID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcForAuction]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcForAuction](
	[ForAuctionId] [int] IDENTITY(0,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_rfcForAuction] PRIMARY KEY CLUSTERED 
(
	[ForAuctionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'flag for determining bid item for request to convert to auction' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'rfcForAuction', @level2type=N'COLUMN',@level2name=N'Description'
GO
/****** Object:  Table [dbo].[rfcDepartments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rfcDepartments](
	[DeptID] [int] IDENTITY(1,1) NOT NULL,
	[DeptName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_rfcDepartments] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rfcCurrency]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcCurrency](
	[ID] [char](3) NOT NULL,
	[Currency] [varchar](100) NOT NULL,
	[RateToUSD] [money] NOT NULL,
	[RateToPHP] [money] NOT NULL,
	[AsOf] [datetime] NOT NULL,
	[Deletable] [bit] NOT NULL,
 CONSTRAINT [PK_rfcCurrency] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcCompany]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcCompany](
	[CompanyId] [int] NOT NULL,
	[Company] [varchar](50) NOT NULL,
	[CompanyCode] [varchar](20) NULL,
 CONSTRAINT [PK_rfcCompany] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcCommittee]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcCommittee](
	[CommitteeId] [int] IDENTITY(1,1) NOT NULL,
	[Committee] [varchar](100) NOT NULL,
 CONSTRAINT [PK_rfcCommittee] PRIMARY KEY CLUSTERED 
(
	[CommitteeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcClassification]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcClassification](
	[ClassificationId] [int] IDENTITY(1,1) NOT NULL,
	[ClassificationName] [varchar](200) NOT NULL,
 CONSTRAINT [PK_rfcClassification] PRIMARY KEY CLUSTERED 
(
	[ClassificationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcBasisForAwarding]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcBasisForAwarding](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BasisForAwarding] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcAuctionType]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcAuctionType](
	[AuctionTypeId] [int] IDENTITY(0,1) NOT NULL,
	[AuctionType] [varchar](50) NOT NULL,
	[TEst] [char](10) NULL,
 CONSTRAINT [PK_rfcAuctionType] PRIMARY KEY CLUSTERED 
(
	[AuctionTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[POPORH1]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[POPORH1](
	[PORHSEQ] [decimal](19, 0) NULL,
	[PONUMBER] [char](22) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductMainCategory]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductMainCategory](
	[MainCategoryId] [nvarchar](7) NOT NULL,
	[MainCategoryName] [varchar](50) NOT NULL,
	[MainCategoryDesc] [varchar](100) NULL,
 CONSTRAINT [PK_rfcProductMainCategory] PRIMARY KEY CLUSTERED 
(
	[MainCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductItems]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductItems](
	[ProdItemNo] [int] NOT NULL,
	[ItemName] [varchar](50) NULL,
 CONSTRAINT [PK_rfcProductItems] PRIMARY KEY CLUSTERED 
(
	[ProdItemNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductCategory_bak]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductCategory_bak](
	[CategoryId] [nvarchar](7) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[CategoryDesc] [varchar](100) NULL,
	[MainCategoryId] [nvarchar](7) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductCategory]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductCategory](
	[CategoryId] [nvarchar](7) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[CategoryDesc] [varchar](100) NULL,
	[MainCategoryId] [nvarchar](7) NULL,
 CONSTRAINT [PK_rfcProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuditTrail]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuditTrail](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[ActivityDateTime] [datetime] NULL,
 CONSTRAINT [PK_tblAuditTrail] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionItemComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuctionItemComments](
	[ItemCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[AuctionRefNo] [int] NOT NULL,
	[BuyerId] [varchar](50) NULL,
	[PurchasingId] [int] NULL,
	[Comment] [varchar](255) NULL,
	[DatePosted] [datetime] NOT NULL,
	[AllowVendorView] [bit] NOT NULL,
 CONSTRAINT [PK_tblAuctionItemComments] PRIMARY KEY CLUSTERED 
(
	[ItemCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAdministrator]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAdministrator](
	[AdministratorId] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[EmailAdd] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sp_log]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sp_log](
	[ref_type] [varchar](60) NULL,
	[ref_code] [varchar](30) NULL,
	[ref_info] [text] NULL,
	[ref_date] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcUserType]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcUserType](
	[UserTypeId] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](50) NULL,
 CONSTRAINT [PK_rfcUserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcTypeOfPurchase]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcTypeOfPurchase](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfPurchase] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcSupportingDocu]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcSupportingDocu](
	[ID] [char](3) NOT NULL,
	[DocuName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_rfcSupportingDocu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcSupplyPosition]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcSupplyPosition](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SupplyPosition] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcSupplierType_bkp]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcSupplierType_bkp](
	[SupplierTypeId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierTypeDesc] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcSupplierType]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcSupplierType](
	[SupplierTypeId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierTypeDesc] [varchar](50) NULL,
 CONSTRAINT [PK_rfcSupplierType] PRIMARY KEY CLUSTERED 
(
	[SupplierTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcSourcingStrategy]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcSourcingStrategy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SourcingStrategy] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcServices]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcServices](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](50) NULL,
 CONSTRAINT [PK_rfcServices] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcReferenceType]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcReferenceType](
	[ReferenceTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceTypeName] [varchar](100) NULL,
	[ReferenceExtra] [varchar](100) NULL,
 CONSTRAINT [PK_rfcReferenceType] PRIMARY KEY CLUSTERED 
(
	[ReferenceTypeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductUnitOfMeasure]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rfcProductUnitOfMeasure](
	[UnitOfMeasureID] [nvarchar](7) NOT NULL,
	[UnitOfMeasureName] [nvarchar](50) NULL,
 CONSTRAINT [PK_rfcProductUnitOfMeasure] PRIMARY KEY CLUSTERED 
(
	[UnitOfMeasureID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorCategoriesAndSubcategories_staging]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorCategoriesAndSubcategories_staging](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[IncludesAllSubCategories] [int] NULL,
	[SubCategoryId] [int] NULL,
	[Brandid] [int] NULL,
	[MainCategoryId] [nvarchar](7) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorCurrency]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorCurrency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorCode] [varchar](60) NOT NULL,
	[Currency] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorShortlistingForm]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorShortlistingForm](
	[VSFId] [int] IDENTITY(1,1) NOT NULL,
	[VSFDate] [datetime] NOT NULL,
	[PRNo] [varchar](100) NULL,
	[ProjectName] [varchar](100) NULL,
	[ProponentName] [varchar](100) NULL,
	[GroupDept] [varchar](100) NULL,
	[PRAmount] [float] NOT NULL,
	[PRDescription] [varchar](512) NOT NULL,
	[NumPotentialVendor] [int] NULL,
	[NumShortlistedVendor] [int] NULL,
	[Status] [int] NULL,
	[PreparedDt] [datetime] NULL,
	[DateSavedAsDraft] [datetime] NULL,
	[DateSubmitted] [datetime] NULL,
	[BuyerId] [int] NULL,
	[PurchasingId] [int] NULL,
	[Recomendatation] [text] NULL,
	[ApprovedMemo] [smallint] NULL,
	[BoardApproval] [smallint] NULL,
	[EndorsementMemo] [smallint] NULL,
	[ExcomApproval] [smallint] NULL,
	[Others] [smallint] NULL,
	[OthersDesc] [varchar](100) NULL,
	[Recommendation] [text] NULL,
	[ApprovedDt] [datetime] NULL,
	[RejectedDt] [datetime] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[SubCategoryId] [int] NULL,
	[BrandId] [int] NULL,
 CONSTRAINT [PK_tblVendorShortlistingForm] PRIMARY KEY CLUSTERED 
(
	[VSFId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbltest]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbltest](
	[BidDetailNo] [int] NULL,
	[DetailDesc] [nvarchar](250) NULL,
	[Qty] [int] NULL,
	[1231231] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVSFDetails]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVSFDetails](
	[VSFDetailId] [int] IDENTITY(1,1) NOT NULL,
	[VSFId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Accreditation] [int] NULL,
	[SAPRatingScore] [float] NULL,
	[SAPRatingRank] [int] NULL,
	[MaxExposureLimit] [float] NOT NULL,
	[AmountUnservedPO] [float] NOT NULL,
	[AvailBalance] [float] NULL,
	[FCRank] [int] NULL,
	[EndoresedBy] [varchar](100) NULL,
	[ProductTypeApproval] [varchar](100) NULL,
	[OverallRanking] [float] NULL,
	[DateCreated] [datetime] NULL,
	[Selected] [int] NULL,
 CONSTRAINT [PK_tblVSFDetails] PRIMARY KEY CLUSTERED 
(
	[VSFDetailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVSFComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVSFComments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[VSFId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Comment] [text] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_tblVSFComments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[vwBACRejected]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwBACRejected] as
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_0 ApprovedBy, ApprovedDt_0 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_0 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_1 ApprovedBy, ApprovedDt_1 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_1 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_2 ApprovedBy, ApprovedDt_2 ApprovedDt
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_2 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_3 ApprovedBy, ApprovedDt_3 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_3 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_4 ApprovedBy, ApprovedDt_4 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_4 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_5 ApprovedBy, ApprovedDt_5 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_5 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_6 ApprovedBy, ApprovedDt_6 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_6 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_7 ApprovedBy, ApprovedDt_7 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_7 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_8 ApprovedBy, ApprovedDt_8 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_8 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_9 ApprovedBy, ApprovedDt_9 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_9 is not null
AND	  Status = 4
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_10 ApprovedBy, ApprovedDt_10 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_10 is not null
AND	  Status = 4
GO
/****** Object:  View [dbo].[vwBACForApproval]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwBACForApproval] as
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_0 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_0 is not null
AND   ApprovedDt_0 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_1 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_1 is not null
AND   ApprovedDt_0 is not null
AND   ApprovedDt_1 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_2 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_2 is not null
AND   ApprovedDt_1 is not null
AND   ApprovedDt_2 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_3 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_3 is not null
AND   ApprovedDt_2 is not null
AND   ApprovedDt_3 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_4 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_4 is not null
AND   ApprovedDt_3 is not null
AND   ApprovedDt_4 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_5 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_5 is not null
AND   ApprovedDt_4 is not null
AND   ApprovedDt_5 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_6 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_6 is not null
AND   ApprovedDt_5 is not null
AND   ApprovedDt_6 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_7 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_7 is not null
AND   ApprovedDt_6 is not null
AND   ApprovedDt_7 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_8 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_8 is not null
AND   ApprovedDt_7 is not null
AND   ApprovedDt_8 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_9 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_9 is not null
AND   ApprovedDt_8 is not null
AND   ApprovedDt_9 is null
AND	  Status = 1
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_10 ApprovedBy 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_10 is not null
AND   ApprovedDt_9 is not null
AND   ApprovedDt_10 is null
AND	  Status = 1
GO
/****** Object:  View [dbo].[vwBacBidApprovers]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vwBacBidApprovers] as
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_1
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_2
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_3
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_4
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_5
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_6
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_7
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_8
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_9
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_10
GO
/****** Object:  View [dbo].[vwBacBidApprover]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  view [dbo].[vwBacBidApprover] as
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_1
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_2
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_3
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_4
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_5
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_6
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_7
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_8
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_9
    UNION
	SELECT t2.BidRefNo, t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit 
	FROM tblBidAwardingCommittee t1, tblBacBidItems t2  
	WHERE t1.BACId = t2.Approver_10
GO
/****** Object:  View [dbo].[vwBACApproved]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwBACApproved] as
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_0 ApprovedBy, ApprovedDt_0 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_0 is not null
AND   ApprovedDt_0 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_1 ApprovedBy, ApprovedDt_1 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_1 is not null
AND   ApprovedDt_1 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_2 ApprovedBy, ApprovedDt_2 ApprovedDt
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_2 is not null
AND   ApprovedDt_2 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_3 ApprovedBy, ApprovedDt_3 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_3 is not null
AND   ApprovedDt_3 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_4 ApprovedBy, ApprovedDt_4 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_4 is not null
AND   ApprovedDt_4 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_5 ApprovedBy, ApprovedDt_5 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_5 is not null
AND   ApprovedDt_5 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_6 ApprovedBy, ApprovedDt_6 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_6 is not null
AND   ApprovedDt_6 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_7 ApprovedBy, ApprovedDt_7 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_7 is not null
AND   ApprovedDt_7 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_8 ApprovedBy, ApprovedDt_8 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_8 is not null
AND   ApprovedDt_8 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_9 ApprovedBy, ApprovedDt_9 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_9 is not null
AND   ApprovedDt_2 is not null
AND   ApprovedDt_9 is not null
AND	  Status = 3
UNION
SELECT t1.BacRefNo, t1.BidRefNo, t1.ItemDesc, t1.PreparedDt, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo)+';'+CONVERT(VARCHAR, t1.BuyerId) AS BACBID,
       Approver_10 ApprovedBy, ApprovedDt_10 ApprovedDt 
FROM tblBacBidItems t1
WHERE PreparedDt is not null
AND   Approver_10 is not null
AND   ApprovedDt_2 is not null
AND   ApprovedDt_10 is not null
AND	  Status = 3
GO
/****** Object:  View [dbo].[vwApprovers]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vwApprovers] as 
select bacid, substring(FirstName,1,1) + substring(MiddleName,1,1) + substring(replace(replace(LastName,'dela ',''),'de ',''),1,1) AS Initials,
       LastName + ', ' + FirstName + ' ' + MiddleName AS Name,
       ApprovingLimitOnLowestPrice LimitLowest, ApprovingLimitOnLowestPriceTo LimitLowestTo, 
       case when (ApprovingLimitOnLowestPrice/cast(1000000 as float)) > 1 then
               cast(cast( (ApprovingLimitOnLowestPrice/cast(1000000 as float)) as int) as varchar) + 'M'
            when (ApprovingLimitOnLowestPrice/cast(1000 as float)) > 1 then
               cast(cast( (ApprovingLimitOnLowestPrice/cast(1000 as float)) as int) as varchar) + 'K'
            when (cast(ApprovingLimitOnLowestPrice as int)=0 and cast(ApprovingLimitOnLowestPriceTo as int)=0) then
               ''
            when (cast(ApprovingLimitOnLowestPrice as int)=0 and cast(ApprovingLimitOnLowestPriceTo as int)>0) then
               ' 0'
            else
               cast(cast( (ApprovingLimitOnLowestPrice/cast(1000 as float)) as int) as varchar)
       end + 
       case when (ApprovingLimitOnLowestPriceTo/cast(1000000000 as float)) >= 1 then
               ' & up'
            when (ApprovingLimitOnLowestPriceTo/cast(1000000 as float)) >= 1 then
               ' - ' + cast(cast( (ApprovingLimitOnLowestPriceTo/cast(1000000 as float)) as int) as varchar) + 'M'
            when (ApprovingLimitOnLowestPriceTo/cast(1000 as float)) >= 1 then
               ' - ' + cast(cast( (ApprovingLimitOnLowestPriceTo/cast(1000 as float)) as int) as varchar) + 'K'
            when (cast(ApprovingLimitOnLowestPrice as int)=0 and cast(ApprovingLimitOnLowestPriceTo as int)=0) then
               ''
            else
               ' - ' + cast(cast( (ApprovingLimitOnLowestPriceTo/cast(1000 as float)) as int) as varchar)
       end as LimitLowestChar,
       ApprovingLimitOnNonLowestPrice LimitNonLowest, ApprovingLimitOnNonLowestPriceTo LimitNonLowestTo, 
       case when (ApprovingLimitOnNonLowestPrice/cast(1000000 as float)) >= 1 then
               cast(cast( (ApprovingLimitOnNonLowestPrice/cast(1000000 as float)) as int) as varchar) + 'M'
            when (ApprovingLimitOnNonLowestPrice/cast(1000 as float)) >= 1 then
               cast(cast( (ApprovingLimitOnNonLowestPrice/cast(1000 as float)) as int) as varchar) + 'K'
            when (cast(ApprovingLimitOnNonLowestPrice as int)=0 and cast(ApprovingLimitOnNonLowestPriceTo as int)=0) then
               ''
            when (cast(ApprovingLimitOnNonLowestPrice as int)=0 and cast(ApprovingLimitOnNonLowestPriceTo as int)>0) then
               ' 0'
            else
               cast(cast( (ApprovingLimitOnNonLowestPrice/cast(1000 as float)) as int) as varchar)
       end + 
       case when (ApprovingLimitOnNonLowestPriceTo/cast(1000000000 as float)) >= 1 then
               ' & up'
            when (ApprovingLimitOnNonLowestPriceTo/cast(1000000 as float)) >= 1 then
               ' - ' + cast(cast( (ApprovingLimitOnNonLowestPriceTo/cast(1000000 as float)) as int) as varchar) + 'M'
            when (ApprovingLimitOnNonLowestPriceTo/cast(1000 as float)) >= 1 then
               ' - ' + cast(cast( (ApprovingLimitOnNonLowestPriceTo/cast(1000 as float)) as int) as varchar) + 'K'
            when (cast(ApprovingLimitOnNonLowestPrice as int)=0 and cast(ApprovingLimitOnNonLowestPriceTo as int)=0) then
               ''
            else
               ' - ' + cast(cast( (ApprovingLimitOnNonLowestPriceTo/cast(1000 as float)) as int) as varchar)
       end as LimitNonLowestChar,
       ISNULL(Committee,0) as Committee,
       OIC, 
       Approver
from tblBidAwardingCommittee t1 INNER JOIN tblUsers t3 ON t1.BACId = t3.UserId 
WHERE t3.Status <> 0
GO
/****** Object:  View [dbo].[vmBacBidApprovers]    Script Date: 04/30/2015 15:38:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE view [dbo].[vmBacBidApprovers] as 
select BidRefNo, Approver_1 AS Approver, ApprovedDt_1 ApprovedDt from tblBacBidItems where Approver_1  is not null UNION
select BidRefNo, Approver_2 AS Approver, ApprovedDt_2 ApprovedDt from tblBacBidItems where Approver_2  is not null UNION
select BidRefNo, Approver_3 AS Approver, ApprovedDt_3 ApprovedDt from tblBacBidItems where Approver_3  is not null UNION
select BidRefNo, Approver_4 AS Approver, ApprovedDt_4 ApprovedDt from tblBacBidItems where Approver_4  is not null UNION
select BidRefNo, Approver_5 AS Approver, ApprovedDt_5 ApprovedDt from tblBacBidItems where Approver_5  is not null UNION
select BidRefNo, Approver_6 AS Approver, ApprovedDt_6 ApprovedDt from tblBacBidItems where Approver_6  is not null UNION
select BidRefNo, Approver_7 AS Approver, ApprovedDt_7 ApprovedDt from tblBacBidItems where Approver_7  is not null UNION
select BidRefNo, Approver_8 AS Approver, ApprovedDt_8 ApprovedDt from tblBacBidItems where Approver_8  is not null UNION
select BidRefNo, Approver_9 AS Approver, ApprovedDt_9 ApprovedDt from tblBacBidItems where Approver_9  is not null UNION
select BidRefNo, Approver_10 AS Approver, ApprovedDt_10 ApprovedDt from tblBacBidItems where Approver_10 is not null
GO
/****** Object:  Table [dbo].[tblPurchasing]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPurchasing](
	[PurchasingId] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[EmailAdd] [varchar](100) NOT NULL,
	[DeptID] [int] NOT NULL,
 CONSTRAINT [PK_tblPurchasing] PRIMARY KEY CLUSTERED 
(
	[PurchasingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductSubCategory]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductSubCategory](
	[SubCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [nvarchar](7) NULL,
	[SubCategoryName] [varchar](50) NULL,
	[SubCategoryDesc] [varchar](100) NULL,
	[CategoryIdx] [nvarchar](7) NULL,
 CONSTRAINT [PK_rfcProductSubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionItems]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuctionItems](
	[AuctionRefNo] [int] IDENTITY(1,1) NOT NULL,
	[PRRefNo] [bigint] NULL,
	[Requestor] [nvarchar](50) NULL,
	[ItemDesc] [nvarchar](1000) NULL,
	[AuctionType] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Category] [nvarchar](7) NULL,
	[SubCategory] [int] NULL,
	[EstItemValue] [float] NULL,
	[BuyerId] [int] NULL,
	[ApprovedBy] [int] NULL,
	[GroupDeptSec] [int] NULL,
	[CompanyId] [int] NULL,
	[BidRefNo] [int] NULL,
	[BidCurrency] [char](3) NULL,
	[AuctionDeadline] [datetime] NULL,
	[AuctionStartDateTime] [datetime] NULL,
	[AuctionEndDateTime] [datetime] NULL,
	[PreviousEndTimes] [nvarchar](4000) NULL,
	[PRDate] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateSavedAsDraft] [datetime] NULL,
	[DateSubmitted] [datetime] NULL,
	[DateApproved] [datetime] NULL,
	[DateCancelled] [datetime] NULL,
	[DateRejected] [datetime] NULL,
	[DeliveryDate] [datetime] NULL,
	[DateSentForReedit] [datetime] NULL,
 CONSTRAINT [PK_tblAuctionItems] PRIMARY KEY CLUSTERED 
(
	[AuctionRefNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Forward; 1:Reverse;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionItems', @level2type=N'COLUMN',@level2name=N'AuctionType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionItems', @level2type=N'COLUMN',@level2name=N'EstItemValue'
GO
/****** Object:  Table [dbo].[tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendors](
	[VendorId] [int] NOT NULL,
	[VendorName] [varchar](200) NOT NULL,
	[VendorCode] [nvarchar](10) NULL,
	[Accredited] [int] NULL,
	[VendorEmail] [varchar](300) NULL,
	[MobileNo] [nvarchar](50) NULL,
	[VendorAddress] [varchar](400) NULL,
	[VendorAddress1] [varchar](400) NULL,
	[VendorAddress2] [varchar](400) NULL,
	[VendorAddress3] [varchar](400) NULL,
	[Classification] [int] NULL,
	[AccredDate] [datetime] NULL,
	[ContactPerson] [varchar](50) NULL,
	[SalesPersonTelNo] [varchar](50) NULL,
	[VendorCategory] [nvarchar](7) NULL,
	[VendorSubCategory] [nvarchar](20) NULL,
	[Syskey] [varchar](5) NULL,
	[TelephoneNo] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Extension] [varchar](20) NULL,
	[BranchTelephoneNo] [varchar](20) NULL,
	[BranchFax] [varchar](20) NULL,
	[BranchExtension] [varchar](20) NULL,
	[VatRegNo] [varchar](50) NULL,
	[TIN] [varchar](50) NULL,
	[POBox] [varchar](50) NULL,
	[TermsofPayment] [varchar](100) NULL,
	[SpecialTerms] [varchar](100) NULL,
	[MinOrderValue] [float] NULL,
	[PostalCode] [varchar](20) NULL,
	[OwnershipFilipino] [int] NULL,
	[OwnershipOther] [int] NULL,
	[OrgTypeID] [int] NULL,
	[Specialization] [varchar](50) NULL,
	[SoleSupplier1] [varchar](200) NULL,
	[SoleSupplier2] [varchar](200) NULL,
	[KeyPersonnel] [varchar](100) NULL,
	[KpPosition] [varchar](100) NULL,
	[ISOStandard] [varchar](4) NULL,
	[PCABClass] [int] NULL,
	[IsBlackListed] [smallint] NOT NULL,
	[Vendor_Code] [varchar](60) NULL,
	[SLA_SIR_Date] [datetime] NULL,
	[SLA_Date_Approved] [datetime] NULL,
	[Accreditation_Duration] [datetime] NULL,
	[Accreditation_From] [datetime] NULL,
	[Accreditation_To] [datetime] NULL,
	[Perf_Evaluation_Date] [datetime] NULL,
	[Perf_Evaluation_Rate] [int] NULL,
	[Composite_Rating_SIR_Date] [datetime] NULL,
	[Composite_Rating_Rate] [int] NULL,
	[Maximum_Exposure_SIR_Date] [datetime] NULL,
	[Maximum_Exposure_Amount] [float] NULL,
	[Enrollment_Date] [datetime] NULL,
	[IR_Date] [datetime] NULL,
	[IR_Number] [int] NULL,
	[IR_Description] [varchar](60) NULL,
	[VendorMainCategory] [nvarchar](7) NULL,
	[VendorCodeBak] [nvarchar](10) NULL,
 CONSTRAINT [PK_tblVendors] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBuyers]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBuyers](
	[BuyerId] [int] NOT NULL,
	[BuyerFirstName] [varchar](100) NULL,
	[BuyerLastName] [varchar](100) NULL,
	[BuyerMidName] [varchar](100) NULL,
	[CompanyId] [int] NULL,
	[BuyerCode] [varchar](20) NULL,
	[EmailAdd] [varchar](100) NULL,
 CONSTRAINT [PK_tblBuyers] PRIMARY KEY CLUSTERED 
(
	[BuyerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBidOpeningCommittee]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidOpeningCommittee](
	[BOCId] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[EmailAdd] [varchar](150) NOT NULL,
	[CommitteeId] [int] NOT NULL,
 CONSTRAINT [PK_tblBidOpeningCommittee] PRIMARY KEY CLUSTERED 
(
	[BOCId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBACSupportingDocuments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBACSupportingDocuments](
	[FileUploadID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[BuyerID] [int] NULL,
	[DocuName] [varchar](100) NULL,
	[DateUploaded] [datetime] NOT NULL,
	[OriginalFileName] [varchar](100) NOT NULL,
	[ActualFileName] [varchar](100) NOT NULL,
	[IsDetachable] [bit] NOT NULL,
	[ContentType] [varchar](100) NULL,
	[Filesize] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionItemTrail]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAuctionItemTrail](
	[AuctionTrailId] [int] IDENTITY(1,1) NOT NULL,
	[AuctionDetailNo] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Bid] [money] NOT NULL,
	[DateSubmitted] [datetime] NOT NULL,
	[AuctionTrailStatus] [int] NOT NULL,
 CONSTRAINT [PK_tblAuctionItemTrail] PRIMARY KEY CLUSTERED 
(
	[AuctionTrailId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Connected to tblAuctionItemDetail.AuditDetailNo - richard' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionItemTrail', @level2type=N'COLUMN',@level2name=N'AuctionDetailNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Connected to tblUsersl.UserId - richard' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionItemTrail', @level2type=N'COLUMN',@level2name=N'VendorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionItemTrail', @level2type=N'COLUMN',@level2name=N'Bid'
GO
/****** Object:  Table [dbo].[tblBidItems]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidItems](
	[BidRefNo] [int] IDENTITY(1,1) NOT NULL,
	[PRRefNo] [bigint] NOT NULL,
	[Requestor] [varchar](50) NULL,
	[ItemDesc] [varchar](1000) NULL,
	[EstItemValue] [float] NULL,
	[BuyerId] [int] NULL,
	[ApprovedBy] [int] NULL,
	[GroupDeptSec] [int] NULL,
	[Category] [nvarchar](7) NULL,
	[SubCategory] [int] NULL,
	[Status] [int] NOT NULL,
	[ForAuction] [int] NOT NULL,
	[CompanyId] [int] NULL,
	[Currency] [char](3) NOT NULL,
	[DeliverTo] [varchar](50) NULL,
	[Incoterm] [int] NULL,
	[Deadline] [datetime] NULL,
	[RenegotiationDeadline] [datetime] NULL,
	[PRDate] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL,
	[DeliveryDate] [datetime] NULL,
	[DateSubmitted] [datetime] NULL,
	[DateApproved] [datetime] NULL,
	[DateRejected] [datetime] NULL,
	[DateEndorsed] [datetime] NULL,
	[DateSavedAsDraft] [datetime] NULL,
	[DateOfRequestToConvertToAuction] [datetime] NULL,
	[DateOfApprovedForAucton] [datetime] NULL,
	[DateOfDeniedForConversion] [datetime] NULL,
	[DateApprovedForReedit] [datetime] NULL,
	[IsApprovedByPurchasing] [smallint] NOT NULL,
	[IsApprovedByFinance] [smallint] NOT NULL,
	[IsApprovedByAudit] [smallint] NOT NULL,
	[IsOpenedByPurchasing] [smallint] NOT NULL,
	[IsOpenedByFinance] [smallint] NOT NULL,
	[IsOpenedByAudit] [smallint] NOT NULL,
	[QualifiedSourcing] [bit] NOT NULL,
	[boc_sent_email] [char](1) NOT NULL,
	[PRSubRefNo] [bigint] NULL,
	[VSFId] [int] NULL,
	[MainCategory] [nvarchar](7) NULL,
	[EndorsedToAccpac] [tinyint] NULL,
 CONSTRAINT [PK_tblBidItems] PRIMARY KEY CLUSTERED 
(
	[BidRefNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:No(Default) ; 1:Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'IsApprovedByPurchasing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:No(Default) ; 1:Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'IsApprovedByFinance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:No(Default) ; 1:Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'IsApprovedByAudit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:No(Default) ; 1:Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'IsOpenedByPurchasing'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:No(Default) ; 1:Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'IsOpenedByFinance'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:No(Default) ; 1:Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'IsOpenedByAudit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 No, 1 Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItems', @level2type=N'COLUMN',@level2name=N'QualifiedSourcing'
GO
/****** Object:  Table [dbo].[tblProducts]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblProducts](
	[SKU] [nvarchar](10) NOT NULL,
	[ItemName] [varchar](200) NULL,
	[ProductDescription] [varchar](1000) NULL,
	[UnitOfMeasure] [nvarchar](7) NULL,
	[SubCategoryId] [int] NULL,
	[Service] [int] NULL,
	[Brand] [int] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[MainCategoryId] [nvarchar](7) NULL,
 CONSTRAINT [PK_tblProduct] PRIMARY KEY CLUSTERED 
(
	[SKU] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblPresentServices]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblPresentServices](
	[PresentServiceID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[PlanID] [int] NOT NULL,
	[AccountNo] [varchar](100) NOT NULL,
	[CreditLimit] [varchar](20) NOT NULL,
 CONSTRAINT [PK_tblPresentServices] PRIMARY KEY CLUSTERED 
(
	[PresentServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorRelative]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorRelative](
	[VendorRelativeID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[VendorRelative] [varchar](100) NULL,
	[Title] [varchar](50) NULL,
	[Relationship] [varchar](50) NULL,
 CONSTRAINT [PK_tblVendorRelative] PRIMARY KEY CLUSTERED 
(
	[VendorRelativeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorReferences]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorReferences](
	[ReferencesNo] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[AveMonthlySales] [varchar](20) NULL,
	[CreditLine] [varchar](20) NULL,
	[KindOfBusiness] [varchar](100) NULL,
	[LegalCounsel] [varchar](100) NULL,
	[ReferenceType] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorReferences] PRIMARY KEY CLUSTERED 
(
	[ReferencesNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorProdItems]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorProdItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[ProdItemNo] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorProdItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorLocation]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorLocation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorLocation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rfcProductBrands]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductBrands](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [char](10) NULL,
	[SubCategoryId] [int] NULL,
 CONSTRAINT [PK_rfcProductBrands] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionItemFileUploads]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuctionItemFileUploads](
	[FileUploadID] [int] IDENTITY(1,1) NOT NULL,
	[AuctionRefNo] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[DateUploaded] [datetime] NOT NULL,
	[OriginalFileName] [varchar](100) NOT NULL,
	[ActualFileName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tblAuctionItemFileUploads] PRIMARY KEY CLUSTERED 
(
	[FileUploadID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionParticipants]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAuctionParticipants](
	[ParticipantId] [int] IDENTITY(1,1) NOT NULL,
	[AuctionRefNo] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Ticket] [nvarchar](200) NOT NULL,
	[DateReplied] [datetime] NULL,
	[DateParticipated] [datetime] NULL,
	[EmailSent] [int] NOT NULL,
 CONSTRAINT [PK_tblAuctionParticipants] PRIMARY KEY CLUSTERED 
(
	[ParticipantId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:Invited; 1:Confirmed; 2:Declined; 3:Participated;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionParticipants', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Table [dbo].[tblSupervisor]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupervisor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BuyerId] [int] NOT NULL,
	[PurchasingId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[OrderId] [smallint] NOT NULL,
 CONSTRAINT [PK_tblSuperior] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorServices]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorServices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[ServiceID] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorServices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorClassification]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorClassification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[ClassificationId] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorClassification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserPassword] [nvarchar](400) NOT NULL,
	[UserType] [int] NOT NULL,
	[RefNo] [nvarchar](20) NULL,
	[Status] [smallint] NOT NULL,
	[TempPassword] [nvarchar](400) NULL,
	[IsAuthenticated] [smallint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[SessionId] [nvarchar](50) NULL,
	[LoginStatus] [bit] NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[clientId] [int] NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [un_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1: Buyer; 2: Vendor; 3: Purchasing; 4: Administrator;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUsers', @level2type=N'COLUMN',@level2name=N'UserType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Inactive; 1: Active; 2: Deleted;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUsers', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: No; 1: Yes;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblUsers', @level2type=N'COLUMN',@level2name=N'IsAuthenticated'
GO
/****** Object:  Table [dbo].[tblVendorCategoriesAndSubcategories]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorCategoriesAndSubcategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[IncludesAllSubCategories] [int] NULL,
	[SubCategoryId] [int] NULL,
	[Brandid] [int] NULL,
	[MainCategoryId] [nvarchar](7) NULL
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'if 1, subcategoryid is null meaning all no subcategory is selected; all subcategories qualify' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorCategoriesAndSubcategories', @level2type=N'COLUMN',@level2name=N'IncludesAllSubCategories'
GO
/****** Object:  Table [dbo].[tblVendorsInAuctions]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorsInAuctions](
	[VendorsInAuctionId] [int] IDENTITY(1,1) NOT NULL,
	[AuctionRefNo] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVendorsInAuctions] PRIMARY KEY CLUSTERED 
(
	[VendorsInAuctionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for this table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInAuctions', @level2type=N'COLUMN',@level2name=N'VendorsInAuctionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: For Approval/Invited; 1: Invitation Confirmed; 2: Invitation Declined;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInAuctions', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Table [dbo].[tblVendorEquipments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorEquipments](
	[EquipmentID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[EquipmentType] [varchar](100) NULL,
	[Units] [int] NULL,
	[Remarks] [varchar](100) NULL,
 CONSTRAINT [PK_tblVendorEquipments] PRIMARY KEY CLUSTERED 
(
	[EquipmentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorsInBids]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorsInBids](
	[VendorInBidsId] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
	[DateInvited] [datetime] NOT NULL,
	[DateConfirmed] [datetime] NULL,
	[DateDeclined] [datetime] NULL,
	[EmailSent] [smallint] NOT NULL,
	[AwardEmailSent] [smallint] NULL,
 CONSTRAINT [PK_tblVendorsInBids] PRIMARY KEY CLUSTERED 
(
	[VendorInBidsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Primary Key for this table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'VendorInBidsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'REQUIRED!' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'BidRefNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'REQUIRED!' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'VendorId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: For Approval/Invited; 1: Invitation Confirmed; 2: Invitation Declined;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date vendor was invited' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'DateInvited'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date vendor confirmed invitation' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'DateConfirmed'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date vendor declined invitation' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'DateDeclined'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'No of email invitation sent to this vendor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorsInBids', @level2type=N'COLUMN',@level2name=N'EmailSent'
GO
/****** Object:  Table [dbo].[tblVendorBrands]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorBrands](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
 CONSTRAINT [PK_tblVendorBrands] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAuctionParticipantComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuctionParticipantComments](
	[ItemCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[ParticipantId] [int] NOT NULL,
	[Comment] [varchar](255) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [PK_tblAuctionParticipantComments] PRIMARY KEY CLUSTERED 
(
	[ItemCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionItemDetails]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAuctionItemDetails](
	[AuctionDetailNo] [int] IDENTITY(1,1) NOT NULL,
	[AuctionRefNo] [int] NOT NULL,
	[Item] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Quantity] [float] NULL,
	[UnitOfMeasure] [varchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[Status] [int] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[SubCategoryId] [int] NULL,
	[StartingPrice] [float] NULL,
	[IncrementDecrement] [float] NULL,
	[ForConversion] [smallint] NULL,
	[BidDetailNo] [int] NULL,
 CONSTRAINT [PK_tblAuctionItemDetails] PRIMARY KEY CLUSTERED 
(
	[AuctionDetailNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAuctionEndorsements]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAuctionEndorsements](
	[AuctionEndorsementId] [int] IDENTITY(1,1) NOT NULL,
	[AuctionTrailId] [int] NOT NULL,
	[BuyerId] [int] NOT NULL,
	[PurchasingId] [int] NULL,
	[Status] [smallint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[DateApproved] [datetime] NULL,
	[DateRejected] [datetime] NULL,
 CONSTRAINT [PK_tblAuctionEndorsements] PRIMARY KEY CLUSTERED 
(
	[AuctionEndorsementId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Auction bid info' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionEndorsements', @level2type=N'COLUMN',@level2name=N'AuctionTrailId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Endorsement creator' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionEndorsements', @level2type=N'COLUMN',@level2name=N'BuyerId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Endorsement approver' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionEndorsements', @level2type=N'COLUMN',@level2name=N'PurchasingId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: Pending/For Approval; 1: Approved; 2: Rejected; 3: For Re-Edit' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblAuctionEndorsements', @level2type=N'COLUMN',@level2name=N'Status'
GO
/****** Object:  Table [dbo].[tblVendorFileUploads]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorFileUploads](
	[FileUploadID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[VendorID] [int] NOT NULL,
	[DateUploaded] [datetime] NOT NULL,
	[OriginalFileName] [varchar](100) NOT NULL,
	[ActualFileName] [varchar](100) NOT NULL,
	[IsDetachable] [bit] NOT NULL,
	[AsDraft] [bit] NOT NULL,
	[Filesize] [float] NULL,
 CONSTRAINT [PK_tblVendorFileUploads] PRIMARY KEY CLUSTERED 
(
	[FileUploadID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The original filename used with reference to the filename uploaded' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorFileUploads', @level2type=N'COLUMN',@level2name=N'OriginalFileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The filename used when saved in the server directory' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorFileUploads', @level2type=N'COLUMN',@level2name=N'ActualFileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 No, 1 Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorFileUploads', @level2type=N'COLUMN',@level2name=N'IsDetachable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 No, 1 Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblVendorFileUploads', @level2type=N'COLUMN',@level2name=N'AsDraft'
GO
/****** Object:  Table [dbo].[tblAuthDept]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAuthDept](
	[AuthDeptID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[PurchDept] [int] NOT NULL,
	[IntAuditDept] [int] NOT NULL,
	[FinanceDept] [int] NOT NULL,
 CONSTRAINT [PK_tblAuthDept] PRIMARY KEY CLUSTERED 
(
	[AuthDeptID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBidItemFileUploads]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidItemFileUploads](
	[FileUploadID] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[BuyerID] [int] NOT NULL,
	[DateUploaded] [datetime] NOT NULL,
	[OriginalFileName] [varchar](100) NOT NULL,
	[ActualFileName] [varchar](100) NOT NULL,
	[Filesize] [float] NULL,
 CONSTRAINT [PK_tblBidItemFileUploads] PRIMARY KEY CLUSTERED 
(
	[FileUploadID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBidItemDetails]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidItemDetails](
	[BidDetailNo] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[Item] [nvarchar](10) NOT NULL,
	[DetailDesc] [varchar](1000) NOT NULL,
	[UnitOfMeasure] [varchar](50) NOT NULL,
	[UnitPrice] [float] NOT NULL,
	[Qty] [float] NOT NULL,
	[TotalUnitPrice]  AS ([UnitPrice] * [Qty]),
	[DeliveryDate] [datetime] NOT NULL,
	[ConversionStatus] [smallint] NOT NULL,
	[DateSentForConversion] [datetime] NULL,
	[DateApprovedForConversion] [datetime] NULL,
	[DateDisapprovedForConversion] [datetime] NULL,
	[DateConverted] [datetime] NULL,
	[WithdrawalStatus] [smallint] NOT NULL,
	[DateSentForWithdrawal] [datetime] NULL,
	[DateApprovedForWithdrawal] [datetime] NULL,
	[DateDisapprovedForWithdrawal] [datetime] NULL,
	[DateWithdrawned] [datetime] NULL,
	[SavedAsDraftOnAuction] [bit] NULL,
	[AuctionRefNoOnAuction] [int] NULL,
	[PRGroupName] [varchar](100) NULL,
 CONSTRAINT [PK_tblBidItemDetails] PRIMARY KEY CLUSTERED 
(
	[BidDetailNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: No; 1:For Approvall; 2: Approved; 3: Disapproved; 4: Converted;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItemDetails', @level2type=N'COLUMN',@level2name=N'ConversionStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: No;1:  For Approval; 2: Approved; 3: Disapproved;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItemDetails', @level2type=N'COLUMN',@level2name=N'WithdrawalStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 No, 1 Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItemDetails', @level2type=N'COLUMN',@level2name=N'SavedAsDraftOnAuction'
GO
/****** Object:  Table [dbo].[tblBidItemComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidItemComments](
	[ItemCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[BidRefNo] [int] NOT NULL,
	[BuyerId] [varchar](50) NOT NULL,
	[PurchasingId] [int] NOT NULL,
	[Comment] [varchar](1000) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
	[AllowVendorView] [bit] NOT NULL,
 CONSTRAINT [PK_tblBidItemComments] PRIMARY KEY CLUSTERED 
(
	[ItemCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: False; 1: True' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItemComments', @level2type=N'COLUMN',@level2name=N'AllowVendorView'
GO
/****** Object:  Table [dbo].[tblBidParticipantComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidParticipantComments](
	[ItemCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[VendorsInBidId] [int] NOT NULL,
	[Comment] [varchar](255) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [PK_tblBidParticipantComments] PRIMARY KEY CLUSTERED 
(
	[ItemCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblBidItemDetailComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidItemDetailComments](
	[ItemCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[BidDetailNo] [int] NOT NULL,
	[Comment] [varchar](255) NOT NULL,
	[PurchasingId] [int] NOT NULL,
	[DatePosted] [datetime] NOT NULL,
	[AllowVendorView] [bit] NOT NULL,
 CONSTRAINT [PK_tblWithdrawnedItemComments] PRIMARY KEY CLUSTERED 
(
	[ItemCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 No, 1 Yes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidItemDetailComments', @level2type=N'COLUMN',@level2name=N'AllowVendorView'
GO
/****** Object:  Table [dbo].[tblBidTenders]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidTenders](
	[BidTenderNo] [int] IDENTITY(1,1) NOT NULL,
	[BidDetailNo] [int] NOT NULL,
	[VendorID] [int] NOT NULL,
	[Status] [smallint] NOT NULL,
	[RenegotiationStatus] [smallint] NOT NULL,
	[Amount] [float] NOT NULL,
	[DeliveryCost] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[Warranty] [varchar](50) NULL,
	[Remarks] [varchar](255) NULL,
	[DeliveryDate] [datetime] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateSubmitted] [datetime] NULL,
	[DateEndorsed] [datetime] NULL,
	[DateRenegotiated] [datetime] NULL,
	[DateAwarded] [datetime] NULL,
	[AsClarified] [bit] NULL,
	[InitialAmount] [float] NULL,
	[InitialDiscount] [float] NULL,
	[InitialDeliveryCost] [float] NULL,
	[LeadTime] [varchar](50) NULL,
	[Incoterm] [varchar](50) NULL,
	[Currency] [varchar](25) NULL,
	[PaymentTerms] [varchar](50) NULL,
	[DutiesTaxes] [float] NULL,
	[Forwarding] [float] NULL,
	[PONumber] [varchar](15) NULL,
	[POApproved] [tinyint] NULL,
	[AwardedStatus] [varchar](50) NULL,
 CONSTRAINT [PK_tblBidTenders] PRIMARY KEY CLUSTERED 
(
	[BidTenderNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: None; 1: Purchasing to Buyer; 2: Buyer to Vendor; 3: Vendor to Buyer; 4: Buyer to Purchasing;' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidTenders', @level2type=N'COLUMN',@level2name=N'RenegotiationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Renegotiated, 1 Clarified' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidTenders', @level2type=N'COLUMN',@level2name=N'AsClarified'
GO
/****** Object:  Table [dbo].[tblBidTenderComments]    Script Date: 04/30/2015 15:38:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblBidTenderComments](
	[TenderCommentNo] [int] IDENTITY(1,1) NOT NULL,
	[BidTenderNo] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [varchar](1000) NOT NULL,
	[CommentType] [char](2) NOT NULL,
	[DatePosted] [datetime] NOT NULL,
 CONSTRAINT [PK_tblBidTenderComments] PRIMARY KEY CLUSTERED 
(
	[TenderCommentNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'BP: Buyer to Purchasing; BV: Buyer to Vendor; VB: Vendor to Buyer; PB: Purchasing to Buyer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblBidTenderComments', @level2type=N'COLUMN',@level2name=N'CommentType'
GO
/****** Object:  Default [DF_rfcCurrency_RateToUSDollar]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcCurrency] ADD  CONSTRAINT [DF_rfcCurrency_RateToUSDollar]  DEFAULT (0.0) FOR [RateToUSD]
GO
/****** Object:  Default [DF_rfcCurrency_RateToPHP]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcCurrency] ADD  CONSTRAINT [DF_rfcCurrency_RateToPHP]  DEFAULT (0.0) FOR [RateToPHP]
GO
/****** Object:  Default [DF_rfcCurrency_LastUpdate]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcCurrency] ADD  CONSTRAINT [DF_rfcCurrency_LastUpdate]  DEFAULT (getdate()) FOR [AsOf]
GO
/****** Object:  Default [DF_rfcCurrency_Deletable]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcCurrency] ADD  CONSTRAINT [DF_rfcCurrency_Deletable]  DEFAULT (1) FOR [Deletable]
GO
/****** Object:  Default [DF_tblAuctionEndorsements_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionEndorsements] ADD  CONSTRAINT [DF_tblAuctionEndorsements_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblAuctionEndorsements_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionEndorsements] ADD  CONSTRAINT [DF_tblAuctionEndorsements_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblAuctionItemComments_AllowVendorView]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemComments] ADD  CONSTRAINT [DF_tblAuctionItemComments_AllowVendorView]  DEFAULT (0) FOR [AllowVendorView]
GO
/****** Object:  Default [DF_tblAuctionItemDetails_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails] ADD  CONSTRAINT [DF_tblAuctionItemDetails_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblAuctionItemDetails_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails] ADD  CONSTRAINT [DF_tblAuctionItemDetails_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblAuctionItemDetails_StartingPrice]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails] ADD  CONSTRAINT [DF_tblAuctionItemDetails_StartingPrice]  DEFAULT (0.00) FOR [StartingPrice]
GO
/****** Object:  Default [DF_tblAuctionItemDetails_IncrementDecrement]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails] ADD  CONSTRAINT [DF_tblAuctionItemDetails_IncrementDecrement]  DEFAULT (1.00) FOR [IncrementDecrement]
GO
/****** Object:  Default [DF_tblAuctionItemFileUploads_DateUploaded]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemFileUploads] ADD  CONSTRAINT [DF_tblAuctionItemFileUploads_DateUploaded]  DEFAULT (getdate()) FOR [DateUploaded]
GO
/****** Object:  Default [DF_tblAuctionItems_AuctionType]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItems] ADD  CONSTRAINT [DF_tblAuctionItems_AuctionType]  DEFAULT (0) FOR [AuctionType]
GO
/****** Object:  Default [DF_tblAuctionItems_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItems] ADD  CONSTRAINT [DF_tblAuctionItems_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblAuctionItems_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItems] ADD  CONSTRAINT [DF_tblAuctionItems_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblAuctionItemTrail_LastBid]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemTrail] ADD  CONSTRAINT [DF_tblAuctionItemTrail_LastBid]  DEFAULT (0.00) FOR [Bid]
GO
/****** Object:  Default [DF_tblAuctionItemTrail_DateSubmitted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemTrail] ADD  CONSTRAINT [DF_tblAuctionItemTrail_DateSubmitted]  DEFAULT (getdate()) FOR [DateSubmitted]
GO
/****** Object:  Default [DF__tblAuctio__Aucti__5992AC31]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemTrail] ADD  DEFAULT ((0)) FOR [AuctionTrailStatus]
GO
/****** Object:  Default [DF_tblAuctionParticipantComments_DatePosted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionParticipantComments] ADD  CONSTRAINT [DF_tblAuctionParticipantComments_DatePosted]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF_tblAuctionParticipants_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionParticipants] ADD  CONSTRAINT [DF_tblAuctionParticipants_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblAuctionParticipants_EmailSent]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionParticipants] ADD  CONSTRAINT [DF_tblAuctionParticipants_EmailSent]  DEFAULT (0) FOR [EmailSent]
GO
/****** Object:  Default [DF_tblAuthDept_PurchDept]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuthDept] ADD  CONSTRAINT [DF_tblAuthDept_PurchDept]  DEFAULT (0) FOR [PurchDept]
GO
/****** Object:  Default [DF_tblAuthDept_IntAuditDept]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuthDept] ADD  CONSTRAINT [DF_tblAuthDept_IntAuditDept]  DEFAULT (0) FOR [IntAuditDept]
GO
/****** Object:  Default [DF_tblAuthDept_FinanceDept]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuthDept] ADD  CONSTRAINT [DF_tblAuthDept_FinanceDept]  DEFAULT (0) FOR [FinanceDept]
GO
/****** Object:  Default [DF__tblBacBid__BidAm__3C375374]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__BidAm__3C375374]  DEFAULT (0.0) FOR [BidAmount]
GO
/****** Object:  Default [DF__tblBacBid__Statu__3D2B77AD]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Statu__3D2B77AD]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF__tblBacBid__Budge__3E1F9BE6]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Budge__3E1F9BE6]  DEFAULT (0) FOR [Budgeted]
GO
/****** Object:  Default [DF__tblBacBid__Compa__3F13C01F]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Compa__3F13C01F]  DEFAULT (0) FOR [CompanyId]
GO
/****** Object:  Default [DF__tblBacBid__PT_Ad__45C0BDAE]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__PT_Ad__45C0BDAE]  DEFAULT (0) FOR [Payment_Terms]
GO
/****** Object:  Default [DF__tblBacBid__SS_No__4C6DBB3D]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__SS_No__4C6DBB3D]  DEFAULT (0) FOR [SS_NoBidders]
GO
/****** Object:  Default [DF__tblBacBid__SS_No__4D61DF76]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__SS_No__4D61DF76]  DEFAULT (0) FOR [SS_NoBidsRcvd]
GO
/****** Object:  Default [DF__tblBacBid__SS_Te__4E5603AF]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__SS_Te__4E5603AF]  DEFAULT (0) FOR [SS_TechCompliance]
GO
/****** Object:  Default [DF__tblBacBid__Savin__55F72577]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Savin__55F72577]  DEFAULT (0.0) FOR [SavingsGen_Nature]
GO
/****** Object:  Default [DF__tblBacBid__Savin__56EB49B0]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Savin__56EB49B0]  DEFAULT (0.0) FOR [SavingsGen_Amount]
GO
/****** Object:  Default [DF__tblBacBid__Savin__57DF6DE9]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Savin__57DF6DE9]  DEFAULT (0.0) FOR [SavingsGen_PctSpend]
GO
/****** Object:  Default [DF__tblBacBid__Prepa__58D39222]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBacBidItems] ADD  CONSTRAINT [DF__tblBacBid__Prepa__58D39222]  DEFAULT (getdate()) FOR [PreparedDt]
GO
/****** Object:  Default [DF__tblBACCla__DateP__5F167B5D]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBACClarifications] ADD  CONSTRAINT [DF__tblBACCla__DateP__5F167B5D]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF__tblBACCom__DateP__600A9F96]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBACComments] ADD  CONSTRAINT [DF__tblBACCom__DateP__600A9F96]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF__tblBACEvalu__Qty__69FE1E24]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBACEvaluationDetails] ADD  CONSTRAINT [DF__tblBACEvalu__Qty__69FE1E24]  DEFAULT (0.0) FOR [Qty]
GO
/****** Object:  Default [DF__tblBACEva__Total__6AF2425D]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBACEvaluationDetails] ADD  CONSTRAINT [DF__tblBACEva__Total__6AF2425D]  DEFAULT (0.0) FOR [TotalCost]
GO
/****** Object:  Default [DF__tblBACEva__DateC__6BE66696]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBACEvaluationSummary] ADD  CONSTRAINT [DF__tblBACEva__DateC__6BE66696]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblBACExe__DateP__65C378EC]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBACExecutiveSummary] ADD  CONSTRAINT [DF__tblBACExe__DateP__65C378EC]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF_tblBidItemComments_BuyerId]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemComments] ADD  CONSTRAINT [DF_tblBidItemComments_BuyerId]  DEFAULT (0) FOR [BuyerId]
GO
/****** Object:  Default [DF_tblBidItemComments_PurchasingId]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemComments] ADD  CONSTRAINT [DF_tblBidItemComments_PurchasingId]  DEFAULT (0) FOR [PurchasingId]
GO
/****** Object:  Default [DF_tblBidItemComments_DatePosted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemComments] ADD  CONSTRAINT [DF_tblBidItemComments_DatePosted]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF_tblBidItemComments_AllowVendorView]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemComments] ADD  CONSTRAINT [DF_tblBidItemComments_AllowVendorView]  DEFAULT (0) FOR [AllowVendorView]
GO
/****** Object:  Default [DF_tblWithdrawnedItemComments_DatePosted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetailComments] ADD  CONSTRAINT [DF_tblWithdrawnedItemComments_DatePosted]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF_tblBidItemDetailComments_AllowVendorView]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetailComments] ADD  CONSTRAINT [DF_tblBidItemDetailComments_AllowVendorView]  DEFAULT (0) FOR [AllowVendorView]
GO
/****** Object:  Default [DF_tblBidItemDetails_UnitOfMeasure]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_UnitOfMeasure]  DEFAULT ('PC') FOR [UnitOfMeasure]
GO
/****** Object:  Default [DF_tblBidItemDetails_UnitPrice]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_UnitPrice]  DEFAULT (0.0) FOR [UnitPrice]
GO
/****** Object:  Default [DF_tblBidItemDetails_Qty]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_Qty]  DEFAULT (0) FOR [Qty]
GO
/****** Object:  Default [DF_tblBidItemDetails_DeliveryDate]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_DeliveryDate]  DEFAULT (getdate()) FOR [DeliveryDate]
GO
/****** Object:  Default [DF_tblBidItemDetails_ForAuction]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_ForAuction]  DEFAULT (0) FOR [ConversionStatus]
GO
/****** Object:  Default [DF_tblBidItemDetails_WithdrawalStatus]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_WithdrawalStatus]  DEFAULT (0) FOR [WithdrawalStatus]
GO
/****** Object:  Default [DF_tblBidItemDetails_SavedAsDraftOnAuction]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails] ADD  CONSTRAINT [DF_tblBidItemDetails_SavedAsDraftOnAuction]  DEFAULT (0) FOR [SavedAsDraftOnAuction]
GO
/****** Object:  Default [DF_tblBidItemFileUploads_DateUploaded]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemFileUploads] ADD  CONSTRAINT [DF_tblBidItemFileUploads_DateUploaded]  DEFAULT (getdate()) FOR [DateUploaded]
GO
/****** Object:  Default [DF_tblBidItems_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_Status]  DEFAULT ((0)) FOR [Status]
GO
/****** Object:  Default [DF_tblBidItems_ForAuctionId]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_ForAuctionId]  DEFAULT ((0)) FOR [ForAuction]
GO
/****** Object:  Default [DF_tblBidItems_BidCurrency]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_BidCurrency]  DEFAULT ('PHP') FOR [Currency]
GO
/****** Object:  Default [DF_tblBidItems_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblBidItems_DateSubmitted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_DateSubmitted]  DEFAULT (getdate()) FOR [DateSubmitted]
GO
/****** Object:  Default [DF_tblBidItems_DateRejected]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_DateRejected]  DEFAULT ((0)) FOR [DateRejected]
GO
/****** Object:  Default [DF_tblBidItems_DateOfRequestToConvertToAuction]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_DateOfRequestToConvertToAuction]  DEFAULT ((0)) FOR [DateOfRequestToConvertToAuction]
GO
/****** Object:  Default [DF_tblBidItems_IsApprovedByPurchasing]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_IsApprovedByPurchasing]  DEFAULT ((0)) FOR [IsApprovedByPurchasing]
GO
/****** Object:  Default [DF_tblBidItems_IsApprovedByFinance]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_IsApprovedByFinance]  DEFAULT ((0)) FOR [IsApprovedByFinance]
GO
/****** Object:  Default [DF_tblBidItems_IsApprovedByAudit]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_IsApprovedByAudit]  DEFAULT ((0)) FOR [IsApprovedByAudit]
GO
/****** Object:  Default [DF_tblBidItems_IsOpenedByPurchasing]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_IsOpenedByPurchasing]  DEFAULT ((0)) FOR [IsOpenedByPurchasing]
GO
/****** Object:  Default [DF_tblBidItems_IsOpenedByFinance]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_IsOpenedByFinance]  DEFAULT ((0)) FOR [IsOpenedByFinance]
GO
/****** Object:  Default [DF_tblBidItems_IsOpenedByAudit]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_IsOpenedByAudit]  DEFAULT ((0)) FOR [IsOpenedByAudit]
GO
/****** Object:  Default [DF_tblBidItems_QualifiedSourcing]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF_tblBidItems_QualifiedSourcing]  DEFAULT ((0)) FOR [QualifiedSourcing]
GO
/****** Object:  Default [DF__tblBidIte__boc_s__1CBEA81B]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems] ADD  CONSTRAINT [DF__tblBidIte__boc_s__1CBEA81B]  DEFAULT ('N') FOR [boc_sent_email]
GO
/****** Object:  Default [DF_tblBidOpeningCommittee_Committee]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidOpeningCommittee] ADD  CONSTRAINT [DF_tblBidOpeningCommittee_Committee]  DEFAULT (1) FOR [CommitteeId]
GO
/****** Object:  Default [DF_tblBidParticipantComments_DatePosted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidParticipantComments] ADD  CONSTRAINT [DF_tblBidParticipantComments_DatePosted]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF_tblBidTenderComments_CommentType]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenderComments] ADD  CONSTRAINT [DF_tblBidTenderComments_CommentType]  DEFAULT ('BP') FOR [CommentType]
GO
/****** Object:  Default [DF_tblBidTenderComments_DatePosted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenderComments] ADD  CONSTRAINT [DF_tblBidTenderComments_DatePosted]  DEFAULT (getdate()) FOR [DatePosted]
GO
/****** Object:  Default [DF_tblBidTenders_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblBidTenders_RenegotiationStatus]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_RenegotiationStatus]  DEFAULT (0) FOR [RenegotiationStatus]
GO
/****** Object:  Default [DF_tblBidTenders_Amount]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_Amount]  DEFAULT (0.0) FOR [Amount]
GO
/****** Object:  Default [DF_tblBidTenders_DeliveryCost]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_DeliveryCost]  DEFAULT (0.0) FOR [DeliveryCost]
GO
/****** Object:  Default [DF_tblBidTenders_Discount]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_Discount]  DEFAULT (0.0) FOR [Discount]
GO
/****** Object:  Default [DF_tblBidTenders_DeliveryDate]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_DeliveryDate]  DEFAULT (getdate()) FOR [DeliveryDate]
GO
/****** Object:  Default [DF_tblBidTenders_TenderDate]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_TenderDate]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblBidTenders_AsClarified]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_AsClarified]  DEFAULT (0) FOR [AsClarified]
GO
/****** Object:  Default [DF_tblBidTenders_DutiesTaxes]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_DutiesTaxes]  DEFAULT ((0)) FOR [DutiesTaxes]
GO
/****** Object:  Default [DF_tblBidTenders_Forwarding]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders] ADD  CONSTRAINT [DF_tblBidTenders_Forwarding]  DEFAULT ((0)) FOR [Forwarding]
GO
/****** Object:  Default [DF_tblBidTendersAddedCosts_Freight]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTendersAddedCosts.bak] ADD  CONSTRAINT [DF_tblBidTendersAddedCosts_Freight]  DEFAULT ((0.0)) FOR [InLandFreight]
GO
/****** Object:  Default [DF_tblBidTendersAddedCosts_Brokerage]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTendersAddedCosts.bak] ADD  CONSTRAINT [DF_tblBidTendersAddedCosts_Brokerage]  DEFAULT ((0.0)) FOR [FowardingBrokerage]
GO
/****** Object:  Default [DF_tblBidTendersAddedCosts_DutiesTaxes]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTendersAddedCosts.bak] ADD  CONSTRAINT [DF_tblBidTendersAddedCosts_DutiesTaxes]  DEFAULT ((0.0)) FOR [DutiesTaxes]
GO
/****** Object:  Default [DF_tblBidTendersAddedCosts_Insurance]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTendersAddedCosts.bak] ADD  CONSTRAINT [DF_tblBidTendersAddedCosts_Insurance]  DEFAULT ((0.0)) FOR [Insurance]
GO
/****** Object:  Default [DF_tblBidTendersAddedCosts_Delivery]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTendersAddedCosts.bak] ADD  CONSTRAINT [DF_tblBidTendersAddedCosts_Delivery]  DEFAULT ((0.0)) FOR [DeliveryCostToSite]
GO
/****** Object:  Default [DF_tblContent_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblContent] ADD  CONSTRAINT [DF_tblContent_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblContent_DateModified]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblContent] ADD  CONSTRAINT [DF_tblContent_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
/****** Object:  Default [DF_tblContent_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblContent] ADD  CONSTRAINT [DF_tblContent_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblContent_Type]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblContent] ADD  CONSTRAINT [DF_tblContent_Type]  DEFAULT (0) FOR [Type]
GO
/****** Object:  Default [DF_tblProduct_IsDeleted]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblProducts] ADD  CONSTRAINT [DF_tblProduct_IsDeleted]  DEFAULT (0) FOR [IsDeleted]
GO
/****** Object:  Default [DF_tblPurchasing_DeptID]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblPurchasing] ADD  CONSTRAINT [DF_tblPurchasing_DeptID]  DEFAULT (1) FOR [DeptID]
GO
/****** Object:  Default [DF_tblSuperior_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblSupervisor] ADD  CONSTRAINT [DF_tblSuperior_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblSuperior_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblSupervisor] ADD  CONSTRAINT [DF_tblSuperior_Status]  DEFAULT (1) FOR [OrderId]
GO
/****** Object:  Default [DF_tblUsers_UserType]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_UserType]  DEFAULT (2) FOR [UserType]
GO
/****** Object:  Default [DF_tblUsers_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_Status]  DEFAULT (1) FOR [Status]
GO
/****** Object:  Default [DF_tblUsers_IsAuthenticated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_IsAuthenticated]  DEFAULT (0) FOR [IsAuthenticated]
GO
/****** Object:  Default [DF_tblUsers_DateCreated]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblUsers_LoginStatus]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_LoginStatus]  DEFAULT (0) FOR [LoginStatus]
GO
/****** Object:  Default [DF_tblVendorFileUploads_DateUploaded]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorFileUploads] ADD  CONSTRAINT [DF_tblVendorFileUploads_DateUploaded]  DEFAULT (getdate()) FOR [DateUploaded]
GO
/****** Object:  Default [DF_tblVendorFileUploads_IsDetachable]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorFileUploads] ADD  CONSTRAINT [DF_tblVendorFileUploads_IsDetachable]  DEFAULT (0) FOR [IsDetachable]
GO
/****** Object:  Default [DF_tblVendorFileUploads_AsDraft]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorFileUploads] ADD  CONSTRAINT [DF_tblVendorFileUploads_AsDraft]  DEFAULT (0) FOR [AsDraft]
GO
/****** Object:  Default [DF_tblVendors_Accredited]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendors] ADD  CONSTRAINT [DF_tblVendors_Accredited]  DEFAULT ((0)) FOR [Accredited]
GO
/****** Object:  Default [DF_tblVendors_OrgTypeID]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendors] ADD  CONSTRAINT [DF_tblVendors_OrgTypeID]  DEFAULT ((1)) FOR [OrgTypeID]
GO
/****** Object:  Default [DF_tblVendors_isBlackListed]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendors] ADD  CONSTRAINT [DF_tblVendors_isBlackListed]  DEFAULT ((0)) FOR [IsBlackListed]
GO
/****** Object:  Default [DF__tblVendor__PRAmo__27072C64]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [PRAmount]
GO
/****** Object:  Default [DF__tblVendor__PRDes__27FB509D]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [PRDescription]
GO
/****** Object:  Default [DF__tblVendor__Statu__28EF74D6]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF__tblVendor__Prepa__29E3990F]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (getdate()) FOR [PreparedDt]
GO
/****** Object:  Default [DF__tblVendor__Appro__2F9C7265]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [ApprovedMemo]
GO
/****** Object:  Default [DF__tblVendor__Board__3090969E]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [BoardApproval]
GO
/****** Object:  Default [DF__tblVendor__Endor__3184BAD7]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [EndorsementMemo]
GO
/****** Object:  Default [DF__tblVendor__Excom__3278DF10]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [ExcomApproval]
GO
/****** Object:  Default [DF__tblVendor__Other__336D0349]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorShortlistingForm] ADD  DEFAULT (0) FOR [Others]
GO
/****** Object:  Default [DF_tblVendorsInAuctions_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInAuctions] ADD  CONSTRAINT [DF_tblVendorsInAuctions_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblVendorsInBids_Status]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInBids] ADD  CONSTRAINT [DF_tblVendorsInBids_Status]  DEFAULT (0) FOR [Status]
GO
/****** Object:  Default [DF_tblVendorsInBids_DateInvited]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInBids] ADD  CONSTRAINT [DF_tblVendorsInBids_DateInvited]  DEFAULT (getdate()) FOR [DateInvited]
GO
/****** Object:  Default [DF_tblVendorsInBids_EmailSent]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInBids] ADD  CONSTRAINT [DF_tblVendorsInBids_EmailSent]  DEFAULT (0) FOR [EmailSent]
GO
/****** Object:  Default [DF__tblVSFCom__DateC__3A1A00D8]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVSFComments] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVSFDet__MaxEx__2CC005BA]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVSFDetails] ADD  DEFAULT (0) FOR [MaxExposureLimit]
GO
/****** Object:  Default [DF__tblVSFDet__Amoun__2DB429F3]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVSFDetails] ADD  DEFAULT (0) FOR [AmountUnservedPO]
GO
/****** Object:  Default [DF__tblVSFDet__DateC__2EA84E2C]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVSFDetails] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVSFDet__Selec__44978F4B]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVSFDetails] ADD  DEFAULT (1) FOR [Selected]
GO
/****** Object:  ForeignKey [FK_rfcProductBrands_rfcProductSubCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcProductBrands]  WITH NOCHECK ADD  CONSTRAINT [FK_rfcProductBrands_rfcProductSubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[rfcProductSubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[rfcProductBrands] CHECK CONSTRAINT [FK_rfcProductBrands_rfcProductSubCategory]
GO
/****** Object:  ForeignKey [FK_rfcProductBrands_rfcProductSubCategory1]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcProductBrands]  WITH CHECK ADD  CONSTRAINT [FK_rfcProductBrands_rfcProductSubCategory1] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[rfcProductSubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[rfcProductBrands] CHECK CONSTRAINT [FK_rfcProductBrands_rfcProductSubCategory1]
GO
/****** Object:  ForeignKey [FK_rfcProductSubCategory_rfcProductCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcProductSubCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_rfcProductSubCategory_rfcProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[rfcProductCategory] ([CategoryId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[rfcProductSubCategory] NOCHECK CONSTRAINT [FK_rfcProductSubCategory_rfcProductCategory]
GO
/****** Object:  ForeignKey [FK_rfcProductSubCategory_rfcProductCategory1]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[rfcProductSubCategory]  WITH NOCHECK ADD  CONSTRAINT [FK_rfcProductSubCategory_rfcProductCategory1] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[rfcProductCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[rfcProductSubCategory] NOCHECK CONSTRAINT [FK_rfcProductSubCategory_rfcProductCategory1]
GO
/****** Object:  ForeignKey [FK_tblAuctionEndorsements_tblAuctionItemTrail]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionEndorsements]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionEndorsements_tblAuctionItemTrail] FOREIGN KEY([AuctionTrailId])
REFERENCES [dbo].[tblAuctionItemTrail] ([AuctionTrailId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAuctionEndorsements] CHECK CONSTRAINT [FK_tblAuctionEndorsements_tblAuctionItemTrail]
GO
/****** Object:  ForeignKey [FK_tblAuctionEndorsements_tblBuyers]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionEndorsements]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionEndorsements_tblBuyers] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[tblBuyers] ([BuyerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAuctionEndorsements] CHECK CONSTRAINT [FK_tblAuctionEndorsements_tblBuyers]
GO
/****** Object:  ForeignKey [FK_tblAuctionEndorsements_tblPurchasing]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionEndorsements]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionEndorsements_tblPurchasing] FOREIGN KEY([PurchasingId])
REFERENCES [dbo].[tblPurchasing] ([PurchasingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAuctionEndorsements] CHECK CONSTRAINT [FK_tblAuctionEndorsements_tblPurchasing]
GO
/****** Object:  ForeignKey [FK_tblAuctionItemDetails_rfcProductCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails]  WITH CHECK ADD  CONSTRAINT [FK_tblAuctionItemDetails_rfcProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[rfcProductCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[tblAuctionItemDetails] CHECK CONSTRAINT [FK_tblAuctionItemDetails_rfcProductCategory]
GO
/****** Object:  ForeignKey [FK_tblAuctionItemDetails_rfcProductSubCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionItemDetails_rfcProductSubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[rfcProductSubCategory] ([SubCategoryId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAuctionItemDetails] CHECK CONSTRAINT [FK_tblAuctionItemDetails_rfcProductSubCategory]
GO
/****** Object:  ForeignKey [FK_tblAuctionItemDetails_tblProducts]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionItemDetails_tblProducts] FOREIGN KEY([Item])
REFERENCES [dbo].[tblProducts] ([SKU])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAuctionItemDetails] CHECK CONSTRAINT [FK_tblAuctionItemDetails_tblProducts]
GO
/****** Object:  ForeignKey [FK_tblAuctionItemFileUploads_tblBuyers]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemFileUploads]  WITH CHECK ADD  CONSTRAINT [FK_tblAuctionItemFileUploads_tblBuyers] FOREIGN KEY([BuyerID])
REFERENCES [dbo].[tblBuyers] ([BuyerId])
GO
ALTER TABLE [dbo].[tblAuctionItemFileUploads] CHECK CONSTRAINT [FK_tblAuctionItemFileUploads_tblBuyers]
GO
/****** Object:  ForeignKey [FK_tblAuctionItemTrail_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionItemTrail]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionItemTrail_tblVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON DELETE CASCADE
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tblAuctionItemTrail] CHECK CONSTRAINT [FK_tblAuctionItemTrail_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblAuctionParticipantComments_tblAuctionParticipants]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionParticipantComments]  WITH CHECK ADD  CONSTRAINT [FK_tblAuctionParticipantComments_tblAuctionParticipants] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[tblAuctionParticipants] ([ParticipantId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAuctionParticipantComments] CHECK CONSTRAINT [FK_tblAuctionParticipantComments_tblAuctionParticipants]
GO
/****** Object:  ForeignKey [FK_tblAuctionParticipants_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuctionParticipants]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuctionParticipants_tblVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON DELETE CASCADE
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tblAuctionParticipants] CHECK CONSTRAINT [FK_tblAuctionParticipants_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblAuthDept_tblBidItems]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblAuthDept]  WITH NOCHECK ADD  CONSTRAINT [FK_tblAuthDept_tblBidItems] FOREIGN KEY([BidRefNo])
REFERENCES [dbo].[tblBidItems] ([BidRefNo])
GO
ALTER TABLE [dbo].[tblAuthDept] CHECK CONSTRAINT [FK_tblAuthDept_tblBidItems]
GO
/****** Object:  ForeignKey [FK_tblBidItemComments_tblBidItems]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemComments]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItemComments_tblBidItems] FOREIGN KEY([BidRefNo])
REFERENCES [dbo].[tblBidItems] ([BidRefNo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidItemComments] CHECK CONSTRAINT [FK_tblBidItemComments_tblBidItems]
GO
/****** Object:  ForeignKey [FK_tblWithdrawnedItemComments_tblBidItemDetails]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetailComments]  WITH CHECK ADD  CONSTRAINT [FK_tblWithdrawnedItemComments_tblBidItemDetails] FOREIGN KEY([BidDetailNo])
REFERENCES [dbo].[tblBidItemDetails] ([BidDetailNo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidItemDetailComments] CHECK CONSTRAINT [FK_tblWithdrawnedItemComments_tblBidItemDetails]
GO
/****** Object:  ForeignKey [FK_tblWithdrawnedItemComments_tblPurchasing]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetailComments]  WITH CHECK ADD  CONSTRAINT [FK_tblWithdrawnedItemComments_tblPurchasing] FOREIGN KEY([PurchasingId])
REFERENCES [dbo].[tblPurchasing] ([PurchasingId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidItemDetailComments] CHECK CONSTRAINT [FK_tblWithdrawnedItemComments_tblPurchasing]
GO
/****** Object:  ForeignKey [FK_tblBidItemDetails_tblProducts]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItemDetails_tblProducts] FOREIGN KEY([Item])
REFERENCES [dbo].[tblProducts] ([SKU])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidItemDetails] CHECK CONSTRAINT [FK_tblBidItemDetails_tblProducts]
GO
/****** Object:  ForeignKey [FK_tblBidItemFileUploads_tblBidItems]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItemFileUploads]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItemFileUploads_tblBidItems] FOREIGN KEY([BidRefNo])
REFERENCES [dbo].[tblBidItems] ([BidRefNo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidItemFileUploads] CHECK CONSTRAINT [FK_tblBidItemFileUploads_tblBidItems]
GO
/****** Object:  ForeignKey [FK_tblBidItems_rfcCurrency]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItems_rfcCurrency] FOREIGN KEY([Currency])
REFERENCES [dbo].[rfcCurrency] ([ID])
GO
ALTER TABLE [dbo].[tblBidItems] CHECK CONSTRAINT [FK_tblBidItems_rfcCurrency]
GO
/****** Object:  ForeignKey [FK_tblBidItems_rfcForAuction]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItems_rfcForAuction] FOREIGN KEY([ForAuction])
REFERENCES [dbo].[rfcForAuction] ([ForAuctionId])
GO
ALTER TABLE [dbo].[tblBidItems] CHECK CONSTRAINT [FK_tblBidItems_rfcForAuction]
GO
/****** Object:  ForeignKey [FK_tblBidItems_rfcProductCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItems_rfcProductCategory] FOREIGN KEY([Category])
REFERENCES [dbo].[rfcProductCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[tblBidItems] CHECK CONSTRAINT [FK_tblBidItems_rfcProductCategory]
GO
/****** Object:  ForeignKey [FK_tblBidItems_rfcProductSubCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItems_rfcProductSubCategory] FOREIGN KEY([SubCategory])
REFERENCES [dbo].[rfcProductSubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[tblBidItems] CHECK CONSTRAINT [FK_tblBidItems_rfcProductSubCategory]
GO
/****** Object:  ForeignKey [FK_tblBidItems_tblBuyers]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidItems]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidItems_tblBuyers] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[tblBuyers] ([BuyerId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidItems] CHECK CONSTRAINT [FK_tblBidItems_tblBuyers]
GO
/****** Object:  ForeignKey [FK_tblBidOpeningCommittee_rfcCommittee]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidOpeningCommittee]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidOpeningCommittee_rfcCommittee] FOREIGN KEY([CommitteeId])
REFERENCES [dbo].[rfcCommittee] ([CommitteeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidOpeningCommittee] CHECK CONSTRAINT [FK_tblBidOpeningCommittee_rfcCommittee]
GO
/****** Object:  ForeignKey [FK_tblBidParticipantComments_tblVendorsInBids]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidParticipantComments]  WITH CHECK ADD  CONSTRAINT [FK_tblBidParticipantComments_tblVendorsInBids] FOREIGN KEY([VendorsInBidId])
REFERENCES [dbo].[tblVendorsInBids] ([VendorInBidsId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidParticipantComments] CHECK CONSTRAINT [FK_tblBidParticipantComments_tblVendorsInBids]
GO
/****** Object:  ForeignKey [FK_tblBidTenderComments_tblBidTenders]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenderComments]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidTenderComments_tblBidTenders] FOREIGN KEY([BidTenderNo])
REFERENCES [dbo].[tblBidTenders] ([BidTenderNo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidTenderComments] CHECK CONSTRAINT [FK_tblBidTenderComments_tblBidTenders]
GO
/****** Object:  ForeignKey [FK_tblBidTenderComments_tblUsers]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenderComments]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidTenderComments_tblUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUsers] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidTenderComments] CHECK CONSTRAINT [FK_tblBidTenderComments_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblBidTenders_tblBidItemDetails]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidTenders_tblBidItemDetails] FOREIGN KEY([BidDetailNo])
REFERENCES [dbo].[tblBidItemDetails] ([BidDetailNo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidTenders] CHECK CONSTRAINT [FK_tblBidTenders_tblBidItemDetails]
GO
/****** Object:  ForeignKey [FK_tblBidTenders_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBidTenders]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBidTenders_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblBidTenders] CHECK CONSTRAINT [FK_tblBidTenders_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblBuyers_rfcCompany]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblBuyers]  WITH NOCHECK ADD  CONSTRAINT [FK_tblBuyers_rfcCompany] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[rfcCompany] ([CompanyId])
GO
ALTER TABLE [dbo].[tblBuyers] CHECK CONSTRAINT [FK_tblBuyers_rfcCompany]
GO
/****** Object:  ForeignKey [FK_tblPresentServices_rfcGlobePlans]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblPresentServices]  WITH NOCHECK ADD  CONSTRAINT [FK_tblPresentServices_rfcGlobePlans] FOREIGN KEY([PlanID])
REFERENCES [dbo].[rfcGlobePlans] ([PlanID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblPresentServices] CHECK CONSTRAINT [FK_tblPresentServices_rfcGlobePlans]
GO
/****** Object:  ForeignKey [FK_tblPresentServices_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblPresentServices]  WITH NOCHECK ADD  CONSTRAINT [FK_tblPresentServices_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblPresentServices] CHECK CONSTRAINT [FK_tblPresentServices_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblProduct_rfcProductCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblProducts]  WITH NOCHECK ADD  CONSTRAINT [FK_tblProduct_rfcProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[rfcProductCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[tblProducts] CHECK CONSTRAINT [FK_tblProduct_rfcProductCategory]
GO
/****** Object:  ForeignKey [FK_tblProduct_rfcProductSubCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblProducts]  WITH NOCHECK ADD  CONSTRAINT [FK_tblProduct_rfcProductSubCategory] FOREIGN KEY([SubCategoryId])
REFERENCES [dbo].[rfcProductSubCategory] ([SubCategoryId])
GO
ALTER TABLE [dbo].[tblProducts] CHECK CONSTRAINT [FK_tblProduct_rfcProductSubCategory]
GO
/****** Object:  ForeignKey [FK_tblProduct_rfcProductUnitOfMeasure]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblProducts]  WITH NOCHECK ADD  CONSTRAINT [FK_tblProduct_rfcProductUnitOfMeasure] FOREIGN KEY([UnitOfMeasure])
REFERENCES [dbo].[rfcProductUnitOfMeasure] ([UnitOfMeasureID])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tblProducts] CHECK CONSTRAINT [FK_tblProduct_rfcProductUnitOfMeasure]
GO
/****** Object:  ForeignKey [FK_tblPurchasing_rfcDepartments]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblPurchasing]  WITH NOCHECK ADD  CONSTRAINT [FK_tblPurchasing_rfcDepartments] FOREIGN KEY([DeptID])
REFERENCES [dbo].[rfcDepartments] ([DeptID])
GO
ALTER TABLE [dbo].[tblPurchasing] CHECK CONSTRAINT [FK_tblPurchasing_rfcDepartments]
GO
/****** Object:  ForeignKey [FK_tblSuperior_tblBuyers]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblSupervisor]  WITH NOCHECK ADD  CONSTRAINT [FK_tblSuperior_tblBuyers] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[tblBuyers] ([BuyerId])
ON DELETE CASCADE
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tblSupervisor] CHECK CONSTRAINT [FK_tblSuperior_tblBuyers]
GO
/****** Object:  ForeignKey [FK_tblSuperior_tblPurchasing]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblSupervisor]  WITH NOCHECK ADD  CONSTRAINT [FK_tblSuperior_tblPurchasing] FOREIGN KEY([PurchasingId])
REFERENCES [dbo].[tblPurchasing] ([PurchasingId])
ON DELETE CASCADE
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[tblSupervisor] CHECK CONSTRAINT [FK_tblSuperior_tblPurchasing]
GO
/****** Object:  ForeignKey [FK_tblVendorBrands_rfcProductBrands]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorBrands]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorBrands_rfcProductBrands] FOREIGN KEY([BrandId])
REFERENCES [dbo].[rfcProductBrands] ([BrandId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorBrands] CHECK CONSTRAINT [FK_tblVendorBrands_rfcProductBrands]
GO
/****** Object:  ForeignKey [FK_tblVendorBrands_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorBrands]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorBrands_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorBrands] CHECK CONSTRAINT [FK_tblVendorBrands_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorCategoriesAndSubcategories_rfcProductCategory]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorCategoriesAndSubcategories]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorCategoriesAndSubcategories_rfcProductCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[rfcProductCategory] ([CategoryId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorCategoriesAndSubcategories] CHECK CONSTRAINT [FK_tblVendorCategoriesAndSubcategories_rfcProductCategory]
GO
/****** Object:  ForeignKey [FK_tblVendorCategoriesAndSubcategories_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorCategoriesAndSubcategories]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorCategoriesAndSubcategories_tblVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorCategoriesAndSubcategories] CHECK CONSTRAINT [FK_tblVendorCategoriesAndSubcategories_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorClassification_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorClassification]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorClassification_tblVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorClassification] CHECK CONSTRAINT [FK_tblVendorClassification_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorEquipments_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorEquipments]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorEquipments_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorEquipments] CHECK CONSTRAINT [FK_tblVendorEquipments_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorFileUploads_tblBidItems]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorFileUploads]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorFileUploads_tblBidItems] FOREIGN KEY([BidRefNo])
REFERENCES [dbo].[tblBidItems] ([BidRefNo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorFileUploads] CHECK CONSTRAINT [FK_tblVendorFileUploads_tblBidItems]
GO
/****** Object:  ForeignKey [FK_tblVendorFileUploads_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorFileUploads]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorFileUploads_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorFileUploads] CHECK CONSTRAINT [FK_tblVendorFileUploads_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorLocation_rfcLocations]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorLocation]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorLocation_rfcLocations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[rfcLocations] ([LocationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorLocation] CHECK CONSTRAINT [FK_tblVendorLocation_rfcLocations]
GO
/****** Object:  ForeignKey [FK_tblVendorLocation_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorLocation]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorLocation_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorLocation] CHECK CONSTRAINT [FK_tblVendorLocation_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorProdItems_rfcItemsCarried]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorProdItems]  WITH CHECK ADD  CONSTRAINT [FK_tblVendorProdItems_rfcItemsCarried] FOREIGN KEY([ProdItemNo])
REFERENCES [dbo].[rfcItemsCarried] ([ItemNo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorProdItems] CHECK CONSTRAINT [FK_tblVendorProdItems_rfcItemsCarried]
GO
/****** Object:  ForeignKey [FK_tblVendorProdItems_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorProdItems]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorProdItems_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorProdItems] CHECK CONSTRAINT [FK_tblVendorProdItems_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorReferences_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorReferences]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorReferences_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorReferences] CHECK CONSTRAINT [FK_tblVendorReferences_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorRelative_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorRelative]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorRelative_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorRelative] CHECK CONSTRAINT [FK_tblVendorRelative_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendors_rfcOrganizationType1]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendors]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendors_rfcOrganizationType1] FOREIGN KEY([OrgTypeID])
REFERENCES [dbo].[rfcOrganizationType] ([OrgTypeID])
GO
ALTER TABLE [dbo].[tblVendors] CHECK CONSTRAINT [FK_tblVendors_rfcOrganizationType1]
GO
/****** Object:  ForeignKey [FK_tblVendors_rfcPCABClass]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendors]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendors_rfcPCABClass] FOREIGN KEY([PCABClass])
REFERENCES [dbo].[rfcPCABClass] ([PCAB Class Id])
GO
ALTER TABLE [dbo].[tblVendors] CHECK CONSTRAINT [FK_tblVendors_rfcPCABClass]
GO
/****** Object:  ForeignKey [FK_tblVendors_rfcSupplierType]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendors]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendors_rfcSupplierType] FOREIGN KEY([Accredited])
REFERENCES [dbo].[rfcSupplierType] ([SupplierTypeId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[tblVendors] CHECK CONSTRAINT [FK_tblVendors_rfcSupplierType]
GO
/****** Object:  ForeignKey [FK_tblVendorServices_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorServices]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorServices_tblVendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorServices] CHECK CONSTRAINT [FK_tblVendorServices_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorsInAuctions_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInAuctions]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorsInAuctions_tblVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorsInAuctions] CHECK CONSTRAINT [FK_tblVendorsInAuctions_tblVendors]
GO
/****** Object:  ForeignKey [FK_tblVendorsInBids_tblBidItems]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInBids]  WITH NOCHECK ADD  CONSTRAINT [FK_tblVendorsInBids_tblBidItems] FOREIGN KEY([BidRefNo])
REFERENCES [dbo].[tblBidItems] ([BidRefNo])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorsInBids] CHECK CONSTRAINT [FK_tblVendorsInBids_tblBidItems]
GO
/****** Object:  ForeignKey [FK_tblVendorsInBids_tblVendors]    Script Date: 04/30/2015 15:38:04 ******/
ALTER TABLE [dbo].[tblVendorsInBids]  WITH CHECK ADD  CONSTRAINT [FK_tblVendorsInBids_tblVendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[tblVendors] ([VendorId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblVendorsInBids] CHECK CONSTRAINT [FK_tblVendorsInBids_tblVendors]
GO
