using System.Threading.Tasks;

namespace TFPAW.Services
{
	public interface IChatGPTService
	{
		Task<string> GetNextQuestion(string previousAnswer);
	}
}
