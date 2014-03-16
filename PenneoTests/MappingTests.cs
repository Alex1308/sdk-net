﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Penneo;
using Penneo.Mapping;

namespace PenneoTests
{
    [TestClass]
    public class MappingTests
    {
        [TestMethod]
        public void StringMappingTest()
        {
            const string title = "My Title";

            var mapping = new MappingBuilder<CaseFile>()
                .ForCreate()
                .Map(x => x.Title)
                .ForUpdate()
                .Map(x => x.Title)
                .GetMapping();

            var obj = new CaseFile {Title = title};
            var createValues = mapping.GetCreateValues(obj);
            var updateValues = mapping.GetUpdateValues(obj);

            Assert.IsNotNull(createValues);
            Assert.IsTrue(createValues.Count == 1);
            Assert.AreEqual(title, (string)createValues["Title"]);

            Assert.IsNotNull(updateValues);
            Assert.IsTrue(updateValues.Count == 1);
            Assert.AreEqual(title, (string)updateValues["Title"]);
        }

        [TestMethod]
        public void IntegerMappingTest()
        {
            const int status = 3;

            var mapping = new MappingBuilder<CaseFile>()
                .ForCreate()
                .Map(x => x.Status)
                .ForUpdate()
                .Map(x => x.Status)
                .GetMapping();

            var obj = new CaseFile {Status = 3};
            var createValues = mapping.GetCreateValues(obj);
            var updateValues = mapping.GetUpdateValues(obj);

            Assert.IsNotNull(createValues);
            Assert.IsTrue(createValues.Count == 1);
            Assert.AreEqual(status, (int)createValues["Status"]);

            Assert.IsNotNull(updateValues);
            Assert.IsTrue(updateValues.Count == 1);
            Assert.AreEqual(status, (int) updateValues["Status"]);
        }

        [TestMethod]
        public void CreateMappingTest()
        {
            var mappings = new Mappings();

            Assert.IsNull(mappings.GetMapping(typeof(CaseFile)));

            new MappingBuilder<CaseFile>(mappings)
                .ForCreate()
                .Map(x => x.Status)
                .Create();

            Assert.IsNotNull(mappings.GetMapping(typeof(CaseFile)));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void KeyNotFoundTest()
        {
            var mapping = new MappingBuilder<CaseFile>()
                .ForCreate()
                .Map(x => x.Title)              
                .GetMapping();

            var obj = new CaseFile {Status = 3};
            mapping.GetUpdateValues(obj); //only create mapping created, so this should fail
        }
    }
}