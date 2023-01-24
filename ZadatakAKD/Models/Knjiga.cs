namespace ZadatakAKD.Models
{
    public class Knjiga
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public string Naslov { get; set; }
        public string GodinaIzdavanja { get; set; }

        public virtual Autor Autor { get; set; }
    }
}
