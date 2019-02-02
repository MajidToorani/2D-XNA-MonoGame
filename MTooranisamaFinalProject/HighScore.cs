using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MTooranisamaFinalProject
{
    public class HighScore
    {
        private static string fileName = "highScores.xml";

        public List<Score> HighScores { get; private set; }

        public List<Score> Scores { get; private set; }

        public HighScore()
            : this(new List<Score>())
        {

        }

        public HighScore(List<Score> scores)
        {
            Scores = scores;

            UpdateHighScores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(s => s.Value).ToList();

            UpdateHighScores();
        }

        public static HighScore Load()
        {
            try
            {
                if (!File.Exists(fileName))                
                    return new HighScore();
             
                using (var reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
                {
                    var serilizer = new XmlSerializer(typeof(List<Score>));

                    var scores = (List<Score>)serilizer.Deserialize(reader);

                    return new HighScore(scores);
                }                               
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid file");
            }
        }

        public void UpdateHighScores()
        {
            HighScores = Scores.Take(3).ToList();
        }

        public static void Save(HighScore highScore)
        {
            try
            {
                using (var writer = new StreamWriter(new FileStream(fileName, FileMode.Create)))
                {
                    var serilizer = new XmlSerializer(typeof(List<Score>));

                    serilizer.Serialize(writer, highScore.Scores);                   
                }                
            }
            catch (Exception)
            {
                throw new ArgumentException("Error on saving scores");
            }
        }
    }
}
