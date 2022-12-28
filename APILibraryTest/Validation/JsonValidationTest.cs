using APILibrary.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APILibraryTest.Validation
{
    public class JsonValidationTest
    {
        [Theory]
        [InlineData("{ \"ErrorName\": \"Slut\", \"ErrorMessage\": \"UrDadLeft\" }", false)]
        [InlineData("{\"IsAllowed\":true, \"HasWon\":false, \"HasDrawn\":false}", true)]
        public void IsJsonAPIResponseMoveModelTest(string json, bool expected)
        {
            bool actual = APIValidation.IsJsonAPIResponseMoveModel(json);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("{\"ErrorName\":\"ErrorName\", \"ErrorMessage\":\"ErrorMessage\"}", true)]
        [InlineData("{\"WetWipe\":\"ErrorName\", \"ErrorMessage\":\"ErrorMessage\"}", false)]
        [InlineData("{\"IsAllowed\":true, \"HasWon\":false, \"HasDrawn\":false}", false)]
        public void IsJsonAPIErrorModelTest(string json, bool expected)
        {
            bool actual = APIValidation.IsJsonAPIErrorModel(json);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("{\"IsSuccessful\":true}", true)]
        [InlineData("{\"Slut\":true}", false)]
        [InlineData("{\"IsSuccessful\":balls}", false)]
        [InlineData("{\"Slut\":balls}", false)]
        public void IsJsonAPIResponseInitModelTest(string json, bool expected)
        {
            bool actual = APIValidation.IsJsonAPIResponseInitModel(json);

            Assert.Equal(expected, actual);
        }
    }
}
