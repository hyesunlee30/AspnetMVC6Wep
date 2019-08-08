using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.MVC6.DataContext;
using Emma.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetNote.MVC6.Controllers
{
    public class NoteController : Controller
    {

        /// <summary>
        /// 게시판 리스트 출력
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {           
            if(HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                using (var db = new AspnetNoteDbContext())
                {
                    //모두 출력
                    var list = db.Notes.ToList();
                    return View(list);
                }
            }            
        }

        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }

        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
       public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
           
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {

            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //int로 cast
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());

            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    db.Notes.Add(model);

                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");

                    }


                }
                //전형적인 에러메시지
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            }
            return View(model);
                                         
        }

        public IActionResult Edit(int noteNo)
        {
                       
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }

        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Note model)
        {

            var noteNo = model.NoteNo;

            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    using (var db = new AspnetNoteDbContext())
                    {

                        var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));

                        if( note != null)
                        {
                            note.NoteTitle = model.NoteTitle;
                            note.NoteContents = model.NoteContents;

                            db.SaveChanges();

                            return Redirect("Index");
                        }

                    }
                    //전형적인 에러메시지
                    ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
                }
                return View(model);
            }
            
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int noteNo)
        {

            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));

                if (note != null)
                {
                    db.Notes.Remove(note);

                    db.SaveChanges();

                    return Redirect("Index");
                }

            }

            return View();
        }
    }
}