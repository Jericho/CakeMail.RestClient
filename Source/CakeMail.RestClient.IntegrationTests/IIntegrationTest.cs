using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CakeMail.RestClient.IntegrationTests
{
	public interface IIntegrationTest
	{
		Task Execute(ICakeMailRestClient client, string userKey, long clientId, TextWriter log, CancellationToken cancellationToken);
	}
}
