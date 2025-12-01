using Core.Interfaces.Services;
using FruitShopReborn.Models.Ai;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopReborn.Controllers;

public class AiController(IAiService aiService,
    MarkdownPipeline markdownPipeline) : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AskAi(AiViewModel model)
    {
        if (!ModelState.IsValid) return View("Index",model);
        
        TempData["Response"] = Markdown.ToHtml(aiService.Ask(model.Prompt!) ?? "", markdownPipeline);
        return RedirectToAction("Index");
    }
}