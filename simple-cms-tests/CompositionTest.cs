namespace simple_cms_tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using simple_cms.App_Start;
    using Autofac;
    using simple_cms.Controllers;

    /// <summary>
    /// Summary description for CompositionTest
    /// </summary>
    [TestClass]
    public class CompositionTest
    {
        [TestMethod]
        public void ControllerCompositionTest()
        {
            IContainer container= AutofacConfig.RegisterContentController();
            ContentController cntrl=container.Resolve<ContentController>();
            Assert.IsNotNull(cntrl);
        }
    }
}
