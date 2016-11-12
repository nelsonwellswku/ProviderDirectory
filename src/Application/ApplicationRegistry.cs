using Marten;
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

			// Mediator registrations not handled during assembly scanning
			For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
			For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

			// Marten registrations
			ForSingletonOf<IDocumentStore>().Use("Build the DocumentStore", () =>
			{
				return DocumentStore.For(_ =>
				{
					_.Connection("host=localhost;database=ProviderDirectory;password=password;username=postgres");
					_.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

					// other Marten configuration options
				});
			});

			For<IDocumentSession>().Use("Lightweight Session", c => c.GetInstance<IDocumentStore>().LightweightSession());
		}
	}
}