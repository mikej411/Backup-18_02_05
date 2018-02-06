using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
    /// A utility class for manipulating elements within cme360. This class is an alternative to ElemSet.cs. I created this because 
    /// CME360's HTML design is really bad and obsolete in terms of newer HTML standards and practices, and so a majority of the 
    /// methods inside ElemSet will not work with cme360. If you find that a method inside ElemSet works, then use that, if not, 
    /// then you will have to create (or find) a custom method inside this class just for CME360
    /// </summary>
    public class ElemSet_CME360
    {

        #region Checkbox


        #endregion Checkbox

        #region dropdown custom



        #endregion dropdown custom

        #region Select Elements


        #endregion Select Elements

        #region Grids

        /// <summary>
        /// Clicks on a button (Either X, Edit, or Pencil) within a user-specified row
        /// </summary>
        /// <param name="row">The row that contains the element you want to click on. To get the row, <see cref="ElemGet_CME360.Grid_GetRowByRowName(IWebElement, By, string, string)"/></param>
        /// <param name="action">Either "Edit" "Delete" "Remove" "Add" or "View"</param>
        public static void Grid_ClickElementWithoutTextInsideRow(IWebElement row, string tagnameWhereElemExists, string action)
        {
            string xpath = string.Format("descendant::{0}[contains(@alt, '{1}')]", tagnameWhereElemExists, action);

            var button = row.FindElement(By.XPath(xpath));

            button.Click();
        }


        #endregion Grids

        #region Radio Buttons



        #endregion Radio Buttons


        #region General



        #endregion General


    }
}

