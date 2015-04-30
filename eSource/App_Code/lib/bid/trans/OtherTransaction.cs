using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using EBid.lib.constant;
/// <summary>
/// Summary description for DeliveryDateTransaction
/// </summary>
/// 

namespace EBid.lib.bid.trans
{
    public class OtherTransaction
    {
      
    public ListItemCollection GetMonth2(string selecteditem) 
    {
        ListItemCollection lst = new ListItemCollection();
        lst = GetMonth();
        lst.IndexOf(lst.FindByValue(selecteditem));
        return lst;
    }

        public ListItemCollection GetMonth()
        {
            ListItemCollection lst = new ListItemCollection();
            ListItem lstitem = new ListItem();
            lstitem.Value = "";
            lstitem.Text = "";
            lst.Add(lstitem);
            ListItem lstitem1 = new ListItem();
            lstitem1.Value = "1";
            lstitem1.Text = "Jan";
            lst.Add(lstitem1);
            ListItem lstitem2 = new ListItem();
            lstitem2.Value = "2";
            lstitem2.Text = "Feb";
            lst.Add(lstitem2);
            ListItem lstitem3 = new ListItem();
            lstitem3.Value = "3";
            lstitem3.Text = "Mar";
            lst.Add(lstitem3);
            ListItem lstitem4 = new ListItem();
            lstitem4.Value = "4";
            lstitem4.Text = "Apr";
            lst.Add(lstitem4);
            ListItem lstitem5 = new ListItem();
            lstitem5.Value = "5";
            lstitem5.Text = "May";
            lst.Add(lstitem5);
            ListItem lstitem6 = new ListItem();
            lstitem6.Value = "6";
            lstitem6.Text = "Jun";
            lst.Add(lstitem6);
            ListItem lstitem7 = new ListItem();
            lstitem7.Value = "7";
            lstitem7.Text = "Jul";
            lst.Add(lstitem7);
            ListItem lstitem8 = new ListItem();
            lstitem8.Value = "8";
            lstitem8.Text = "Aug";
            lst.Add(lstitem8);
            ListItem lstitem9 = new ListItem();
            lstitem9.Value = "9";
            lstitem9.Text = "Sep";
            lst.Add(lstitem9);
            ListItem lstitem10 = new ListItem();
            lstitem10.Value = "10";
            lstitem10.Text = "Oct";
            lst.Add(lstitem10);
            ListItem lstitem11 = new ListItem();
            lstitem11.Value = "11";
            lstitem11.Text = "Nov";
            lst.Add(lstitem11);
            ListItem lstitem12 = new ListItem();
            lstitem12.Value = "12";
            lstitem12.Text = "Dec";
            lst.Add(lstitem12);

            return lst;
        }

        public ListItemCollection GetAMPM()
        {
            ListItemCollection lst = new ListItemCollection();
            ListItem lstitem = new ListItem();
            lstitem.Value = "";
            lstitem.Text = "";
            lst.Add(lstitem);
            ListItem lstitem1 = new ListItem();
            lstitem1.Value = "AM";
            lstitem1.Text = "AM";
            lst.Add(lstitem1);
            ListItem lstitem2 = new ListItem();
            lstitem2.Value = "PM";
            lstitem2.Text = "PM";
            lst.Add(lstitem2);
            
            return lst;
        }

        public ListItemCollection GetHour()
        {
            ListItemCollection lst = new ListItemCollection();
            ListItem lstitem = new ListItem();
            lstitem.Value = "";
            lstitem.Text = "";
            lst.Add(lstitem);
            ListItem lstitem1 = new ListItem();
            lstitem1.Value = "1";
            lstitem1.Text = "01";
            lst.Add(lstitem1);
            ListItem lstitem2 = new ListItem();
            lstitem2.Value = "2";
            lstitem2.Text = "02";
            lst.Add(lstitem2);
            ListItem lstitem3 = new ListItem();
            lstitem3.Value = "3";
            lstitem3.Text = "03";
            lst.Add(lstitem3);
            ListItem lstitem4 = new ListItem();
            lstitem4.Value = "4";
            lstitem4.Text = "04";
            lst.Add(lstitem4);
            ListItem lstitem5 = new ListItem();
            lstitem5.Value = "5";
            lstitem5.Text = "05";
            lst.Add(lstitem5);
            ListItem lstitem6 = new ListItem();
            lstitem6.Value = "6";
            lstitem6.Text = "06";
            lst.Add(lstitem6);
            ListItem lstitem7 = new ListItem();
            lstitem7.Value = "7";
            lstitem7.Text = "07";
            lst.Add(lstitem7);
            ListItem lstitem8 = new ListItem();
            lstitem8.Value = "8";
            lstitem8.Text = "08";
            lst.Add(lstitem8);
            ListItem lstitem9 = new ListItem();
            lstitem9.Value = "9";
            lstitem9.Text = "09";
            lst.Add(lstitem9);
            ListItem lstitem10 = new ListItem();
            lstitem10.Value = "10";
            lstitem10.Text = "10";
            lst.Add(lstitem10);
            ListItem lstitem11 = new ListItem();
            lstitem11.Value = "11";
            lstitem11.Text = "11";
            lst.Add(lstitem11);
            ListItem lstitem12 = new ListItem();
            lstitem12.Value = "12";
            lstitem12.Text = "12";
            lst.Add(lstitem12);

            return lst;
        }

        public ListItemCollection GetBidCurrency()
        {
            ListItemCollection lst = new ListItemCollection();
            ListItem lstitem = new ListItem();
            lstitem.Value = "";
            lstitem.Text = "";
            lst.Add(lstitem);
            ListItem lstitem1 = new ListItem();
            lstitem1.Value = "0";
            lstitem1.Text = "US Dollar";
            lst.Add(lstitem1);
            ListItem lstitem2 = new ListItem();
            lstitem2.Value = "1";
            lstitem2.Text = "Philippine Peso";
            lst.Add(lstitem2);

            return lst;
        }

        public string GetBidCurrency(string strCurrency)
        {
            switch (strCurrency)
            {
                case "0":
                    return "US Dollar";
                    
                case "1":
                    return "Philippine Peso";
                    
                default:
                    return "";
            }
        }


        
        public string Month(string monthnum)
        {

            switch (monthnum)
            {
                case "1":
                    return "January";                    
                case "2":
                    return "February";                    
                case "3":
                    return "March";                    
                case "4":
                    return "April";                    
                case "5":
                    return "May";                    
                case "6":
                    return "June";                    
                case "7":
                    return "July";                    
                case "8":
                    return "August";                    
                case "9":
                    return "September";                    
                case "10":
                    return "October";                    
                case "11":
                    return "November";                    
                case "12":
                    return "December";                    
                default:
                    return "";                    

            }
        }


        public ListItemCollection GetCurrency()
        {
            ListItemCollection lst = new ListItemCollection();
            ListItem lstitem = new ListItem();
            lstitem.Value = "";
            lstitem.Text = "(Select a Currency from the List)";
            lst.Add(lstitem);
            ListItem lstitem1 = new ListItem();
            lstitem1.Value = "THB";
            lstitem1.Text = "baht: Thai";
            lst.Add(lstitem1);
            ListItem lstitem2 = new ListItem();
            lstitem2.Value = "PAB";
            lstitem2.Text = "balboa: Panamanian";
            lst.Add(lstitem2);
            ListItem lstitem3 = new ListItem();
            lstitem3.Value = "VEB";
            lstitem3.Text = "bolivar: Venezuelan";
            lst.Add(lstitem3);
            ListItem lstitem4 = new ListItem();
            lstitem4.Value = "GHC";
            lstitem4.Text = "cedi: Ghanaian";
            lst.Add(lstitem4);
            ListItem lstitem5 = new ListItem();
            lstitem5.Value = "TND";
            lstitem5.Text = "dinar: Tunisian";
            lst.Add(lstitem5);
            ListItem lstitem6 = new ListItem();
            lstitem6.Value = "MAD";
            lstitem6.Text = "dirham: Moroccan";
            lst.Add(lstitem6);
            ListItem lstitem7 = new ListItem();
            lstitem7.Value = "AUD";
            lstitem7.Text = "dollar: Australian";
            lst.Add(lstitem7);
            ListItem lstitem8 = new ListItem();
            lstitem8.Value = "BSD";
            lstitem8.Text = "dollar: Bahamian";
            lst.Add(lstitem8);
            ListItem lstitem9 = new ListItem();
            lstitem9.Value = "BBD";
            lstitem9.Text = "dollar: Barbadian";
            lst.Add(lstitem9);
            ListItem lstitem10 = new ListItem();
            lstitem10.Value = "BZD";
            lstitem10.Text = "dollar: Belizean";
            lst.Add(lstitem10);
            ListItem lstitem11 = new ListItem();
            lstitem11.Value = "BMD";
            lstitem11.Text = "dollar: Bermudian";
            lst.Add(lstitem11);
            ListItem lstitem12 = new ListItem();
            lstitem12.Value = "BND";
            lstitem12.Text = "dollar: Bruneian";
            lst.Add(lstitem12);
            ListItem lstitem13 = new ListItem();
            lstitem13.Value = "CAD";
            lstitem13.Text = "dollar: Canadian";
            lst.Add(lstitem13);
            ListItem lstitem14 = new ListItem();
            lstitem14.Value = "XCD";
            lstitem14.Text = "dollar: East Caribbean";
            lst.Add(lstitem14);
            ListItem lstitem15 = new ListItem();
            lstitem15.Value = "FJD";
            lstitem15.Text = "dollar: Fijian";
            lst.Add(lstitem15);
            ListItem lstitem16 = new ListItem();
            lstitem16.Value = "HKD";
            lstitem16.Text = "dollar: Hong Kong";
            lst.Add(lstitem16);
            ListItem lstitem17 = new ListItem();
            lstitem17.Value = "JMD";
            lstitem17.Text = "dollar: Jamaican";
            lst.Add(lstitem17);
            ListItem lstitem18 = new ListItem();
            lstitem18.Value = "NAD";
            lstitem18.Text = "dollar: Namibian";
            lst.Add(lstitem18);
            ListItem lstitem19 = new ListItem();
            lstitem19.Value = "NZD";
            lstitem19.Text = "dollar: New Zealand";
            lst.Add(lstitem19);
            ListItem lstitem20 = new ListItem();
            lstitem20.Value = "SGD";
            lstitem20.Text = "dollar: Singapore";
            lst.Add(lstitem20);
            ListItem lstitem21 = new ListItem();
            lstitem21.Value = "TTD";
            lstitem21.Text = "dollar: Trin. and Tobag.";
            lst.Add(lstitem21);
            ListItem lstitem22 = new ListItem();
            lstitem22.Value = "USD";
            lstitem22.Text = "dollar: United States";
            lst.Add(lstitem22);
            ListItem lstitem23 = new ListItem();
            lstitem23.Value = "PTE";
            lstitem23.Text = "escudo: Portuguese";
            lst.Add(lstitem23);
            ListItem lstitem24 = new ListItem();
            lstitem24.Value = "EUR";
            lstitem24.Text = "euro: European Union";
            lst.Add(lstitem24);
            ListItem lstitem25 = new ListItem();
            lstitem25.Value = "HUF";
            lstitem25.Text = "forint: Hungarian";
            lst.Add(lstitem25);
            ListItem lstitem26 = new ListItem();
            lstitem26.Value = "XOF";
            lstitem26.Text = "franc BCEAO: CFA";
            lst.Add(lstitem26);
            ListItem lstitem27 = new ListItem();
            lstitem27.Value = "XAF";
            lstitem27.Text = "franc BEAC: CFA";
            lst.Add(lstitem27);
            ListItem lstitem28 = new ListItem();
            lstitem28.Value = "BEF";
            lstitem28.Text = "franc: Belgian";
            lst.Add(lstitem28);
            ListItem lstitem29 = new ListItem();
            lstitem29.Value = "XPF";
            lstitem29.Text = "franc: CFP";
            lst.Add(lstitem29);
            ListItem lstitem30 = new ListItem();
            lstitem30.Value = "DJF";
            lstitem30.Text = "franc: Djiboutian";
            lst.Add(lstitem30);
            ListItem lstitem31 = new ListItem();
            lstitem31.Value = "FRF";
            lstitem31.Text = "franc: French";
            lst.Add(lstitem31);
            ListItem lstitem32 = new ListItem();
            lstitem32.Value = "LUF";
            lstitem32.Text = "franc: Luxembourg";
            lst.Add(lstitem32);
            ListItem lstitem33 = new ListItem();
            lstitem33.Value = "CHF";
            lstitem33.Text = "franc: Swiss";
            lst.Add(lstitem33);
            ListItem lstitem34 = new ListItem();
            lstitem34.Value = "AWG";
            lstitem34.Text = "guilder: Aruban";
            lst.Add(lstitem34);
            ListItem lstitem35 = new ListItem();
            lstitem35.Value = "ANG";
            lstitem35.Text = "guilder: Neth. Antillian";
            lst.Add(lstitem35);
            ListItem lstitem36 = new ListItem();
            lstitem36.Value = "NLG";
            lstitem36.Text = "guilder: Netherlands";
            lst.Add(lstitem36);
            ListItem lstitem37 = new ListItem();
            lstitem37.Value = "CZK";
            lstitem37.Text = "koruna: Czech";
            lst.Add(lstitem37);
            ListItem lstitem38 = new ListItem();
            lstitem38.Value = "SKK";
            lstitem38.Text = "koruna: Slovak";
            lst.Add(lstitem38);
            ListItem lstitem39 = new ListItem();
            lstitem39.Value = "ISK";
            lstitem39.Text = "krona: Icelandic";
            lst.Add(lstitem39);
            ListItem lstitem40 = new ListItem();
            lstitem40.Value = "SEK";
            lstitem40.Text = "krona: Swedish";
            lst.Add(lstitem40);
            ListItem lstitem41 = new ListItem();
            lstitem41.Value = "DKK";
            lstitem41.Text = "krone: Danish";
            lst.Add(lstitem41);
            ListItem lstitem42 = new ListItem();
            lstitem42.Value = "NOK";
            lstitem42.Text = "krone: Norwegian";
            lst.Add(lstitem42);
            ListItem lstitem43 = new ListItem();
            lstitem43.Value = "EEK";
            lstitem43.Text = "kroon: Estonian";
            lst.Add(lstitem43);
            ListItem lstitem44 = new ListItem();
            lstitem44.Value = "HRK";
            lstitem44.Text = "kuna: Croatian";
            lst.Add(lstitem44);
            ListItem lstitem45 = new ListItem();
            lstitem45.Value = "MMK";
            lstitem45.Text = "kyat: Burmese";
            lst.Add(lstitem45);
            ListItem lstitem46 = new ListItem();
            lstitem46.Value = "HNL";
            lstitem46.Text = "lempira: Honduran";
            lst.Add(lstitem46);
            ListItem lstitem47 = new ListItem();
            lstitem47.Value = "SZL";
            lstitem47.Text = "lilangeni: Swazi";
            lst.Add(lstitem47);
            ListItem lstitem48 = new ListItem();
            lstitem48.Value = "ITL";
            lstitem48.Text = "lira: Italian";
            lst.Add(lstitem48);
            ListItem lstitem49 = new ListItem();
            lstitem49.Value = "LTL";
            lstitem49.Text = "litas: Lithuanian";
            lst.Add(lstitem49);
            ListItem lstitem50 = new ListItem();
            lstitem50.Value = "LSL";
            lstitem50.Text = "loti: Lesotho";
            lst.Add(lstitem50);
            ListItem lstitem51 = new ListItem();
            lstitem51.Value = "DEM";
            lstitem51.Text = "mark: German";
            lst.Add(lstitem51);
            ListItem lstitem52 = new ListItem();
            lstitem52.Value = "FIM";
            lstitem52.Text = "markka: Finnish";
            lst.Add(lstitem52);
            ListItem lstitem53 = new ListItem();
            lstitem53.Value = "BAM";
            lstitem53.Text = "marks: Bos. and Herz.";
            lst.Add(lstitem53);
            ListItem lstitem54 = new ListItem();
            lstitem54.Value = "TWD";
            lstitem54.Text = "new dollar: Taiwanese";
            lst.Add(lstitem54);
            ListItem lstitem55 = new ListItem();
            lstitem55.Value = "ILS";
            lstitem55.Text = "new shekel: Israeli";
            lst.Add(lstitem55);
            ListItem lstitem56 = new ListItem();
            lstitem56.Value = "BTN";
            lstitem56.Text = "ngultrum: Bhutanese";
            lst.Add(lstitem56);
            ListItem lstitem57 = new ListItem();
            lstitem57.Value = "PEN";
            lstitem57.Text = "nuevo sol: Peruvian";
            lst.Add(lstitem57);
            ListItem lstitem58 = new ListItem();
            lstitem58.Value = "ADP";
            lstitem58.Text = "peseta: Andorran";
            lst.Add(lstitem58);
            ListItem lstitem59 = new ListItem();
            lstitem59.Value = "ESP";
            lstitem59.Text = "peseta: Spanish";
            lst.Add(lstitem59);
            ListItem lstitem60 = new ListItem();
            lstitem60.Value = "ARS";
            lstitem60.Text = "peso: Argentine";
            lst.Add(lstitem60);
            ListItem lstitem61 = new ListItem();
            lstitem61.Value = "CLP";
            lstitem61.Text = "peso: Chilean";
            lst.Add(lstitem61);
            ListItem lstitem62 = new ListItem();
            lstitem62.Value = "COP";
            lstitem62.Text = "peso: Colombian";
            lst.Add(lstitem62);
            ListItem lstitem63 = new ListItem();
            lstitem63.Value = "CUP";
            lstitem63.Text = "peso: Cuban";
            lst.Add(lstitem63);
            ListItem lstitem65 = new ListItem();
            lstitem65.Value = "MXN";
            lstitem65.Text = "peso: Mexican";
            lst.Add(lstitem65);
            ListItem lstitem66 = new ListItem();
            lstitem66.Value = "PHP";
            lstitem66.Text = "peso: Philippine";
            lst.Add(lstitem66);
            ListItem lstitem67 = new ListItem();
            lstitem67.Value = "GBP";
            lstitem67.Text = "pound sterling: British";
            lst.Add(lstitem67);
            ListItem lstitem68 = new ListItem();
            lstitem68.Value = "FKP";
            lstitem68.Text = "pound: Falkland";
            lst.Add(lstitem68);
            ListItem lstitem69 = new ListItem();
            lstitem69.Value = "GIP";
            lstitem69.Text = "pound: Gibraltar";
            lst.Add(lstitem69);
            ListItem lstitem70 = new ListItem();
            lstitem70.Value = "IEP";
            lstitem70.Text = "pound: Irish";
            lst.Add(lstitem70);
            ListItem lstitem71 = new ListItem();
            lstitem71.Value = "SHP";
            lstitem71.Text = "pound: Saint Helenian";
            lst.Add(lstitem71);
            ListItem lstitem72 = new ListItem();
            lstitem72.Value = "GTQ";
            lstitem72.Text = "quetzal: Guatemalan";
            lst.Add(lstitem72);
            ListItem lstitem73 = new ListItem();
            lstitem73.Value = "ZAR";
            lstitem73.Text = "rand: South African";
            lst.Add(lstitem73);
            ListItem lstitem74 = new ListItem();
            lstitem74.Value = "BRL";
            lstitem74.Text = "real: Brazilian";
            lst.Add(lstitem74);
            ListItem lstitem75 = new ListItem();
            lstitem75.Value = "OMR";
            lstitem75.Text = "rial: Omani";
            lst.Add(lstitem75);
            ListItem lstitem76 = new ListItem();
            lstitem76.Value = "MYR";
            lstitem76.Text = "ringgit: Malaysian";
            lst.Add(lstitem76);
            ListItem lstitem77 = new ListItem();
            lstitem77.Value = "RUR";
            lstitem77.Text = "ruble: Russian";
            lst.Add(lstitem77);
            ListItem lstitem78 = new ListItem();
            lstitem78.Value = "INR";
            lstitem78.Text = "rupee: Indian";
            lst.Add(lstitem78);
            ListItem lstitem79 = new ListItem();
            lstitem79.Value = "PKR";
            lstitem79.Text = "rupee: Pakistani";
            lst.Add(lstitem79);
            ListItem lstitem80 = new ListItem();
            lstitem80.Value = "LKR";
            lstitem80.Text = "rupee: Sri Lankan";
            lst.Add(lstitem80);
            ListItem lstitem81 = new ListItem();
            lstitem81.Value = "IDR";
            lstitem81.Text = "rupiah: Indonesian";
            lst.Add(lstitem81);
            ListItem lstitem82 = new ListItem();
            lstitem82.Value = "ATS";
            lstitem82.Text = "schilling: Austrian";
            lst.Add(lstitem82);
            ListItem lstitem83 = new ListItem();
            lstitem83.Value = "SIT";
            lstitem83.Text = "tolar: Slovenian";
            lst.Add(lstitem83);
            ListItem lstitem84 = new ListItem();
            lstitem84.Value = "KRW";
            lstitem84.Text = "won: South Korean";
            lst.Add(lstitem84);
            ListItem lstitem85 = new ListItem();
            lstitem85.Value = "JPY";
            lstitem85.Text = "yen: Japanese";
            lst.Add(lstitem85);
            ListItem lstitem86 = new ListItem();
            lstitem86.Value = "CNY";
            lstitem86.Text = "yuan renminbi: Chinese";
            lst.Add(lstitem86);
            ListItem lstitem87 = new ListItem();
            lstitem87.Value = "PLN";
            lstitem87.Text = "zloty: Polish";
            lst.Add(lstitem87);
            return lst;
        }

        public string GetStatus(int vStatus)
        {

            switch (vStatus)
            {
                case 0:
                    return "Save As Draft";                    
                case 1:
                    return "Submitted";                    
                case 2:
                    return "Rejected";                    
                case 3:
                    return "Re-edit";                    
                case 4:
                    return "Approved";                    
                case 5:
                    return "Renegotiated";                    
                default:
                    return "";                    
            }
            
        }
        public string GetAuctionStatus(int vStatus)
        {

            switch (vStatus)
            {
                case 0:
                    return "Save As Draft";                    
                case 1:
                    return "Submitted";                    
                case 2:
                    return "Rejected";                    
                case 3:
                    return "Re-edit";                    
                case 4:
                    return "Approved";                    
                case 5:
                    return "Cancelled";                    
                case 6:
                    return "Ongoing";                    
                case 7:
                    return "Awarded";                    
                default:
                    return "";                    
            }
        }

        public string Replace(string originalstring)
        {
            string newstring = originalstring.Replace("'", "''");
            newstring = newstring.Replace("&quot;", "&quot;&quot;");
            return newstring.Trim();
        }
    }
}