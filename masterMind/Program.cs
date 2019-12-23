using System;
using System.Collections.Generic;

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

            for (int indx = 0; indx < 4; indx++)
            {
                m_CurrentNumberToGuess.Add(m_randomNumberGenerator.Next(1, 6));
            }
            m_CurrentGuess = null;
        }

        public void newNumber()
        {
            m_CurrentNumberToGuess = new List<int>();

            for (int indx = 0; indx < 4; indx++)
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
            List<string> MtchResult = new List<string>();

            for( int indx = 0; indx < 4;indx++)
            {
               if(m_CurrentGuess[3-indx] == m_CurrentNumberToGuess[indx])
                {
                    MtchResult.Add( "+");
                }
               else if(m_CurrentNumberToGuess.Contains(m_CurrentGuess[3-indx]))
                {
                    MtchResult.Add("-");
                }
               else
                {
                    MtchResult.Add(" ");
                }
            }

            return MtchResult;
        }

        public String dumpSecret()
        {
            String theSecret = "";

            List<int> localGuess = new List<int>(m_CurrentNumberToGuess);
            localGuess.Reverse();
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
            Console.WriteLine("The secret number " + theGame.dumpSecret());

            for (int attempt = 0; attempt < 10; attempt++)
            {
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
                    foreach(var eachDigit in result)
                    {
                        Console.Write(eachDigit);
                    }
                    Console.WriteLine("'");
                }
            }
        }
    }
}
