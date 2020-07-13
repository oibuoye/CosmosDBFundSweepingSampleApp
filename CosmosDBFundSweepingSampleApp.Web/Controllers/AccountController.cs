using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using CosmosDBFundSweepingSampleApp.Core.Exceptions;
using CosmosDBFundSweepingSampleApp.Core.HelperModels;
using CosmosDBFundSweepingSampleApp.Core.Logger.Contracts;
using CosmosDBFundSweepingSampleApp.Core.Services.Contracts;
using CosmosDBFundSweepingSampleApp.Web.Controllers.Handlers.Contracts;
using CosmosDBFundSweepingSampleApp.Web.Models.ViewModels;

namespace CosmosDBFundSweepingSampleApp.Web.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {

        private readonly IAccountRequestHandler _accountRequestHandler;
        private readonly IBankManager _bankManager;

        private static ILogger _log;
        private static readonly int dataPickSize = 20;

        public AccountController(ILogger log, IAccountRequestHandler accountRequestHandler, IBankManager bankManager)
        {
            _accountRequestHandler = accountRequestHandler;
            _bankManager = bankManager;
            _log = log;
        }
        
        public async Task<IActionResult> Index(int page)
        {
            _log.Info($"Get account list");
            if (TempData.ContainsKey("Message"))
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            var accounts = await _accountRequestHandler.GetAccounts(page, dataPickSize);
            return View(accounts);
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            _log.Info($"Account creation");
            ViewBag.Banks = _bankManager.GetCollection().Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccount([FromForm] CreateAccountViewModel model)
        {
            try
            {
                string dump = JsonConvert.SerializeObject(model);
                _log.Info($"Account creation beginning of the request: {dump}");
                if (ModelState.IsValid)
                {
                    if(model.AccountNumber.Length != 10)
                    {
                        BindViewBag(model.BankId);
                        ViewBag.Error = "Account number should be 10 digits";
                        return View(model);
                    }

                    await _accountRequestHandler.CreateAccount(new CreateAccountModel
                    {
                        AccountName = model.AccountName,
                        AccountNumber = model.AccountNumber,
                        BankId = model.BankId
                    });

                    TempData["Message"] = $"{model.AccountName} account created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _log.Info($"Invalid account details, Kindly fill the form correctly");
                    BindViewBag(model.BankId);
                    ViewBag.Error = "Invalid account details, Kindly fill the form correctly";
                    return View(model);
                }
            }
            catch (AccountDetailsAlreadyExistException ex)
            {
                BindViewBag(model.BankId);
                ViewBag.Error = $"Error!! {ex.Message}";
                _log.Error($"Error creating account", ex);
                return View();
            }
            catch (Exception ex)
            {
                BindViewBag(model.BankId);
                ViewBag.Error = $"Error!! unable to create account";
                _log.Error($"Error creating account", ex);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            ViewBag.Banks = _bankManager.GetCollection().Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
            return View(await _accountRequestHandler.GetAccount(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] AccountDetailsModel model)
        {
            try
            {
                string dump = JsonConvert.SerializeObject(model);
                _log.Info($"update account account. dump::: {dump}");
                if (ModelState.IsValid)
                {
                    await _accountRequestHandler.UpdateAccount(model);
                    TempData["Message"] = $"{model.AccountName} updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _log.Info($"Invalid account details, Kindly fill the form correctly");
                    BindViewBag(model.BankId);
                    ViewBag.Error = "Invalid account details, Kindly fill the form correctly";
                    return View(model);
                }
            }
            catch (AccountDetailsAlreadyExistException ex)
            {
                BindViewBag(model.BankId);
                ViewBag.Error = $"Error!! {ex.Message}";
                _log.Error($"Error {ex}", ex);
                return View(model);
            }
            catch (Exception ex)
            {
                BindViewBag(model.BankId);
                ViewBag.Error = $"Error!! unable to update account.";
                _log.Error($"Error updating account.", ex);
                return View(model);
            }
        }

        private void BindViewBag(string bankId)
        {
            _log.Info($"About to get bank list");
            ViewBag.Banks = _bankManager.GetCollection().Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() });
        }

        public IActionResult GetAccountsMoveToNextPage(int page)
        {
            return Json(_accountRequestHandler.GetAccounts(page, dataPickSize));
        }

    }
}