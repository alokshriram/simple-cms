using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using simple_cms.Repository;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace simple_cms_tests
{
    [TestClass]
    public class CloudRepositoryTest
    {
        CloudRepository cloudRepo;
        public CloudRepositoryTest()
        {
            ISettingRepository repo = new CloudSettings();
            this.cloudRepo = new CloudRepository(repo);
        }

        [TestMethod]
        public void GetAllContainer()
        {
            List<string> urls=this.cloudRepo.GetUrlsForArea(simple_cms.ContentType.Image);
            Assert.IsTrue(urls.Count == 2);
        }

        [TestMethod]
        public void CreateANewBlob_AddAFile()
        {
            string url=this.cloudRepo.AddNewFile("testarea", @"C:\Blb\HTML.txt");
            Assert.IsTrue(url.Contains("testarea"));
        }

        //[TestMethod]
        //public void GetAllAreas_ReturnsAllContainerNamesAdded()
        //{
        //    this.cloudRepo.CreateNewArea("testArea1");
        //    this.cloudRepo.CreateNewArea("testArea2");
        //    List<string> areas = this.cloudRepo.GetAllAreas();
        //    Assert.IsTrue(areas.Contains("testarea1"));
        //    Assert.IsTrue(areas.Contains("testarea2"));
        //}

        //[TestMethod]
        //public void AddImageTest_ValidatedImageGetsLoadedCorrectly()
        //{
        //    Image img=Image.FromFile(Path.Combine(@".\", "Resource","TestImage.jpg"));
        //    this.cloudRepo.AddImage(img, "testpathtest3", "testimage.jpeg");

        //}


        
    }
}
