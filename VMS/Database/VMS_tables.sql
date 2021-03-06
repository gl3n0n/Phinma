USE [vms_transasia]
GO
/****** Object:  Table [dbo].[tblVendorTopCompetitors]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorTopCompetitors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorTopCompetitors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSuppliersDeclaration]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSuppliersDeclaration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[FileName] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorSuppliersDeclaration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSupplierReferences]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSupplierReferences](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[SupplierName] [varchar](100) NOT NULL,
	[ContactPerson] [varchar](100) NULL,
	[majaddress] [varchar](100) NULL,
	[ContactNo] [varchar](100) NULL,
	[Terms] [varchar](30) NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorSupplierReferences] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSupplierDeclarationOnSafety]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSupplierDeclarationOnSafety](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[DateOfEvaluation] [date] NULL,
	[Q1] [varchar](3) NULL,
	[Q2] [varchar](3) NULL,
	[Q3] [varchar](3) NULL,
	[Q4] [varchar](3) NULL,
	[Q5] [varchar](3) NULL,
	[Q6] [varchar](3) NULL,
	[Q7] [varchar](3) NULL,
	[Q8] [varchar](3) NULL,
	[Q9] [varchar](3) NULL,
	[Q10] [varchar](3) NULL,
	[Q11] [varchar](3) NULL,
	[Q12a] [varchar](250) NULL,
	[Q12b] [varchar](250) NULL,
	[Q12c] [varchar](250) NULL,
	[Q12d] [varchar](250) NULL,
	[ApprovedDate] [date] NULL,
	[PrintedName] [varchar](150) NULL,
	[Position] [varchar](150) NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSupplierDeclarationOnBusiness]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSupplierDeclarationOnBusiness](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[DateOfEvaluation] [date] NULL,
	[A_Rating_Q1] [tinyint] NULL,
	[A_Remarks_Q1] [varchar](50) NULL,
	[A_Rating_Q2] [tinyint] NULL,
	[A_Remarks_Q2] [varchar](50) NULL,
	[A_Rating_Q3] [tinyint] NULL,
	[A_Remarks_Q3] [varchar](50) NULL,
	[A_Rating_Q4] [tinyint] NULL,
	[A_Remarks_Q4] [varchar](50) NULL,
	[A_Rating_Q5] [tinyint] NULL,
	[A_Remarks_Q5] [varchar](50) NULL,
	[A_Rating_Q6] [tinyint] NULL,
	[A_Remarks_Q6] [varchar](50) NULL,
	[A_Rating_Q7] [tinyint] NULL,
	[A_Remarks_Q7] [varchar](50) NULL,
	[A_Rating_Q8] [tinyint] NULL,
	[A_Remarks_Q8] [varchar](50) NULL,
	[B_Rating_Q1] [tinyint] NULL,
	[B_Remarks_Q1] [varchar](50) NULL,
	[B_Rating_Q2] [tinyint] NULL,
	[B_Remarks_Q2] [varchar](50) NULL,
	[B_Rating_Q3] [tinyint] NULL,
	[B_Remarks_Q3] [varchar](50) NULL,
	[B_Rating_Q4] [tinyint] NULL,
	[B_Remarks_Q4] [varchar](50) NULL,
	[B_Rating_Q5] [tinyint] NULL,
	[B_Remarks_Q5] [varchar](50) NULL,
	[B_Rating_Q6] [tinyint] NULL,
	[B_Remarks_Q6] [varchar](50) NULL,
	[B_Rating_Q7] [tinyint] NULL,
	[B_Remarks_Q7] [varchar](50) NULL,
	[B_Rating_Q8] [tinyint] NULL,
	[B_Remarks_Q8] [varchar](50) NULL,
	[B_Rating_Q9] [tinyint] NULL,
	[B_Remarks_Q9] [varchar](50) NULL,
	[C_Rating_Q1] [tinyint] NULL,
	[C_Rating_Q2] [tinyint] NULL,
	[C_Rating_Q3] [tinyint] NULL,
	[C_Remarks_Q1] [varchar](50) NULL,
	[C_Remarks_Q2] [varchar](50) NULL,
	[C_Remarks_Q3] [varchar](50) NULL,
	[D_Rating_Q1] [tinyint] NULL,
	[D_Rating_Q2] [tinyint] NULL,
	[D_Rating_Q3] [tinyint] NULL,
	[D_Rating_Q4] [tinyint] NULL,
	[D_Remarks_Q1] [varchar](50) NULL,
	[D_Remarks_Q2] [varchar](50) NULL,
	[D_Remarks_Q3] [varchar](50) NULL,
	[D_Remarks_Q4] [varchar](50) NULL,
	[E_Rating_Q1] [tinyint] NULL,
	[E_Remarks_Q1] [varchar](50) NULL,
	[E_Rating_Q2] [tinyint] NULL,
	[E_Remarks_Q2] [varchar](50) NULL,
	[F_Rating_Q1] [tinyint] NULL,
	[F_Remarks_Q1] [varchar](50) NULL,
	[F_Rating_Q2] [tinyint] NULL,
	[F_Remarks_Q2] [varchar](50) NULL,
	[G_Rating_Q1] [tinyint] NULL,
	[G_Remarks_Q1] [varchar](50) NULL,
	[G_Rating_Q2] [tinyint] NULL,
	[G_Remarks_Q2] [varchar](50) NULL,
	[G_Rating_Q3] [tinyint] NULL,
	[G_Remarks_Q3] [varchar](50) NULL,
	[ApprovedDate] [date] NULL,
	[PrintedName] [varchar](150) NULL,
	[Position] [varchar](150) NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorSubsidiaries]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorSubsidiaries](
	[SubsidiaryId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[subCompanyName] [varchar](100) NOT NULL,
	[subAddr] [varchar](100) NULL,
	[SubContact] [varchar](100) NULL,
	[SubFax] [varchar](100) NULL,
	[SubEmailAdd] [varchar](100) NULL,
	[subEquity] [float] NULL,
	[subOwned] [varchar](30) NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorSubsidiaries] PRIMARY KEY CLUSTERED 
(
	[SubsidiaryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorShareHolders]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorShareHolders](
	[ShareHolderId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[shShareHolderName] [varchar](100) NULL,
	[shNationality] [varchar](60) NULL,
	[shSubsribedCapital] [float] NULL,
	[DateCreated] [datetime] NOT NULL,
	[shAuthorizedCapital] [float] NULL,
	[shPaidupCapital] [float] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorRegulatoryRequirements]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorRegulatoryRequirements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[RegulatoryRequirement] [varchar](100) NOT NULL,
	[DateRegistered] [date] NULL,
	[PermitNo] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorRegulatoryRequirements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorProductsAndServices]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorProductsAndServices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[CategoryId] [nvarchar](7) NULL,
	[SubCategoryId] [int] NULL,
	[BrandId] [int] NULL,
	[NoYears] [int] NULL,
	[MajorClients] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[BrandTxt] [varchar](100) NULL,
	[Exclusive] [varchar](5) NULL,
 CONSTRAINT [PK_tblVendorProductsAndServices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorNatureOfBusiness]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorNatureOfBusiness](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[NatureOfBusinessId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorNatureOfBusiness] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorLegalCompliance]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorLegalCompliance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[FileName] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[VendorId] [int] NULL,
 CONSTRAINT [PK_tblVendorLegalCompliance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorInsuranceInformation]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorInsuranceInformation](
	[InsuranceId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[iCompanyName] [varchar](100) NULL,
	[iAddress] [varchar](60) NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorInformationFacilities]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorInformationFacilities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Facilities] [varchar](100) NULL,
	[DateCreated] [datetime] NULL,
	[Owned] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorInformationAssets]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorInformationAssets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Assets] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
	[Owned] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorInformation]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorInformation](
	[VendorId] [int] NOT NULL,
	[VendorCode] [varchar](100) NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[regBldgUnit] [varchar](100) NULL,
	[regBldgBldg] [varchar](100) NULL,
	[regBldgLotNo] [varchar](100) NULL,
	[regBldgBlock] [varchar](100) NULL,
	[regBldgPhase] [varchar](100) NULL,
	[regBldgHouseNo] [varchar](100) NULL,
	[regBldgStreet] [varchar](100) NULL,
	[regBldgSubd] [varchar](100) NULL,
	[regBrgy] [varchar](100) NULL,
	[regCity] [varchar](100) NULL,
	[regProvince] [varchar](100) NULL,
	[regCountry] [varchar](100) NULL,
	[regPostal] [varchar](100) NULL,
	[regArea] [varchar](100) NULL,
	[regOwned] [varchar](30) NULL,
	[conBidName] [varchar](100) NULL,
	[conBidPosition] [varchar](60) NULL,
	[conBidEmail] [varchar](100) NULL,
	[conBidMobile] [varchar](40) NULL,
	[conBidTelNo] [varchar](40) NULL,
	[conBidFaxNo] [varchar](40) NULL,
	[conLegName] [varchar](100) NULL,
	[conLegPosition] [varchar](60) NULL,
	[conLegEmail] [varchar](100) NULL,
	[conLegMobile] [varchar](40) NULL,
	[conLegTelNo] [varchar](40) NULL,
	[conLegFaxNo] [varchar](40) NULL,
	[conBonName] [varchar](100) NULL,
	[conBonPosition] [varchar](60) NULL,
	[conBonEmail] [varchar](100) NULL,
	[conBonMobile] [varchar](40) NULL,
	[conBonTelNo] [varchar](40) NULL,
	[conBonFaxNo] [varchar](40) NULL,
	[conTecName] [varchar](100) NULL,
	[conTecPosition] [varchar](60) NULL,
	[conTecEmail] [varchar](100) NULL,
	[conTecMobile] [varchar](40) NULL,
	[conTecTelNo] [varchar](40) NULL,
	[conTecFaxNo] [varchar](40) NULL,
	[conSalName] [varchar](100) NULL,
	[conSalPosition] [varchar](60) NULL,
	[conSalEmail] [varchar](100) NULL,
	[conSalMobile] [varchar](40) NULL,
	[conSalTelNo] [varchar](40) NULL,
	[conSalFaxNo] [varchar](40) NULL,
	[parentCompanyName] [varchar](100) NULL,
	[parentCompanyAddr] [varchar](250) NULL,
	[repOfcCompanyName] [varchar](100) NULL,
	[repOfcCompanyAddr] [varchar](260) NULL,
	[repOfcMobile] [varchar](140) NULL,
	[repOfcTelNo] [varchar](140) NULL,
	[repOfcFaxNo] [varchar](140) NULL,
	[manResOffice] [int] NULL,
	[manResEngr] [int] NULL,
	[manResSales] [int] NULL,
	[manResWare] [int] NULL,
	[manResProd] [int] NULL,
	[manResOthers] [int] NULL,
	[manResourceTotal] [varchar](150) NULL,
	[benefitsPagibig] [int] NULL,
	[benefitsPagibigFileName] [text] NULL,
	[benefitsPHIC] [int] NULL,
	[benefitsPHICFileName] [text] NULL,
	[benefitsSSS] [int] NULL,
	[benefitsSSSFileName] [text] NULL,
	[benefits13th] [int] NULL,
	[benefits13thFileName] [text] NULL,
	[benefitsOtherMed] [int] NULL,
	[benefitsOtherMedFName] [text] NULL,
	[benefitsOthers] [varchar](100) NULL,
	[benefitsOthersFileName] [text] NULL,
	[assetsMachineries] [varchar](100) NULL,
	[assetsMachineriesFileName] [text] NULL,
	[assetsCompanyProfile] [varchar](100) NULL,
	[assetsCompanyProfileFileName] [text] NULL,
	[assetsOthers] [varchar](100) NULL,
	[assetsOthersFileName] [text] NULL,
	[legalStrucOrgType] [varchar](100) NULL,
	[legalStrucDateReg] [date] NULL,
	[legalStrucRegNo] [varchar](100) NULL,
	[legalStrucSECAttachement] [text] NULL,
	[legalStrucDateStartedOp] [date] NULL,
	[legalStrucPrevBusName] [varchar](100) NULL,
	[legalStrucDateChanged] [date] NULL,
	[legalStrucTIN] [varchar](40) NULL,
	[busPermitDateReg] [date] NULL,
	[busPermitNo] [varchar](100) NULL,
	[busPermitAttachement] [text] NULL,
	[busPermitAttachementOthers] [text] NULL,
	[birRegTIN] [varchar](100) NULL,
	[birRegTINVAT] [varchar](20) NULL,
	[birRegAttachement] [text] NULL,
	[corpAuthorizedCapital] [float] NULL,
	[corpSubscribedCapital] [float] NULL,
	[corpPaidUpCapital] [float] NULL,
	[corpPerValue] [float] NULL,
	[corpAsOfDate] [date] NULL,
	[SecurityArangement] [text] NULL,
	[step8FullName] [varchar](100) NULL,
	[step8OfficialTitle] [varchar](100) NULL,
	[step8OfCompanyName] [varchar](100) NULL,
	[step8bindCompanyName] [varchar](100) NULL,
	[repOfcEmail] [varchar](100) NULL,
	[suppDeclarationQ1] [varchar](100) NULL,
	[suppDeclarationQ2] [varchar](100) NULL,
	[suppDeclarationQ3] [varchar](100) NULL,
	[suppDeclarationQ4] [varchar](100) NULL,
	[suppDeclarationQ5] [varchar](100) NULL,
	[suppDeclarationQ6] [varchar](100) NULL,
	[suppDeclarationQ7] [varchar](100) NULL,
	[suppDeclarationQ8] [varchar](100) NULL,
	[suppDeclarationQ9] [varchar](100) NULL,
	[suppDeclarationQ10] [varchar](100) NULL,
	[suppDeclarationQ11] [varchar](100) NULL,
	[prodServ_DescLineOfBusiness] [text] NULL,
	[prodServ_DAC_Attachment] [text] NULL,
	[facltyLandTxt] [varchar](150) NULL,
	[facltyLandOwned] [varchar](50) NULL,
	[facltyBldgTxt] [varchar](150) NULL,
	[facltyBldgOwned] [varchar](50) NULL,
	[facltyLocation] [varchar](50) NULL,
	[facltyPremissesAs] [varchar](50) NULL,
	[insurInfoEmplyrLia_Limit] [varchar](150) NULL,
	[insurInfoEmplyrLia_InsuCo] [varchar](150) NULL,
	[insurInfoPropInsu_Limit] [varchar](150) NULL,
	[insurInfoPropInsu_InsuCo] [varchar](150) NULL,
	[insurInfoPartyLia_Limit] [varchar](150) NULL,
	[insurInfoPartyLia_InsuCo] [varchar](150) NULL,
	[insurInfoOthers_Limit] [varchar](150) NULL,
	[insurInfoOthers_InsuCo] [varchar](150) NULL,
	[othersQltyMangmtSys] [varchar](10) NULL,
	[regOwnedAttachment] [text] NULL,
	[legalStrucCorpAttch_geninfo] [varchar](150) NULL,
	[legalStrucCorpAttch_IdentityAuthorizdSigna] [varchar](150) NULL,
	[legalStrucCorpAttch_Identitytaxcert] [varchar](150) NULL,
	[legalStrucCorpAttch_BoardAuthorizdSigna] [varchar](150) NULL,
	[legalStrucSoleAttch_DTIReg] [varchar](150) NULL,
	[legalStrucSoleAttch_OwnersId1] [varchar](150) NULL,
	[legalStrucSoleAttch_OwnersId2] [varchar](150) NULL,
	[legalStrucSoleAttch_CTC] [varchar](150) NULL,
	[finanInfo_Type] [varchar](50) NULL,
	[othersQltyMangmtSys_File] [text] NULL,
	[legalStrucCorpAttch_geninfo2] [text] NULL,
	[legalStrucCorpAttch_geninfo3] [text] NULL,
	[legalStrucCorpAttch_geninfo4] [text] NULL,
	[legalStrucCorpAttch_geninfo4Text] [varchar](150) NULL,
	[othersQltyMangmtSys_File2] [text] NULL,
	[CertAndWarranty_AttachedSigned] [text] NULL,
 CONSTRAINT [PK_tblVendors] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorIncidentReport]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorIncidentReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[irDetails] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[VendorId] [int] NULL,
 CONSTRAINT [PK_tblVendorIncidentReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorFinancialInformation]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorFinancialInformation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Year] [varchar](10) NOT NULL,
	[YearInfo] [varchar](10) NULL,
	[Revenue] [float] NULL,
	[NetIncome] [float] NULL,
	[CurrentAssets] [float] NULL,
	[TotalAssets] [float] NULL,
	[CurrentLiabilities] [float] NULL,
	[TotalLiabilities] [float] NULL,
	[NetEquity] [float] NULL,
	[Inventories] [float] NULL,
	[FileName] [text] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorFinancialInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorCustomerReferences]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorCustomerReferences](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[custrefCustomerName] [varchar](100) NOT NULL,
	[custrefContactPerson] [varchar](100) NULL,
	[custrefAddress] [varchar](100) NULL,
	[custrefContactNo] [varchar](100) NULL,
	[custrefTerms] [varchar](30) NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorCourtCases]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorCourtCases](
	[CourtCaseId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[TypeOfCase] [varchar](150) NULL,
	[DateRegistered] [date] NULL,
	[Status] [varchar](150) NULL,
	[DateCreated] [datetime] NOT NULL,
	[Attachment] [varchar](85) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorConflictOfInterest]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorConflictOfInterest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[YesNo] [int] NOT NULL,
	[NatureOfBusinessId] [int] NULL,
	[CompetitorName] [varchar](100) NULL,
	[NoYears] [int] NULL,
	[DateCreated] [datetime] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Position] [varchar](100) NULL,
	[GTEmployee] [varchar](100) NULL,
	[GTEmployeePosition] [varchar](100) NULL,
	[Relationship] [varchar](100) NULL,
 CONSTRAINT [PK_tblVendorConflictOfInterest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorCertifications]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorCertifications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Certification] [varchar](100) NOT NULL,
	[FileName] [datetime] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorCertifications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBranches]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBranches](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[brAddr] [varchar](100) NULL,
	[brUsedAs] [varchar](60) NULL,
	[brEmplNo] [varchar](100) NULL,
	[brArea] [varchar](40) NULL,
	[brOwned] [varchar](30) NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorBranches] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBoardMembers]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBoardMembers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[bmMemberOfTheBoard] [varchar](100) NOT NULL,
	[bmNationality] [varchar](100) NULL,
	[bmPostion] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBankInformation]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBankInformation](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[biBankName] [varchar](100) NULL,
	[biBranch] [varchar](100) NULL,
	[biAccountType] [varchar](60) NULL,
	[biContact] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[biAttachment] [text] NULL,
	[biBankAddress] [varchar](100) NULL,
	[biAccountNo] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorBackOnKeyPersonnel]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorBackOnKeyPersonnel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Position] [varchar](100) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DegreeEarned] [varchar](100) NULL,
	[EducInstitution] [varchar](100) NULL,
	[YearGraduated] [varchar](100) NULL,
	[Nationality] [varchar](100) NULL,
	[Age] [varchar](100) NULL,
	[PastWorkExp] [varchar](100) NULL,
	[DateCreated] [datetime] NOT NULL,
	[CurriculumVitae] [varchar](150) NULL,
 CONSTRAINT [PK_tblVendorBackOnKeyPersonnel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmTech]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyVmTech](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vmTechUserId] [int] NOT NULL,
	[newVendor] [tinyint] NULL,
	[validThruProdInfo] [tinyint] NULL,
	[validThruProdPresent] [tinyint] NULL,
	[validThruSiteVisit] [tinyint] NULL,
	[historicalPOIssuedMo] [varchar](50) NULL,
	[historicalPOIssuedPO] [varchar](50) NULL,
	[historicalLatestPerfEval] [int] NULL,
	[evalProdServQ1Score] [int] NULL,
	[evalProdServQ1Remarks] [varchar](100) NULL,
	[evalProdServQ2Score] [int] NULL,
	[evalProdServQ2Remarks] [varchar](100) NULL,
	[evalCompetenceQ1Score] [int] NULL,
	[evalCompetenceQ1Remarks] [varchar](100) NULL,
	[evalCompetenceQ2Score] [int] NULL,
	[evalCompetenceQ2Remarks] [varchar](100) NULL,
	[evalTrackRecQ1Score] [int] NULL,
	[evalTrackRecQ1Remarks] [varchar](100) NULL,
	[evalTrackRecQ2Score] [int] NULL,
	[evalTrackRecQ2Remarks] [varchar](100) NULL,
	[evalSuppSysQ1Score] [int] NULL,
	[evalSuppSysQ1Remarks] [varchar](100) NULL,
	[evalSuppSysQ2Score] [int] NULL,
	[evalSuppSysQ2Remarks] [varchar](100) NULL,
	[evalSuppSysQ3Score] [int] NULL,
	[evalSuppSysQ3Remarks] [varchar](100) NULL,
	[evalSuppSysQ4Score] [int] NULL,
	[evalSuppSysQ4Remarks] [varchar](100) NULL,
	[TotalScore] [int] NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]
SET ANSI_PADDING OFF
ALTER TABLE [dbo].[tblVendorApprovalbyVmTech] ADD [FileAttachement] [varchar](100) NULL
ALTER TABLE [dbo].[tblVendorApprovalbyVmTech] ADD  CONSTRAINT [PK_tblVendorApprovalbyVmTech] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmReco]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyVmReco](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL,
	[FileAttachement] [varchar](100) NULL,
	[Recommendation] [int] NULL,
	[AccreDuration] [varchar](50) NULL,
	[Others] [text] NULL,
	[OverallEvalRemarks] [text] NULL,
 CONSTRAINT [PK_tblVendorApprovalbyVmReco] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyVmIssue]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyVmIssue](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vmIssueUserId] [int] NOT NULL,
	[withIncidentReport] [tinyint] NULL,
	[ISRInvolvement] [tinyint] NULL,
	[DateCreated] [datetime] NULL,
	[FileAttachement] [varchar](100) NULL,
 CONSTRAINT [PK_tblVendorApprovalbyVmIssue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyPVMDHead]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyPVMDHead](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL,
	[Recommendation] [tinyint] NULL,
	[FileAttachement] [varchar](100) NULL,
 CONSTRAINT [PK_tblVendorApprovalbyPVMDHead] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyLegal]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyLegal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[legalUserId] [int] NOT NULL,
	[legalApproved] [tinyint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[FileAttachement] [varchar](100) NULL,
	[legalRecollection] [tinyint] NULL,
	[Recommendation] [tinyint] NULL,
 CONSTRAINT [PK_tblVendorApprovalbyLegal] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyFAALogistics]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyFAALogistics](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL,
	[Recommendation] [tinyint] NULL,
	[FileAttachement] [varchar](100) NULL,
 CONSTRAINT [PK_tblVendorApprovalbyFAALogistics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApprovalbyFAALFinance]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApprovalbyFAALFinance](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[vendorApproved] [tinyint] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_tblVendorApprovalbyFAALFinance] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendorApplicants]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendorApplicants](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](100) NOT NULL,
	[EmailAdd] [varchar](100) NULL,
	[LOIFileName] [text] NULL,
	[DateCreated] [datetime] NOT NULL,
	[IsAuthenticated] [smallint] NOT NULL,
	[ApprovedBy] [int] NULL,
	[ApprovedDt] [datetime] NULL,
	[RejectedBy] [int] NULL,
	[RejectedDt] [datetime] NULL,
	[Status] [tinyint] NULL,
	[CategoryId] [nvarchar](7) NULL,
	[EndorsedBy] [varchar](150) NULL,
	[DateStarted] [datetime] NULL,
	[FinancialStatement] [varchar](5) NULL,
	[EndorsedTo] [int] NULL,
 CONSTRAINT [PK_tblVendorApplicants] PRIMARY KEY CLUSTERED 
(
	[CompanyName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVendorApplicantCategory]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVendorApplicantCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorApplicantId] [int] NOT NULL,
	[CategoryId] [nvarchar](7) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblVendorApplicantCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVendor]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblVendor](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[approvedbyProc] [int] NULL,
	[approvedbyProcDate] [datetime] NULL,
	[approvedbyDnb] [int] NULL,
	[approvedbyDnbDate] [datetime] NULL,
	[approvedbyVMOfficer] [int] NULL,
	[approvedbyVMOfficerDate] [datetime] NULL,
	[approvedbyLegal] [int] NULL,
	[approvedbyLegalDate] [datetime] NULL,
	[approvedbyVMTech] [int] NULL,
	[approvedbyVMTechDate] [datetime] NULL,
	[approvedbyVMIssue] [int] NULL,
	[approvedbyVMIssueDate] [datetime] NULL,
	[approvedbyVMReco] [tinyint] NULL,
	[approvedbyVMRecoDate] [datetime] NULL,
	[approvedbyFAALogistics] [int] NULL,
	[approvedbyFAALogisticsDate] [datetime] NULL,
	[approvedbyFAAFinance] [int] NULL,
	[approvedbyFAAFinanceDate] [datetime] NULL,
	[Status] [int] NULL,
	[DateCreated] [datetime] NULL,
	[AuthenticationTicket] [varchar](100) NULL,
	[IsAuthenticated] [tinyint] NULL,
	[clarifiedtoVMRecoBy] [tinyint] NULL,
	[clarifiedtoVMRecoDate] [datetime] NULL,
	[paymentProof] [varchar](150) NULL,
	[clarifiedtoPvmdBy] [int] NULL,
	[clarifiedtoPvmdDate] [datetime] NULL,
	[DateSubmittedToDnb] [datetime] NULL,
	[DateAuthenticatedByDnb] [datetime] NULL,
	[NotificationSent] [datetime] NULL,
	[SendToSAP_Status] [tinyint] NULL,
	[AccGroup] [varchar](10) NULL,
	[VendorAlias] [varchar](10) NULL,
	[VendorCode] [varchar](10) NULL,
	[PurchasingOrg] [varchar](10) NULL,
	[CountryCode] [varchar](10) NULL,
	[Currency] [varchar](10) NULL,
	[EndorsedTo] [int] NULL,
	[TradeNonTrade] [char](1) NULL,
	[renewaldate] [datetime] NULL,
 CONSTRAINT [PK_tblVendorApprovalStatus] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserTypes]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserTypes](
	[UserId] [int] NOT NULL,
	[UserType] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblUserTypes] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[UserType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsersForVendors]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsersForVendors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblUsersForVendors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserPassword] [nvarchar](400) NOT NULL,
	[RefNo] [nvarchar](20) NULL,
	[Status] [smallint] NOT NULL,
	[TempPassword] [nvarchar](400) NULL,
	[IsAuthenticated] [smallint] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[SessionId] [nvarchar](50) NULL,
	[LoginStatus] [bit] NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[FirstName] [varchar](100) NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[EmailAdd] [varchar](100) NULL,
	[CompanyName] [varchar](100) NULL,
	[AuthenticationTicket] [varchar](100) NULL,
 CONSTRAINT [PK_tblLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserProcurementType]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserProcurementType](
	[ProcurementId] [int] NULL,
	[ProcurementType] [int] NULL,
	[DateCreated] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProcurementProductCategory]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProcurementProductCategory](
	[ProcurementType] [int] NULL,
	[CategoryId] [nvarchar](7) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDnbReport]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[dnbUserId] [int] NOT NULL,
	[dnbDuns] [varchar](50) NOT NULL,
	[dnbScore] [int] NULL,
	[dnbFinCapScore] [int] NOT NULL,
	[dnbFinCapScore_Remarks] [text] NULL,
	[dnbLegalConfScore] [int] NOT NULL,
	[dnbLegalConfScore_Remarks] [text] NULL,
	[dnbTechCompScore] [int] NOT NULL,
	[dnbTechCompScore_Remarks] [text] NULL,
	[dnbCurrentRevenue] [float] NOT NULL,
	[dnbMaxExposureLimit] [float] NULL,
	[dnbSupplierInfoReport] [varchar](250) NULL,
	[dnbOtherDocuments] [varchar](250) NULL,
	[DateCreated] [datetime] NOT NULL,
	[vmoNew_Vendor] [tinyint] NULL,
	[vmoOverallScore] [float] NULL,
	[vmoIssue_risk_to_Globe] [tinyint] NULL,
	[vmoConflict_of_Interest] [tinyint] NULL,
	[vmoRegistered_Court_Case] [varchar](150) NULL,
	[vmoRegistered_Court_Case_Attach] [text] NULL,
	[vmoWith_Type_Approved_Products] [tinyint] NULL,
	[vmoWith_Approved_Proof_of_Concept] [tinyint] NULL,
	[vmoGTPerf_Eval] [int] NULL,
	[vmoNo_POs] [int] NULL,
	[vmoSpend] [int] NULL,
	[vmoWith_Existing_Frame_Arg] [tinyint] NULL,
	[vmoIssues_bond_claims] [tinyint] NULL,
	[vmoIssues_ISR_involvement] [tinyint] NULL,
	[vmoIssues_Loss_Incidents] [tinyint] NULL,
	[vmoIssues_Others] [tinyint] NULL,
	[vmoIssues_Remarks] [varchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDnbRating]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbRating](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[dnbUserId] [int] NULL,
	[dnbDuns] [varchar](50) NULL,
	[dnbRating] [varchar](50) NULL,
	[dnbCompRating] [int] NULL,
	[condition] [varchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[SIRAttachment] [varchar](100) NULL,
 CONSTRAINT [PK_tblDnb] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDnbLegalReport]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbLegalReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeOfCase] [varchar](100) NOT NULL,
	[DateFiled] [datetime] NULL,
	[Attachment] [varchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[VendorId] [int] NOT NULL,
 CONSTRAINT [PK_tblDnbLegalReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDnbFinancialReport]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDnbFinancialReport](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[Year1] [datetime] NULL,
	[yr1Revenue] [float] NULL,
	[yr1NetIncome] [float] NULL,
	[yr1NetEquity] [float] NULL,
	[Year2] [datetime] NULL,
	[yr2Revenue] [float] NULL,
	[yr2NetIncome] [float] NULL,
	[yr2NetEquity] [float] NULL,
	[Year3] [datetime] NULL,
	[yr3Revenue] [float] NULL,
	[yr3NetIncome] [float] NULL,
	[yr3NetEquity] [float] NULL,
	[DateCreated] [datetime] NOT NULL,
	[maxExpLimit] [varchar](100) NULL,
	[creditExpLimit] [varchar](100) NULL,
	[yr3Ratio] [float] NULL,
	[yr2Ratio] [float] NULL,
	[yr1Ratio] [float] NULL,
 CONSTRAINT [PK_tblDnbFinancialReport] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCommentsProcurement]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCommentsProcurement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorApplicantId] [int] NULL,
	[UserId] [int] NOT NULL,
	[Comment] [text] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblCommentsProcurement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCommentsDnbClarify]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCommentsDnbClarify](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [text] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblCommentsDnbClarify] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblComments]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblComments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Comment] [text] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_tblComments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[scheduleLog]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[scheduleLog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[status] [tinyint] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rfcUserTypes]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rfcUserTypes](
	[UserType] [int] NOT NULL,
	[UserTypeDesc] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_rfcUserTypes] PRIMARY KEY CLUSTERED 
(
	[UserType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rfcProductSubCategory]    Script Date: 04/30/2015 15:42:21 ******/
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
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_rfcProductSubCategory] PRIMARY KEY CLUSTERED 
(
	[SubCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductCategory]    Script Date: 04/30/2015 15:42:21 ******/
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
	[DateCreated] [datetime] NOT NULL,
	[NatureOfBusinessId] [int] NULL,
	[Visible] [tinyint] NULL,
 CONSTRAINT [PK_rfcProductCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProductBrands]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProductBrands](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [char](10) NULL,
	[SubCategoryId] [int] NOT NULL,
 CONSTRAINT [PK_rfcProductBrands] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcProcurementType]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcProcurementType](
	[ProcurementType] [int] NOT NULL,
	[ProcurementTypeDesc] [varchar](50) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_rfcProcurementType] PRIMARY KEY CLUSTERED 
(
	[ProcurementType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[rfcNatureOfBusiness]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[rfcNatureOfBusiness](
	[NatureOfBusinessId] [int] IDENTITY(1,1) NOT NULL,
	[NatureOfBusinessName] [varchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[Visible] [tinyint] NULL,
 CONSTRAINT [PK_rfcNatureOfBusiness] PRIMARY KEY CLUSTERED 
(
	[NatureOfBusinessId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[APVGR]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APVGR](
	[GROUPID] [char](6) NOT NULL,
	[AUDTDATE] [decimal](9, 0) NOT NULL,
	[AUDTTIME] [decimal](9, 0) NOT NULL,
	[AUDTUSER] [char](8) NOT NULL,
	[AUDTORG] [char](6) NOT NULL,
	[DESCRIPTN] [char](60) NOT NULL,
	[ACTIVESW] [smallint] NOT NULL,
	[INACTIVEDT] [decimal](9, 0) NOT NULL,
	[LSTMNTDATE] [decimal](9, 0) NOT NULL,
	[ACCTSETID] [char](6) NOT NULL,
	[CURNCODE] [char](3) NOT NULL,
	[RATETYPEID] [char](2) NOT NULL,
	[BANKID] [char](8) NOT NULL,
	[PRTSEPCHKS] [smallint] NOT NULL,
	[DISTSETID] [char](6) NOT NULL,
	[DISTCODE] [char](6) NOT NULL,
	[GLACCTID] [char](45) NOT NULL,
	[TERMCODE] [char](6) NOT NULL,
	[DUPLINVC] [smallint] NOT NULL,
	[DUPLAMT] [smallint] NOT NULL,
	[DUPLDATE] [smallint] NOT NULL,
	[TAXGRP] [char](12) NOT NULL,
	[TAXCLASS1] [smallint] NOT NULL,
	[TAXCLASS2] [smallint] NOT NULL,
	[TAXCLASS3] [smallint] NOT NULL,
	[TAXCLASS4] [smallint] NOT NULL,
	[TAXCLASS5] [smallint] NOT NULL,
	[TAXRPTSW] [smallint] NOT NULL,
	[SUBJWTHHSW] [smallint] NOT NULL,
	[CLASSID] [char](6) NOT NULL,
	[PAYMCODE] [char](12) NOT NULL,
	[SWDISTBY] [smallint] NOT NULL,
	[SWTXINC1] [smallint] NOT NULL,
	[SWTXINC2] [smallint] NOT NULL,
	[SWTXINC3] [smallint] NOT NULL,
	[SWTXINC4] [smallint] NOT NULL,
	[SWTXINC5] [smallint] NOT NULL,
	[VALUES] [int] NOT NULL,
 CONSTRAINT [APVGR_KEY_0] PRIMARY KEY CLUSTERED 
(
	[GROUPID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[APVEN]    Script Date: 04/30/2015 15:42:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[APVEN](
	[VENDORID] [char](12) NOT NULL,
	[VENDNAME] [text] NULL,
	[TEXTSTRE1] [text] NULL,
	[TEXTSTRE2] [text] NULL,
	[NAMECITY] [text] NULL,
	[CODESTTE] [text] NULL,
	[CODEPSTL] [text] NULL,
	[CODECTRY] [text] NULL,
	[TEXTPHON1] [text] NULL,
	[EMAIL2] [text] NULL,
	[WEBSITE] [text] NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [APVEN_KEY_0] PRIMARY KEY CLUSTERED 
(
	[VENDORID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF__rfcNature__DateC__5FB337D6]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[rfcNatureOfBusiness] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_rfcProcurementType_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[rfcProcurementType] ADD  CONSTRAINT [DF_rfcProcurementType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__rfcProduc__DateC__68487DD7]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[rfcProductCategory] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__rfcProduc__DateC__6B24EA82]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[rfcProductSubCategory] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__rfcUserTy__DateC__5165187F]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[rfcUserTypes] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_scheduleLog_date]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[scheduleLog] ADD  CONSTRAINT [DF_scheduleLog_date]  DEFAULT (getdate()) FOR [date]
GO
/****** Object:  Default [DF__tblDnbFin__DateC__59063A47]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblDnbFinancialReport] ADD  CONSTRAINT [DF__tblDnbFin__DateC__59063A47]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblDnbLegalReport] ADD  CONSTRAINT [DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Dnb_Report_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblDnbReport] ADD  CONSTRAINT [DF_Dnb_Report_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblUserProcurementType_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUserProcurementType] ADD  CONSTRAINT [DF_tblUserProcurementType_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblUsers_Status]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_Status]  DEFAULT (1) FOR [Status]
GO
/****** Object:  Default [DF_tblUsers_IsAuthenticated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_IsAuthenticated]  DEFAULT (0) FOR [IsAuthenticated]
GO
/****** Object:  Default [DF__tblUsers__DateCr__4D94879B]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblUsers_LoginStatus]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF_tblUsers_LoginStatus]  DEFAULT (0) FOR [LoginStatus]
GO
/****** Object:  Default [DF__tblUsersF__DateC__55F4C372]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUsersForVendors] ADD  CONSTRAINT [DF__tblUsersF__DateC__55F4C372]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblUsers_UserType]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUserTypes] ADD  CONSTRAINT [DF_tblUsers_UserType]  DEFAULT (2) FOR [UserType]
GO
/****** Object:  Default [DF__tblUserTy__DateC__5535A963]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblUserTypes] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorApprovalStatus_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendor] ADD  CONSTRAINT [DF_tblVendorApprovalStatus_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__345EC57D]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApplicantCategory] ADD  CONSTRAINT [DF__tblVendor__DateC__345EC57D]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__0C85DE4D]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApplicants] ADD  CONSTRAINT [DF__tblVendor__DateC__0C85DE4D]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__IsAut__0D7A0286]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApplicants] ADD  CONSTRAINT [DF__tblVendor__IsAut__0D7A0286]  DEFAULT ((0)) FOR [IsAuthenticated]
GO
/****** Object:  Default [DF_tblVendorApprovalbyFAALFinance_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApprovalbyFAALFinance] ADD  CONSTRAINT [DF_tblVendorApprovalbyFAALFinance_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorApprovalbyFAALogistics_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApprovalbyFAALogistics] ADD  CONSTRAINT [DF_tblVendorApprovalbyFAALogistics_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorApprovalbyPVMDHead_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApprovalbyPVMDHead] ADD  CONSTRAINT [DF_tblVendorApprovalbyPVMDHead_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorApprovalbyVmIssue_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApprovalbyVmIssue] ADD  CONSTRAINT [DF_tblVendorApprovalbyVmIssue_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorApprovalbyVmReco_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApprovalbyVmReco] ADD  CONSTRAINT [DF_tblVendorApprovalbyVmReco_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorApprovalbyVmTech_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorApprovalbyVmTech] ADD  CONSTRAINT [DF_tblVendorApprovalbyVmTech_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__75A278F5]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorBackOnKeyPersonnel] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__59FA5E80]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorBranches] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__01142BA1]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorCertifications] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__09A971A2]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorConflictOfInterest] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorCourtCases_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorCourtCases] ADD  CONSTRAINT [DF_tblVendorCourtCases_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__7A3223E8]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorCustomerReferences] ADD  CONSTRAINT [DF__tblVendor__DateC__7A3223E8]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__1B9317B3]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorFinancialInformation] ADD  CONSTRAINT [DF__tblVendor__DateC__1B9317B3]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__32AB8735]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorIncidentReport] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorInformationAssets_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorInformationAssets] ADD  CONSTRAINT [DF_tblVendorInformationAssets_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorInformationFacilities_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorInformationFacilities] ADD  CONSTRAINT [DF_tblVendorInformationFacilities_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__06CD04F7]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorLegalCompliance] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__628FA481]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorNatureOfBusiness] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__6FE99F9F]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorProductsAndServices] ADD  CONSTRAINT [DF__tblVendor__DateC__6FE99F9F]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__7B5B524B]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorRegulatoryRequirements] ADD  CONSTRAINT [DF__tblVendor__DateC__7B5B524B]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__5CD6CB2B]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorSubsidiaries] ADD  CONSTRAINT [DF__tblVendor__DateC__5CD6CB2B]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorSupplierDeclarationOnBusiness_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorSupplierDeclarationOnBusiness] ADD  CONSTRAINT [DF_tblVendorSupplierDeclarationOnBusiness_DateCreated]  DEFAULT (getdate()) FOR [B_Remarks_Q2]
GO
/****** Object:  Default [DF_tblVendorSupplierDeclarationOnBusiness_DateCreated_1]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorSupplierDeclarationOnBusiness] ADD  CONSTRAINT [DF_tblVendorSupplierDeclarationOnBusiness_DateCreated_1]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_tblVendorSupplierDeclarationOnSafety_DateCreated]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorSupplierDeclarationOnSafety] ADD  CONSTRAINT [DF_tblVendorSupplierDeclarationOnSafety_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__656C112C]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorSupplierReferences] ADD  CONSTRAINT [DF__tblVendor__DateC__656C112C]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__03F0984C]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorSuppliersDeclaration] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF__tblVendor__DateC__72C60C4A]    Script Date: 04/30/2015 15:42:21 ******/
ALTER TABLE [dbo].[tblVendorTopCompetitors] ADD  DEFAULT (getdate()) FOR [DateCreated]
GO
