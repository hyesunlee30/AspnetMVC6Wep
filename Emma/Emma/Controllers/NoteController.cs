using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.MVC6.DataContext;
using Emma.Models;
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

            using (var db = new AspnetNoteDbContext())
            {
                //모두 출력
                var list = db.Notes.ToList();
                return View(list);
            }

            
        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
       public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Note model)
        {

            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    db.Notes.Add(model);
                }

            }
            return View();
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {

            return View();
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            return View();
        }
    }
}