using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TesteEditoraLivros.Application.DTO;
using TesteEditoraLivros.Application.Interface;
using TesteEditoraLivros.Models;

namespace TesteEditoraLivros.Controllers
{
    public class AccountController : Controller
    {
        //Apesar dessa Controller Account existir, ela foi criado manualmente.

        private readonly IApplicationServiceAccount _serviceAccount;

        public AccountController(IApplicationServiceAccount applicationServiceAccount)
        {
            _serviceAccount = applicationServiceAccount;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Register()
        {
            try
            {
                //por aqui só as consultas básicas 
                var listRole = await _serviceAccount.GetAllRoles();
                var listRegister = await _serviceAccount.GetAllAccounts();
                return View(new RegisterModelView(listRole,listRegister));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
         
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterModelView model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                _serviceAccount.CreateAccount(
                    new AccountDTO(model.RoleProfile,model.Email, model.Password, model.AvatarUrl, model.Name, model.NickName));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Books","Book");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditAccount(int id)
        {
            try
            {
                var account = await _serviceAccount.GetAccountById(id);
                var listRole = await _serviceAccount.GetAllRoles();
                return View(
                    new RegisterModelView(account.Id, account.UserId, account.Name,account.NickName,account.AvatarUrl,account.RoleId,listRole));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccount(RegisterModelView model,string op)
        {
            try
            {
                var accountDto = new AccountDTO(model.Id,model.RoleProfile,model.UserId, model.Email, model.Password, model.AvatarUrl, model.Name, model.NickName);
                if("Atualizar".Equals(op))_serviceAccount.UpdateAccount(accountDto);
                if ("Deletar".Equals(op)) _serviceAccount.DeleteAccount(accountDto);
                ViewBag.Url = "/Account/Register";
                return PartialView("_SaveChangesConfirm");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("Books", "Book");
        }

       
    }
}
