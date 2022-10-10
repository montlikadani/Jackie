namespace Jackie {
    public class JackieVersenyzo {

        public int Year { get; private set; }
        public int Races { get; private set; }
        public int Wins { get; private set; }
        public int Podiums { get; private set; }
        public int Poles { get; private set; }
        public int Fastests { get; private set; }

        public JackieVersenyzo(string input) {
            string[] split = input.Split('\t');

            Year = int.Parse(split[0]);
            Races = int.Parse(split[1]);
            Wins = int.Parse(split[2]);
            Podiums = int.Parse(split[3]);
            Poles = int.Parse(split[4]);
            Fastests = int.Parse(split[5]);
        }
    }
}
