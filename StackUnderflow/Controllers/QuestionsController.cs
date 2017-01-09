using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StackUnderflow.Models;
using Microsoft.AspNet.Identity;

namespace StackUnderflow.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public async Task<ActionResult> Index()
        {
			var questions = db.Questions.OrderByDescending(Question => Question.QuestionDate).Take(10);
			return View(await questions.ToListAsync());
        }

		// GET: Questions
		public async Task<ActionResult> MyList()
		{
			var questions = db.Questions.Include(q => q.Poster);
			return View(await questions.ToListAsync());
		}

		// GET: Questions/Details/5
		public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

		// GET: Questions/Create
		[Authorize]
		public ActionResult Create()
        {
			//ViewBag.PosterId = new SelectList(db.ApplicationUsers, "Id", "DisplayName");
			return View();
        }

		// POST: Questions/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Title,Body,QuestionDate,Upvotes,Downvotes,ViewCount,PosterId")] Question question)
        {
            if (ModelState.IsValid)
            {
				question.QuestionDate = DateTime.Now;
				question.PosterId = HttpContext.User.Identity.GetUserId();

				db.Questions.Add(question);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            //ViewBag.PosterId = new SelectList(db.ApplicationUsers, "Id", "DisplayName", question.PosterId);
            return View(question);
        }

		// GET: Questions/Edit/5
		[Authorize]
		public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            //ViewBag.PosterId = new SelectList(db.ApplicationUsers, "Id", "DisplayName", question.PosterId);
            return View(question);
        }

		// POST: Questions/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Body,QuestionDate,Upvotes,Downvotes,ViewCount,PosterId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewBag.PosterId = new SelectList(db.ApplicationUsers, "Id", "DisplayName", question.PosterId);
            return View(question);
        }

		// GET: Questions/Delete/5
		[Authorize]
		public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = await db.Questions.FindAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

		// POST: Questions/Delete/5
		[Authorize]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Question question = await db.Questions.FindAsync(id);
            db.Questions.Remove(question);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
