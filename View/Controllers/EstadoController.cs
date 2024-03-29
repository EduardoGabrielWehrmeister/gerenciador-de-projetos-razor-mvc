﻿using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class EstadoController : Controller
    {
        private EstadoRepository repository;

        public EstadoController()
        {
            repository = new EstadoRepository();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObterTodos(string busca)
        {
            List<Estado> estados = repository.ObterTodos(busca);
            return Json(estados, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Store(Estado estado)
        {
            estado.RegistroAtivo = true;
            repository.Inserir(estado);
            return Json(estado);
        }

        [HttpGet]
        [Route("apagar/{id}")]
        public JsonResult Apagar(int id)
        {
            bool apagou = repository.Delete(id);
            return Json(new { status = apagou }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet, Route("obterpeloid/{id}")]
        public JsonResult obterPeloId(int id)
        {
            Estado estado = repository.ObterPeloId(id);
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(Estado estado)
        {
            bool alterou = repository.Update(estado);
            return Json(new { status = alterou });
        }
    }
}