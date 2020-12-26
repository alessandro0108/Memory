using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Memory
{
  class Program
  {
    //static string[] words = {
    //  "mare",
    //  "sole",
    //  "insalata",
    //  "ragnatela",
    //  "anguria",
    //  "gatto",
    //  "casa"
    //};

    static List<string> words = new List<string>();

    static void Main(string[] args)
    {
      do
      {
        PrepareNewGame();

        Console.Clear();
        Console.WriteLine("Memorizza le seguenti parole e premi un tasto quando sei pronto:");
        Console.WriteLine();
        Console.WriteLine(string.Join(", ", words));
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("Inserisci tutte le parole che ti ricordi (scrivi fine per terminare il gioco):");

        int guessed = doGame();
        string message = GetResult(guessed, words.Count);

        Console.WriteLine(message);
        Console.WriteLine("Vuoi giocare un'altra volta (no per finire)?");
        string answer = Console.ReadLine();
        if (answer.ToUpper() == "NO")
        {
          Console.WriteLine("Ciao, alla prossima volta!");
          Console.ReadKey();
          break;
        }
      } while (1 > 0);
    }

    private static void PrepareNewGame()
    {
      var lines = File.ReadAllLines("words.txt");
      words.Clear();
      var rnd = new Random();
      while (words.Count < 10)
      {
        var nextWordIndex = rnd.Next(0, lines.Length - 1);
        if (!words.Contains(lines[nextWordIndex]))
        {
          words.Add(lines[nextWordIndex]);
        }
      }
    }

    private static int doGame()
    {
      int guessed = 0;
      string input = "";
      int totalWords = words.Count;
      do
      {
        input = Console.ReadLine();
        bool isCorrect = words.Select(w => w.ToUpper().Trim()).Contains<string>(input.ToUpper().Trim());
        if (isCorrect)
        {
          guessed++;
        }
        if (guessed == totalWords) break;
      } while (input != "fine");
      return guessed;
    }

    private static string GetResult(int counter, int totaleWords)
    {
      string message = "";
      if (counter == totaleWords)
      {
        message = "Complimenti, le hai indovinate tutte!";
      }
      else if (counter == 0)
      {
        message = "Accidenti, non ne ha indovinata nessuna... ti devi allenare!";
      }
      else
      {
        message = $"Ne hai indovinate {counter} su {totaleWords}";
      }

      return message;
    }
  }
}
