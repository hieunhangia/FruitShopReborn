using Core.Interfaces.Services;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Service;

public class AiService(IChatCompletionService chatCompletionService) : IAiService
{
    public string? Ask(string prompt)
    {
        var response = chatCompletionService.GetChatMessageContentAsync(prompt);
        return response.Result.Content;
    }
}