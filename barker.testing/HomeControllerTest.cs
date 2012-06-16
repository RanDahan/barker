using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using barker.Controllers;

namespace barker.testing
{
    [TestFixture]
    class HomeControllerTest
    {
        [Test]
        public void Puts_ViewBag()
        {
            var controller = new HomeController();
            var result = controller.ViewBagSet();
            Assert.IsNotNull((result as ViewResult).ViewBag.Message);
        }
    }
}
