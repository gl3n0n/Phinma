using System;
using System.Web;


/// <summary>
/// Summary description for CompanyDetails
/// </summary>
/// 
namespace EBid.lib.bid.data
{
    [Serializable]
    public class CompanyDetails
    {
        private int vendorID;
        private string companyName;
        private string address;
        private string address1;
        private string headTelephone;
        private string mobileNo;
        private string headFax;
        private string headExtension;
        private string address2;
        private string address3;
        private string branchTelephone;
        private string branchFax;
        private string branchExtension;
        private string vatRegNo;
        private string tIN;
        private string pOBox;
        private string postalCode;
        private string companyEmail;
        private string termsofPayment;
        private string specialTerms;
        private string minOrderValue;
        private string salesperson;
        private string salespersonPhone;
        private string ownershipFilipino;
        private string ownershipOthers;
        private int classification;
        private int organizationType;
        private string specialization;
        private string soleSupplier;
        private string soleSupplier1;
        private string keyPersonnel;
        private string kpPosition;
        private string iSOStandard;
        private int pCABClass;
        private int isBlackListed;
        private string category;
        private int accredited;
        
        public int VendorID
        {
            get
            {
                return vendorID;
            }
            set
            {
                vendorID = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        // Head Address
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string Address1
        {
            get
            {
                return address1;
            }
            set
            {
                address1 = value;
            }
        }

        public string HeadTelephone
        {
            get
            {
                return headTelephone;
            }
            set
            {
                headTelephone = value;
            }
        }

        public string MobileNo
        {
            get
            {
                return mobileNo;
            }
            set
            {
                mobileNo = value;
            }
        }

        public string HeadFax
        {
            get
            {
                return headFax;
            }
            set
            {
                headFax = value;
            }
        }

        public string HeadExtension
        {
            get
            {
                return headExtension;
            }
            set
            {
                headExtension = value;
            }
        }

        // Branch Address
        public string Address2
        {
            get
            {
                return address2;
            }
            set
            {
                address2 = value;
            }
        }

        public string Address3
        {
            get
            {
                return address3;
            }
            set
            {
                address3 = value;
            }
        }

        public string BranchTelephone
        {
            get
            {
                return branchTelephone;
            }
            set
            {
                branchTelephone = value;
            }
        }

        public string BranchFax
        {
            get
            {
                return branchFax;
            }
            set
            {
                branchFax = value;
            }
        }

        public string BranchExtension
        {
            get
            {
                return branchExtension;
            }
            set
            {
                branchExtension = value;
            }
        }

        public string VatRegNo
        {
            get
            {
                return vatRegNo;
            }
            set
            {
                vatRegNo = value;
            }
        }

        public string TIN
        {
            get
            {
                return tIN;
            }
            set
            {
                tIN = value;
            }
        }

        public string POBox
        {
            get
            {
                return pOBox;
            }
            set
            {
                pOBox = value;
            }
        }

        public string PostalCode
        {
            get
            {
                return postalCode;
            }
            set
            {
                postalCode = value;
            }
        }

        public string CompanyEmail
        {
            get
            {
                return companyEmail;
            }
            set
            {
                companyEmail = value;
            }
        }

        public string TermsofPayment
        {
            get
            {
                return termsofPayment;
            }
            set
            {
                termsofPayment = value;
            }
        }

        public string SpecialTerms
        {
            get
            {
                return specialTerms;
            }
            set
            {
                specialTerms = value;
            }
        }

        public string MinOrderValue
        {
            get
            {
                return minOrderValue;
            }
            set
            {
                minOrderValue = String.Format("{0:f}", float.Parse(value));
            }
        }

        public string SalesPerson
        {
            get
            {
                return salesperson;
            }
            set
            {
                salesperson = value;
            }
        }

        public string SalesPersonPhone
        {
            get
            {
                return salespersonPhone;
            }
            set
            {
                salespersonPhone = value;
            }
        }

        public string Specialization
        {
            get
            {
                return specialization;
            }
            set
            {
                specialization = value;
            }
        }

        public string SoleSupplier
        {
            get
            {
                return soleSupplier;
            }
            set
            {
                soleSupplier = value;
            }
        }

        public string SoleSupplier1
        {
            get
            {
                return soleSupplier1;
            }
            set
            {
                soleSupplier1 = value;
            }
        }

        public string OwnershipFilipino
        {
            get
            {
                return ownershipFilipino;
            }
            set
            {
                if(value != null)
                    ownershipFilipino = value;
                else
                    ownershipFilipino = "N/A";
            }
        }

        public string OwnershipOther
        {
            get
            {
                return ownershipOthers;
            }
            set
            {
                if(value != null)
                    ownershipOthers = value;
                else
                    ownershipOthers = "N/A";
            }
        }

        public int OrganizationType
        {
            get
            {
                return organizationType;
            }
            set
            {
                organizationType = value;
            }
        }

        public int Classification
        {
            get
            {
                return classification;
            }
            set
            {
                classification = value;
            }
        }

        public string KeyPersonnel
        {
            get
            {
                return keyPersonnel;
            }
            set
            {
                keyPersonnel = value;
            }
        }

        public string KpPosition
        {
            get
            {
                return kpPosition;
            }
            set
            {
                kpPosition = value;
            }
        }

        public string ISOStandard
        {
            get
            {
                return iSOStandard;
            }
            set
            {
                iSOStandard = value;
            }
        }

        public int PCABClass
        {
            get
            {
                return pCABClass;
            }
            set
            {
                pCABClass = value;
            }
        }

        public int IsBlackListed
        {
            get
            {
                return isBlackListed;
            }
            set
            {
                isBlackListed = value;
            }
        }

        public int Accredited
        {
            get
            {
                return accredited;
            }
            set
            {
                accredited = value;
            }
        }

        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
    }
}

