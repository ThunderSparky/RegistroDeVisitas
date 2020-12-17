using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Visitas.Models;
using Visitas.Mvc.ExternalServices;
using Visitas.Mvc.Models;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken) { }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {   //Se coloca como parametro "returnUrl" para que luego de logearte te mande en donde te quedaste
            return View(new UserViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(UserViewModel user)
        {
            //Si no es valido, devolvemos la vista
            if (!ModelState.IsValid) return View(user);
            //Si es valido, obtenemos el usuario que ingreso (email, password)
            var validUser = _unit.Users.ValidateUser(user.Email, user.Password);
            //Evaluamos la variable, si es null entonces no se pudo logear xk no hubo coherencia entre el EMAIL o PASSWORD
            if (validUser == null)
            {
                ModelState.AddModelError("Error", "Invalid email or password");
                return View(user);
            }

            var token = GetToken(user.Email, user.Password);

            //En caso sean iguales el EMAIL y PASSWORD, continuamos
            //Este objeto Claim, me sirve para manejar una lista de CLAIMs, para manejar la información del usuario durante la ejecución de la app
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, validUser.Email),
                new Claim(ClaimTypes.Role, validUser.Roles),
                new Claim(ClaimTypes.Name, $"{validUser.FirstName} {validUser.LastName}"),
                new Claim(ClaimTypes.NameIdentifier, validUser.Email),
                new Claim(ClaimTypes.UserData, token)
            }, "ApplicationCookie");
            //Este context, me permite manipular objetos que se comparten durante la ejecucion de la aplicacion 
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;// context.Authentication, es un middleware para validar las peticiones 

            authManager.SignIn(identity); //Con esto nos logueamos

            return RedirectToLocal(user.ReturnUrl);//Lo manda en donde se quedo el usuario en el último instante
        }
        private string GetToken(string email, string password)
        {
            var httpClient = new HttpClient();
            var credential = new Dictionary<string, string>
            {
                {"grant_type", "password" },
                {"username", email },
                {"password", password}
            };
            var response = httpClient.PostAsync("https://localhost:44329/token",
                    new FormUrlEncodedContent(credential));
            var responseContent = response.Result.Content.ReadAsStringAsync().Result;
            var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
            var tokenString = tokenDictionary["access_token"];
            return tokenString;
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new RegisterUserViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterUserViewModel userView)
        {
            if (!ModelState.IsValid) return View(userView);
            //Si fuera valido el userView
            Users user = new Users
            {
                Email = userView.Email,
                FirstName = userView.FirstName,
                LastName = userView.LastName,
                Password = userView.Password,
                Roles = userView.Roles
            };
            var validUser = _unit.Users.CreateUser(user);
            if (validUser == null)
            {
                ModelState.AddModelError("Error", "Correo o contraseña invalida");
                return View(userView);
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Logout()
        {
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;// context.Authentication, es un middleware para validar las peticiones 

            authManager.SignOut("ApplicationCookie"); //Con esto nos deslogueamos, indicando como parametro el tipo de autenticacion que se uso, que es "ApplicationCookie"
            return RedirectToAction("Login", "Account"); //Luego de desloguearlo, lo mandamos al Login
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {   //Si la url es local entonces retornara lo que diga
                return Redirect(returnUrl);
            }
            //Si no es una url de la app, entonces retorna al Home
            return RedirectToAction("Index", "Home");
        }
    }
}