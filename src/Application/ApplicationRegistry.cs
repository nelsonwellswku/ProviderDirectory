using MediatR;
using StructureMap;

namespace Octogami.ProviderDirectory.Application
{
	public class ApplicationRegistry : Registry
	{
		public ApplicationRegistry()
		{
			Scan(scanner =>
			{
				scanner.AssemblyContainingType(typeof(ApplicationRegistry));
				scanner.AssemblyContainingType<IMediator>();
				scanner.WithDefaultConventions();
				scanner.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
				scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncRequestHandler<,>));
				scanner.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
				scanner.ConnectImplementationsToTypesClosing(typeof(IAsyncNotificationHandler<>));
			});

			For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
			For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
		}
	}
}