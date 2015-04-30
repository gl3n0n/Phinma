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
/// Summary description for Product
/// </summary>
/// 
namespace EBid.lib.bid.data
{
    public class Product
    {
        public Product()
        {
            //
            // TODO: Add constructor logic here
            //
            	
        }
        private string productdescription;
		private string unitofmeasure;
        private string sku;
        private string itemname;
        private string category;
        private string subcategory;
        private string brand;
        private string servicetype;

        public string ProductDescription
        {
            get
            {
                return productdescription;
            }
            set
            {
                productdescription = value;
            }
        }

        public string UnitOfMeasure
        {
            get
            {
                return unitofmeasure;
            }
            set
            {
                unitofmeasure = value;
            }
        }


        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }

        public string ItemName
        {
            get
            {
                return itemname;
            }
            set
            {
                itemname = value;
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
        public string SubCategory
        {
            get
            {
                return subcategory;
            }
            set
            {
                subcategory = value;
            }
        }
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                brand = value;
            }
        }
        public string ServiceType
        {
            get
            {
                return servicetype;
            }
            set
            {
                servicetype = value;
            }
        }
    }

    
}