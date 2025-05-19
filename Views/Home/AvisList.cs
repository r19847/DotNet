using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TPLOCAL1.Models
{
    public class ListReviews
    {
        /// <summary>
        /// Fonction permettant de récupérer la liste des avis contenus dans un fichier XML
        /// </summary>
        /// <param name="file">Chemin du fichier</param>
        /// <returns>Liste des avis</returns>
        public List<Avis> GetAvis(string file)
        {
            // Instancier la liste comme vide
            List<Avis> listAvis = new List<Avis>();

            try
            {
                // Création d'un objet XMLDocument pour récupérer des données à partir du fichier physique
                XmlDocument xmlDoc = new XmlDocument();

                // Lecture du fichier à partir d'un objet StreamReader
                using (StreamReader streamDoc = new StreamReader(file))
                {
                    string dataXml = streamDoc.ReadToEnd();

                    // Chargement de données dans le document XmlDocument
                    xmlDoc.LoadXml(dataXml);

                    // Récupération des nœuds pour les passer en tant qu'objet Avis
                    foreach (XmlNode node in xmlDoc.SelectNodes("root/row"))
                    {
                        // Récupération de données dans les nœuds enfants
                        string nom = node["Nom"]?.InnerText ?? "";
                        string prenom = node["Prenom"]?.InnerText ?? "";
                        string avisDonne = node["Avis"]?.InnerText ?? "";

                        // Création et ajout de l'objet à la liste
                        listAvis.Add(new Avis
                        {
                            Nom = nom,
                            Prenom = prenom,
                            AvisDonne = avisDonne
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // En cas d'erreur, on pourrait logger l'exception
                Console.WriteLine($"Erreur lors de la lecture du fichier XML: {ex.Message}");
            }

            return listAvis;
        }
    }

    /// <summary>
    /// Regroupement d'objets : données relatives aux avis
    /// </summary>
    public class Avis
    {
        /// <summary>
        /// Nom de famille
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Prénom
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// Avis donné (Valeurs possibles : O ou N)
        /// </summary>
        public string AvisDonne { get; set; }

        /// <summary>
        /// Indique si l'avis est positif (true) ou négatif (false)
        /// </summary>
        public bool IsPositif => AvisDonne?.ToUpper() == "O";

        /// <summary>
        /// Retourne "Oui" si l'avis est "O", sinon "Non"
        /// </summary>
        public string AvisFormate => IsPositif ? "Oui" : "Non";
    }
}