using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntityListLibrary;
using System.Collections.Generic;

namespace UnitTestGetEntity
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<Entity> ent = new List<Entity>();            
            for(var x=0;x<5;x++)
            {
                var data = new Entity { Id = x+1, Created = DateTime.Now.AddMinutes(x+1), Type = "U", Content = "test"+(x+1).ToString() };
                ent.Add(data);
            }

            GetEntity a = new GetEntity("", "U");
            var list = a.EntityList(ent);
            Assert.IsNotNull(list, "No data found.");
        }

        [TestMethod]
        public void TestMethod2()
        {
            //assumption: configuration file is stored in local drive c:
            GetEntity a = new GetEntity("C:\\configuration.json", "U");
            var list = a.EntityList(a.getData());
            Assert.IsNotNull(list, "No data found.");
        }
    }
}
