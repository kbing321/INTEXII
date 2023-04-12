namespace INTEXII.Models
{
    public class Prediction
    {
        public int squarenorthsouth { get; set; }
        public float depth { get; set; }
        public float southtohead { get; set; }
        public int squareeastwest { get; set; }
        public float westtohead { get; set; }
        public int burialnumber { get; set; }
        public float westtofeet { get; set; }
        public float southtofeet { get; set; }
        public int northsouth_N { get; set; }
        public int eastwest_E { get; set; }
        public int eastwest_W { get; set; }
        public int adultsubadult_A { get; set; }
        public int adultsubadult_C { get; set; }
        public int samplescollected_false { get; set; }
        public int samplescollected_true { get; set; }
        public int samplescollected_unknown { get; set; }
        public int area_NE { get; set; }
        public int area_NW { get; set; }
        public int area_SE { get; set; }
        public int area_SW { get; set; }
        public int ageatdeath_A { get; set; }
        public int ageatdeath_C { get; set; }
        public int ageatdeath_I { get; set; }
        public int ageatdeath_N { get; set; }
    }

}
