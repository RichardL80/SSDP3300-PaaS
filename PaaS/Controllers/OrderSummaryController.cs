using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaaS.Models;

namespace PaaS.Controllers;

public class OrderSummaryController : Controller
{
    private readonly ILogger<OrderSummaryController> _logger;

    public OrderSummaryController(ILogger<OrderSummaryController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult PayPal()
    {
        return View();
    }

    public IActionResult PayPalConfirmation(PayPalConfirmationModel payPalConfirmationModel)
    {
        return View(payPalConfirmationModel);
    }

}