using CustomIdentitySample.Models.Auth;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.Owin.Security;

namespace CustomIdentitySample.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // カスタマイズのポイント：
                //   UserManagerのインスタンス作成時、
                //     型引数:カスタマイズしたユーザー情報
                //     コンストラクタの引数：カスタマイズしたユーザストアのインスタンス
                //   をそれぞれ渡す
                var userManager = new UserManager<MyAppUser>(new MyAppUserStore());

                // カスタマイズのポイント：
                //   パスワードのハッシュ化アルゴリズムとして、IPasswordHasherを実装したカスタムクラスのインスタンスを設定
                userManager.PasswordHasher = new MyPasswordHasher();

                var user = await userManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    var authentication = this.HttpContext.GetOwinContext().Authentication;
                    var identify = await userManager.CreateIdentityAsync(
                        user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identify);

                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "ログインIDまたはパスワードが無効です。");
                    
                }
            }

            // ここで問題が発生した場合はフォームを再表示します
            return View(model);
        }
    }
}