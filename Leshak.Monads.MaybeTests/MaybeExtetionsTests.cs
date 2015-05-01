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
    public class MaybeExtetionsTests
    {
        //test data
        People peopleWithNull;
        People peopleWithData;

        [SetUp]
        public void SetUp()
        {
            // test data
            peopleWithNull = new People();
            peopleWithData = new People(){
                Address=new  Address(){
                    HouseName="Some Name"
                }
            };
        }

        [Test]
        public void With_NullInput_ShouldReturn_NUll()
        {
            People people = null;

            //action
            var name = people.With(p => p.Address).With(p => p.HouseName);
            
            //asserts
            name.Should().BeNull();

        }

        [Test]
        public void With_NullInChain_ShouldReturn_Null()
        {

            //action
            var name = peopleWithNull.With(p => p.Address).With(p => p.HouseName);

            //asserts
            name.Should().BeNull();
        }

        [Test]
        public void With_AllHasValues_ShouldReturn_Value()
        {
            

            //action
            var name = peopleWithData.With(p => p.Address).With(p => p.HouseName);

            //asserts
            name.Should().Be("Some Name");
        }

        [Test]
        public void Result_NullInChain_ShouldReturn_FailValue()
        {
            //action
            var name = peopleWithNull.With(p => p.Address).With(p => p.HouseName)
                        .Return(x=>x,"Default value");

            //asserts
            name.Should().Be("Default value");
        }

        [Test]
        public void Result_AllHasValues_ShouldReturn_Value()
        {
            

            //action
            var name = peopleWithData.With(p => p.Address).With(p => p.HouseName)
                        .Return(x => x, "Default value");

            //asserts
            name.Should().Be("Some Name");
        }
    }
}
