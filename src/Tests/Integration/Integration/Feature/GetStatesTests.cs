using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Octogami.ProviderDirectory.Application.Feature.GetStates;
using Octogami.ProviderDirectory.Tests.Integration.TestSupport;

namespace Octogami.ProviderDirectory.Tests.Integration.Feature
{
	public class GetStatesTests
	{
		[Test]
		public void GetStates_HappyPath()
		{
			// Arrange
			var container = TestContainerFactory.New();
			var mediator = container.GetInstance<Mediator>();

			// Act
			var results = mediator.Send(new GetStatesRequest());

			// Assert
			results.Count().Should().Be(50);
		}
	}
}
