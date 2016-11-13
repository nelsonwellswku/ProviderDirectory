namespace Octogami.ProviderDirectory.Application.Pipeline
{
	public interface IValidator<in T>
	{
		void Validate(T request);
	}
}