using Microsoft.EntityFrameworkCore;
using ZadatakAKD.Data;

namespace ZadatakAKD.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ZadatakAKDContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ZadatakAKDContext>>()))
            {

                if (context.Autors.Any())
                {
                    return; // DB has benn seeded
                }

                var autors = new Autor[]
                {
                    new Autor { Ime = "Borna", Prezime = "Ungar"},
                    new Autor { Ime = "Test", Prezime = "Testko"},
                    new Autor { Ime = "Ivan", Prezime = "Ivko"},
                    new Autor { Ime = "Ivan4", Prezime = "Ivko4"}
                };
                    
                foreach (Autor a in autors)
                {
                    context.Autors.Add(a);
                }
                context.SaveChanges();

                var knjigas = new Knjiga[]
                {
                    new Knjiga { Naslov = "Bornina prva .NET aplikacija", GodinaIzdavanja = "2023.", AutorId = autors[0].Id},
                    new Knjiga { Naslov = "Test naslov", GodinaIzdavanja = "2022.", AutorId = autors[1].Id},
                    new Knjiga { Naslov = "Test naslov 2", GodinaIzdavanja = "2023.", AutorId = autors[1].Id},
                    new Knjiga { Naslov = "Ivanova knjiga", GodinaIzdavanja = "1999.", AutorId = autors[2].Id}
                };

                foreach (Knjiga k in knjigas)
                {
                    context.Knjigas.Add(k);
                }
                context.SaveChanges();

            }
        }
    }
}
