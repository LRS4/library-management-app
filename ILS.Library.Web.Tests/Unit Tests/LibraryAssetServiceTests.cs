using ILS.Library.DataAccess.SecurityDb.Entities;
using ILS.Library.DataAccess.SecurityDb.Entities.Asset;
using ILS.Library.DataAccess.SecurityDb.Entities.Branch;
using ILS.Library.Web.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ILS.Library.Web.Tests
{
    [TestFixture]
    public class LibraryAssetServiceTests
    {
        [Test(Description = "Verifies that GetAll() returns a collection of library assets.")]
        public void GetAllReturnsLibraryAssets()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ILSContext>()
                .UseInMemoryDatabase(databaseName: "GetAllReturnsLibraryAssets")
                .Options;

            using (var context = new ILSContext(options))
            {
                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "The Republic",
                    Author = "Plato",
                    DeweyIndex = "820.119",
                    ISBN = "9780758320209",
                    Year = -380,
                    Cost = 11,
                    ImageUrl = "/images/republic.png",
                    NumberOfCopies = 2,
                    Location = new BranchDetails { Name = "Branch A" },
                    Status = new Status { Name = "Available", Description = "Available" },
                    LibraryCard = new LibraryCard { }
                });

                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "Jane Eyre",
                    Author = "Charlotte Brontë",
                    DeweyIndex = "822.133",
                    ISBN = "9781519133977",
                    Year = 1847,
                    Cost = 15,
                    ImageUrl = "/images/janeeyre.png",
                    NumberOfCopies = 5,
                    LocationId = 1,
                    StatusId = 1
                });

                context.SaveChanges();
            }

            IEnumerable<LibraryAsset> result;
            int count;

            // Act
            using (var context = new ILSContext(options))
            {
                var libraryAssetService = new LibraryAssetService(context);
                result = libraryAssetService.GetAll();
                count = result.Count();
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(count, 2);
        }

        [Test(Description = "Verifies that GetById() returns the Library Asset if it exists.")]
        public void GetByIdReturnsLibraryAsset()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ILSContext>()
                .UseInMemoryDatabase(databaseName: "GetByIdReturnsLibraryAsset")
                .Options;

            using (var context = new ILSContext(options))
            {
                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "The Republic",
                    Author = "Plato",
                    DeweyIndex = "820.119",
                    ISBN = "9780758320209",
                    Year = -380,
                    Cost = 11,
                    ImageUrl = "/images/republic.png",
                    NumberOfCopies = 2,
                    Location = new BranchDetails { Name = "Branch A"},
                    Status = new Status { Name = "Available", Description = "Available"},
                    LibraryCard = new LibraryCard { }
                });

                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "Jane Eyre",
                    Author = "Charlotte Brontë",
                    DeweyIndex = "822.133",
                    ISBN = "9781519133977",
                    Year = 1847,
                    Cost = 15,
                    ImageUrl = "/images/janeeyre.png",
                    NumberOfCopies = 5,
                    LocationId = 1,
                    StatusId = 1
                });

                context.SaveChanges();
            }

            LibraryAsset result;

            // Act
            using (var context = new ILSContext(options))
            {
                var libraryAssetService = new LibraryAssetService(context);
                result = libraryAssetService.GetById(1);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("The Republic", result.Title);
            Assert.AreEqual("Plato", result.Author);
        }

        [Test(Description = "Verifies that GetAuthorOrDirector() returns depending on whether the" +
            "discriminator is set to Book or Video.")]
        public void GetAuthorOrDirectorReturnsForBookOrVideo()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ILSContext>()
                .UseInMemoryDatabase(databaseName: "GetAuthorOrDirectorReturnsForBookOrVideo")
                .Options;

            using (var context = new ILSContext(options))
            {
                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "The Republic",
                    Author = "Plato",
                    DeweyIndex = "820.119",
                    ISBN = "9780758320209",
                    Year = -380,
                    Cost = 11,
                    ImageUrl = "/images/republic.png",
                    NumberOfCopies = 2,
                    LocationId = 2,
                    StatusId = 2
                });

                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Video",
                    Title = "The Matrix",
                    Director = "Lana / Lilly Wachowski",
                    Year = 1999,
                    Cost = 15,
                    ImageUrl = "/images/thematrix.png",
                    NumberOfCopies = 2,
                    LocationId = 1,
                    StatusId = 1
                });

                context.SaveChanges();
            }

            string video;
            string book;

            // Act
            using (var context = new ILSContext(options))
            {
                var libraryAssetService = new LibraryAssetService(context);
                book = libraryAssetService.GetAuthorOrDirector(1);
                video = libraryAssetService.GetAuthorOrDirector(2);
            }

            // Assert
            Assert.IsNotNull(video);
            Assert.IsNotNull(book);
            Assert.AreEqual(video, "Lana / Lilly Wachowski");
            Assert.AreEqual(book, "Plato");
        }

        [Test(Description = "Verifies that GetDeweyIndex() returns a Dewey Index number.")]
        public void GetDeweyIndexReturnsCorrectIndex()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ILSContext>()
                .UseInMemoryDatabase(databaseName: "GetDeweyIndexReturnsCorrectIndex")
                .Options;

            using (var context = new ILSContext(options))
            {
                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "The Republic",
                    Author = "Plato",
                    DeweyIndex = "820.119",
                    ISBN = "9780758320209",
                    Year = -380,
                    Cost = 11,
                    ImageUrl = "/images/republic.png",
                    NumberOfCopies = 2,
                    LocationId = 2,
                    StatusId = 2
                });

                context.LibraryAsset.Add(new LibraryAsset
                {
                    Discriminator = "Book",
                    Title = "Jane Eyre",
                    Author = "Charlotte Brontë",
                    DeweyIndex = "822.133",
                    ISBN = "9781519133977",
                    Year = 1847,
                    Cost = 15,
                    ImageUrl = "/images/janeeyre.png",
                    NumberOfCopies = 5,
                    LocationId = 1,
                    StatusId = 1
                });

                context.SaveChanges();
            }

            string result;

            // Act
            using (var context = new ILSContext(options))
            {
                var libraryAssetService = new LibraryAssetService(context);
                result = libraryAssetService.GetDeweyIndex(2);
            }

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("822.133", result);
        }
    }
}