using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Podcast.DAL;
using Podcast.Models;
using System.IO;

namespace Podcast.Controllers
{
    public class PodcastController : Controller
    {
        private PodcastContext db = new PodcastContext();

        // GET: Podcast
        public ActionResult Index()
        {
            return View(db.Podcasts.ToList());
        }

        public ActionResult IndexJSON()
        {
            return Json(db.Podcasts.ToList().OrderByDescending(p => p.dtGravacao), JsonRequestBehavior.AllowGet);
        }

        // GET: Podcast/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PodcastBase podcastBase = db.Podcasts.Find(id);
            if (podcastBase == null)
            {
                return HttpNotFound();
            }
            return View(podcastBase);
        }

        // GET: Podcast/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Podcast/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PodcastBaseID,dsTitulo,dsPodcast,dtGravacao,nrEdicao,nmArquivoAudio,nmArquivoImagem")] PodcastBase podcastBase)
        {
            if (ModelState.IsValid)
            {
                db.Podcasts.Add(podcastBase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(podcastBase);
        }

        // GET: Podcast/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PodcastBase podcastBase = db.Podcasts.Find(id);
            if (podcastBase == null)
            {
                return HttpNotFound();
            }
            return View(podcastBase);
        }

        // POST: Podcast/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PodcastBaseID,dsTitulo,dsPodcast,dtGravacao,nrEdicao,nmArquivoAudio,nmArquivoImagem")] PodcastBase podcastBase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(podcastBase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(podcastBase);
        }

        // GET: Podcast/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PodcastBase podcastBase = db.Podcasts.Find(id);
            if (podcastBase == null)
            {
                return HttpNotFound();
            }
            return View(podcastBase);
        }

        // POST: Podcast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PodcastBase podcastBase = db.Podcasts.Find(id);
            db.Podcasts.Remove(podcastBase);
            db.SaveChanges();
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

        public ActionResult Upload()
        {
            int arquivosSalvos = 0;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];

                //Salva o arquivo
                if (arquivo.ContentLength > 0)
                {
                    var uploadPath = Server.MapPath("~/media");
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }
            }
            return RedirectToAction("Index");
        }
    }
}
