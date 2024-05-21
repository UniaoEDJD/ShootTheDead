using ShootTheDead.Main;
using System.Xml.Serialization;

namespace ShootTheDead.Managers
{
    public class ScoreManager
    {
        private static string _fileName = "scores.xml";

        public List<Score> Scores { get; set; }

        public List<Score> HighScores { get; set; }

        public ScoreManager() : this(new List<Score>())
        {

        }
        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighScores();
        }

        public void UpdateHighScores()
        {
            HighScores = Scores.Take(5).ToList();
        }

        public void AddScore(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(s => s.playerScore).ToList(); //ordenar por highscores

            UpdateHighScores();
        }

        public static ScoreManager Load()
        {
            try
            {
                using (var reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
                {
                    var serializer = new XmlSerializer(typeof(List<Score>));
                    var scores = (List<Score>)serializer.Deserialize(reader);
                    return new ScoreManager(scores);
                }
            }
            catch (FileNotFoundException)
            {
                // Se o arquivo não existir, retorna um novo ScoreManager vazio
                return new ScoreManager(new List<Score>());
            }
        }

        public static void SaveScore(ScoreManager scoreManager)
        {
            // sobrepoe o arquivo caso exista
            using(var writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                var serializer = new XmlSerializer(typeof(List<Score>));

                serializer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
