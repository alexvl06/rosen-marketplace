using Moq.EntityFrameworkCore;
using Moq;
using Marketplace.Core.Model;
using Marketplace.Dal;
using Marketplace.Dal.Repositories;
using Marketplace.Core.Dal;
using System.Collections.Generic;
using System;

namespace test;

[TestFixture]
public class OfferRepositoryTest
{
    private Mock<MarketplaceContext> _context;
    private Mock<UserRepository> userRepository;
    private Mock<CategoryRepository> categoryRepository;
    private IOfferRepository offerRepository;

    [SetUp]
    public void Setup()
    {
        _context = new Mock<MarketplaceContext>();

        IEnumerable<User> users = new List<User>
        {
            new User { Id = 1, Username = "Alexis Ávila" },
            new User { Id = 2, Username = "Yina Micanquer" }
        };
        IEnumerable<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Product" },
            new Category { Id = 2, Name = "Service" },
            new Category { Id = 3, Name = "I'm looking for" }
        };
        IEnumerable<Offer> offers = new List<Offer>
        {
            new Offer
            {
                Id = Guid.Parse("a8957388-a311-4f94-b8e6-22e5596015fe"),
                CategoryId = 1,
                Description = "Camisa manga corta a cuadros, de colores azul, blanco y naranja.",
                Location = "Sabaneta",
                PictureUrl =
                    "https://miscelandia.vtexassets.com/arquivos/ids/216362-800-auto?v=637313999364670000&width=800&height=auto&aspect=true",
                Title = "Camisa a Cuadros",
                UserId = 1
            },
            new Offer
            {
                Id = Guid.Parse("55914071-08c0-442c-a571-fbf213952849"),
                CategoryId = 2,
                Description = "Le paseamos su mascota al rededor de la manazana.",
                Location = "Bogotá",
                PictureUrl =
                    "https://imagenes.expreso.ec/files/image_700_402/uploads/2021/08/06/610db5cdbc443.jpeg",
                Title = "Paseo de Mascotas",
                UserId = 2
            },
            new Offer
            {
                Id = Guid.Parse("88c9a91d-4f73-45ca-a8eb-dbaa75364764"),
                CategoryId = 3,
                Description = "Busco auto con cero kilometraje, preferible de segunda mano",
                Location = "Barranquilla",
                PictureUrl =
                    "https://acnews.blob.core.windows.net/imgnews/medium/NAZ_03c56d258d5a4f929a12acb53149c695.jpg",
                Title = "Compro Auto",
                UserId = 1
            }
        };
        _context.Setup(c => c.users).ReturnsDbSet(users);
        _context.Setup(c => c.categories).ReturnsDbSet(categories);
        _context.Setup(c => c.offers).ReturnsDbSet(offers);
        categoryRepository = new Mock<CategoryRepository>(_context.Object);
        userRepository = new Mock<UserRepository>(_context.Object);
        offerRepository = new OfferRepository(
            _context.Object,
            categoryRepository.Object,
            userRepository.Object
        );
    }

    [Test]
    public async Task GetOffersByPageTest()
    {
        int index = 1;
        int pageSize = 3;
        IEnumerable<OfferDTO> offers = await offerRepository.GetOffersByPageIndex(index, pageSize);
        Assert.NotNull(offers);
        Assert.That(offers.Count(), Is.EqualTo(index * pageSize));
    }

    [Test]
    public async Task GetOffersByIdTest()
    {
        Guid Id = Guid.Parse("a8957388-a311-4f94-b8e6-22e5596015fe");
        OfferDTO offer = await offerRepository.GetOfferById(Id);
        Assert.NotNull(offer);
        Assert.That(offer.Title, Is.EqualTo("Camisa a Cuadros"));
    }

    [Test]
    [TestCase("Alexis Ávila")]
    [TestCase("Margarita Rosa de Francisco")]
    public async Task CreateNewOfferWithRegiteredUserTest(string Username)
    {
        OfferDTO offerDTO = new OfferDTO
        {
            CategoryName = "I'm looking for",
            Description = "Habitación con baño interno",
            Location = "Manizales",
            PictureUrl =
                "https://i.pinimg.com/originals/d2/83/ab/d283ab01c0082fa804e7d2705254b238.jpg",
            Title = "Habitación",
            Username = Username
        };
        OfferDTO offer = await offerRepository.CreateOffer(offerDTO);
        Assert.NotNull(offer);
        Assert.That(offer.Username, Is.EqualTo(Username));
    }

    [Test]
    public async Task OffersQuantityTest()
    {
        int offersNumber = await offerRepository.OffersQuantity();
       Assert.That(offersNumber, Is.EqualTo(3));
    }
}
