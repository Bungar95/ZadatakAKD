namespace ZadatakAKD.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        
        public ICollection<Knjiga> Knjigas { get; set; }

        public string PunoIme { 
            get { return $"{Prezime}, {Ime}"; } 
        }
    }
}
