using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Leshak.Monads.Maybe;
using FluentAssertions;

namespace Leshak.Monads.MaybeTests
{
    [TestFixture]
    public class MaybeByExpressionExtentionsTests
    {
        //test data
        People peopleWithNull;
        People peopleWithData;

        [SetUp]
        public void SetUp()
        {
            // test data
            peopleWithNull = new People();
            peopleWithData = new People()
            {
                Address = new Address()
                {
                    HouseName = "Some Name"
                }
            };
        }

        [Test]
        public void Maybe_NullInput_ShouldReturn_NUll()
        {
            People people = null;

            //action
            var name = people.Maybe(p => p.Address.HouseName);

            //asserts
            name.Should().BeNull();

        }

        [Test]
        public void Maybe_NullInChain_ShouldReturn_Null()
        {

            //action
            var name = peopleWithNull.Maybe(p => p.Address.HouseName);

            //asserts
            name.Should().BeNull();
        }

        [Test]
        public void Maybe_AllHasValues_ShouldReturn_Value()
        {


            //action
            var name = peopleWithData.Maybe(p => p.Address.HouseName);

            //asserts
            name.Should().Be("Some Name");
        }

        [Test]
        public void MaybeOrig_AllHasValues_ShouldReturn_Value()
        {


            //action
            var name = MaybeByExpressionExtentions.Maybe(()=>peopleWithData.Address.HouseName);

            //asserts
            name.Should().Be("Some Name");
        }

    }
}
