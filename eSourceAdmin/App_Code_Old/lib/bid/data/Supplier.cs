using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Supplier
/// </summary>
/// 
namespace EBid.lib.bid.data
{
    public class Supplier
    {
        public Supplier()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private string vendorid;
        private string vendorname;
        private string vendoraddress;
        private string vendoraddress1;
        private string vendoraddress2;
        private string vendoraddress3;
        private string contactperson;
        private string telephonenumber;
        private string vendoremail;
        
        private string fax; 
        private string extension; 
        private string branchtelephoneno; 
        private string branchfax; 
        private string branchextension; 
        private string vatregno; 
        private string tin; 
        private string pobox; 
        private string postalcode; 
        private string termsofpayment; 
        private string specialterms; 
        private string minordervalue; 
        private string salespersontelno; 
        private string orgtypeid; 
        private string ownershipfilipino; 
        private string ownershipother; 
        private string classification; 
        private string solesupplier1; 
        private string solesupplier2;
        private string specialization;

        private string accredited;
        private string vendorcategory;
        private string pcabclass; 
        private string vendorsubcategory;
        private string isostandard;

        private string keypersonnel;
        private string keyposition;
        
        public string VendorId
        {
            get
            {
                return vendorid;
            }
            set
            {
                vendorid = value;
            }
        }

        public string VendorName
        {
            get
            {
                return vendorname;
            }
            set
            {
                vendorname = value;
            }
        }

        public string VendorAddress
        {
            get
            {
                return vendoraddress;
            }
            set
            {
                vendoraddress = value;
            }
        }

        public string VendorAddress1
        {
            get
            {
                return vendoraddress1;
            }
            set
            {
                vendoraddress1 = value;
            }
        }

        public string VendorAddress2
        {
            get
            {
                return vendoraddress2;
            }
            set
            {
                vendoraddress2 = value;
            }
        }

        public string VendorAddress3
        {
            get
            {
                return vendoraddress3;
            }
            set
            {
                vendoraddress3 = value;
            }
        }
        public string ContactPerson
        {
            get
            {
                return contactperson;
            }
            set
            {
                contactperson = value;
            }
        }
        public string TelephoneNumber
        {
            get
            {
                return telephonenumber;
            }
            set
            {
                telephonenumber = value;
            }
        }
        public string VendorEmail
        {
            get
            {
                return vendoremail;
            }
            set
            {
                vendoremail = value;
            }
        }

        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                fax = value;
            }
        }
        public string Extension
        {
            get
            {
                return extension;
            }
            set
            {
                extension = value;
            }
        }
        public string BranchTelephoneNo
        {
            get
            {
                return branchtelephoneno;
            }
            set
            {
                branchtelephoneno = value;
            }
        }
        public string BranchFax
        {
            get
            {
                return branchfax;
            }
            set
            {
                branchfax = value;
            }
        }
        public string BranchExtension
        {
            get
            {
                return branchextension;
            }
            set
            {
                branchextension = value;
            }
        }
        public string VatRegNo
        {
            get
            {
                return vatregno;
            }
            set
            {
                vatregno = value;
            }
        }
        public string TIN
        {
            get
            {
                return tin;
            }
            set
            {
                tin = value;
            }
        }
        public string POBOX
        {
            get
            {
                return pobox;
            }
            set
            {
                pobox = value;
            }
        }
        public string PostalCode
        {
            get
            {
                return postalcode;
            }
            set
            {
                postalcode = value;
            }
        }

        public string TermsOfPayment
        {
            get
            {
                return termsofpayment;
            }
            set
            {
                termsofpayment = value;
            }
        }

        public string SpecialTerms
        {
            get
            {
                return specialterms;
            }
            set
            {
                specialterms = value;
            }
        }
        public string MinOrderValue
        {
            get
            {
                return minordervalue;
            }
            set
            {
                minordervalue = value;
            }
        }
        public string SalesPersonTelNo
        {
            get
            {
                return salespersontelno;
            }
            set
            {
                salespersontelno = value;
            }
        }

        public string OrgTypeId
        {
            get
            {
                return orgtypeid;
            }
            set
            {
                orgtypeid = value;
            }
        }
        public string OwnershipFilipino
        {
            get
            {
                return ownershipfilipino;
            }
            set
            {
                ownershipfilipino = value;
            }
        }
        public string OwnershipOther
        {
            get
            {
                return ownershipother;
            }
            set
            {
                ownershipother = value;
            }
        }
        public string Classification
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

        public string SoleSupplier1
        {
            get
            {
                return solesupplier1;
            }
            set
            {
                solesupplier1 = value;
            }
        }
        public string SoleSupplier2
        {
            get
            {
                return solesupplier2;
            }
            set
            {
                solesupplier2 = value;
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

        public string Accredited
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

        public string PCABClass
        {
            get
            {
                return pcabclass;
            }
            set
            {
                pcabclass = value;
            }
        }

        public string VendorCategory
        {
            get
            {
                return vendorcategory;
            }
            set
            {
                vendorcategory = value;
            }
        }

        public string VendorSubCategory
        {
            get
            {
                return vendorsubcategory;
            }
            set
            {
                vendorsubcategory = value;
            }
        }

        public string ISOStandard
        {
            get
            {
                return isostandard;
            }
            set
            {
                isostandard = value;
            }
        }

        public string KeyPersonnel
        {
            get
            {
                return keypersonnel;
            }
            set
            {
                keypersonnel = value;
            }
        }

        public string KeyPosition
        {
            get
            {
                return keyposition;
            }
            set
            {
                keyposition = value;
            }
        }
    }


}
