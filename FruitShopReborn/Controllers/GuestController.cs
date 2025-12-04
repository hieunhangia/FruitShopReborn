using System.Security.Claims;
using Core.Interfaces.Services;
using FruitShopReborn.Models;
using Markdig;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopReborn.Controllers;

[Route("/[action]")]
public class GuestController(IAiService aiService,
    MarkdownPipeline markdownPipeline) : Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index() => View();
    
    [HttpGet]
    public IActionResult AskAi() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AskAi(AiViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        TempData["Response"] = Markdown.ToHtml(aiService.Ask(model.Prompt!) ?? "", markdownPipeline);
        return RedirectToAction("AskAi");
    }
}