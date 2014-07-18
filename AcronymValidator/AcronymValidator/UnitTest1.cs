using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AcronymValidator;

namespace AcronymValidatorTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GoogleSearchEngine_isValid()
        {
            List<string> googleSearchEngine = new List<string>("Google Search Engine".ToUpper().Split(null));

            Assert.IsTrue(Utility.isValid("GSE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("GOOSE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("GEANIE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("OOSN", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("LEACHE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("OOGLEE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("GOOSEEE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("GGSEE", googleSearchEngine));
            Assert.IsTrue(Utility.isValid("GGSE", googleSearchEngine));

            Assert.IsFalse(Utility.isValid("SGE", googleSearchEngine));
            Assert.IsFalse(Utility.isValid("GEANIEOOSN", googleSearchEngine));
            Assert.IsFalse(Utility.isValid("OGGLEE", googleSearchEngine));
            Assert.IsFalse(Utility.isValid("GGG", googleSearchEngine));
            Assert.IsFalse(Utility.isValid("GGSEA", googleSearchEngine));
        }

        [TestMethod]
        public void AAcroBat_isValid()
        {
            List<string> aacrobat = new List<string>("A Acro Bat".ToUpper().Split(null));

            Assert.IsFalse(Utility.isValid("ABA", aacrobat));
        }

        [TestMethod]
        public void AardvarksAAAnonymous_isValid()
        {
            List<string> aardvarks = new List<string>("Aardvarks AA Anonymous".ToUpper().Split(null));

            Assert.IsFalse(Utility.isValid("AAASA", aardvarks));
        }

        [TestMethod]
        public void GZT3k_isValid()
        {
            List<string> GZT3k = new List<string>("GISi Zombie Tracker 3000".ToUpper().Split(null));

            Assert.IsTrue(Utility.isValid("GZT3", GZT3k));
            Assert.IsFalse(Utility.isValid("GZT3k", GZT3k));
        }

        [TestMethod]
        public void Rec4_isValid()
        {
            List<string> Rec4 = new List<string>("AAB BCC DDE EFF".ToUpper().Split(null));

            Assert.IsTrue(Utility.isValid("ABCEE", Rec4));
            Assert.IsFalse(Utility.isValid("ABCE", Rec4));
        }

        [TestMethod]
        public void tDup_isValid()
        {
            Assert.IsTrue(Utility.isValid("ABDEEFHH", "AAB BCC DDE EFF GGH HII".Split().ToList()));
        }

        [TestMethod]
        public void tBlank_isValid()
        {
            Assert.IsFalse(Utility.isValid("", "AAB BCC DDE EFF GGH HII".Split().ToList()));
            Assert.IsFalse(Utility.isValid("ABDEEFHH", (new string[] { }).ToList()));
            Assert.IsFalse(Utility.isValid("ABDEEFHH", (new string[] { "" }).ToList()));
        }
    }
}
