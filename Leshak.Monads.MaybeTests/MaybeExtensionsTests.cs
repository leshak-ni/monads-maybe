﻿using System;
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
    public class MaybeExtensionsTests
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
        public void With_NullInput_ShouldReturn_Null()
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
        public void Default_AllHasValue_ShouldReturnValue()
        {
            var name = peopleWithData.With(p => p.Address).With(p => p.HouseName)
                .Default("default name"); // show not returned

            //assert
            name.Should().Be(peopleWithData.Address.HouseName);
        }

        [Test]
        public void Default_TargetPropIsNull_ShouldReturnDefaultValue()
        {
            //arrange
            peopleWithData.Address.HouseName = null; // default value should returned

            var name = peopleWithData.With(p => p.Address).With(p => p.HouseName)
                .Default("default name"); // show not returned

            //assert
            name.Should().Be("default name");
        }

        [Test]
        public void Default_NullInChain_ShouldReturnDefaultValue()
        {
            
            var name = peopleWithNull.With(p => p.Address).With(p => p.HouseName)
                .Default("default name"); // show not returned

            //assert
            name.Should().Be("default name");
        }

     
        [Test]
        public void If_NullInChain_ShouldReturnNull()
        {
            peopleWithNull
                .With(p => p.Address) // it's null
                .If(p => true) // evaluator return true,input is null 
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
        public void Do_AllHasValues_Executed()
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
