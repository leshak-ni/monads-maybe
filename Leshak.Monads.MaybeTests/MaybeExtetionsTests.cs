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
            People people = new People();

            //action
            var name = people.With(p => p.Address).With(p => p.HouseName);

            //asserts
            name.Should().BeNull();
        }

        [Test]
        public void With_AllHasValues_ShouldReturn_Value()
        {
            People people = new People()
            {
                Address = new Address()
                {
                    HouseName="Some Name"
                }
            };

            //action
            var name = people.With(p => p.Address).With(p => p.HouseName);

            //asserts
            name.Should().Be("Some Name");
        }
    }
}
