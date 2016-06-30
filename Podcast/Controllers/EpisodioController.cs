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
    public class EpisodioController : Controller
    {
        private PodcastContext db = new PodcastContext();

        // GET: Podcast
        public ActionResult Index()
        {
            return View(db.Episodios.ToList());
        }

        public ActionResult IndexJSON()
        {
            return Json(db.Episodios.ToList().OrderByDescending(p => p.dtGravacao), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(Episodio episodio)
        {
            if ((ModelState.IsValid) &&
                (episodio.dsTitulo != ""))
            {
                if (episodio.EpisodioID == 0)
                    db.Episodios.Add(episodio);
                else
                    db.Entry(episodio).State = EntityState.Modified;

                db.SaveChanges();
            }
            else
            {
                throw new FormatException("Informações inválidas");
            }

            return RedirectToAction("Index");
        }

        // GET: Podcast/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Episodio podcastBase = db.Episodios.Find(id);
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
        public ActionResult Edit([Bind(Include = "PodcastBaseID,dsTitulo,dsPodcast,dtGravacao,nrEdicao,nmArquivoAudio,nmArquivoImagem")] Episodio podcastBase)
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
            Episodio podcastBase = db.Episodios.Find(id);
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
            Episodio podcastBase = db.Episodios.Find(id);
            db.Episodios.Remove(podcastBase);
            db.SaveChanges();

            DeleteArquivo("/media/audio/" + podcastBase.nmArquivoAudio);
            DeleteArquivo("/media/image/" + podcastBase.nmArquivoImagem);

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
                var uploadPath = Server.MapPath("~/media");
                var audioPath = "/audio";
                var imagePath = "/image";
                string caminhoArquivo = "";

                HttpPostedFileBase arquivo = Request.Files[i];

                //Salva o arquivo
                if (arquivo.ContentLength > 0)
                {
                    if (arquivo.ContentType.ToString().Contains("audio"))
                        caminhoArquivo = Path.Combine(@uploadPath + audioPath, Path.GetFileName(arquivo.FileName));
                    else if (arquivo.ContentType.ToString().Contains("image"))
                        caminhoArquivo = Path.Combine(@uploadPath + imagePath, Path.GetFileName(arquivo.FileName));

                    arquivo.SaveAs(caminhoArquivo);
                    arquivosSalvos++;
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteArquivo(string caminhoArquivo)
        {
            var path = Server.MapPath("~/");
            FileInfo file = new FileInfo(path + caminhoArquivo);
            if (file.Exists)
                file.Delete();

            return RedirectToAction("Index");
        }
    }
}
