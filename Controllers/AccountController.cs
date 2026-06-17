using Almocar2.Models;
using Almocar2.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Almocar2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserAcount> _userManager;
        private readonly SignInManager<UserAcount> _signManager;

        public AccountController(UserManager<UserAcount> userManager, SignInManager<UserAcount> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user != null)
            {
                var result = await _signManager.PasswordSignInAsync(user.UserName,

                loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(loginVM.ReturnUrl);
                    }
                }
            }
            ModelState.AddModelError("", "Falha ao fazer login");
            return View(loginVM);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistroViewModel registroVm)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAcount
                {
                    UserName = registroVm.UserName,
                    Nome = registroVm.Nome,
                    Endereco = registroVm.Endereco,
                    Numero = registroVm.Numero,
                    Bairro = registroVm.Bairro,
                    Cidade = registroVm.Cidade,
                    Estado = registroVm.Estado,
                    Cep = registroVm.Cep
                };
                var result = await _userManager.CreateAsync(user,

                registroVm.Password);

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        string mensagem = error.Description;
                        
                        // Traduzir mensagens de validação de senha
                        if (mensagem.Contains("Passwords must be at least"))
                            mensagem = "A senha deve ter no mínimo 8 caracteres.";
                        else if (mensagem.Contains("Passwords must have at least one non alphanumeric character"))
                            mensagem = "A senha deve conter pelo menos um caractere especial (!@#$%^&*)";
                        else if (mensagem.Contains("Passwords must have at least one uppercase"))
                            mensagem = "A senha deve conter pelo menos uma letra maiúscula.";
                        else if (mensagem.Contains("Passwords must have at least one lowercase"))
                            mensagem = "A senha deve conter pelo menos uma letra minúscula.";
                        else if (mensagem.Contains("Passwords must have at least one digit"))
                            mensagem = "A senha deve conter pelo menos um número.";
                        else if (mensagem.Contains("already taken"))
                            mensagem = "Este email já está registrado.";
                        
                        ModelState.AddModelError("Registro", mensagem);
                    }
                }
            }
            return View(registroVm);
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}