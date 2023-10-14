using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BSIT3L_Movies.Models;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace BSIT3L_Movies.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly List<MovieViewModel> _movies;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _movies = new List<MovieViewModel>
        {
             new MovieViewModel { Id = 13, Name ="Wolf Children", Rating = "8.1", ReleaseYear = 2012, Genre = "Drama", Trailer ="https://www.youtube.com/watch?v=8xLji7WsW0w", Image ="https://www.crunchyroll.com/imgsrv/display/thumbnail/640x360/catalog/crunchyroll/f1bb5da76e963a3b29a58bf833f5fba7.jpe", About ="Hana marries a wolf man and raises their two children alone after he dies. They move to the countryside and the children have adventures in the woods and at school."},
             new MovieViewModel { Id = 14, Name ="Weathering with You", Rating = "7.5", ReleaseYear = 2019, Genre = "Drama", Trailer ="https://www.youtube.com/watch?v=Q6iK6DjV_iE", Image ="https://cdn.theatlantic.com/thumbor/ivQgFYHzm7KNvBzNWr6DixsxHyw=/0x0:1023x575/960x540/media/img/mt/2020/01/weathering/original.jpg", About ="Set during a period of exceptionally rainy weather, high-school boy Hodaka Morishima runs away from his troubled rural home to Tokyo and befriends an orphan girl who can manipulate the weather."},
             new MovieViewModel { Id = 15, Name ="Violet Evergarden: The Movie", Rating = "8.3", ReleaseYear = 2020, Genre = "Drama", Trailer ="https://www.youtube.com/watch?v=BUfSen2rYQs", Image ="https://mahoganyreviews.com/wp-content/uploads/2022/01/VioletEvergardenMovie_BackgroundwLogo.jpg", About ="After the aftermath of a war, a young girl who was used as a tool for war learns to properly live. With the scars of burns, she goes back to her past to discover her true feelings towards the Major."},
             new MovieViewModel { Id = 1, Name = "Titanic", Rating = "5", ReleaseYear = 1997, Genre = "Romance/Drama", Trailer ="https://www.youtube.com/watch?v=I7c1etV7D7g", Image ="https://flxt.tmsimg.com/assets/p20056_v_v8_ab.jpg", About ="James Cameron's \"Titanic\" is an epic, action-packed romance set against the ill-fated maiden voyage of the R.M.S. Titanic; the pride and joy of the White Star Line and, at the time, the largest moving object ever built. She was the most luxurious liner of her era -- the \"ship of dreams\" -- which ultimately carried over 1,500 people to their death in the ice cold waters of the North Atlantic in the early hours of April 15, 1912." },
             new MovieViewModel { Id = 2, Name = "Inception", Rating = "4", ReleaseYear = 2010, Genre = "Science Fiction/Thriller", Trailer ="https://www.youtube.com/watch?v=YoHD9XEInc0", Image ="https://flxt.tmsimg.com/assets/p7825626_p_v8_af.jpg", About ="Dom Cobb (Leonardo DiCaprio) is a thief with the rare ability to enter people's dreams and steal their secrets from their subconscious. His skill has made him a hot commodity in the world of corporate espionage but has also cost him everything he loves. Cobb gets a chance at redemption when he is offered a seemingly impossible task: Plant an idea in someone's mind. If he succeeds, it will be the perfect crime, but a dangerous enemy anticipates Cobb's every move."  },
             new MovieViewModel { Id = 3, Name = "The Shawshank Redemption", Rating = "5", ReleaseYear = 1994, Genre = "Drama", Trailer ="https://www.youtube.com/watch?v=PLl99DlL6b4", Image ="https://flxt.tmsimg.com/assets/p15987_k_h9_ac.jpg", About ="Andy Dufresne (Tim Robbins) is sentenced to two consecutive life terms in prison for the murders of his wife and her lover and is sentenced to a tough prison. However, only Andy knows he didn't commit the crimes. While there, he forms a friendship with Red (Morgan Freeman), experiences brutality of prison life, adapts, helps the warden, etc., all in 19 years." },
             new MovieViewModel { Id = 4, Name = "The Lorax", Rating = "6.4", ReleaseYear = 2012, Genre = "Adventure", Trailer ="https://www.youtube.com/watch?v=1bHdzTUNw-4", Image ="https://images.mubicdn.net/images/film/94478/cache-49956-1618963232/image-w1280.jpg", About ="Twelve-year-old Ted (Zac Efron) lives in a place virtually devoid of nature; no flowers or trees grow in the town of Thneedville. Ted would very much like to win the heart of Audrey (Taylor Swift), the girl of his dreams, but to do this, he must find that which she most desires: a Truffula tree. To get it, Ted delves into the story of the Lorax (Danny DeVito), once the gruff guardian of the forest, and the Once-ler (Ed Helms), who let greed overtake his respect for nature." },
             new MovieViewModel { Id = 5, Name = "The Dictator", Rating = "6.4", ReleaseYear = 2012, Genre = "Comedy", Trailer ="https://www.youtube.com/watch?v=cYplvwBvGA4", Image ="https://static.timesofisrael.com/www/uploads/2012/05/Dictator_Quad-1024x768-1024x640.jpg", About ="Gen. Aladeen (Sacha Baron Cohen) has ruled the oil-rich North African country of Wadiya since the age of six, when 97 stray bullets and a hand grenade killed his father in a hunting accident. After an assassination attempt takes the life of yet another body-double, Tamir (Ben Kingsley), Aladeen's uncle and most trusted adviser, convinces Aladeen to go to New York. Unfortunately, Aladeen has a less-than-friendly reception from exiled Wadiyans, who want their country freed from his despotic rule." },
             new MovieViewModel { Id = 6, Name ="Forgotten Love", Rating = "7.6", ReleaseYear = 2023, Genre = "Drama", Trailer ="https://www.youtube.com/watch?v=SUbs1-j5JOg", Image ="https://flxt.tmsimg.com/assets/p25537876_v_v13_aa.jpg", About ="A once-respected surgeon who's lost his family and his memory gets a chance at redemption when he reconnects with someone from his forgotten past who can help him finds the answers he needs."},
             new MovieViewModel { Id = 7, Name ="Get Hard", Rating = "6", ReleaseYear = 2015, Genre = "Comedy", Trailer ="https://www.youtube.com/watch?v=lEqrpuU9fYI", Image ="https://resizing.flixster.com/0xeunZytdzhupPsg0ApSXyx4i_A=/206x305/v2/https://flxt.tmsimg.com/assets/p11003109_p_v8_al.jpg", About ="When obscenely rich hedge-fund manager James (Will Ferrell) is convicted of fraud and sentenced to a stretch in San Quentin, the judge gives him one month to get his affairs in order. Knowing that he won't survive more than a few minutes in prison on his own, James desperately turns to Darnell (Kevin Hart) -- a black businessman who's never even had a parking ticket -- for help. As Darnell puts James through the wringer, both learn that they were wrong about many things, including each other."},
             new MovieViewModel { Id = 8, Name ="Oblivion", Rating = "7", ReleaseYear = 2013, Genre = "Sci-Fi", Trailer ="https://www.youtube.com/watch?v=UsjdRsrR6cs", Image ="https://images.moviesanywhere.com/b51d892eee8014269172e8595360aa29/e7643c26-015d-4419-9588-ab68af8dfb70.jpg", About ="In the year 2077, Jack Harper (Tom Cruise) works as a security repairman on an Earth left empty and devastated after a war with aliens. Jack has two weeks left before his mission ends and he joins his fellow survivors on a faraway colony. However, Jack's concept of reality comes crashing down after he rescues a beautiful stranger (Olga Kurylenko) from a downed spacecraft. The woman's arrival triggers a chain of events that culminates in Jack's nearly single-handed battle to save mankind."},
             new MovieViewModel { Id = 9, Name ="Heneral Luna", Rating = "7.3", ReleaseYear = 2015, Genre = "History", Trailer ="https://www.youtube.com/watch?v=I_T1ykhy3Fg", Image ="https://flxt.tmsimg.com/assets/p12239269_v_h8_aa.jpg", About ="In 1898, Gen. Antonio Luna (John Arcilla) faces resistance from his own countrymen as he fights for freedom during the Philippine-American War."},
             new MovieViewModel { Id = 10, Name ="In My Mother's Skin", Rating = "5.5", ReleaseYear = 2023, Genre = "Horror", Trailer ="https://www.youtube.com/watch?v=yZVCkRxW1_s", Image ="https://m.media-amazon.com/images/S/pv-target-images/f1615896b8d1105f7f2c69274b6b9a2912b6c4472da34614c9ba489bb4f38e4a.jpg", About ="A young girl desperate to cure her dying mother seeks out a mysterious fairy who gives her a magical insect. Unbeknownst to her, the fairy turns out to be deceitful and plans to devour them all."},
             new MovieViewModel { Id = 11, Name ="Corpse Bride", Rating = "7.3", ReleaseYear = 2005, Genre = "Family", Trailer ="https://www.youtube.com/watch?v=AGACeWVdFqo", Image ="https://flxt.tmsimg.com/assets/p36328_p_v8_aj.jpg", About ="Victor (Johnny Depp) and Victoria's (Emily Watson) families have arranged their marriage. Though they like each other, Victor is nervous about the ceremony. While he's in a forest practicing his lines for the wedding, a tree branch becomes a hand that drags him to the land of the dead. It belongs to Emily, who was murdered after eloping with her love and wants to marry Victor. Victor must get back aboveground before Victoria marries the villainous Barkis Bittern (Richard E. Grant)."},
             new MovieViewModel { Id = 12, Name ="Sinbad: Legend of the Seven Seas", Rating = "6.7", ReleaseYear = 2003, Genre = "Adventure", Trailer ="https://www.youtube.com/watch?v=rwjVhtQRnwk", Image ="https://upload.wikimedia.org/wikipedia/en/c/cd/Sinbad_Legend_of_the_Seven_Seas_poster.jpg", About ="The brave mariner Sinbad battles raging elements, mutinous dogs and fearsome monsters to retrieve the Book of Peace after being framed by the treacherous Goddess of Chaos. A DreamWorks animation."},
        };
    }

    public IActionResult Index()
    {
        return View(_movies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

