using FluentValidation;
using Marten;
using MediatR;
using Octogami.ProviderDirectory.Application.Domain;
using Octogami.ProviderDirectory.Application.Pipeline;
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
				scanner.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
			});

			// Mediator registrations not handled during assembly scanning
			For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
			For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

			// TODO: The ValidationHandler requires an IValidator<T>. Currently, if
			// there's no type that implements IValidator<T>, a dependency resolution exception is thrown.
			// Practically that means that a validator needs to be defined for every request,
			// even if there's nothing to validate.
			// Figure this out.
			var handlerType = For(typeof(IRequestHandler<,>));
			handlerType.DecorateAllWith(typeof(ValidationHandler<,>));

			// Marten registrations
			ForSingletonOf<IDocumentStore>().Use("Build the DocumentStore", () =>
			{
				return DocumentStore.For(_ =>
				{
					_.Connection("host=localhost;database=ProviderDirectory;password=password;username=postgres");
					_.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

					_.Schema.For<Provider>().Index(x => x.NPI, x =>
					{
						x.IsUnique = true;
					});
				});
			});

			For<IDocumentSession>().Use("Lightweight Session", c => c.GetInstance<IDocumentStore>().LightweightSession());
		}
	}
}