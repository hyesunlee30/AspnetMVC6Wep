using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.MVC6.DataContext;
using Emma.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetNote.MVC6.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        // GET: /<controller>/ <-설정 안 해놓으면 http get 방식으로 설정된다
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // GET: /<controller>/
        /// <summary>
        /// 회원가입
        /// 로그인에서 받아온 폼 정보를 받아야 하기 떄문에
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(User model)
        {

            

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 회원가입 전송 
        ///  유효화검사
        ///  Required설정된 부분이 Null이 아닌 상태에서 add 시키기 위해
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                //Java try(sqlSession) {} catch(){}
                //데이터베이스 입출력을 할 때에 openconnection after close 
                using (var db = new AspnetNoteDbContext())
                {
                    //메모리까지 올리기
                    db.Users.Add(model);
                    //sql로 저장하기 
                    db.SaveChanges();
                }
                //Home컨트롤러에 있는 Index action으로 리다이렉트하겠다.
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}
