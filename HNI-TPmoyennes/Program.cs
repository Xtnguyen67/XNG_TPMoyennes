using System;
using System.Collections.Generic;
using System.Linq;

namespace HNI_TPmoyennes
{
    // Classes fournies par HNI Institut
    public class Note
    {
        public int Matiere { get; set; } 
        public float note { get; set; }
        public Note(int m, float n)
        {
            Matiere = m;
            note = n;
        }
    }

    //Eleve = le prénom, le nom et la liste de notes de l'élève
    public class Eleve
    {
        public string prenom { get; set; }
        public string nom { get; set; }

        // Liste notes de l'élève
        public List<Note> notes = new List<Note>();
        public IReadOnlyList<Note> Notes => notes.AsReadOnly();

        public Eleve(string prenom, string nom)
        {
            this.prenom = prenom;
            this.nom = nom;
        }
        // add note
        public void ajouterNote(Note note)
        {
            notes.Add(note);
        }

        public double moyenneMatiere(int indexMatiere)
        {
            var notesMatiere = notes.Where(n => n.Matiere == indexMatiere).ToList();
            if (notesMatiere.Count == 0)
                return 0;
            double somme = notesMatiere.Sum(n => n.note);
            return somme / notesMatiere.Count;
        }

        public double moyenneGeneral()
        {
            if (notes.Count == 0)
                return 0;
            double somme = notes.Sum(n => n.note);
            return somme / notes.Count;
        }
    }

    //Classe = la liste des élèves et des matières.
    public class Classe
    {
        public string nomClasse { get; set; }
        public List<Eleve> eleves { get; private set; } = new List<Eleve>();
        public List<string> matieres { get; private set; } = new List<string>();

        public Classe(string nomClasse)
        {
            this.nomClasse = nomClasse;
        }

        // AjouterEleve à la liste
        public void ajouterEleve(string prenom, string nom)
        {
            eleves.Add(new Eleve(prenom, nom));
        }

        // ajouterMatiere à la liste
        public void ajouterMatiere(string matiere)
        {
            matieres.Add(matiere);
        }

        public double moyenneMatiere(int indexMatiere)
        {
            double total = 0;
            int count = 0;
            foreach (Eleve eleve in eleves)
            {
                var notesMatiere = eleve.Notes.Where(n => n.Matiere == indexMatiere).ToList();
                total += notesMatiere.Sum(n => n.note);
                count += notesMatiere.Count;
            }
            return (count == 0) ? 0 : total / count;
        }
        public double moyenneGeneral()
        {
            double total = 0;
            int count = 0;
            foreach (Eleve eleve in eleves)
            {
                total += eleve.Notes.Sum(n => n.note);
                count += eleve.Notes.Count;
            }
            return (count == 0) ? 0 : total / count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[7];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.moyenneMatiere(1).ToString("F2") + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.moyenneGeneral().ToString("F2") + "\n");
            // Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.moyenneMatiere(1).ToString("F2") + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.moyenneGeneral().ToString("F2") + "\n");
            Console.Read();
        }
    }
}



