using System;
using System.Web;

namespace EBid.lib.bid.data
{
	public class ProductUnit
	{
		private int unitId;
		private string unitName;

		public ProductUnit(
			int unitId,
			string unitName)
		{
			this.unitId = unitId;
			this.unitName = unitName;
		}

		public ProductUnit() 
		{
		}

		public int UnitId 
		{
			get 
			{
				return unitId;
			}
			set 
			{
				unitId = value;
			}
		}
		
		public string UnitName
		{
			get 
			{
				return unitName;
			}
			set 
			{
				unitName = value;
			}
		}
	}
}
