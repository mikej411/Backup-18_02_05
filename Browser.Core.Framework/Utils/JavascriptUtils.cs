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
    /// A utility class for executing various javascript commands
    /// </summary>
    public static class JavascriptUtils
    {
        public static void Click(IWebDriver Browser, IWebElement elem)
        {
            Browser.ExecuteScript("arguments[0].click();", elem);
        }

        public static void SendKeys(IWebDriver Browser, IWebElement elem, string textToEnter)
        {
            Browser.ExecuteScript("arguments[0].value = arguments[1]", elem, textToEnter);
        }

        /// <summary>
        /// Firefox has a bug which prevents some events from being executed while the browser window is out of focus. This could be an issue
        /// when you're running your automation tests - which might be typing even if the window is out of focus. You will notice this issue
        /// if you enter text in a required field, click a button which closes a modal, and see that the modal does not close, and the system
        /// warsn you that the required field is empty (the field cleared because of this bug). To fix this, we have to trigger an event through
        /// javascript. Use this method to trigger that event. For more info, see:
        /// https://stackoverflow.com/questions/9505588/selenium-webdriver-is-clearing-out-fields-after-sendkeys-had-previously-populate
        /// </summary>
        /// <param name="Browser"></param>
        /// <param name="whatDateTxt"></param>
        public static void TriggerChangeEvent(IWebDriver Browser, IWebElement whatDateTxt)
        {
            Browser.ExecuteScript("$(arguments[0]).change();", whatDateTxt);
        }
    }
}

