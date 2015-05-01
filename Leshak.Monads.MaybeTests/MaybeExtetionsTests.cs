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

        [Test]
        public void IsNull_NullInChain_ShouldReturn_True()
        {
            peopleWithNull
                .With(p => p.Address).With(p => p.HouseName)
                .IsNull()
                // assert
                  .Should().Be(true);
        }

        [Test]
        public void IsNull_AllHasValues_ShouldReturn_False()
        {
            peopleWithData
                .With(p => p.Address).With(p => p.HouseName)
                .IsNull()
                // assert
                  .Should().Be(false);
        }


        [Test]
        public void IsNotNull_NullInChain_ShouldReturn_False()
        {
            peopleWithNull
                .With(p => p.Address).With(p => p.HouseName)
                .IsNotNull()
                // assert
                  .Should().Be(false);
        }

        [Test]
        public void IsNotNull_AllHasValues_ShouldReturn_True()
        {
            peopleWithData
                .With(p => p.Address).With(p => p.HouseName)
                .IsNotNull()
                // assert
                  .Should().Be(true);
        }

        [Test]
        public void If_NullInChain_ShouldReturnNull()
        {
            peopleWithNull
                .With(p => p.Address)
                .If(p => true) // input is null, evaluator return false
                // assert
                .Should().BeNull();
        }

        [Test]
        public void If_EvaluatorReturnFalse_ShouldReturnNull()
        {
            peopleWithData
                .With(p => p.Address)
                .If(p => p.HouseName.Length<3) // input is not null, evaluator return false
                // assert
                .With(p => p.HouseName)
                .Should().BeNull();
        }

        [Test]
        public void If_EvaluatorReturnTrue_ShouldReturnValue()
        {
            peopleWithData
                .With(p => p.Address)
                .If(p => p.HouseName.Length > 3) // input is not null, evaluator return false
                // assert
                .With(p=>p.HouseName)
                .Should().Be("Some Name");
        }

        [Test]
        public void Do_NullInChain_DoNotExecuted()
        {
            bool executed=false;

            peopleWithNull
                .With(p => p.Address)
                .Do(p => executed = true)
                .Should().BeNull();

            //assert
            executed.Should().BeFalse();

        }

        [Test]
        public void Do_NullInChain_Executed()
        {
            bool executed = false;

            peopleWithData
                .With(p => p.Address)
                .Do(a => {
                    executed = true;
                    a.Should().Be(peopleWithData.Address);// check value passed to action
                })
                .Should().Be(peopleWithData.Address); // check value passed to chain

            //assert
            executed.Should().BeTrue();

        }
    }
}
