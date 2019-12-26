using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace masterMind
{
    class MasterMindEngine
    {
        List<int> m_CurrentNumberToGuess;
        List<int> m_CurrentGuess;
        Random m_randomNumberGenerator;

        public MasterMindEngine()
        {
            m_randomNumberGenerator = new Random(DateTime.Now.Second);

            m_CurrentNumberToGuess = new List<int>();

            for (int indx = 3; indx >= 0; indx--)
            {
                m_CurrentNumberToGuess.Add(m_randomNumberGenerator.Next(1, 6));
            }
            m_CurrentGuess = null;
        }

        public void newNumber()
        {
            m_CurrentNumberToGuess = new List<int>();

            for (int indx = 3; indx >= 0; indx--)
            {
                m_CurrentNumberToGuess.Add(m_randomNumberGenerator.Next(1, 6));
            }
        }

        public bool newGuess(List<int> newGuess)
        {
            if(newGuess.Count != 4)
            {
                return false;
            }

            foreach(var digit in newGuess)
            {
                if(digit < 1 || digit > 6)
                {
                    return false;
                }
            }
            m_CurrentGuess = newGuess;
            return true;
        }

        public List<String> testForMatch()
        {
            Regex RegexAllPluses = new Regex(@"\+{4,4}");

            List<string> guessMatch = new List<string>();

            for (var guessIndex = 3; guessIndex >= 0; guessIndex--)
            {
                if (m_CurrentGuess[guessIndex] == m_CurrentNumberToGuess[guessIndex])
                {
                    guessMatch.Add("+");
                }
                else 
                {
                    guessMatch.Add(" ");
                }

            }
            String theGuessAsString = String.Join("", guessMatch.ToArray());

            for(var item=3; item>=0; item--)
            {
                if(guessMatch[item] == " ")
                {
                    // gigits don't match
                    for(var innerItem=3;innerItem >= 0; innerItem--)
                    {
                        if(item != innerItem)
                        {
                            if(m_CurrentGuess[item] == m_CurrentNumberToGuess[innerItem])
                            {
                                guessMatch[innerItem] = "-";
                            }
                        }
                    }
                }
            }
            return guessMatch;
        }

        public String dumpSecret()
        {
            String theSecret = "";

            List<int> localGuess = new List<int>(m_CurrentNumberToGuess);
            foreach(var item in (localGuess))
            {
                theSecret += item.ToString("D");
            }
            return theSecret;
        }

    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            MasterMindEngine theGame = new MasterMindEngine();
            // Console.WriteLine("The secret number " + theGame.dumpSecret());

            for (int attempt = 1; attempt <= 10; attempt++)
            {
                Console.WriteLine("Attempt # {0}", attempt);
                Console.Write("Enter guess: ");
                var userResponse = Console.ReadLine();
                List<int> aGuess = new List<int>();
                foreach (char aChar in userResponse)
                {
                    aGuess.Add(int.Parse(Char.ToString(aChar)));
                }
                if(! theGame.newGuess(aGuess) )
                {
                    Console.WriteLine("Illegal Guess");
                    continue;
                }
                var result = theGame.testForMatch();
                bool guessCorrect = true;
                foreach(var guessItem in result)
                {
                    if(guessItem == " " || guessItem == "-")
                    {
                        guessCorrect = false;
                    }
                }
                if (guessCorrect)
                {
                    Console.WriteLine("you win");
                    return;
                }
                else
                {
                    Console.Write("Incorrect '");
                    result.Reverse();
                    foreach(var eachDigit in result)
                    {
                        Console.Write(eachDigit);
                    }
                    Console.WriteLine("'");
                }
            }
            Console.WriteLine("You lose!! " + theGame.dumpSecret());
        }
    }
}
