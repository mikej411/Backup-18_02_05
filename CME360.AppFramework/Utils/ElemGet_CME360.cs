using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Browser.Core.Framework
{
    /// <summary>
    /// A utility class retrieving data and properties from elements within cme360. This class is an alternative to ElemGet.cs. I created this because 
    /// CME360's HTML design is really bad and obsolete in terms of newer HTML standards and practices, and so a majority of the 
    /// methods inside ElemGet will not work with cme360. If you find that a method inside ElemGet works, then use that, if not, 
    /// then you will have to create (or find) a custom method inside this class just for CME360
    /// </summary>
    public class ElemGet_CME360
    {
        #region Checkbox


        #endregion Checkbox

        #region dropdown custom



        #endregion dropdown custom

        #region Select Elements


        #endregion Select Elements

        #region Grids

        /// <summary>
        /// Returns the row that contains the user-specified cell
        /// </summary>
        /// <param name="tblElem">You table element that is found within the your Page class. i.e. OP.PendingAcceptanceTbl</param>
        /// <param name="firstRowby">Send any row (Most likely just send the iwebelement that is a generic row 'tableId/tr' in the table, as it exists in your By type. i.e. Bys.CBDObserverPage,PendingAcceptanceTblRowBody). We need any row so that we can wait for it to appear before proceeding with the test"/></param>
        /// <param name="firstColumnCellText">The name of the row. i.e. The exact text from cell inside the first column</param>
        /// <param name="tagNameWhereTextExists">Inspect the text in the first row cell element and extract the tag name</param>
        /// <returns></returns>
        public static IWebElement Grid_GetRowByRowName(IWebElement tblElem, By firstRowby, string firstColumnCellText, string tagNameWhereTextExists)
        {
            IWebElement firstColumnCell = null;

            // First wait for the table
            tblElem.WaitForElement(firstRowby, TimeSpan.FromSeconds(120), ElementCriteria.HasText, ElementCriteria.IsEnabled);

            // Sometimes the text of the cell is contained within the A tag, and sometimes it is contained within the td tag, and even
            // a div tag. We are using a parameter (tagNameWhereTextExists) to handle this conditions. We will use an IF statements to 
            // condition when cells have leading and trailing white space
            string xpath = string.Format(".//{0}[text()='{1}']", tagNameWhereTextExists, firstColumnCellText);
            string xpathWithExtraSpaces = string.Format(".//{0}[contains(., '{1}')]", tagNameWhereTextExists, firstColumnCellText);

            // If we find elements by using the TD tag text equals xpathstring for specific RCP table
            if (tblElem.FindElements(By.XPath(xpath)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpath))[0];
            }
            // If we find elements by using the TD tag text equals xpathstring
            else if (tblElem.FindElements(By.XPath(xpathWithExtraSpaces)).Count > 0)
            {
                firstColumnCell = tblElem.FindElements(By.XPath(xpathWithExtraSpaces))[0];
            }
        
            else
            {
                throw new Exception("The cell text for either column could not be found in the table you have specified. Either the row");
            }

            // Then get the 1st parent row element
            IWebElement row = XpathUtils.GetParentOrChildElemWithSpecifiedCriteria(firstColumnCell, "parent", "tr[1]");

            return row;
        }

        #endregion Grids

        #region Radio Buttons



        #endregion Radio Buttons


        #region General



        #endregion General
    }


}