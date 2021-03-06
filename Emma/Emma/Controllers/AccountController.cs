﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.MVC6.DataContext;
using AspnetNote.MVC6.ViewModel;
using Emma.Models;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Login(LoginViewModelClass model)
        {
            //Id, Pw -필수 
            //using 문에 한 줄이라도 줄면 속도가 빨라진다
            if (ModelState.IsValid)
            {

                using (var db = new AspnetNoteDbContext())
                {
                    //Linq - 메서드 체이닝
                    //=> : A Go to B 
                    // == 메모리에 누수가 발생할 수 있다. UserID는 DB에서 받아왔을 거고 ==는 새로운 객체, String을 선언하게 된다. 그래서 일치여부는 
                    //
                    //var user = db.Users.FirstOrDefault(u=>u.UserId == model.UserId && u.UserPassword == model.UserPassword);
                    //equals를 사용해야 메모리누수가 발생하지않는다. 
                    var user = db.Users.FirstOrDefault(u=>u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));

                    if(user != null)
                    {
                        //로그인 성공 페이지로 이동
                        //HttpContext.Session.SetInt32(key, value);
                        //로그인시 등재 로그아웃되면 빠져나감
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);
                        return RedirectToAction("LoginSuccess", "Home"); 
                    }

                }
                //로그인에 실패했을 때
                ModelState.AddModelError(string.Empty, "로그인 정보가 올바르지 않습니다.");             


            }



            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");

            //HttpContext.Session.Clear();

            return RedirectToAction("Index","Home");
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
