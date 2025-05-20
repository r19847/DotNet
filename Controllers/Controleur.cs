using Microsoft.AspNetCore.Mvc;
using TPLOCAL1.Models;
using System.IO;
using System.Xml;
using System.Collections.Generic;

public class HomeController : Controller
{
    // Variable statique pour stocker les données du formulaire
    private static FormModel _formData;

    public ActionResult Index(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return View();
        else
        {
            switch (id)
            {
                case "AvisList":
                    // Utilisation de la classe OpinionList pour charger les données
                    var opinionListManager = new OpinionList();
                    string xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "XML File", "DataAvis.xml");
                    var avisList = opinionListManager.GetAvis(xmlFilePath);
                    return View(id, avisList);
                case "Form":
                    return View(id, new FormModel());
                case "Confirmation": // Nouveau cas pour la page de validation
                    if (_formData == null)
                        return RedirectToAction("Index", new { id = "Form" });
                    return View(id, _formData);
                default:
                    return View();
            }
        }
    }

    [HttpPost]
    public ActionResult Index(FormModel formData, string id = "Form")
    {
        if (ModelState.IsValid)
        {
            // Stocker les données du formulaire dans la variable statique
            _formData = formData;
            // Rediriger vers la page de la validation
            return RedirectToAction("Index", new { id = "Confirmation" });
        }
        return View(id, formData);
    }
}