using System;

namespace EBid.lib.bid.data
{
	public class Category
	{
        private string categoryId;
		private string categoryName;
		private string categoryDesc;
        private string idcount;
        private string namecount;

		public Category()
		{
		}

		public string CategoryId 
		{
			get 
			{
				return categoryId;
			}
			set 
			{
				categoryId = value;
			}
		}

		public string CategoryName
		{
			get 
			{
				return categoryName;
			}
			set 
			{
				categoryName = value;
			}
		}

		public string CategoryDesc
		{
			get 
			{
				return categoryDesc;
			}
			set 
			{
				categoryDesc = value;
			}
		}

        public string IdCount
        {
            get
            {
                return idcount;
            }
            set
            {
                idcount = value;
            }
        }

        public string NameCount
        {
            get
            {
                return namecount;
            }
            set
            {
                namecount = value;
            }
        }
	}
}
